using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public enum Classes { mage, archer, gunner, knight };
    public enum Souls { Ice, Fire, Poison, Electric, Earth, Sweet, Dark, Mage, Empty };
    public enum States { Burned, Stunned, Slowed, Poisoned, Ragdolled, Normal, Dead };
    public enum WeaponTypes { Pistol, Sword, Wand, Bow, Empty };

    public States pState;
    public Souls pSoul;
    public WeaponTypes pWeapon;
    public Classes playerClas;

    public List<GameObject> trashCan = new List<GameObject>();
    public Transform trashPosition;

    public TextMeshProUGUI hpValueUI;

    public InventoryScript inventory;

    private ActiveWeaponScript activeWeaponScript;

    public Colors colorsMenu;
    public Scalers scalersMenu;

    public GameObject popUp;
    public GameObject fuzzyEffect; //partikle to efektów leczenia etc
    public GameObject healEffect; //partikle do heala
    public GameObject eyeBall;

    public Slider HPbar;

    public TextMeshProUGUI adUI;
    public TextMeshProUGUI apUI;
    public TextMeshProUGUI msUI;
    public TextMeshProUGUI rangeUI;
    public TextMeshProUGUI attackRateUI;
    public TextMeshProUGUI cdrUI;

    private GameObject parentCanvas;
    private GameObject inventoryObj;

    public bool canPickUp;
    public bool canUseWeaponAfterPickUp;
    public bool inventoryIsOpen = false;

    //player stats
    public float attackSpeed; //UI
    public float attackDamage; //UI
    public float abilityPower; //UI
    public float moveSpeed; //UI
    public float lifeRegeneration;
    public float attackRange; //UI
    public float maxHp;
    public float coolDownReduction; //UI
    public float additionalEffectsTime;
    public float reloadTime;
    public float weaponCoolDown;

    [HideInInspector] public float additionalApScaler;

    public int grenadePower = 0;
    public int wandPower = 0;
    public int swordPower = 0;
    public int bowPower = 0;

    public int hp;

    public int LifeRegenOngoing = 0;

    public RollWeapon roll;

    void Start()
    {
        hp = (int)maxHp;
        canUseWeaponAfterPickUp = true;
        canPickUp = true;
        inventoryObj = inventory.gameObject;
        activeWeaponScript = GetComponentInChildren<ActiveWeaponScript>();
        activeWeaponScript.activeWeaponAW = ActiveWeaponScript.WeaponTypesAW.Empty;
        activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Empty;
        parentCanvas = GameObject.FindGameObjectWithTag("c");
        pState = States.Normal;
        pWeapon = WeaponTypes.Empty;
        pSoul = Souls.Empty;
    }
    void OnPistolChange(DropPistol p)
    {
        pWeapon = WeaponTypes.Pistol;
        activeWeaponScript.DropWeapon();
        activeWeaponScript.SetActivePistol(p.isShootgun, p.piercing, p.fireRate, p.damage, p.bulletSpeed, p.bulletRange, p.size, p.bulletQuanitity, p.rareLevel, p.bulletsLeft, p.magazineSizeMax, p.gunSprite);
    }
    void OnSwordChange(DropSword s)
    {
        pWeapon = WeaponTypes.Sword;
        activeWeaponScript.DropWeapon();
        activeWeaponScript.SetActiveSword(s.bouncesBulletBack, s.isRapier, s.rareLevel, s.attackRate, s.swordDamage, s.attackRadious, s.passiveSwordCritChance, s.swordSprite);
    }
    void OnWandChange(DropWand w)
    {
        pWeapon = WeaponTypes.Wand;
        activeWeaponScript.DropWeapon();
        activeWeaponScript.SetActiveWand(w.isDruid, w.damage, w.additionalApScaler, w.attackSpeed, w.additionalCDR, w.autoAbilitySpeed, w.autoAbilityRange, w.chargingSpeed, w.rareLevel, w.wandSprite);
    }
    void OnBowChange(DropBow b)
    {
        pWeapon = WeaponTypes.Bow;
        activeWeaponScript.DropWeapon();
        activeWeaponScript.SetActiveBow(b.isCrossBow, b.bowDamage, b.chargingAttackTime, b.arrowsSize, b.arrowStayTime, b.arrowSpeed, b.arrowRange, b.rareLevel, b.bowSprite);
    }
    void Update()
    {
        hpValueUI.text = hp.ToString() + "/" + maxHp.ToString();
        if(pState != States.Dead)
        {
            adUI.text = "Ad: " + attackDamage.ToString();
            apUI.text = "Ap: " + abilityPower.ToString();
            rangeUI.text = "Rng: " + attackRange.ToString();
            cdrUI.text = "Cdr: " + ((int)(100 * coolDownReduction)).ToString() + "%";
            attackRateUI.text = "Ar: " + attackSpeed.ToString();
            msUI.text = "Ms: " + moveSpeed.ToString();
        }
        HPbar.maxValue = maxHp;
        HPbar.value = hp;
        if (Input.GetKeyDown(KeyCode.Space)) Instantiate(roll.PickWeapon(roll.RandomWeaponType()), transform.position, Quaternion.identity);
        if (Input.GetKeyDown(KeyCode.M)) Instantiate(roll.PickSoul(roll.RandomSoulType()), transform.position, Quaternion.identity);
        if (Input.GetKeyDown(KeyCode.N)) Instantiate(roll.PickCoinValue(roll.RandomLootType()), transform.position, Quaternion.identity);
        if (Input.GetKeyDown(KeyCode.B)) Instantiate(roll.PickLoot(), transform.position, Quaternion.identity);
        if (Input.GetKeyDown(KeyCode.P)) SceneManager.LoadScene(0);
        
        // inventory input
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryIsOpen = !inventoryIsOpen;
            if (inventoryIsOpen) OpenInventory();
            else CloseInventory();
        }

        // input
        if (pState != States.Ragdolled)
        {
            if (Input.GetKeyDown(KeyCode.I)) hp -= 50;
            if (Input.GetKey(KeyCode.W)) //move up
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * moveSpeed / 100);
            }
            if (Input.GetKey(KeyCode.S)) // move down
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - Time.deltaTime * moveSpeed / 100);
            }
            if (Input.GetKey(KeyCode.A)) // move left
            {
                transform.position = new Vector2(transform.position.x - Time.deltaTime * moveSpeed / 100, transform.position.y);
            }
            if (Input.GetKey(KeyCode.D)) // move right
            {
                transform.position = new Vector2(transform.position.x + Time.deltaTime * moveSpeed / 100, transform.position.y);
            }
            if (Input.GetKeyDown(KeyCode.R) && pWeapon == WeaponTypes.Pistol && !activeWeaponScript.isReloading && activeWeaponScript.bulletsLeft < activeWeaponScript.magazineSizeMax) // reload
            {
                activeWeaponScript.canShoot = false;
                StartCoroutine(activeWeaponScript.Reload());
            }
            if (Input.GetMouseButton(0) && canUseWeaponAfterPickUp && !inventoryIsOpen) // attacking
            {
                canPickUp = false;
                activeWeaponScript.StopCoroutine(activeWeaponScript.Shoot());
                activeWeaponScript.StopCoroutine(activeWeaponScript.SwordAttack());
                activeWeaponScript.StopCoroutine(activeWeaponScript.WandAttack());
                activeWeaponScript.StopCoroutine(activeWeaponScript.ShootBow());
                switch (pWeapon)
                {
                    case WeaponTypes.Pistol:
                        if (activeWeaponScript.canShoot && !activeWeaponScript.isReloading) StartCoroutine(activeWeaponScript.Shoot());
                        break;
                    case WeaponTypes.Sword:
                        if (activeWeaponScript.canSlash) StartCoroutine(activeWeaponScript.SwordAttack());
                        break;
                    case WeaponTypes.Wand:
                        if (activeWeaponScript.canUseWand) StartCoroutine(activeWeaponScript.WandAttack());
                        break;
                    case WeaponTypes.Bow:
                        if (activeWeaponScript.canShootBow) StartCoroutine(activeWeaponScript.ShootBow());
                        break;
                    case WeaponTypes.Empty:
                        break;
                }
            }
            if (Input.GetKeyUp(KeyCode.Z) && !activeWeaponScript.isReloading)
            {
                canPickUp = true;
            }
        }
    }
    public void Vampirism(int healthToRegen)
    {
        if (LifeRegenOngoing <= 0)
        {
            LifeRegenOngoing = healthToRegen;
            StartCoroutine(RegenHealth());
        }
        else LifeRegenOngoing += healthToRegen;
    }
    IEnumerator RegenHealth()
    {
        while (LifeRegenOngoing > 0 && hp < maxHp)
        {
            hp += 1;
            GameObject go = Instantiate(popUp, transform.position, Quaternion.identity, parentCanvas.transform);
            go.GetComponent<TextMeshProUGUI>().text = "+1";
            go.GetComponent<TextMeshProUGUI>().color = colorsMenu.healthState_heal;
            Destroy(go, 3f);
            GameObject healEffectGO = Instantiate(healEffect, transform.position, transform.rotation, transform);
            ParticleSystem.MainModule ps1 = healEffectGO.GetComponent<ParticleSystem>().main;
            ps1.startColor = colorsMenu.healthState_heal;
            Destroy(healEffectGO, 2f);
            yield return new WaitForSeconds(0.2f);
            LifeRegenOngoing--;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject clone = collision.gameObject;
        switch (collision.tag)
        {
            case "dropWeaponSword":
                if (Input.GetKey(KeyCode.Z) && canPickUp)
                {
                    if (pWeapon == WeaponTypes.Empty) PickUpWeapon(collision.tag, collision.gameObject);
                    else if (inventory.AddToInventory(clone, clone.GetComponent<DropSword>().itemSprite, InventoryScript.ItemTypeEnum.weapon, clone.GetComponent<DropSword>().rareLevel, scalersMenu.weaponCost + (int)(scalersMenu.weaponCost * Mathf.Pow(clone.GetComponent<DropSword>().rareLevel, 2))))
                    {
                        trashCan.Add(collision.gameObject);
                        collision.gameObject.transform.position = trashPosition.position;
                    }
                    else PickUpWeapon(collision.tag, collision.gameObject);
                }
                break;
            case "dropWeaponPistol":
                if (Input.GetKey(KeyCode.Z) && canPickUp)
                {
                    if (pWeapon == WeaponTypes.Empty) PickUpWeapon(collision.tag, collision.gameObject);
                    else if (inventory.AddToInventory(clone.gameObject, clone.GetComponent<DropPistol>().itemSprite, InventoryScript.ItemTypeEnum.weapon, clone.GetComponent<DropPistol>().rareLevel, scalersMenu.weaponCost + (int)(scalersMenu.weaponCost * Mathf.Pow(clone.GetComponent<DropPistol>().rareLevel, 2))))
                    {
                        trashCan.Add(collision.gameObject);
                        collision.gameObject.transform.position = trashPosition.position;
                    }
                    else PickUpWeapon(collision.tag, collision.gameObject);
                }
                break;
            case "dropWeaponWand":
                if (Input.GetKey(KeyCode.Z) && canPickUp)
                {
                    if (pWeapon == WeaponTypes.Empty) PickUpWeapon(collision.tag, collision.gameObject);
                    else if (inventory.AddToInventory(clone.gameObject, clone.GetComponent<DropWand>().itemSprite, InventoryScript.ItemTypeEnum.weapon, clone.GetComponent<DropWand>().rareLevel, scalersMenu.weaponCost + (int)(scalersMenu.weaponCost * Mathf.Pow(clone.GetComponent<DropWand>().rareLevel, 2))))
                    {
                        trashCan.Add(collision.gameObject);
                        collision.gameObject.transform.position = trashPosition.position;
                    }
                    else PickUpWeapon(collision.tag, collision.gameObject);
                }
                break;
            case "dropWeaponBow":
                if (Input.GetKey(KeyCode.Z) && canPickUp)
                {
                    if (pWeapon == WeaponTypes.Empty) PickUpWeapon(collision.tag, collision.gameObject);
                    else if (inventory.AddToInventory(clone.gameObject, clone.GetComponent<DropBow>().itemSprite, InventoryScript.ItemTypeEnum.weapon, clone.GetComponent<DropBow>().rareLevel, scalersMenu.weaponCost + (int)(scalersMenu.weaponCost * Mathf.Pow(clone.GetComponent<DropBow>().rareLevel, 2))))
                    {
                        trashCan.Add(collision.gameObject);
                        collision.gameObject.transform.position = trashPosition.position;
                    }
                    else PickUpWeapon(collision.tag, collision.gameObject);
                }
                break;
            case "Soul":
                if (Input.GetKey(KeyCode.Z) && canPickUp)
                {
                    StartCoroutine(collision.GetComponent<SoulItemScript>().OnPickUpSoul());
                }
                break;
            case "loot":
                if (Input.GetKey(KeyCode.Z))
                {
                    if (inventory.AddToInventory(clone, clone.GetComponent<ValueItemScript>().uiSprite, InventoryScript.ItemTypeEnum.loot, clone.GetComponent<ValueItemScript>().rareLevel, clone.GetComponent<ValueItemScript>().itemValue))
                    {
                        trashCan.Add(collision.gameObject);
                        collision.gameObject.transform.position = trashPosition.position;
                    }
                }
                break;
            case "coin":
                if(collision.GetComponent<ValueItemScript>().canPickUp)
                {
                    inventory.coinQuantity += collision.GetComponent<ValueItemScript>().itemValue;
                    GameObject go = Instantiate(popUp, transform.position, Quaternion.identity, parentCanvas.transform);
                    go.GetComponent<TextMeshProUGUI>().text = "+$" + collision.GetComponent<ValueItemScript>().itemValue;
                    go.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                    Destroy(go, 3f);
                    Destroy(collision.gameObject);
                }
                break;
            case "EyeDrop":
                if (Input.GetKey(KeyCode.Z))
                {
                    ChangeEyeDrop(collision.gameObject);
                    collision.transform.position = trashPosition.position;
                }
                break;
        }
    }
    public void ChangeEyeDrop(GameObject go)
    {
        EyeDropsItem edp = go.GetComponent<EyeDropsItem>();
        edp.eyeDropsBasicSC.PassiveArmorAbility(this);
    }
    public void PickUpWeapon(string tag, GameObject go)
    {
        switch (tag)
        {
            case "dropWeaponSword":
                if (canPickUp)
                {
                    canUseWeaponAfterPickUp = false;
                    canPickUp = false;
                    DropSword ds = go.GetComponent<DropSword>();
                    OnSwordChange(ds);
                    Destroy(go);
                    StartCoroutine(PickUpCoolDown());
                }
                break;
            case "dropWeaponPistol":
                if (canPickUp)
                {
                    canUseWeaponAfterPickUp = false;
                    canPickUp = false;
                    DropPistol dp = go.GetComponent<DropPistol>();
                    OnPistolChange(dp);
                    Destroy(go);
                    StartCoroutine(PickUpCoolDown());
                }
                break;
            case "dropWeaponWand":
                if (canPickUp)
                {
                    canUseWeaponAfterPickUp = false;
                    canPickUp = false;
                    DropWand dw = go.GetComponent<DropWand>();
                    OnWandChange(dw);
                    Destroy(go);
                    StartCoroutine(PickUpCoolDown());
                }
                break;
            case "dropWeaponBow":
                if (canPickUp)
                {
                    canUseWeaponAfterPickUp = false;
                    canPickUp = false;
                    DropBow db = go.GetComponent<DropBow>();
                    OnBowChange(db);
                    Destroy(go);
                    StartCoroutine(PickUpCoolDown());
                }
                break;
        }
    }
    IEnumerator PickUpCoolDown()
    {
        gameObject.GetComponent<BasicAbilityScript>().OnWeaponChange();
        yield return new WaitForSeconds(0.5f);
        canUseWeaponAfterPickUp = true;
    }
    public void ChangePlayerSoul()
    {
        switch (pSoul)
        {
            case Souls.Earth:
                //activeWeaponScript.ChangeColorSoulTypes(ActiveWeaponScript.SoulTypes.Earth, eyeBall);
                UseFullTools.ChangeColors_soulType_SpriteRenderer(colorsMenu, eyeBall, ActiveWeaponScript.SoulTypes.Earth);
                activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Earth;
                activeWeaponScript.OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
                break;
            case Souls.Electric:
                //activeWeaponScript.ChangeColorSoulTypes(ActiveWeaponScript.SoulTypes.Electric, eyeBall);
                UseFullTools.ChangeColors_soulType_SpriteRenderer(colorsMenu, eyeBall, ActiveWeaponScript.SoulTypes.Electric);
                activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Electric;
                activeWeaponScript.OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
                break;
            case Souls.Poison:
                //activeWeaponScript.ChangeColorSoulTypes(ActiveWeaponScript.SoulTypes.Poison, eyeBall);
                UseFullTools.ChangeColors_soulType_SpriteRenderer(colorsMenu, eyeBall, ActiveWeaponScript.SoulTypes.Poison);
                activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Poison;
                activeWeaponScript.OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
                break;
            case Souls.Fire:
                //activeWeaponScript.ChangeColorSoulTypes(ActiveWeaponScript.SoulTypes.Fire, eyeBall);
                UseFullTools.ChangeColors_soulType_SpriteRenderer(colorsMenu, eyeBall, ActiveWeaponScript.SoulTypes.Fire);
                activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Fire;
                activeWeaponScript.OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
                break;
            case Souls.Ice:
                //activeWeaponScript.ChangeColorSoulTypes(ActiveWeaponScript.SoulTypes.Ice, eyeBall);
                UseFullTools.ChangeColors_soulType_SpriteRenderer(colorsMenu, eyeBall, ActiveWeaponScript.SoulTypes.Ice);
                activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Ice;
                activeWeaponScript.OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
                break;
            case Souls.Dark:
                //activeWeaponScript.ChangeColorSoulTypes(ActiveWeaponScript.SoulTypes.Dark, eyeBall);
                UseFullTools.ChangeColors_soulType_SpriteRenderer(colorsMenu, eyeBall, ActiveWeaponScript.SoulTypes.Dark);
                activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Dark;
                activeWeaponScript.OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
                break;
            case Souls.Mage:
                //activeWeaponScript.ChangeColorSoulTypes(ActiveWeaponScript.SoulTypes.Mage, eyeBall);
                UseFullTools.ChangeColors_soulType_SpriteRenderer(colorsMenu, eyeBall, ActiveWeaponScript.SoulTypes.Mage);
                activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Mage;
                activeWeaponScript.OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
                break;
            case Souls.Sweet:
                //activeWeaponScript.ChangeColorSoulTypes(ActiveWeaponScript.SoulTypes.Sweet, eyeBall);
                UseFullTools.ChangeColors_soulType_SpriteRenderer(colorsMenu, eyeBall, ActiveWeaponScript.SoulTypes.Sweet);
                activeWeaponScript.activeSoulTypeAW = ActiveWeaponScript.SoulTypes.Sweet;
                activeWeaponScript.OnWeaponChange(GameObject.FindGameObjectWithTag("UIweaponSprite"), GameObject.FindGameObjectWithTag("UIsoul"), GameObject.FindGameObjectWithTag("UIweapon"), GameObject.FindGameObjectWithTag("rare").GetComponent<TextMeshProUGUI>());
                break;
        }
    }
    public void OpenInventory()
    {
        inventoryObj.GetComponent<Animator>().SetTrigger("in");
    }
    public void CloseInventory()
    {
        inventoryObj.GetComponent<Animator>().SetTrigger("out");
    }
}
    
