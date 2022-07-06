using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject weaponUIobject;
    public GameObject soulUIobject;
    public GameObject costUIobject;
    public Transform canvas;
    public Transform soulUIplace;
    public Transform weaponUIplace;
    public Image takeThisColorBro;

    public ActiveWeaponScript weaponScript;
    public PlayerScript playerScript;
    public Scalers menuScalers;
    public PlayerDamageScaler pds;


    public void OnMouseHitUIweapon()
    {
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("weaponUI"))
        {
            Destroy(r);
        }
        // first go stats
        string weaponName = "error";
        int rareLevel = 0;
        float attackDamage = 0;
        float attackSpeed = 0;
        float attackRange = 0;
        float size = 0;
        float bulletSpeed = 0;
        float additionalCDR = 0;
        float additionalAP = 0;
        float maxAmmo = 0;
        Sprite weaponSprite = null;
        Color rareColor = Color.white;

        switch (weaponScript.activeWeaponAW)
        {
            case ActiveWeaponScript.WeaponTypesAW.Bow:
                rareLevel = weaponScript.rareLevel;
                attackDamage = weaponScript.damage;
                attackSpeed = weaponScript.chargingAttackTime;
                attackRange = weaponScript.arrowRange;
                size = weaponScript.arrowsSize;
                bulletSpeed = weaponScript.arrowSpeed;
                additionalCDR = 0;
                additionalAP = 0;
                maxAmmo = weaponScript.arrowStayTime;
                rareColor = GameObject.FindGameObjectWithTag("UIweapon").GetComponent<Image>().color;
                //
                if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.bow_defBow)
                {
                    weaponSprite = weaponScript.SpritesMenu.bow_defBowUI;
                    weaponName = "Bow";
                }
                else if (weaponScript.SpritesMenu == weaponScript.SpritesMenu.bow_longBow)
                {
                    weaponSprite = weaponScript.SpritesMenu.bow_longBowUI;
                    weaponName = "Longbow";
                }
                else
                {
                    weaponSprite = weaponScript.SpritesMenu.bow_crossBowUI;
                    weaponName = "Crossbow";
                }
                //
                break;
            case ActiveWeaponScript.WeaponTypesAW.Wand:
                rareLevel = weaponScript.rareLevel;
                attackDamage = weaponScript.damage;
                attackSpeed = weaponScript.chargingSpeed;
                attackRange = weaponScript.autoAbilityRange;
                size = 1;
                bulletSpeed = weaponScript.autoAbilitySpeed;
                additionalCDR = weaponScript.additionalCDR;
                additionalAP = weaponScript.additionalApScaler;
                maxAmmo = 0;
                rareColor = GameObject.FindGameObjectWithTag("UIweapon").GetComponent<Image>().color;
                //
                if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.wand_defWand)
                {
                    weaponSprite = weaponScript.SpritesMenu.wand_defWandUI;
                    weaponName = "Wand";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.wand_occultScepter)
                {
                    weaponSprite = weaponScript.SpritesMenu.wand_occultScepterUI;
                    weaponName = "Occult Scepter";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.wand_druidWand)
                {
                    weaponSprite = weaponScript.SpritesMenu.wand_druidWandUI;
                    weaponName = "Druid Wand";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.wand_crystalWand)
                {
                    weaponSprite = weaponScript.SpritesMenu.wand_crystalWandUI;
                    weaponName = "Crystal Wand";
                }
                else
                {
                    weaponSprite = weaponScript.SpritesMenu.wand_darkLordWandUI;
                    weaponName = "Dark Wand";
                }
                //
                break;
            case ActiveWeaponScript.WeaponTypesAW.Pistol:
                rareLevel = weaponScript.rareLevel;
                attackDamage = weaponScript.damage;
                attackSpeed = weaponScript.fireRate;
                attackRange = weaponScript.bulletRange;
                size = weaponScript.bulletSizeG;
                bulletSpeed = weaponScript.bulletSpeed;
                additionalCDR = 0;
                additionalAP = 0;
                maxAmmo = weaponScript.magazineSizeMax;
                rareColor = GameObject.FindGameObjectWithTag("UIweapon").GetComponent<Image>().color;
                //
                if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.gun_Pistol)
                {
                    weaponSprite = weaponScript.SpritesMenu.gun_PistolUI;
                    weaponName = "Pistol";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.gun_MachineGun)
                {
                    weaponSprite = weaponScript.SpritesMenu.gun_MachineGunUI;
                    weaponName = "M16";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.gun_ShootGun)
                {
                    weaponSprite = weaponScript.SpritesMenu.gun_ShootGunUI;
                    weaponName = "Shootgun";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.gun_Sniper)
                {
                    weaponSprite = weaponScript.SpritesMenu.gun_SniperUI;
                    weaponName = "Hecate I";
                }
                else
                {
                    weaponSprite = weaponScript.SpritesMenu.gun_UziUI;
                    weaponName = "UwUzi";
                }
                //
                break;
            case ActiveWeaponScript.WeaponTypesAW.Sword:
                rareLevel = weaponScript.rareLevel;
                attackDamage = weaponScript.damage;
                attackSpeed = weaponScript.attackRate;
                attackRange = weaponScript.attackRadious;
                size = 0;
                bulletSpeed = 0;
                additionalCDR = 0;
                additionalAP = 0;
                maxAmmo = 0;
                rareColor = GameObject.FindGameObjectWithTag("UIweapon").GetComponent<Image>().color;
                //
                if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.sword_stealSword)
                {
                    weaponSprite = weaponScript.SpritesMenu.sword_stealSwordUI;
                    weaponName = "Sword";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.sword_lightsaber)
                {
                    weaponSprite = weaponScript.SpritesMenu.sword_lightsaberUI;
                    weaponName = "Lightsaber";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.sword_wideSword)
                {
                    weaponSprite = weaponScript.SpritesMenu.sword_wideSwordUI;
                    weaponName = "Wide Sword";
                }
                else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.sword_scythe)
                {
                    weaponSprite = weaponScript.SpritesMenu.sword_scytheUI;
                    weaponName = "Scythe";
                }
                else
                {
                    weaponSprite = weaponScript.SpritesMenu.sword_rapierUI;
                    weaponName = "Rapier";
                }
                //
                break;
        }
        if (rareLevel == 0) rareColor = Color.white;
        UIweaponObject uiValues = Instantiate(weaponUIobject, weaponUIplace.position, Quaternion.identity, canvas).GetComponent<UIweaponObject>();
        uiValues.hand.SetActive(false);
        uiValues.colorObj = rareColor;
        uiValues.weaponName.text = weaponName;
        uiValues.weaponImage.sprite = weaponSprite;
        uiValues.rare.text = RareLevelName(rareLevel);
        uiValues.ad.text = "(   " + Mathf.Ceil(attackDamage * 100).ToString() + "%) - " + Mathf.Ceil(playerScript.attackDamage * attackDamage).ToString();
        if (weaponScript.activeWeaponAW != ActiveWeaponScript.WeaponTypesAW.Bow && weaponScript.activeWeaponAW != ActiveWeaponScript.WeaponTypesAW.Wand) uiValues.attackSpeed.text = "(   " + Mathf.Ceil(attackSpeed * 100).ToString() + "%) - " + System.Math.Round(playerScript.attackSpeed * attackSpeed, 1).ToString();
        else uiValues.attackSpeed.text = "     - " + System.Math.Round(attackSpeed, 1).ToString() + "s";
        uiValues.attackRange.text = "(   " + Mathf.Ceil(attackRange * 100).ToString() + "%) - " + System.Math.Round(playerScript.attackRange * attackRange, 1).ToString();
        uiValues.size.text = "     - " + System.Math.Round(size, 1).ToString();
        uiValues.bulletSpeed.text = "     - " + System.Math.Round(bulletSpeed, 1).ToString();
        uiValues.additionalAP.text = "     - " + Mathf.Ceil(additionalAP * 100).ToString() + "%";
        uiValues.additionalCDR.text = "     - " + Mathf.Ceil(additionalCDR * 100).ToString() + "%";
        if (weaponScript.activeWeaponAW == ActiveWeaponScript.WeaponTypesAW.Pistol) uiValues.ammo.text = maxAmmo.ToString();
        else if (weaponScript.activeWeaponAW == ActiveWeaponScript.WeaponTypesAW.Bow) uiValues.ammo.text = System.Math.Round(maxAmmo, 1).ToString() + "s";
        else uiValues.ammo.text = "";
    }
    public void OnMouseHitUIsoul()
    {
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("lootUI"))
        {
            Destroy(r);
        }
        SoulUIobject suo = Instantiate(soulUIobject, soulUIplace.position, Quaternion.identity, canvas).GetComponent<SoulUIobject>();
        SetSoulValues(suo);
    }
    public void OnMouseHitUImouseAbbility()
    {

    }
    public void OnMouseHitUI_Q_Abbility()
    {

    }
    public void OnMouseHitUI_E_Abbility()
    {

    }
    public void OnMouseHitUI_SHIFT_Abbility()
    {

    }
    public void OnMouseHitSlot(InventoryScript.ItemTypeEnum ite, int cost, int rareLevel, Transform spawnPoint, string itemName)
    {
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("lootUI"))
        {
            Destroy(r);
        }
        ItemCostUI slotCostUI = Instantiate(costUIobject, spawnPoint.position, Quaternion.identity, spawnPoint.parent).GetComponent<ItemCostUI>();
        slotCostUI.GetComponent<Transform>().SetParent(canvas);
        UseFullTools.ChangeColors_rareLevel_Image(weaponScript.ColorsMenu, slotCostUI.uiImage.gameObject, rareLevel);
        if (rareLevel == 0) slotCostUI.uiImage.color = Color.white;
        slotCostUI.itemName.text = itemName;
        slotCostUI.costText.text = ": " + cost.ToString();
    }
    public void OnMouseHitSoul(RollWeapon.SoulType st, GameObject soulItem, string name)
    {
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("lootUI"))
        {
            Destroy(r);
        }
        Vector2 spawnPos;
        Vector2 itemPos = soulItem.transform.position;
        if (itemPos.x + 6 > menuScalers.xBoundsMax) spawnPos = new Vector2(itemPos.x - 1.5f, itemPos.y);
        else spawnPos = new Vector2(itemPos.x + 1.5f, itemPos.y);
        if (itemPos.y + 4 > menuScalers.yBoundsMax) spawnPos = new Vector2(spawnPos.x, itemPos.y - 1.2f);
        else spawnPos = new Vector2(spawnPos.x, itemPos.y + 1.2f);

        SoulUIobject suo = Instantiate(soulUIobject, spawnPos, Quaternion.identity, canvas).GetComponent<SoulUIobject>();
        SetSoulValues(suo);
    }
    public void OnMouseHitWeapon(GameObject item, ActiveWeaponScript.WeaponTypesAW weaponType)
    {
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("weaponUI"))
        {
            Destroy(r);
        }
        // first go stats
        string weaponName = "error";
        int rareLevel = 0;
        float attackDamage = 0;
        float attackSpeed = 0;
        float attackRange = 0;
        float size = 0;
        float bulletSpeed = 0;
        float additionalCDR = 0;
        float additionalAP = 0;
        float maxAmmo = 0;
        Sprite weaponSprite = null;
        Color rareColor = Color.white;

        // obliczanie spawnu ui
        Vector2 spawnPos;
        Vector2 itemPos = item.transform.position;
        if (itemPos.x + 6 > menuScalers.xBoundsMax) spawnPos = new Vector2(itemPos.x - 2.4f, itemPos.y);
        else spawnPos = new Vector2(itemPos.x + 4.2f, itemPos.y);
        if(itemPos.y + 5 > menuScalers.yBoundsMax) spawnPos = new Vector2(spawnPos.x, itemPos.y - 1.5f);
        else spawnPos = new Vector2(spawnPos.x, itemPos.y + 1.5f);
        //
        switch (weaponType)
        {
            case ActiveWeaponScript.WeaponTypesAW.Bow:
                DropBow db = item.GetComponent<DropBow>();
                rareLevel = db.rareLevel;
                attackDamage = db.bowDamage;
                attackSpeed = db.chargingAttackTime;
                attackRange = db.arrowRange;
                size = db.arrowsSize;
                bulletSpeed = db.arrowSpeed;
                additionalCDR = 0;
                additionalAP = 0;
                maxAmmo = db.arrowStayTime;
                rareColor = db.ob.GetComponent<SpriteRenderer>().color;
                //
                if (db.bowSprite == db.bowSpritesMenu.bow_defBow)
                {
                    weaponSprite = db.bowSpritesMenu.bow_defBowUI;
                    weaponName = "Bow";
                }
                else if (db.bowSprite == db.bowSpritesMenu.bow_longBow)
                {
                    weaponSprite = db.bowSpritesMenu.bow_longBowUI;
                    weaponName = "Longbow";
                }
                else
                {
                    weaponSprite = db.bowSpritesMenu.bow_crossBowUI;
                    weaponName = "Crossbow";
                }
                //
                break;
            case ActiveWeaponScript.WeaponTypesAW.Wand:
                DropWand dw = item.GetComponent<DropWand>();
                rareLevel = dw.rareLevel;
                attackDamage = dw.damage;
                attackSpeed = dw.chargingSpeed;
                attackRange = dw.autoAbilityRange;
                size = 1;
                bulletSpeed = dw.autoAbilitySpeed;
                additionalCDR = dw.additionalCDR;
                additionalAP = dw.additionalApScaler;
                maxAmmo = 0;
                rareColor = dw.ob.GetComponent<SpriteRenderer>().color;
                //
                if (dw.wandSprite == dw.wandSpritesMenu.wand_defWand)
                {
                    weaponSprite = dw.wandSpritesMenu.wand_defWandUI;
                    weaponName = "Wand";
                }
                else if (dw.wandSprite == dw.wandSpritesMenu.wand_occultScepter)
                {
                    weaponSprite = dw.wandSpritesMenu.wand_occultScepterUI;
                    weaponName = "Occult Scepter";
                }
                else if (dw.wandSprite == dw.wandSpritesMenu.wand_druidWand)
                {
                    weaponSprite = dw.wandSpritesMenu.wand_druidWandUI;
                    weaponName = "Druid Wand";
                }
                else if (dw.wandSprite == dw.wandSpritesMenu.wand_crystalWand)
                {
                    weaponSprite = dw.wandSpritesMenu.wand_crystalWandUI;
                    weaponName = "Crystal Wand";
                }
                else
                {
                    weaponSprite = dw.wandSpritesMenu.wand_darkLordWandUI;
                    weaponName = "Dark Wand";
                }
                //
                break;
            case ActiveWeaponScript.WeaponTypesAW.Pistol:
                DropPistol dp = item.GetComponent<DropPistol>();
                rareLevel = dp.rareLevel;
                attackDamage = dp.damage;
                attackSpeed = dp.fireRate;
                attackRange = dp.bulletRange;
                size = dp.size;
                bulletSpeed = dp.bulletSpeed;
                additionalCDR = 0;
                additionalAP = 0;
                maxAmmo = dp.magazineSizeMax;
                rareColor = dp.ob.GetComponent<SpriteRenderer>().color;
                //
                if (dp.gunSprite == dp.gunSpritesMenu.gun_Pistol)
                {
                    weaponSprite = dp.gunSpritesMenu.gun_PistolUI;
                    weaponName = "Pistol";
                }
                else if (dp.gunSprite == dp.gunSpritesMenu.gun_MachineGun)
                {
                    weaponSprite = dp.gunSpritesMenu.gun_MachineGunUI;
                    weaponName = "M16";
                }
                else if (dp.gunSprite == dp.gunSpritesMenu.gun_ShootGun)
                {
                    weaponSprite = dp.gunSpritesMenu.gun_ShootGunUI;
                    weaponName = "Shootgun";
                }
                else if (dp.gunSprite == dp.gunSpritesMenu.gun_Sniper)
                {
                    weaponSprite = dp.gunSpritesMenu.gun_SniperUI;
                    weaponName = "Hecate I";
                }
                else
                {
                    weaponSprite = dp.gunSpritesMenu.gun_UziUI;
                    weaponName = "UwUzi";
                }
                //
                break;
            case ActiveWeaponScript.WeaponTypesAW.Sword:
                DropSword ds = item.GetComponent<DropSword>();
                rareLevel = ds.rareLevel;
                attackDamage = ds.swordDamage;
                attackSpeed = ds.attackRate;
                attackRange = ds.attackRadious;
                size = 0;
                bulletSpeed = 0;
                additionalCDR = 0;
                additionalAP = 0;
                maxAmmo = 0;
                rareColor = ds.ob.GetComponent<SpriteRenderer>().color;
                //
                if (ds.swordSprite == ds.swordSpritesMenu.sword_stealSword)
                {
                    weaponSprite = ds.swordSpritesMenu.sword_stealSwordUI;
                    weaponName = "Sword";
                }
                else if (ds.swordSprite == ds.swordSpritesMenu.sword_lightsaber)
                {
                    weaponSprite = ds.swordSpritesMenu.sword_lightsaberUI;
                    weaponName = "Lightsaber";
                }
                else if (ds.swordSprite == ds.swordSpritesMenu.sword_wideSword)
                {
                    weaponSprite = ds.swordSpritesMenu.sword_wideSwordUI;
                    weaponName = "Wide Sword";
                }
                else if (ds.swordSprite == ds.swordSpritesMenu.sword_scythe)
                {
                    weaponSprite = ds.swordSpritesMenu.sword_scytheUI;
                    weaponName = "Scythe";
                }
                else
                {
                    weaponSprite = ds.swordSpritesMenu.sword_rapierUI;
                    weaponName = "Rapier";
                }
                //
                break;
        }
        if (rareLevel == 0) rareColor = Color.white;
        UIweaponObject uiValues = Instantiate(weaponUIobject, spawnPos, Quaternion.identity, canvas).GetComponent<UIweaponObject>();
        uiValues.hand.SetActive(false);
        uiValues.colorObj = rareColor;
        uiValues.weaponName.text = weaponName;
        uiValues.weaponImage.sprite = weaponSprite;
        uiValues.rare.text = RareLevelName(rareLevel);
        uiValues.ad.text = "(   " + Mathf.Ceil(attackDamage * 100).ToString() + "%) - " + Mathf.Ceil(playerScript.attackDamage * attackDamage).ToString();
        if (weaponType != ActiveWeaponScript.WeaponTypesAW.Bow && weaponType != ActiveWeaponScript.WeaponTypesAW.Wand) uiValues.attackSpeed.text = "(   " + Mathf.Ceil(attackSpeed * 100).ToString() + "%) - " + System.Math.Round(playerScript.attackSpeed * attackSpeed, 1).ToString();
        else uiValues.attackSpeed.text = "     - " + System.Math.Round(attackSpeed, 1).ToString() + "s";
        uiValues.attackRange.text = "(   " + Mathf.Ceil(attackRange * 100).ToString() + "%) - " + System.Math.Round(playerScript.attackRange * attackRange, 1).ToString();
        uiValues.size.text = "     - " + System.Math.Round(size, 1).ToString();
        uiValues.bulletSpeed.text = "     - " + System.Math.Round(bulletSpeed, 1).ToString();
        uiValues.additionalAP.text = "     - " + Mathf.Ceil(additionalAP * 100).ToString() + "%";
        uiValues.additionalCDR.text = "     - " + Mathf.Ceil(additionalCDR * 100).ToString() + "%";
        if (weaponType == ActiveWeaponScript.WeaponTypesAW.Pistol) uiValues.ammo.text = maxAmmo.ToString();
        else if (weaponType == ActiveWeaponScript.WeaponTypesAW.Bow) uiValues.ammo.text = System.Math.Round(maxAmmo, 1).ToString() + "s";
        else uiValues.ammo.text = "";

        if(weaponScript.activeWeaponAW != ActiveWeaponScript.WeaponTypesAW.Empty)
        {
            // obliczanie spawnu ui
            if (itemPos.y + 5 > menuScalers.yBoundsMax) spawnPos = new Vector2(spawnPos.x, itemPos.y - 3.9f);
            else spawnPos = new Vector2(spawnPos.x, itemPos.y + 3.9f);
            // drugi item
            float attackSpeed1 = 0;
            float attackRange1 = 0;
            float size1 = 1;
            float bulletSpeed1 = 0;
            float additionalCDR1 = 0;
            float additionalAP1 = 0;
            float maxAmmo1 = 0;
            Sprite weaponSprite1 = null;
            string weaponName1 = "error";
            float attackDamage1 = weaponScript.damage;
            int rareLevel1 = weaponScript.rareLevel;
            Color rareColor1 = takeThisColorBro.color;
            ActiveWeaponScript.WeaponTypesAW weaponType1 = weaponScript.activeWeaponAW;
            switch (weaponType1)
            {
                case ActiveWeaponScript.WeaponTypesAW.Bow:
                    attackSpeed1 = weaponScript.chargingAttackTime;
                    attackRange1 = weaponScript.arrowRange;
                    size1 = weaponScript.arrowsSize;
                    bulletSpeed1 = weaponScript.arrowSpeed;
                    additionalCDR1 = 0;
                    additionalAP1 = 0;
                    maxAmmo1 = weaponScript.arrowStayTime;
                    //
                    if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.bow_defBow)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.bow_defBowUI;
                        weaponName1 = "Bow";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.bow_longBow)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.bow_longBowUI;
                        weaponName1 = "Longbow";
                    }
                    else
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.bow_crossBowUI;
                        weaponName1 = "Crossbow";
                    }
                    //
                    break;
                case ActiveWeaponScript.WeaponTypesAW.Wand:
                    attackSpeed1 = weaponScript.chargingSpeed;
                    attackRange1 = weaponScript.autoAbilityRange;
                    size1 = 1;
                    bulletSpeed1 = weaponScript.autoAbilitySpeed;
                    additionalCDR1 = weaponScript.additionalCDR;
                    additionalAP1 = weaponScript.additionalApScaler;
                    maxAmmo1 = 0;
                    //
                    if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.wand_defWand)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.wand_defWandUI;
                        weaponName1 = "Wand";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.wand_occultScepter)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.wand_occultScepterUI;
                        weaponName1 = "Occult Scepter";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.wand_druidWand)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.wand_druidWandUI;
                        weaponName1 = "Druid Wand";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.wand_crystalWand)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.wand_crystalWandUI;
                        weaponName1 = "Crystal Wand";
                    }
                    else
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.wand_darkLordWandUI;
                        weaponName1 = "Dark Wand";
                    }
                    //
                    break;
                case ActiveWeaponScript.WeaponTypesAW.Pistol:
                    attackSpeed1 = weaponScript.fireRate;
                    attackRange1 = weaponScript.bulletRange;
                    size1 = weaponScript.bulletSizeG;
                    bulletSpeed1 = weaponScript.bulletSpeed;
                    additionalCDR1 = 0;
                    additionalAP1 = 0;
                    maxAmmo1 = weaponScript.magazineSizeMax;
                    //
                    if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.gun_Pistol)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.gun_PistolUI;
                        weaponName1 = "Pistol";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.gun_MachineGun)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.gun_MachineGunUI;
                        weaponName1 = "M16";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.gun_ShootGun)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.gun_ShootGunUI;
                        weaponName1 = "Shootgun";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.gun_Sniper)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.gun_SniperUI;
                        weaponName1 = "Hecate I";
                    }
                    else
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.gun_UziUI;
                        weaponName1 = "UwUzi";
                    }
                    //
                    break;
                case ActiveWeaponScript.WeaponTypesAW.Sword:
                    attackSpeed1 = weaponScript.attackRate;
                    attackRange1 = weaponScript.attackRadious;
                    size1 = 0;
                    bulletSpeed1 = 0;
                    additionalCDR1 = 0;
                    additionalAP1 = 0;
                    maxAmmo1 = 0;
                    //
                    if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.sword_stealSword)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.sword_stealSwordUI;
                        weaponName1 = "Sword";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.sword_lightsaber)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.sword_lightsaberUI;
                        weaponName1 = "Lightsaber";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.sword_wideSword)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.sword_wideSwordUI;
                        weaponName1 = "Wide Sword";
                    }
                    else if (weaponScript.activeWeaponSprite == weaponScript.SpritesMenu.sword_scythe)
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.sword_scytheUI;
                        weaponName1 = "Scythe";
                    }
                    else
                    {
                        weaponSprite1 = weaponScript.SpritesMenu.sword_rapierUI;
                        weaponName1 = "Rapier";
                    }
                    //
                    break;
            }
            if (rareLevel1 == 0) rareColor1 = Color.white;
            UIweaponObject uiValues1 = Instantiate(weaponUIobject, spawnPos, Quaternion.identity, canvas).GetComponent<UIweaponObject>();
            uiValues1.hand.SetActive(true);
            uiValues1.colorObj = rareColor1;
            uiValues1.weaponName.text = weaponName1;
            uiValues1.weaponImage.sprite = weaponSprite1;
            uiValues1.rare.text = RareLevelName(rareLevel1);
            uiValues1.ad.text = "(   " + Mathf.Ceil(attackDamage1 * 100).ToString() + "%) - " + Mathf.Ceil(playerScript.attackDamage * attackDamage1).ToString();
            if (weaponType1 != ActiveWeaponScript.WeaponTypesAW.Bow && weaponType1 != ActiveWeaponScript.WeaponTypesAW.Wand) uiValues1.attackSpeed.text = "(   " + Mathf.Ceil(attackSpeed1 * 100).ToString() + "%) - " + System.Math.Round(playerScript.attackSpeed * attackSpeed1, 1).ToString();
            else uiValues1.attackSpeed.text = "     - " + System.Math.Round(attackSpeed1, 1).ToString() + "s";
            uiValues1.attackRange.text = "(   " + Mathf.Ceil(attackRange1 * 100).ToString() + "%) - " + System.Math.Round(playerScript.attackRange * attackRange1, 1).ToString();
            uiValues1.size.text = "     - " + System.Math.Round(size1, 1).ToString();
            uiValues1.bulletSpeed.text = "     - " + System.Math.Round(bulletSpeed1, 1).ToString();
            uiValues1.additionalAP.text = "     - " + Mathf.Ceil(additionalAP1 * 100).ToString() + "%";
            uiValues1.additionalCDR.text = "     - " + Mathf.Ceil(additionalCDR1 * 100).ToString() + "%";
            if (weaponType1 == ActiveWeaponScript.WeaponTypesAW.Pistol) uiValues1.ammo.text = maxAmmo1.ToString();
            else if (weaponType1 == ActiveWeaponScript.WeaponTypesAW.Bow) uiValues1.ammo.text = System.Math.Round(maxAmmo1, 1).ToString() + "s";
            else uiValues1.ammo.text = "";


            if (System.Math.Round(additionalCDR, 2) > System.Math.Round(additionalCDR1, 2))
            {
                uiValues.additionalCDR.color = Color.green;
                uiValues1.additionalCDR.color = Color.red;
            }
            else if(System.Math.Round(additionalCDR, 2) < System.Math.Round(additionalCDR1, 2))
            {
                uiValues1.additionalCDR.color = Color.green;
                uiValues.additionalCDR.color = Color.red;
            }
            //
            if (System.Math.Round(additionalAP, 2) > System.Math.Round(additionalAP1, 2))
            {
                uiValues.additionalAP.color = Color.green;
                uiValues1.additionalAP.color = Color.red;
            }
            else if(System.Math.Round(additionalAP, 2) < System.Math.Round(additionalAP1, 2))
            {
                uiValues1.additionalAP.color = Color.green;
                uiValues.additionalAP.color = Color.red;
            }
            //
            if (System.Math.Round(bulletSpeed, 1) > System.Math.Round(bulletSpeed1, 1) && weaponType != ActiveWeaponScript.WeaponTypesAW.Sword && weaponType1 != ActiveWeaponScript.WeaponTypesAW.Sword)
            {
                uiValues.bulletSpeed.color = Color.green;
                uiValues1.bulletSpeed.color = Color.red;
            }
            else if(System.Math.Round(bulletSpeed, 1) < System.Math.Round(bulletSpeed1, 1) && weaponType != ActiveWeaponScript.WeaponTypesAW.Sword && weaponType1 != ActiveWeaponScript.WeaponTypesAW.Sword)
            {
                uiValues1.bulletSpeed.color = Color.green;
                uiValues.bulletSpeed.color = Color.red;
            }
            //
            if(System.Math.Round(size, 1) > System.Math.Round(size1, 1))
            {
                uiValues.size.color = Color.green;
                uiValues1.size.color = Color.red;
            }
            else if(System.Math.Round(size, 1) < System.Math.Round(size1, 1))
            {
                uiValues1.size.color = Color.green;
                uiValues.size.color = Color.red;
            }
            //
            if(System.Math.Round(attackRange, 2) > System.Math.Round(attackRange1, 2))
            {
                uiValues.attackRange.color = Color.green;
                uiValues1.attackRange.color = Color.red;
            }
            else if(System.Math.Round(attackRange, 2) < System.Math.Round(attackRange1, 2))
            {
                uiValues1.attackRange.color = Color.green;
                uiValues.attackRange.color = Color.red;
            }
            //
            if(System.Math.Round(attackDamage, 2) > System.Math.Round(attackDamage1, 2))
            {
                uiValues.ad.color = Color.green;
                uiValues1.ad.color = Color.red;
            }
            else if(System.Math.Round(attackDamage, 2) < System.Math.Round(attackDamage1, 2))
            {
                uiValues1.ad.color = Color.green;
                uiValues.ad.color = Color.red;
            }
            //
            if(System.Math.Round(attackSpeed, 2) > System.Math.Round(attackSpeed1, 2) && weaponType == weaponType1)
            {
                if(weaponType == ActiveWeaponScript.WeaponTypesAW.Bow || weaponType == ActiveWeaponScript.WeaponTypesAW.Wand)
                {
                    uiValues1.attackSpeed.color = Color.green;
                    uiValues.attackSpeed.color = Color.red;
                }
                else
                {
                    uiValues.attackSpeed.color = Color.green;
                    uiValues1.attackSpeed.color = Color.red;
                }
            }
            else if(System.Math.Round(attackSpeed, 2) < System.Math.Round(attackSpeed1, 2) && weaponType == weaponType1)
            {
                if (weaponType == ActiveWeaponScript.WeaponTypesAW.Bow || weaponType == ActiveWeaponScript.WeaponTypesAW.Wand)
                {
                    uiValues.attackSpeed.color = Color.green;
                    uiValues1.attackSpeed.color = Color.red;
                }
                else
                {
                    uiValues1.attackSpeed.color = Color.green;
                    uiValues.attackSpeed.color = Color.red;
                }
            }
        }
    }
    private string RareLevelName(int r)
    {
        switch(r)
        {
            case 0:
                return "I";
            case 1:
                return "II";
            case 2:
                return "III";
            case 3:
                return "IV";
            case 4:
                return "V";
        }
        return "error";
    }
    private void SetSoulValues(SoulUIobject suo) // tutaj ustawiam nazwe dusz w ui oraz wypisuje ich efekty
    {
        switch (weaponScript.activeSoulTypeAW)
        {
            case ActiveWeaponScript.SoulTypes.Earth:
                suo.soulName.text = "Earthen\n";
                suo.colorObj = weaponScript.ColorsMenu.soulEarth;
                suo.soulText.text = "Effect: Each autoattack applies one stack, uppon applying three stacks, deal additional " + pds.earthDamage.ToString() + "Ad (20% AD + 30% AP). For the next 1,5s, every damage you apply will be treated as a true damage.";
                break;
            case ActiveWeaponScript.SoulTypes.Poison:
                suo.soulName.text = "Poisonous\n";
                suo.colorObj = weaponScript.ColorsMenu.soulPoison;
                suo.soulText.text = "Effect: Each autoattack applies poison effect on a target. Poison deals " + pds.poisonDamage.ToString() + "AP (10% AD + 20% AP). every 0,7s for 3,5s. If the enemy is already poisoned, the poison duration is restored.";
                break;
            case ActiveWeaponScript.SoulTypes.Electric:
                suo.soulName.text = "Electric\n";
                suo.colorObj = weaponScript.ColorsMenu.soulElectric;
                suo.soulText.text = "Effect: Each autoattack deals areal damage. Each enemy in range receive " + pds.electricDamage.ToString() + "AP (20% AD + 10% AP).";
                break;
            case ActiveWeaponScript.SoulTypes.Ice:
                suo.soulName.text = "Ice\n";
                suo.colorObj = weaponScript.ColorsMenu.soulIce;
                suo.soulText.text = "Effect: Each autoattack applies slow effect on a hit enemy for " + (1f + playerScript.roll.rollLevel).ToString() + "s. If enemy is already slowed the next dealt damage will stun him for " + (1f + playerScript.roll.rollLevel).ToString() + "s.";
                break;
            case ActiveWeaponScript.SoulTypes.Fire:
                suo.soulName.text = "Fiery\n";
                suo.colorObj = weaponScript.ColorsMenu.soulFire;
                suo.soulText.text = "Effect: Each autoattack applies burn effect on a hit enemy. Upon damaging burned enemy, each time apply additional damage equal " + ((int)pds.fireDamage).ToString() + "AP (1% AP + 5% AD). Each auto restores burn effect duration.";
                break;
            case ActiveWeaponScript.SoulTypes.Dark:
                suo.soulName.text = "Dark\n";
                suo.colorObj = weaponScript.ColorsMenu.soulDark;
                suo.soulText.text = "Effect: Each autoattack applies burn effect on a hit enemy. Upon damaging burned enemy, each time apply additional damage equal " + ((int)pds.fireDamage).ToString() + "AP. Each auto restores burn effect duration.";
                break;
            case ActiveWeaponScript.SoulTypes.Sweet:
                suo.soulName.text = "Sweet\n";
                suo.colorObj = weaponScript.ColorsMenu.soulSweet;
                suo.soulText.text = "Effect: Each autoattack applies one stack on a hit enemy. Upon applying two stacks, enemy becomes charmed, and moves towards the player. If player won't damage enemy for the next: " + System.Math.Round(3.5f - playerScript.attackSpeed/300, 1).ToString() + "s, the next dealt damage will deal: " + pds.sweetDamage.ToString() + "AP (60% AP + 20%AD).";
                break;
            case ActiveWeaponScript.SoulTypes.Mage:
                suo.soulName.text = "Mage\n";
                suo.colorObj = weaponScript.ColorsMenu.soulMage;
                suo.soulText.text = "Effect: Each autoattack deals aditional " + ((int)pds.mageDamage).ToString() + "AP (40% AP). Additionaly your weapon ability now stun hit enemies for " + ((1f + playerScript.roll.rollLevel) / 2f).ToString() + "s.";
                break;
            case ActiveWeaponScript.SoulTypes.Empty:
                suo.soulName.text = "Empty\n";
                suo.colorObj = Color.white;
                suo.soulText.text = "Effect: No additional effects. Lame I know.";
                break;
        }
    }
}
