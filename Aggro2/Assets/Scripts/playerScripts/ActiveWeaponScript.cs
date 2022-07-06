using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveWeaponScript : MonoBehaviour
{
    public enum SoulTypes {Fire, Ice, Poison, Earth, Electric, Dark, Mage, Sweet, Empty};
    public enum WeaponTypesAW { Pistol, Sword, Wand, Bow, Empty };
    public WeaponTypesAW activeWeaponAW;
    public SoulTypes activeSoulTypeAW;

    public GameObject wandBulletObject;
    public GameObject bulletObject;
    public GameObject arrowObject;
    public GameObject swordProjectileObject;
    public GameObject chargeParticleObject;
    public GameObject gunShootParticle1;

    public GameObject rotateCenter;
    public GameObject trueLookObject; // ten obiekt patrzy się w stronę myszki w miejscu gracza

    public Transform shootPosition;
    public Transform wandShootPoint;
    public Transform arrowShootPoint;
    public Transform swordShootPosition;
    public Transform chargePoint;

    public Transform reflectionSpawnPoint; // kiedy wyrzucona broń miałaby pojawić się za planszą, pojaw obiekt w tym miejscu

    public SpriteRenderer playerSprite;
    public SpriteRenderer ebSprite;

    //charge bar values
    public Slider chargeBar;
    public Image chargeBarFillImage;
    public TextMeshProUGUI maxText;
    public Gradient chargeBarGradient;

    public Vector2 mousePosition;

    public float angle;

    public bool isChangingWeapon = false;
    public bool isReloading = false;
    public bool rangeSwordAttacks = false;

    public GameObject dropPistol;
    public GameObject dropBow;
    public GameObject dropWand;
    public GameObject dropSword;

    public GameObject swordTrailEffect;
    public GameObject swordFuzzyEffect;

    public GameObject stillTextObject;

    public GameObject ammoTrack;

    public Sprite pLookUpleft;
    public Sprite pLookUpRight;
    public Sprite pLookDownLeft;
    public Sprite pLookDownRight;
    public Sprite pLookMidLeft;
    public Sprite pLookMidRight;

    public Sprite pLookUpleftEB;
    public Sprite pLookUpRightEB;
    public Sprite pLookDownLeftEB;
    public Sprite pLookDownRightEB;
    public Sprite pLookMidLeftEB;
    public Sprite pLookMidRightEB;

    public Colors ColorsMenu;
    public Sprites SpritesMenu;
    public Scalers ScalersMenu;
    public PlayerDamageScaler pdScaler;

    public Animator weaponAnim;

    [HideInInspector] public PlayerScript player;

    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool canSlash;
    [HideInInspector] public bool canShootBow;
    [HideInInspector] public bool canUseWand;

    [HideInInspector] public bool firstUsed = false; // nie każdy autos z dystansowego miecza stunuje wiec mam osobną zmienną która pilnuje by tylko pierwszy autos stunował

    //basic stats
    [HideInInspector] public Sprite activeWeaponSprite;
    [HideInInspector] public float damage = 0;
    [HideInInspector] public int rareLevel;

    //pistol stats
    [HideInInspector] public bool isShootgun; 
    [HideInInspector] public bool piercing; 
    [HideInInspector] public float fireRate; //UI
    [HideInInspector] public float bulletSpeed; //UI
    [HideInInspector] public float bulletRange; //UI
    [HideInInspector] public float bulletSizeG; //UI
    [HideInInspector] public int bulletQuanitity; //UI
    [HideInInspector] public int magazineSizeMax; //UI
    [HideInInspector] public int bulletsLeft;

    //sword stats
    [HideInInspector] public bool bouncesBulletBack; //UI b
    [HideInInspector] public bool isRapier; //UI b
    [HideInInspector] public float attackRate; //UI
    [HideInInspector] public float attackRadious; //UI
    [HideInInspector] public float passiveSwordCritChance; //UI

    //wand stats
    [HideInInspector] public bool isDruid; //UI b
    [HideInInspector] public float additionalApScaler; //UI
    [HideInInspector] public float wandAttackRate; //UI
    [HideInInspector] public float additionalCDR; //UI
    [HideInInspector] public float autoAbilitySpeed; //UI
    [HideInInspector] public float autoAbilityRange; //UI
    [HideInInspector] public float chargingSpeed; //UI

    //bow stats
    [HideInInspector] public bool isCrossBow; //UI b
    [HideInInspector] public float chargingAttackTime;//UI
    [HideInInspector] public float arrowsSize; //UI
    [HideInInspector] public float arrowStayTime;
    [HideInInspector] public float arrowSpeed; //UI
    [HideInInspector] public float arrowRange; //UI
    void Start()
    {
        pdScaler = GameObject.FindGameObjectWithTag("dmgScaler").GetComponent<PlayerDamageScaler>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        canShoot = true;
        canUseWand = true;
        canShootBow = true;
        canSlash = true;
        ammoTrack = GameObject.FindGameObjectWithTag("ammo");
        playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        weaponAnim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        //chargeBar.transform.position = new Vector2(transform.position.x, transform.position.y + 0.4f);
        chargeBar.transform.position = new Vector2(rotateCenter.transform.position.x, rotateCenter.transform.position.y + 0.4f);

        //wlaczanie animacji łuku
        if (activeWeaponAW == WeaponTypesAW.Bow) weaponAnim.SetBool("Bow", true);
        else weaponAnim.SetBool("Bow", false);

        //właczanie ui amunicji
        if (activeWeaponAW == WeaponTypesAW.Pistol) ammoTrack.SetActive(true);
        else ammoTrack.SetActive(false);

        //mouse position
        mousePosition = Input.mousePosition;
        Vector2 awPosition = Camera.main.WorldToScreenPoint(rotateCenter.transform.position);
        mousePosition.x -= awPosition.x;
        mousePosition.y -= awPosition.y;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        trueLookObject.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        rotateCenter.transform.rotation = Quaternion.Euler(0, 0, angle);

        //obracanie gracza 
        if (mousePosition.x < rotateCenter.transform.position.x) rotateCenter.transform.Rotate(180, 0, 0);
        else rotateCenter.transform.Rotate(0, 0, 0);

        //zmiana spritow gracza
        OnPlayerLookChange();
        UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, gameObject, activeSoulTypeAW);
    }
    public void SetActiveBow(bool isCrossBowSAB, float damageSAB, float chargingAttackSpeedSAB, float arrowsSizeSAB, float arrowsStayTimeSAB, float arrowSpeedSAB, float arrowRangeSAB, int rareLevelSAB, Sprite spriteSAB)
    {
        weaponAnim.SetBool("Bow", true);
        canShootBow = false;
        isChangingWeapon = true;
        isCrossBow = isCrossBowSAB;
        damage = damageSAB;
        chargingAttackTime = chargingAttackSpeedSAB;
        arrowsSize = arrowsSizeSAB;
        arrowStayTime = arrowsStayTimeSAB;
        arrowSpeed = arrowSpeedSAB;
        arrowRange = arrowRangeSAB;
        rareLevel = rareLevelSAB;
        activeWeaponSprite = spriteSAB;
        gameObject.GetComponent<SpriteRenderer>().sprite = activeWeaponSprite;
        UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, gameObject, activeSoulTypeAW);
        activeWeaponAW = WeaponTypesAW.Bow;
        canShootBow = true;
        isChangingWeapon = false;
        OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
    }
    public void SetActiveWand(bool isDruidSAW, float damageSAW, float additionalApScalerSAW, float attackSpeedSAW, float additionalcrdSAW, float autoAbilitySpeedSAW, float autoAbilityRangeSAW, float chargingSpeedSAW, int rareLevelSAW, Sprite wandSpriteSAW)
    {
        StopCoroutine(WandAttack());
        isChangingWeapon = true;
        canUseWand = false;
        isDruid = isDruidSAW;
        additionalApScaler = additionalApScalerSAW;
        wandAttackRate = attackSpeedSAW;
        additionalCDR = additionalcrdSAW;
        autoAbilityRange = autoAbilityRangeSAW;
        autoAbilitySpeed = autoAbilitySpeedSAW;
        chargingSpeed = chargingSpeedSAW;
        rareLevel = rareLevelSAW;
        damage = damageSAW;
        activeWeaponSprite = wandSpriteSAW;
        player.additionalApScaler = additionalApScaler * damage;
        gameObject.GetComponent<SpriteRenderer>().sprite = activeWeaponSprite;
        UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, gameObject, activeSoulTypeAW);
        activeWeaponAW = WeaponTypesAW.Wand;
        canUseWand = true;
        isChangingWeapon = false;
        player.coolDownReduction += additionalCDR;
        OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
    }
    public void SetActivePistol(bool shootGunSAP, bool piercingSAP, float fireRateSAP, float damageSAP, float bulletSpeedSAP, float bulletRangeSAP, float sizeSAP, int bulletQuantitySAP, int rareLevelSAP, int bulletsLeftSAP, int magazineSizeMaxSAP, Sprite spriteSAP)
    {
        StopCoroutine(Shoot());
        isChangingWeapon = true;
        canShoot = false;
        isShootgun = shootGunSAP;
        piercing = piercingSAP;
        fireRate = fireRateSAP;
        damage = damageSAP;
        bulletSpeed = bulletSpeedSAP;
        bulletRange = bulletRangeSAP;
        bulletSizeG = sizeSAP;
        bulletQuanitity = bulletQuantitySAP;
        rareLevel = rareLevelSAP;
        activeWeaponSprite = spriteSAP;
        magazineSizeMax = magazineSizeMaxSAP;
        bulletsLeft = bulletsLeftSAP;
        ammoTrack.GetComponent<TextMeshProUGUI>().text = bulletsLeft.ToString() + "/" + magazineSizeMax.ToString();
        gameObject.GetComponent<SpriteRenderer>().sprite = activeWeaponSprite;
        UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, gameObject, activeSoulTypeAW);
        activeWeaponAW = WeaponTypesAW.Pistol;
        canShoot = true;
        isChangingWeapon = false;
        OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
    }
    public void SetActiveSword(bool bouncesBulletBackSAS, bool isRapierSAS, int rareLevelSAS, float attackRateSAS, float damageSwordSAS, float attackRadiousSAS, float passiveSwordCritChanceSAS, Sprite swordSpriteSAS)
    {
        StopCoroutine(SwordAttack());
        isChangingWeapon = true;
        canSlash = false;
        bouncesBulletBack = bouncesBulletBackSAS;
        isRapier = isRapierSAS;
        rareLevel = rareLevelSAS;
        attackRate = attackRateSAS;
        damage = damageSwordSAS;
        attackRadious = attackRadiousSAS;
        passiveSwordCritChance = passiveSwordCritChanceSAS;
        activeWeaponSprite = swordSpriteSAS;
        gameObject.GetComponent<SpriteRenderer>().sprite = activeWeaponSprite;
        UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, gameObject, activeSoulTypeAW);
        activeWeaponAW = WeaponTypesAW.Sword;
        canSlash = true;
        isChangingWeapon = false;
        OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
    }
    public void DropWeapon()
    {
        Vector2 spawnWeaponPos = transform.position;
        if(!CheckPos(transform))
        {
            spawnWeaponPos = reflectionSpawnPoint.position;
        }
        switch (activeWeaponAW)
        {
            case WeaponTypesAW.Pistol:
                StopCoroutine(Shoot());
                canShoot = false;
                DropPistol dp = Instantiate(dropPistol, spawnWeaponPos, Quaternion.identity).GetComponent<DropPistol>();
                dp.isShootgun = isShootgun;
                dp.piercing = piercing;
                dp.fireRate = fireRate;
                dp.damage = damage;
                dp.bulletSpeed = bulletSpeed;
                dp.bulletRange = bulletRange;
                dp.size = bulletSizeG;
                dp.bulletQuanitity = bulletQuanitity;
                dp.rareLevel = rareLevel;
                dp.magazineSizeMax = magazineSizeMax;
                dp.bulletsLeft = bulletsLeft;
                dp.gunSprite = activeWeaponSprite;
                break;
            case WeaponTypesAW.Wand:
                StopCoroutine(WandAttack());
                canUseWand = false;
                DropWand dw = Instantiate(dropWand, spawnWeaponPos, Quaternion.identity).GetComponent<DropWand>();
                player.coolDownReduction -= additionalCDR;
                dw.isDruid = isDruid;
                dw.additionalApScaler = additionalApScaler;
                dw.attackSpeed = wandAttackRate;
                dw.additionalCDR = additionalCDR;
                dw.autoAbilityRange = autoAbilityRange;
                dw.autoAbilitySpeed = autoAbilitySpeed;
                dw.chargingSpeed = chargingSpeed;
                dw.damage = damage;
                dw.rareLevel = rareLevel;
                dw.wandSprite = activeWeaponSprite;
                break;
            case WeaponTypesAW.Bow:
                canShootBow = false;
                DropBow db = Instantiate(dropBow, spawnWeaponPos, Quaternion.identity).GetComponent<DropBow>();
                db.isCrossBow = isCrossBow;
                db.bowDamage = damage;
                db.arrowSpeed = arrowSpeed;
                db.arrowsSize = arrowsSize;
                db.arrowStayTime = arrowStayTime;
                db.arrowRange = arrowRange;
                db.chargingAttackTime = chargingAttackTime;
                db.bowSprite = activeWeaponSprite;
                db.rareLevel = rareLevel;
                break;
            case WeaponTypesAW.Sword:
                StopCoroutine(SwordAttack());
                canShoot = false;
                DropSword ds = Instantiate(dropSword, spawnWeaponPos, Quaternion.identity).GetComponent<DropSword>();
                ds.isRapier = isRapier;
                ds.bouncesBulletBack = bouncesBulletBack;
                ds.rareLevel = rareLevel;
                ds.attackRate = attackRate;
                ds.swordDamage = damage;
                ds.attackRadious = attackRadious;
                ds.passiveSwordCritChance = passiveSwordCritChance;
                ds.swordSprite = activeWeaponSprite;
                break;
            case WeaponTypesAW.Empty:
                break;
        }
    }
    public IEnumerator ShootBow()
    {
        canShootBow = false;
        float chargingProces = 0;
        float realDamage = 0;
        float realSpeed = 0;
        float realLifeTime = 0;
        ArrowScript asc = Instantiate(arrowObject, arrowShootPoint.position, Quaternion.Euler(0, 0, angle -90), transform).GetComponent<ArrowScript>();
        UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, asc.transform.GetChild(0).gameObject, activeSoulTypeAW);
        asc.gameObject.transform.localScale = new Vector2(arrowsSize, arrowsSize);
        asc.GetComponent<Collider2D>().enabled = false;
        chargeBar.gameObject.SetActive(true);
        chargeBar.maxValue = chargingAttackTime;
        chargeBar.value = 0;
        chargeBarFillImage.color = chargeBarGradient.Evaluate(1);
        maxText.text = "";
        while (Input.GetMouseButton(0))
        {
            if(chargingProces < chargingAttackTime)
            {
                realDamage += damage * player.attackDamage / (chargingAttackTime * 10);
                realSpeed += arrowSpeed / (chargingAttackTime * 10);
                realLifeTime += arrowRange * player.attackRange/100 / (chargingAttackTime * 10);
                chargingProces += 0.1f;
                chargeBar.value = chargingProces;
                chargeBarFillImage.color = chargeBarGradient.Evaluate(chargingProces / chargingAttackTime);
            }
            else  
            {
                maxText.text = "Max";
                if (Input.GetMouseButtonUp(0)) break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        chargeBar.gameObject.SetActive(false);
        if (realSpeed >= 2f)
        {
            asc.stayTime = arrowStayTime;
            if (chargingProces >= chargingAttackTime) asc.damage = realDamage * 1.5f;
            else asc.damage = realDamage;
            asc.speed = realSpeed;
            asc.range = realLifeTime;
            asc.canFly = true;
            asc.arrowSoulType = activeSoulTypeAW;
            asc.GetComponent<Collider2D>().enabled = true;
        }
        else Destroy(asc.gameObject);
        yield return new WaitForSeconds(0.3f);
        canShootBow = true;
    }
    public IEnumerator WandAttack()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().enabled = true;
        canUseWand = false;
        player.canPickUp = false;
        weaponAnim.SetTrigger("UseWand");
        weaponAnim.speed = 1;
        weaponAnim.speed /= chargingSpeed;
        GameObject go =Instantiate(stillTextObject, player.GetComponent<Transform>().position, Quaternion.identity, GameObject.FindGameObjectWithTag("c").transform);
        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = "Charging";
        float fullTimeCharge = 0;
        StartChargingParticles();
        while (Input.GetMouseButton(0) && !isChangingWeapon)
        {
            fullTimeCharge += 0.1f;
            yield return new WaitForSeconds(0.1f);
            if(fullTimeCharge>=chargingSpeed)
            {
                WandBullet wandBullet = Instantiate(wandBulletObject, wandShootPoint.position, Quaternion.Euler(0, 0, angle - 90)).GetComponent<WandBullet>();
                wandBullet.bulletSoulType = activeSoulTypeAW;
                wandBullet.damage = Mathf.Ceil(player.abilityPower * damage);
                wandBullet.range = player.attackRange/100 * autoAbilityRange;
                wandBullet.speed = player.attackSpeed/100 * autoAbilitySpeed;
                UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, wandBullet.gameObject, activeSoulTypeAW);
                break;
            }
        }
        StopChargingParticles();
        weaponAnim.Play("DefAnim");
        Destroy(go);
        yield return new WaitForSeconds(1 / (player.attackSpeed/100 * wandAttackRate));
        canUseWand = true;
        player.canPickUp = true;
    }
    public bool CheckIfEnemyWasHit(List<GameObject> list, GameObject c)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (GameObject.ReferenceEquals(list[i], c.gameObject)) return true;
        }
        return false;
    }
    public IEnumerator SwordAttack()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().enabled = true;
        canSlash = false;
        weaponAnim.SetTrigger("Swing");
        weaponAnim.speed = player.attackSpeed/100 * attackRate;

        //sword trail effect
        GameObject trailEffect = Instantiate(swordTrailEffect, transform.position, transform.rotation, transform);
        ParticleSystem.MainModule ps = trailEffect.GetComponent<ParticleSystem>().main;
        if (activeSoulTypeAW != SoulTypes.Empty)  UseFullTools.ChangeColors_soulType_ParticleSystem(ColorsMenu, trailEffect, activeSoulTypeAW);
        else ps.startColor = Color.white;
        Destroy(trailEffect, 0.2f + (1 / (player.attackSpeed/100 * attackRate)));

        //sword fuzzy effect
        GameObject fuzzyEffect = Instantiate(swordFuzzyEffect, transform.position, transform.rotation, transform);
        UseFullTools.ChangeColors_soulType_ParticleSystem(ColorsMenu, fuzzyEffect, activeSoulTypeAW);
        Destroy(fuzzyEffect, 0.5f + (1 / (player.attackSpeed/100 * attackRate)));

        //range attack
        if(rangeSwordAttacks)
        {
            SwordProjectileScript sps = Instantiate(swordProjectileObject, swordShootPosition.position, Quaternion.Euler(0, 0, angle - 90)).GetComponent<SwordProjectileScript>();
            UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, sps.gameObject, activeSoulTypeAW);
            sps.damage = pdScaler.swordAbilityDamage;
            sps.speed = (player.moveSpeed / 100) + (player.moveSpeed / 100) * attackRate;
            sps.range = player.attackRange/100 * attackRadious / 1.25f;
            if (activeSoulTypeAW == SoulTypes.Mage && !firstUsed) 
            {
                sps.first = true;
                firstUsed = true;
            }
            else sps.first = false;
            sps.projectileSoulType = activeSoulTypeAW;
        }

        //normal attack
        List<GameObject> hitEnemyList = new List<GameObject>();
        float maxWaitTime = 1 / (player.attackSpeed / 100 * attackRate);
        maxWaitTime -= maxWaitTime*0.2f;
        yield return new WaitForSeconds(maxWaitTime * 0.2f);
        float waitTimeOG = 0;
        while (waitTimeOG < maxWaitTime - 0.7f * maxWaitTime)
        {
            foreach (Collider2D c in Physics2D.OverlapCircleAll(rotateCenter.transform.position, attackRadious))
            {
                Vector2 toTarget = (c.transform.position - trueLookObject.transform.position).normalized;
                if (c.CompareTag("Enemy") && Vector3.Dot(toTarget, trueLookObject.transform.up) >= -0.3f && Vector2.Distance(transform.position, c.transform.position) <= attackRadious)
                {
                    if (!CheckIfEnemyWasHit(hitEnemyList, c.gameObject))
                    {
                        switch (activeSoulTypeAW)
                        {
                            case SoulTypes.Earth:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Earth, isRapier);
                                break;
                            case SoulTypes.Poison:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Poison, isRapier);
                                break;
                            case SoulTypes.Fire:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Fire, isRapier);
                                break;
                            case SoulTypes.Ice:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Ice, isRapier);
                                break;
                            case SoulTypes.Electric:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Electric, isRapier);
                                break;
                            case SoulTypes.Empty:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Empty, isRapier);
                                break;
                            case SoulTypes.Dark:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Dark, isRapier);
                                break;
                            case SoulTypes.Sweet:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Sweet, isRapier);
                                break;
                            case SoulTypes.Mage:
                                c.GetComponent<Enemy_Main>().DamageEnemy(player.attackDamage * damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Mage, isRapier);
                                break;
                        }
                        hitEnemyList.Add(c.gameObject);
                    }
                } 
            }
            waitTimeOG += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.7f * maxWaitTime);
        canSlash = true;
    }
    public IEnumerator Reload()
    {
        StopCoroutine(Shoot());
        float lastSpeed = player.moveSpeed;
        player.moveSpeed /= 2;
        player.canPickUp = false;
        isReloading = true;
        GameObject go = Instantiate(stillTextObject, player.GetComponent<Transform>().position, Quaternion.identity, GameObject.FindGameObjectWithTag("c").transform);
        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = "Reloading";
        weaponAnim.SetTrigger("Reload");
        weaponAnim.speed = 1;
        weaponAnim.speed /= player.reloadTime;
        yield return new WaitForSeconds(player.reloadTime);
        weaponAnim.Play("DefAnim");
        weaponAnim.speed = 1;
        Destroy(go);
        bulletsLeft = magazineSizeMax;
        player.canPickUp = true;
        isReloading = false;
        canShoot = true;
        player.moveSpeed = lastSpeed;
        ammoTrack.GetComponent<TextMeshProUGUI>().text = bulletsLeft.ToString() + "/" + magazineSizeMax.ToString();
    }
    public IEnumerator Shoot() // shoot
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().enabled = true;
        GameObject shootPart1 = Instantiate(gunShootParticle1, shootPosition.position, Quaternion.Euler(0, 0, angle), transform);
        ParticleSystem.MainModule ps1 = shootPart1.GetComponent<ParticleSystem>().main;
        if (activeSoulTypeAW != SoulTypes.Empty) UseFullTools.ChangeColors_soulType_ParticleSystem(ColorsMenu, shootPart1, activeSoulTypeAW);
        else ps1.startColor = Color.white;
        weaponAnim.speed = 1;
        canShoot = false;
        if (bulletsLeft > 0)
        {
            float lastAngle = angle;
            for (int i = 0; i < bulletQuanitity; i++)
            {
                float bulletAngle = angle;
                float sgAngle = 0;
                while (isShootgun && Mathf.Abs(sgAngle - lastAngle) <= 10)
                {
                    sgAngle = Random.Range(-5 * bulletQuanitity, 5 * bulletQuanitity);
                }
                lastAngle = sgAngle;
                bulletAngle += sgAngle;
                BulletScript bs = Instantiate(bulletObject, shootPosition.position, Quaternion.Euler(0, 0, bulletAngle - 90)).GetComponent<BulletScript>();
                if (isShootgun) bs.speed = Random.Range(bulletSpeed, bulletSpeed / 1.1f);
                else bs.speed = bulletSpeed;
                bs.range = player.attackRange/100 * bulletRange;
                bs.piercing = piercing;
                bs.transform.localScale *= bulletSizeG;
                bs.damage = player.attackDamage * damage;
                bs.bulletSoulType = activeSoulTypeAW;
                UseFullTools.ChangeColors_soulType_SpriteRenderer(ColorsMenu, bs.gameObject, activeSoulTypeAW);
            }
            weaponAnim.SetTrigger("Shoot");
            bulletsLeft--;
            ammoTrack.GetComponent<TextMeshProUGUI>().text = bulletsLeft.ToString() + "/" + magazineSizeMax.ToString();
            yield return new WaitForSeconds(1 / (player.attackSpeed/100 * fireRate));
            canShoot = true;
        }
        if(bulletsLeft==0 && !isReloading) StartCoroutine(Reload());
    }
    public void OnWeaponChange(GameObject weaponSpriteUI, GameObject soulUI, GameObject weaponUI, TextMeshProUGUI rareUI)
    {
        switch (activeWeaponAW)
        {
            case WeaponTypesAW.Wand:
                if (activeWeaponSprite == SpritesMenu.wand_defWand) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.wand_defWandUI;
                else if (activeWeaponSprite == SpritesMenu.wand_occultScepter) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.wand_occultScepterUI;
                else if (activeWeaponSprite == SpritesMenu.wand_druidWand) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.wand_druidWandUI;
                else if (activeWeaponSprite == SpritesMenu.wand_crystalWand) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.wand_crystalWandUI;
                else weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.wand_darkLordWandUI;
                break;
            case WeaponTypesAW.Bow:
                if (activeWeaponSprite == SpritesMenu.bow_defBow) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.bow_defBowUI;
                else if (activeWeaponSprite == SpritesMenu.bow_longBow) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.bow_longBowUI;
                else weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.bow_crossBowUI;
                break;
            case WeaponTypesAW.Pistol:
                if (activeWeaponSprite == SpritesMenu.gun_Pistol) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.gun_PistolUI;
                else if (activeWeaponSprite == SpritesMenu.gun_Uzi) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.gun_UziUI;
                else if (activeWeaponSprite == SpritesMenu.gun_MachineGun) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.gun_MachineGunUI;
                else if (activeWeaponSprite == SpritesMenu.gun_ShootGun) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.gun_ShootGunUI;
                else weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.gun_SniperUI;
                break;
            case WeaponTypesAW.Sword:
                if (activeWeaponSprite == SpritesMenu.sword_lightsaber) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.sword_lightsaberUI;
                else if (activeWeaponSprite == SpritesMenu.sword_rapier) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.sword_rapierUI;
                else if (activeWeaponSprite == SpritesMenu.sword_scythe) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.sword_scytheUI;
                else if (activeWeaponSprite == SpritesMenu.sword_stealSword) weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.sword_stealSwordUI;
                else weaponSpriteUI.GetComponent<Image>().sprite = SpritesMenu.sword_wideSwordUI;
                break;
        }
        switch (activeSoulTypeAW)
        {
            case SoulTypes.Earth:
                soulUI.GetComponent<Image>().color = ColorsMenu.soulEarth;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Earth";
                break;
            case SoulTypes.Electric:
                soulUI.GetComponent<Image>().color = ColorsMenu.soulElectric;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Electric";
                break;
            case SoulTypes.Ice:
                soulUI.GetComponent<Image>().color = ColorsMenu.soulIce;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Ice";
                break;
            case SoulTypes.Fire:
                soulUI.GetComponent<Image>().color = ColorsMenu.soulFire;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Fire";
                break;
            case SoulTypes.Poison:
                soulUI.GetComponent<Image>().color = ColorsMenu.soulPoison;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Poison";
                break;
            case SoulTypes.Empty:
                soulUI.GetComponent<Image>().color = Color.white;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
                break;
            case SoulTypes.Dark:
                soulUI.GetComponent<Image>().color = ColorsMenu.soulDark;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Dark";
                break;
            case SoulTypes.Sweet:
                soulUI.GetComponent<Image>().color = ColorsMenu.soulSweet;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Sweet";
                break;
            case SoulTypes.Mage:
                soulUI.GetComponent<Image>().color = ColorsMenu.soulMage;
                soulUI.GetComponentInChildren<TextMeshProUGUI>().text = "Mage";
                break;
        }
        switch (rareLevel)
        {
            case 0:
                weaponSpriteUI.GetComponent<Image>().color = Color.white;
                weaponUI.GetComponent<Image>().color = Color.white;
                rareUI.text = "I";
                break;
            case 1:
                weaponSpriteUI.GetComponent<Image>().color = ColorsMenu.rareColor1;
                weaponUI.GetComponent<Image>().color = ColorsMenu.rareColor1;
                rareUI.text = "II";
                break;
            case 2:
                weaponSpriteUI.GetComponent<Image>().color = ColorsMenu.rareColor2;
                weaponUI.GetComponent<Image>().color = ColorsMenu.rareColor2;
                rareUI.text = "III";
                break;
            case 3:
                weaponSpriteUI.GetComponent<Image>().color = ColorsMenu.rareColor3;
                weaponUI.GetComponent<Image>().color = ColorsMenu.rareColor3;
                rareUI.text = "IV";
                break;
            case 4:
                weaponSpriteUI.GetComponent<Image>().color = ColorsMenu.rareColor4;
                weaponUI.GetComponent<Image>().color = ColorsMenu.rareColor4;
                rareUI.text = "V";
                break;
        }
    }
    public void StartChargingParticles()
    {
        UseFullTools.ChangeColors_soulType_ParticleSystem(ColorsMenu, chargeParticleObject, activeSoulTypeAW);
        chargeParticleObject.GetComponent<ParticleSystem>().Play();
    }
    public void StopChargingParticles()
    {
        chargeParticleObject.GetComponent<ParticleSystem>().Stop();
    }
    public void OnPlayerLookChange()
    {
        GameObject.FindGameObjectWithTag("playerInventoryImage").GetComponent<IplayerSprite>().OnPlayerLookChange(gameObject.GetComponent<ActiveWeaponScript>());
        if (Mathf.Abs(mousePosition.y) - Mathf.Abs(rotateCenter.transform.position.y) < 60 && mousePosition.x > rotateCenter.transform.position.x)
        {
            playerSprite.sprite = pLookMidRight;
            ebSprite.sprite = pLookMidRightEB;
        }
        else if (Mathf.Abs(mousePosition.y) - Mathf.Abs(rotateCenter.transform.position.y) < 60 && mousePosition.x < rotateCenter.transform.position.x)
        {
            playerSprite.sprite = pLookMidLeft;
            ebSprite.sprite = pLookMidLeftEB;
        }
        else if (mousePosition.x > rotateCenter.transform.position.x && mousePosition.y > rotateCenter.transform.position.y)
        {
            playerSprite.sprite = pLookUpRight;
            ebSprite.sprite = pLookUpRightEB;
        }
        else if (mousePosition.x < rotateCenter.transform.position.x && mousePosition.y > rotateCenter.transform.position.y)
        {
            playerSprite.sprite = pLookUpleft;
            ebSprite.sprite = pLookUpleftEB;
        }
        else if (mousePosition.x > rotateCenter.transform.position.x)
        {
            playerSprite.sprite = pLookDownRight;
            ebSprite.sprite = pLookDownRightEB;
        }
        else
        {
            playerSprite.sprite = pLookDownLeft;
            ebSprite.sprite = pLookDownLeftEB;
        }
    }
    public bool CheckPos(Transform pos)
    {
        if (pos.position.x > ScalersMenu.xBoundsMax) return false;
        if (pos.position.x < ScalersMenu.xBoundsMin) return false;
        if (pos.position.y > ScalersMenu.yBoundsMax) return false;
        if (pos.position.y < ScalersMenu.yBoundsMin) return false;
        return true;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rotateCenter.transform.position, attackRadious);
    }
}
