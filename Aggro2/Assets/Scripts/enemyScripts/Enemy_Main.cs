using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_Main : MonoBehaviour
{
    PlayerScript player;
    public enum EnemyMoveStates { Stuned, Slowed, DoesNotMove, Silenced, Charmed, Empty};
    public enum EnemyHealthStates { Burning, Poisoned, Raged, Dead, Empty }
    public enum SoulTypes {Fire, Ice, Poison, Electric, Earth, Dark, Sweet, Mage, Empty};
    public enum DamageType {abilityPower, attackDamage, trueDamage};

    [HideInInspector] public EnemyMoveStates states_move;
    [HideInInspector] public EnemyHealthStates states_health;

    [HideInInspector] public bool canChangeEnemyStates;

    public GameObject popUp;
    public GameObject fuzzyEffect;
    public GameObject poisonEffect;
    public GameObject fireEffect;
    public GameObject slowEffect;
    public GameObject brownSoulEffect;
    public GameObject bloodEffect;
    public GameObject bloodBoomEffect;
    public GameObject stunIdentificatorObject; // obiekt nad przeciwnikiem oznajmiający że przrciwnik jest zestunowany
    public GameObject charmIdentificatorObject; // obiekt nad przeciwnikiem oznajmiający że przeciwnik jest zcharmowany
    public GameObject charmCircle;

    public GameObject shieldHolder;

    public GameObject deadEnemyObject;

    public Transform followObject; // jeśli przeciwnik nachodzi na drugiego zmieniamy tą zmienna na przeciwnika który uprzednio blokował

    private Vector2[] deathPoint = new Vector2[4];

    public Sprite deathSprite;
    public Sprite enemySprite;
    public Sprite hitSprite;

    public Sprite charmState1;
    public Sprite charmState2;
    public Sprite charmState3;

    private GameObject canvasParent;

    public Colors ColorsMenu;
    public Sprites SpritesMenu;
    public Scalers ScalersMenu;

    private PlayerDamageScaler damageScaler;

    //enemy stats
    public float hp_enemyMax;
    public float attackRate_enemyMax;
    [Range(0, 99)] public float armour;
    [Range(0, 99)] public float magicRessists;
    public float movementSpeed_enemyMax;

    //enemy onRun stats
    private float hp_enemy;
    private float movementSpeed_enemy;
    private float attackRate_enemy;

    public float BurningCumulator = 0;

    [HideInInspector] public float burnDurationMax;
    [HideInInspector] public float poisonDurationMax;
    [HideInInspector] public float slowDurationMax;
    [HideInInspector] public float stunDurationMax;
    [HideInInspector] public float earthEffectDurationMax;
    [HideInInspector] public float charmDurationMax;

    public float burnDuration = 0;
    public float poisonDuration = 0;
    public float slowDuration = 0;
    public float stunDuration = 0;
    public float earthEffectDuration = 0;
    public float charmDuration = 0;

    public bool canMove;
    public bool canShoot;
    public bool deepWoundedByEarthEffect;
    public bool iceDamage = false;

    public int shieldStacksState = -1;
    public int charmStacksState = -1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        damageScaler = GameObject.FindGameObjectWithTag("dmgScaler").GetComponent<PlayerDamageScaler>();
        canvasParent = GameObject.FindGameObjectWithTag("c");
        followObject = player.transform;

        if (canMove) states_move = EnemyMoveStates.Empty;
        else states_move = EnemyMoveStates.DoesNotMove;

        SetAllValues();
        SetDeathPoint();
        shieldHolder.transform.position = new Vector2(transform.position.x, transform.position.y + GetComponent<Collider2D>().bounds.size.y + 0.05f);
        charmIdentificatorObject.transform.position = new Vector2(transform.position.x, transform.position.y + GetComponent<Collider2D>().bounds.size.y + 0.05f);
        shieldHolder.GetComponent<SpriteRenderer>().color = ColorsMenu.soulEarth;
    }
    void Update()
    {
        UpdateEffectsDurationTime();
        earthEffectDurationMax = 1.5f + (Mathf.Ceil(player.attackDamage / 25)) + player.additionalEffectsTime;
        if (hp_enemy <= 0 && states_health != EnemyHealthStates.Dead)
        {
            canChangeEnemyStates = false;
            states_health = EnemyHealthStates.Dead;
            OnDeath();
            Debug.Log("dead");
        }
        switch (states_move)
        {
            case EnemyMoveStates.Empty:
                canShoot = true;
                movementSpeed_enemy = movementSpeed_enemyMax;
                break;
            case EnemyMoveStates.Silenced:
                canShoot = false;
                movementSpeed_enemy = movementSpeed_enemyMax;
                break;
            case EnemyMoveStates.Stuned:
                movementSpeed_enemy = 0;
                canShoot = false;
                break;
            case EnemyMoveStates.Slowed:
                movementSpeed_enemy *= 1 / (player.abilityPower / 10);
                canShoot = true;
                break;
        }
    }
    public void DamageEnemy(float damage, DamageType damageType, SoulTypes soulType, bool deepWounds)
    {
        GameObject bloodEf = Instantiate(bloodEffect, transform.position, Quaternion.identity);
        Destroy(bloodEf, 2f);
        if (deepWoundedByEarthEffect) deepWounds = true;
        if (deepWounds) damageType = DamageType.trueDamage;
        StartCoroutine(HitEnemy());
        switch (damageType)
        {
            case DamageType.attackDamage:
                hp_enemy -= Mathf.Ceil(damage * 0.01f * (100 - armour));
                GameObject go = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                go.GetComponent<TextMeshProUGUI>().color = ColorsMenu.damage_ad;
                go.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(damage * 0.01f * (100 - armour)).ToString();
                Destroy(go, 1);
                break;
            case DamageType.abilityPower:
                if (player.pWeapon == PlayerScript.WeaponTypes.Wand)
                {
                    hp_enemy -= Mathf.Ceil((damage + (player.abilityPower * player.additionalApScaler)) * 0.01f * (100 - magicRessists));
                    GameObject go1 = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                    go1.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil((damage + (player.abilityPower * player.additionalApScaler)) * 0.01f * (100 - magicRessists)).ToString();
                    go1.GetComponent<TextMeshProUGUI>().color = ColorsMenu.damage_ap;
                    Destroy(go1, 1);
                }
                else
                {
                    hp_enemy -= Mathf.Ceil(damage * 0.01f * (100 - magicRessists));
                    GameObject go1 = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                    go1.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(damage * 0.01f * (100 - magicRessists)).ToString();
                    go1.GetComponent<TextMeshProUGUI>().color = ColorsMenu.damage_ap;
                    Destroy(go1, 1);
                }
                break;
            case DamageType.trueDamage:
                if(player.pWeapon == PlayerScript.WeaponTypes.Wand)
                {
                    hp_enemy -= Mathf.Ceil(damage + player.additionalApScaler * player.abilityPower);
                    GameObject go2 = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                    go2.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(damage + player.additionalApScaler * player.abilityPower).ToString();
                    go2.GetComponent<TextMeshProUGUI>().color = Color.white;
                    Destroy(go2, 1);
                }
                else
                {
                    hp_enemy -= damage;
                    GameObject go2 = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                    go2.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(damage).ToString();
                    go2.GetComponent<TextMeshProUGUI>().color = Color.white;
                    Destroy(go2, 1);
                }
                break;
        }
        switch(soulType)
        {
            case SoulTypes.Earth:
                states_move = EnemyMoveStates.Stuned;
                shieldStacksState++;
                if (!deepWoundedByEarthEffect)
                {
                    switch (shieldStacksState)
                    {
                        case 0:
                            shieldHolder.GetComponent<SpriteRenderer>().sprite = SpritesMenu.brownStacks_faze1;
                            if (earthEffectDuration <= 0) StartCoroutine(EarthEffect());
                            earthEffectDuration = earthEffectDurationMax;
                            GameObject earthEffectPart = Instantiate(brownSoulEffect, transform.position, transform.rotation, transform);
                            ParticleSystem.MainModule efp = earthEffectPart.GetComponent<ParticleSystem>().main;
                            efp.startColor = ColorsMenu.soulEarth;
                            break;
                        case 1:
                            shieldHolder.GetComponent<SpriteRenderer>().sprite = SpritesMenu.brownStacks_faze2;
                            earthEffectDuration = earthEffectDurationMax;
                            GameObject earthEffectPart1 = Instantiate(brownSoulEffect, transform.position, transform.rotation, transform);
                            ParticleSystem.MainModule efp1 = earthEffectPart1.GetComponent<ParticleSystem>().main;
                            efp1.startColor = ColorsMenu.soulEarth;
                            break;
                        case 2:
                            shieldHolder.GetComponent<SpriteRenderer>().sprite = SpritesMenu.brownStacks_faze3;
                            deepWoundedByEarthEffect = true;
                            earthEffectDuration = earthEffectDurationMax;
                            shieldStacksState = -1;
                            GameObject go = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                            go.GetComponent<TextMeshProUGUI>().color = ColorsMenu.soulEarth;
                            go.GetComponent<TextMeshProUGUI>().text = "Brittle";
                            go.GetComponent<Pop_damage>().isState = true;
                            GameObject earthEffectPart2 = Instantiate(brownSoulEffect, transform.position, transform.rotation, transform);
                            ParticleSystem.MainModule efp2 = earthEffectPart2.GetComponent<ParticleSystem>().main;
                            efp2.startColor = ColorsMenu.soulEarth;
                            DamageEnemy(Mathf.Ceil(damageScaler.earthDamage), DamageType.trueDamage, SoulTypes.Empty, true);
                            break;
                    }
                }
                break;
            case SoulTypes.Fire:
                if (states_health == EnemyHealthStates.Burning)
                {
                    BurningCumulator += Mathf.Ceil(damageScaler.fireDamage);
                    burnDuration = burnDurationMax;
                    states_health = EnemyHealthStates.Burning;
                    DamageEnemy(BurningCumulator, DamageType.attackDamage, SoulTypes.Empty, false);
                }
                else
                {
                    GameObject go = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                    go.GetComponent<TextMeshProUGUI>().color = ColorsMenu.soulFire;
                    go.GetComponent<TextMeshProUGUI>().text = "Burned";
                    go.GetComponent<Pop_damage>().isState = true;
                    states_health = EnemyHealthStates.Burning;
                    BurningCumulator = Mathf.Ceil(damageScaler.fireDamage);
                    burnDuration = burnDurationMax;
                    DamageEnemy(BurningCumulator, DamageType.attackDamage, SoulTypes.Empty, false);
                    StartCoroutine(Burning());
                }
                break;
            case SoulTypes.Poison:
                if(states_health == EnemyHealthStates.Poisoned)
                {
                    poisonDuration = poisonDurationMax;
                }
                else
                {
                    states_health = EnemyHealthStates.Poisoned;
                    GameObject go3 = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                    go3.GetComponent<TextMeshProUGUI>().color = ColorsMenu.soulPoison;
                    go3.GetComponent<TextMeshProUGUI>().text = "Poisoned";
                    go3.GetComponent<Pop_damage>().isState = true;
                    poisonDuration = poisonDurationMax;
                    StartCoroutine(Poisoned());
                }
                break;
            case SoulTypes.Ice:
                if (slowDuration <= 0 && states_move != EnemyMoveStates.Stuned) 
                {
                    slowDuration = slowDurationMax;
                    Debug.Log(slowDuration);
                    states_move = EnemyMoveStates.Slowed;
                    GameObject go1 = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
                    go1.GetComponent<TextMeshProUGUI>().color = ColorsMenu.soulIce;
                    go1.GetComponent<TextMeshProUGUI>().text = "Slowed";
                    go1.GetComponent<Pop_damage>().isState = true;
                    StartCoroutine(Slowed());
                }
                else if(states_move != EnemyMoveStates.Stuned)
                {
                    StartCoroutine(Stuned(ColorsMenu.soulIce));
                }
                break;
            case SoulTypes.Electric:
                GameObject electricEffect = Instantiate(fuzzyEffect, transform.position, transform.rotation, transform);
                ParticleSystem.MainModule ps1 = electricEffect.GetComponent<ParticleSystem>().main;
                ps1.startColor = ColorsMenu.soulElectric;
                foreach (GameObject r in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if(Vector2.Distance(transform.position, r.transform.position) <= player.attackRange * 0.5f || !ReferenceEquals(r, gameObject))
                    {
                        r.GetComponent<Enemy_Main>().DamageEnemy(Mathf.Ceil(damageScaler.electricDamage), DamageType.abilityPower, SoulTypes.Empty, false);
                        GameObject electricEffect1 = Instantiate(fuzzyEffect, r.transform.position, transform.rotation, r.transform);
                        ParticleSystem.MainModule ps2 = electricEffect1.GetComponent<ParticleSystem>().main;
                        ps2.startColor = ColorsMenu.soulElectric;
                        Destroy(electricEffect, 2f);
                    }
                }
                break;
            case SoulTypes.Mage:
                GameObject electricEffect2 = Instantiate(fuzzyEffect, transform.position, transform.rotation, transform);
                ParticleSystem.MainModule ps3 = electricEffect2.GetComponent<ParticleSystem>().main;
                ps3.startColor = ColorsMenu.soulMage;
                DamageEnemy(Mathf.Ceil(damageScaler.mageDamage), DamageType.abilityPower, SoulTypes.Empty, false);
                break;
            case SoulTypes.Sweet:
                switch(charmStacksState)
                {
                    case -1:
                        if(states_move != EnemyMoveStates.Stuned)
                        {
                            StartCoroutine(CharmLoader());
                            charmStacksState = 0;
                            charmIdentificatorObject.SetActive(true);
                            charmIdentificatorObject.GetComponent<SpriteRenderer>().color = ColorsMenu.soulSweet;
                            charmIdentificatorObject.GetComponent<SpriteRenderer>().sprite = charmState1;
                            GameObject sweetEffectPart = Instantiate(brownSoulEffect, transform.position, transform.rotation, transform);
                            ParticleSystem.MainModule sfp = sweetEffectPart.GetComponent<ParticleSystem>().main;
                            sfp.startColor = ColorsMenu.soulSweet;
                        }
                        break;
                    case 0:
                        StartCoroutine(Charmed());
                        break;
                    case 1:
                        charmStacksState = -1;
                        states_move = EnemyMoveStates.Empty;
                        break;
                }
                break;
            case SoulTypes.Empty:
                break;
        }
    }
    IEnumerator Slowed()
    {
        while(states_move == EnemyMoveStates.Slowed)
        {
            attackRate_enemy = attackRate_enemyMax - (0.25f * attackRate_enemyMax);
            movementSpeed_enemy = movementSpeed_enemy - (0.25f * movementSpeed_enemyMax);
            slowDuration -= 0.5f;
            GameObject slowEffect1 = Instantiate(slowEffect, transform.position, transform.rotation, transform);
            ParticleSystem.MainModule ps1 = slowEffect1.GetComponent<ParticleSystem>().main;
            ps1.startColor = ColorsMenu.soulIce;
            Destroy(slowEffect1, 2f);
            yield return new WaitForSeconds(0.5f);
            if (slowDuration <= 0 && states_move != EnemyMoveStates.Stuned) states_move = EnemyMoveStates.Empty;
        }
        attackRate_enemy = attackRate_enemyMax;
        movementSpeed_enemy = movementSpeed_enemyMax;
    }
    public void StunFromAnotherObject(Color color)
    {
        if (stunDuration <= 0) StartCoroutine(Stuned(color));
        else stunDuration = stunDurationMax;
    }
    public void CharmedFromAnotherObject()
    {
        StartCoroutine(Charmed());
    }
    IEnumerator CharmLoader()
    {
        charmDuration = 0;
        while(charmDuration < charmDurationMax && states_move != EnemyMoveStates.Charmed)
        {
            charmDuration += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        if(states_move != EnemyMoveStates.Charmed)
        {
            charmIdentificatorObject.SetActive(false);
            charmIdentificatorObject.GetComponent<SpriteRenderer>().sprite = null;
            charmStacksState = -1;
        }
    }
    IEnumerator Charmed()
    {
        charmStacksState = 1;
        states_move = EnemyMoveStates.Charmed;
        charmIdentificatorObject.SetActive(true);
        charmIdentificatorObject.GetComponent<SpriteRenderer>().sprite = charmState2;
        charmDuration = charmDurationMax;
        gameObject.GetComponent<SpriteRenderer>().color *= ColorsMenu.soulSweet;
        CharmCircleObject cco = Instantiate(charmCircle, charmIdentificatorObject.transform.position, Quaternion.identity, canvasParent.transform).GetComponent<CharmCircleObject>();
        cco.follow = gameObject;
        float scaleMod = GetComponent<Collider2D>().bounds.size.x * 3;
        cco.transform.localScale = new Vector2(cco.transform.localScale.x * scaleMod, cco.transform.localScale.y * scaleMod);
        float maxTimeAfter = charmDurationMax;
        if (maxTimeAfter < 1f) maxTimeAfter = 1;
        while(charmDuration > -maxTimeAfter)
        {
            charmDuration -= 0.01f;
            if (canMove && Vector2.Distance(transform.position, followObject.position) > GetComponent<Collider2D>().bounds.size.x) transform.position = Vector2.MoveTowards(transform.position, followObject.position, 0.005f);
            if (charmDuration <= 0) charmIdentificatorObject.GetComponent<SpriteRenderer>().sprite = charmState3;
            else cco.circleImage.fillAmount = 1f - (charmDuration / charmDurationMax);
            if (charmStacksState == -1)
            {
                float cuteDamage = damageScaler.sweetDamage;
                if(charmDuration<=0)
                {
                    cuteDamage *= 2;
                }
                else
                {
                    cuteDamage *= charmDurationMax - charmDuration;
                }
                DamageEnemy(cuteDamage, DamageType.abilityPower, SoulTypes.Empty, false);
                GameObject sweetEffectPart = Instantiate(brownSoulEffect, transform.position, transform.rotation, transform);
                ParticleSystem.MainModule sfp = sweetEffectPart.GetComponent<ParticleSystem>().main;
                sfp.startColor = ColorsMenu.soulSweet;
                StartCoroutine(Stuned(ColorsMenu.soulSweet));
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(cco.gameObject);
        charmIdentificatorObject.SetActive(false);
        charmStacksState = -1;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    IEnumerator Stuned(Color color)
    {
        states_move = EnemyMoveStates.Stuned;
        stunDuration = stunDurationMax;
        slowDuration = 0;
        if (color == ColorsMenu.soulMage || color == ColorsMenu.soulSweet) stunDuration /= 2f;
        // stuned text pop up
        GameObject go2 = Instantiate(popUp, RandomPos(0.5f), Quaternion.identity, canvasParent.transform);
        go2.GetComponent<TextMeshProUGUI>().color = color;
        go2.GetComponent<TextMeshProUGUI>().text = "Stuned";
        go2.GetComponent<Pop_damage>().isState = true;
        //
        SpriteRenderer sr = Instantiate(stunIdentificatorObject, shieldHolder.transform.position, Quaternion.identity, transform).GetComponent<SpriteRenderer>();
        sr.color = color;
        while (states_move == EnemyMoveStates.Stuned)
        {
            attackRate_enemy = 0;
            movementSpeed_enemy = 0;
            stunDuration -= 0.5f;
            GameObject stunEffect = Instantiate(slowEffect, transform.position, transform.rotation, transform);
            ParticleSystem.MainModule ps1 = stunEffect.GetComponent<ParticleSystem>().main;
            ps1.startColor = color;
            Destroy(stunEffect, 2f);
            yield return new WaitForSeconds(0.5f);
            if (stunDuration <= 0) states_move = EnemyMoveStates.Empty;
        }
        Destroy(sr.gameObject);
        attackRate_enemy = attackRate_enemyMax;
        movementSpeed_enemy = movementSpeed_enemyMax;
    }
    IEnumerator Burning()
    {
        while (states_health == EnemyHealthStates.Burning)
        { 
            burnDuration -= 0.5f;
            GameObject burnEffect = Instantiate(fireEffect, transform.position, transform.rotation, transform);
            ParticleSystem.MainModule ps1 = burnEffect.GetComponent<ParticleSystem>().main;
            ps1.startColor = ColorsMenu.soulFire;
            Destroy(burnEffect, 2f);
            yield return new WaitForSeconds(0.5f);
            if (burnDuration <= 0) states_health = EnemyHealthStates.Empty;
        }
    }
    IEnumerator Poisoned()
    {
        while(states_health == EnemyHealthStates.Poisoned)
        {
            GameObject poisonEffect1 = Instantiate(poisonEffect, transform.position, transform.rotation, transform);
            ParticleSystem.MainModule ps1 = poisonEffect1.GetComponent<ParticleSystem>().main;
            ps1.startColor = ColorsMenu.soulPoison;
            Destroy(poisonEffect1, 2f);
            DamageEnemy(Mathf.Ceil(damageScaler.poisonDamage), DamageType.abilityPower, SoulTypes.Empty, false);
            poisonDuration -= 0.8f;
            yield return new WaitForSeconds(0.8f);
            if (poisonDuration <= 0) states_health = EnemyHealthStates.Empty;
        }
    }
    IEnumerator EarthEffect()
    {
        earthEffectDuration = earthEffectDurationMax;
        while(earthEffectDuration > 0)
        {
            earthEffectDuration -= 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        deepWoundedByEarthEffect = false;
        shieldHolder.GetComponent<SpriteRenderer>().sprite = null;
        shieldStacksState = -1;
    }
    IEnumerator Iced()
    {
        iceDamage = true;
        while(iceDamage)
        {
            yield return new WaitForSeconds(0.7f);
            DamageEnemy(damageScaler.iceDamage, DamageType.abilityPower, SoulTypes.Empty, false);
        }
    }
    public void OnDeath()
    {
        Drop();
        player.Vampirism((int)player.lifeRegeneration);
        for(int i=0; i<360; i+=90)
        {
            if (i == 90 || i == 270) i *= -1;
            GameObject enemyDeathpartsSpawn = Instantiate(deadEnemyObject, deathPoint[Mathf.Abs(i) / 90], Quaternion.Euler(0, 0, i));
            enemyDeathpartsSpawn.GetComponent<SpriteRenderer>().sprite = deathSprite;
            enemyDeathpartsSpawn.GetComponent<DeathParts_Enemy>().rotation = Mathf.Abs(i) / 90;
            i = Mathf.Abs(i);
        }
        GameObject go = Instantiate(bloodBoomEffect, transform.position, Quaternion.identity);
        Destroy(go, 2f);
        Destroy(gameObject);
    }
    public void ChangeColorSoulTypes(SoulTypes st, GameObject go)
    {
        switch (st)
        {
            case SoulTypes.Earth:
                go.GetComponent<SpriteRenderer>().color = ColorsMenu.soulEarth;
                break;
            case SoulTypes.Electric:
                go.GetComponent<SpriteRenderer>().color = ColorsMenu.soulElectric;
                break;
            case SoulTypes.Ice:
                go.GetComponent<SpriteRenderer>().color = ColorsMenu.soulIce;
                break;
            case SoulTypes.Fire:
                go.GetComponent<SpriteRenderer>().color = ColorsMenu.soulFire;
                break;
            case SoulTypes.Poison:
                go.GetComponent<SpriteRenderer>().color = ColorsMenu.soulPoison;
                break;
            case SoulTypes.Empty:
                go.GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
    }
    public void UpdateEffectsDurationTime()
    {
        charmDurationMax = 3.5f - player.attackSpeed / 300;
        burnDurationMax = player.additionalEffectsTime + 2f;
        poisonDurationMax = player.additionalEffectsTime + 3.5f;
        stunDurationMax = 1f + player.roll.rollLevel / 2f + player.additionalEffectsTime;
        slowDurationMax = 1f + player.roll.rollLevel + player.additionalEffectsTime;
        earthEffectDurationMax = 1.5f + player.roll.rollLevel + player.additionalEffectsTime;
    }
    public void SetAllValues()
    {
        charmDurationMax = 3.5f - player.attackSpeed / 300;
        burnDurationMax = player.additionalEffectsTime + 2f;
        poisonDurationMax = player.additionalEffectsTime + 3.5f;
        stunDurationMax = 1f + player.roll.rollLevel / 2f + player.additionalEffectsTime;
        slowDurationMax = 1f + player.roll.rollLevel + player.additionalEffectsTime;
        earthEffectDurationMax = 1.5f + player.roll.rollLevel + player.additionalEffectsTime;
        hp_enemy = hp_enemyMax;
        movementSpeed_enemy = movementSpeed_enemyMax;
        attackRate_enemy = attackRate_enemyMax;
        deepWoundedByEarthEffect = false;
        canChangeEnemyStates = true;
        states_health = EnemyHealthStates.Empty;
    }
    public void SetDeathPoint()
    {
        deathPoint[0] = new Vector2(transform.position.x + GetComponent<Collider2D>().bounds.size.y / 4, transform.position.y + GetComponent<Collider2D>().bounds.size.y / 4);
        deathPoint[1] = new Vector2(transform.position.x + GetComponent<Collider2D>().bounds.size.y / 4, transform.position.y - GetComponent<Collider2D>().bounds.size.y / 4);
        deathPoint[2] = new Vector2(transform.position.x - GetComponent<Collider2D>().bounds.size.y / 4, transform.position.y - GetComponent<Collider2D>().bounds.size.y / 4);
        deathPoint[3] = new Vector2(transform.position.x - GetComponent<Collider2D>().bounds.size.y / 4, transform.position.y + GetComponent<Collider2D>().bounds.size.y / 4);
    }
    public Vector2 RandomPos(float off)
    {
        Vector2 ret;
        ret.x = transform.position.x + Random.Range(-off, off + 0.01f);
        ret.y = transform.position.y + Random.Range(0, off + 0.01f);
        return ret;
    }
    public IEnumerator HitEnemy() //zmiana obrazka przeciwnika kiedy dostanie obrażenia
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = hitSprite;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = enemySprite;
    }
    public void Drop()
    {

    }
    private void OnDestroy()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<GrenadeScript>())
            {
                child.transform.parent = null;
                child.GetComponent<GrenadeScript>().MakeGrenadeExplode();
            }
            else Destroy(child.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("zone"))
        {
            switch(collision.gameObject.GetComponent<AbilityZoneObject>().soulType)
            {
                case ActiveWeaponScript.SoulTypes.Poison:
                    DamageEnemy(damageScaler.poisonDamage, DamageType.abilityPower, SoulTypes.Poison, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Ice:
                    StartCoroutine(Iced());
                    DamageEnemy(damageScaler.iceDamage, DamageType.abilityPower, SoulTypes.Ice, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Mage:
                    StartCoroutine(Stuned(ColorsMenu.soulMage));
                    DamageEnemy(damageScaler.mageDamage, DamageType.abilityPower, SoulTypes.Mage, false);
                    break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // sprawdzam czy przeciwnicy się nachodzą
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Vector2.Distance(collision.gameObject.transform.position, player.transform.position) < Vector2.Distance(transform.position, player.transform.position))
            {
                followObject = collision.transform;
            }
            else followObject = player.transform;
        }
        else followObject = player.transform;
        //

        if (collision.gameObject.CompareTag("zone"))
        {
            switch (collision.gameObject.GetComponent<AbilityZoneObject>().soulType)
            {
                case ActiveWeaponScript.SoulTypes.Poison:
                    poisonDuration = poisonDurationMax;
                    break;
                case ActiveWeaponScript.SoulTypes.Mage:
                    stunDuration = stunDurationMax;
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) followObject = player.transform;
        if (collision.gameObject.CompareTag("zone"))
        {
            switch (collision.gameObject.GetComponent<AbilityZoneObject>().soulType)
            {
                case ActiveWeaponScript.SoulTypes.Ice:
                    iceDamage = false;
                    break;
            }
        }
    }
}
