using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollWeapon : MonoBehaviour
{
    public enum SoulType {fire, ice, poisson, earth, electric, dark, sweet, mage, empty};
    public enum WeaponType {gun, wand, sword, bow};
    public enum gunType {pistol, machineGun, shootGun, uzi, sniperRifle};
    public enum wandType {druidWand, defWand, crystalWand, darkLordWand, occultScepter};
    public enum swordType {stealSword, lightSaber, wideSword, scythe, rapier};
    public enum bowType {bow, longBow, crossBow};

    public GameObject bowObject;
    public GameObject gunObject;
    public GameObject wandObject;
    public GameObject swordObject;
    public GameObject soulObject;
    public GameObject lootObject;
    public GameObject eyeDropsObject;

    public Scalers menuScalers;

    public int rollLevel; // zmienna określająca poziom dropiących broni
    public float maxScaler;
    public float rollLevelScaler;
    public  EyeDropsItem.EyeDropType RandomEyeDropType()
    {
        return (EyeDropsItem.EyeDropType)Random.Range(0, 5);
    }
    public WeaponType RandomWeaponType()
    {
        WeaponType ret = (WeaponType)Random.Range(0, 4);
        return ret;
    }
    public SoulType RandomSoulType()
    {
        SoulType ret = (SoulType)Random.Range(0, 8);
        return ret;
    }
    public ValueItemScript.LootType RandomLootType()
    {
        int randomInt = Random.Range(0, 101);
        if (randomInt > 50) return ValueItemScript.LootType.coin;
        else if (randomInt > 20) return ValueItemScript.LootType.coin2;
        else return ValueItemScript.LootType.manyCoins;
    }
    public GameObject PickEyeDrops(EyeDropsItem.EyeDropType edt)
    {
        GameObject ret = eyeDropsObject;
        ret.GetComponent<EyeDropsItem>().eyeDropType = edt;
        ret.GetComponent<EyeDropsItem>().rareLevel = ReturnRareLevel();
        return ret;
    }
    public GameObject PickLoot()
    {
        int rareLevel = ReturnRareLevel();
        GameObject ret = lootObject;
        ret.GetComponent<ValueItemScript>().lootType = ValueItemScript.LootType.sellItem;
        ret.GetComponent<ValueItemScript>().rareLevel = rareLevel;
        ret.GetComponent<SpriteRenderer>().color = Color.yellow;
        ret.gameObject.tag = "loot";
        return ret;
    }
    public GameObject PickCoinValue(ValueItemScript.LootType lt)
    {
        int rareLevel = ReturnRareLevel();
        GameObject ret = lootObject;
        ret.GetComponent<ValueItemScript>().lootType = lt;
        ret.GetComponent<ValueItemScript>().rareLevel = rareLevel;
        switch (lt)
        {
            case ValueItemScript.LootType.coin:
                ret.GetComponent<ValueItemScript>().itemValue = 1 * (1 + rareLevel);
                ret.GetComponent<SpriteRenderer>().sprite = ret.GetComponent<ValueItemScript>().menuSprites.coin_vItem;
                ret.GetComponent<SpriteRenderer>().color = Color.yellow;
                ret.gameObject.tag = "coin";
                break;
            case ValueItemScript.LootType.coin2:
                ret.GetComponent<ValueItemScript>().itemValue = 2 * (1 + rareLevel);
                ret.GetComponent<SpriteRenderer>().sprite = ret.GetComponent<ValueItemScript>().menuSprites.coin2_vItem;
                ret.GetComponent<SpriteRenderer>().color = Color.yellow;
                ret.gameObject.tag = "coin";
                break;
            case ValueItemScript.LootType.manyCoins:
                ret.GetComponent<ValueItemScript>().itemValue = 10 * (1 + rareLevel);
                ret.GetComponent<SpriteRenderer>().sprite = ret.GetComponent<ValueItemScript>().menuSprites.manyCoins_vItem;
                ret.GetComponent<SpriteRenderer>().color = Color.yellow;
                ret.gameObject.tag = "coin";
                break;
        }
        return ret;
    }
    public GameObject PickSoul(SoulType st)
    {
        GameObject retSoul = soulObject;
        retSoul.GetComponent<SoulItemScript>().soulType = st;
        return retSoul;
    }
    public GameObject PickWeapon(WeaponType wt)
    {
        switch(wt)
        {
            case WeaponType.gun:
                GameObject retGun = gunObject;
                DropPistol dp = retGun.GetComponent<DropPistol>();
                gunType gt = (gunType)Random.Range(0, 5);
                int rare = ReturnRareLevel();
                dp.piercing = false;
                dp.fireRate = Random.Range(menuScalers.fireRate_gun + rollLevelScaler * menuScalers.fireRate_gun * rare, menuScalers.fireRate_gun * maxScaler + rollLevelScaler * menuScalers.fireRate_gun * rare);
                dp.damage = Random.Range(menuScalers.damage_gun + rollLevelScaler * menuScalers.damage_gun * rare, menuScalers.damage_gun * maxScaler + rollLevelScaler * menuScalers.damage_gun * rare);
                dp.bulletSpeed = Random.Range(menuScalers.bulletSpeed_gun + rollLevelScaler * menuScalers.bulletSpeed_gun * rare, menuScalers.bulletSpeed_gun * maxScaler + rollLevelScaler * menuScalers.bulletSpeed_gun * rare);
                dp.bulletRange = Random.Range(menuScalers.bulletRange_gun + rollLevelScaler * menuScalers.bulletRange_gun * rare, menuScalers.bulletRange_gun * maxScaler + rollLevelScaler * menuScalers.bulletRange_gun * rare);
                dp.size = Random.Range(menuScalers.size_gun + rollLevelScaler * menuScalers.size_gun * rare, menuScalers.size_gun * maxScaler + rollLevelScaler * menuScalers.size_gun * rare);
                switch (gt)
                {
                    case gunType.machineGun:
                        //
                        dp.fireRate *= 1.5f;
                        dp.damage *= 1f;
                        dp.bulletSpeed *= 1f;
                        dp.bulletRange *= 1.5f;
                        dp.size *= 0.8f;
                        //
                        dp.gunSprite = dp.gunSpritesMenu.gun_MachineGun;
                        dp.isShootgun = false;
                        dp.magazineSizeMax = Random.Range(15 + 5 * rare, 20 + 6 * rare);
                        dp.bulletQuanitity = 1;
                        dp.rareLevel = rare;
                        dp.bulletsLeft = dp.magazineSizeMax;
                        break;
                    case gunType.pistol:
                        //
                        dp.fireRate *= 1f;
                        dp.damage *= 1.5f;
                        dp.bulletSpeed *= 1f;
                        dp.bulletRange *= 1f;
                        dp.size *= 0.9f;
                        //
                        dp.gunSprite = dp.gunSpritesMenu.gun_Pistol;
                        dp.isShootgun = false;
                        dp.magazineSizeMax = Random.Range(7 + 3 * rare, 9 + 4 * rare);
                        dp.bulletQuanitity = 1;
                        dp.rareLevel = rare;
                        dp.bulletsLeft = dp.magazineSizeMax;
                        break;
                    case gunType.uzi:
                        //
                        dp.fireRate *= 2f;
                        dp.damage *= 0.7f;
                        dp.bulletSpeed *= 1f;
                        dp.bulletRange *= 0.7f;
                        dp.size *= 0.6f;
                        //
                        dp.gunSprite = dp.gunSpritesMenu.gun_Uzi;
                        dp.isShootgun = false;
                        dp.magazineSizeMax = Random.Range(10 + 7 * rare, 15 + 9 * rare);
                        dp.bulletQuanitity = 1;
                        dp.rareLevel = rare;
                        dp.bulletsLeft = dp.magazineSizeMax;
                        break;
                    case gunType.shootGun:
                        //
                        dp.fireRate *= 0.7f;
                        dp.damage *= 0.5f;
                        dp.bulletSpeed *= 1f;
                        dp.bulletRange *= 0.5f;
                        dp.size *= 0.9f;
                        //
                        dp.gunSprite = dp.gunSpritesMenu.gun_ShootGun;
                        dp.isShootgun = true;
                        dp.magazineSizeMax = Random.Range(3 + 1 * rare, 4 + 2 * rare);
                        dp.bulletQuanitity = Random.Range(3 + 1 * rare, 4 + 1 * rare);
                        dp.rareLevel = rare;
                        dp.bulletsLeft = dp.magazineSizeMax;
                        break;
                    case gunType.sniperRifle:
                        //
                        dp.fireRate *= 0.5f;
                        dp.damage *= 3f;
                        dp.bulletSpeed *= 2f;
                        dp.bulletRange *= 2f;
                        dp.size *= 1.1f;
                        //
                        dp.gunSprite = dp.gunSpritesMenu.gun_Sniper;
                        dp.isShootgun = false;
                        dp.magazineSizeMax = Random.Range(4 + 1 * rare, 6 + 2 * rare);
                        dp.bulletQuanitity = 1;
                        dp.rareLevel = rare;
                        dp.bulletsLeft = dp.magazineSizeMax;
                        break;
                }
                return retGun;
            case WeaponType.wand:
                GameObject retWand = wandObject;
                DropWand dw = retWand.GetComponent<DropWand>();
                wandType wat = (wandType)Random.Range(0, 5);
                int rare1 = ReturnRareLevel();
                dw.damage = Random.Range(menuScalers.damage_wand + rollLevelScaler * menuScalers.damage_wand * rare1, menuScalers.damage_wand * maxScaler + rollLevelScaler * menuScalers.damage_wand * rare1);
                dw.additionalApScaler = Random.Range(menuScalers.additionalApScaler_wand + rollLevelScaler * menuScalers.additionalApScaler_wand * rare1, menuScalers.additionalApScaler_wand * maxScaler + rollLevelScaler * menuScalers.additionalApScaler_wand * rare1);
                dw.attackSpeed = 10;
                dw.additionalCDR = Random.Range(menuScalers.additionalCDR_wand + 0.5f * menuScalers.additionalCDR_wand * rare1, menuScalers.additionalCDR_wand * maxScaler + rollLevelScaler * menuScalers.additionalCDR_wand * rare1);
                dw.autoAbilityRange = Random.Range(menuScalers.autoAbilityRange_wand + rollLevelScaler * menuScalers.autoAbilityRange_wand * rare1, menuScalers.autoAbilityRange_wand * maxScaler + rollLevelScaler * menuScalers.autoAbilityRange_wand * rare1);
                dw.autoAbilitySpeed = Random.Range(menuScalers.autoAbilitySpeed_wand + rollLevelScaler * menuScalers.autoAbilitySpeed_wand * rare1, menuScalers.autoAbilitySpeed_wand * maxScaler + rollLevelScaler * menuScalers.autoAbilitySpeed_wand * rare1);
                dw.chargingSpeed = Random.Range(menuScalers.chargingSpeed_wand - rollLevelScaler * menuScalers.chargingSpeed_wand * rare1, menuScalers.chargingSpeed_wand * maxScaler - rollLevelScaler * menuScalers.chargingSpeed_wand * rare1);
                switch (wat)
                {
                    case wandType.crystalWand:
                        //
                        dw.additionalApScaler *= 1.5f;
                        dw.additionalCDR *= 1.5f;
                        dw.autoAbilityRange *= 1f;
                        dw.autoAbilitySpeed *= 1f;
                        dw.chargingSpeed *= 0.7f;
                        dw.damage *= 0.7f;
                        //
                        dw.isDruid = false;
                        dw.rareLevel = rare1;
                        dw.wandSprite = dw.wandSpritesMenu.wand_crystalWand;
                        break;
                    case wandType.druidWand:
                        //
                        dw.additionalApScaler *= 0.5f;
                        dw.additionalCDR *= 2f;
                        dw.autoAbilityRange *= 1.75f;
                        dw.autoAbilitySpeed *= 0.5f;
                        dw.chargingSpeed *= 0.5f;
                        dw.damage *= 0.5f;
                        //
                        dw.isDruid = true;
                        dw.rareLevel = rare1;
                        dw.wandSprite = dw.wandSpritesMenu.wand_druidWand;
                        break;
                    case wandType.occultScepter:
                        //
                        dw.additionalApScaler *= 2f;
                        dw.additionalCDR *= 0.5f;
                        dw.autoAbilityRange *= 0.7f;
                        dw.autoAbilitySpeed *= 1f;
                        dw.chargingSpeed *= 1.5f;
                        dw.damage *= 1.5f;
                        //
                        dw.isDruid = false;
                        dw.rareLevel = rare1;
                        dw.wandSprite = dw.wandSpritesMenu.wand_occultScepter;
                        break;
                    case wandType.defWand:
                        //
                        dw.additionalApScaler *= 1f;
                        dw.additionalCDR *= 1f;
                        dw.autoAbilityRange *= 1.5f;
                        dw.autoAbilitySpeed *= 1f;
                        dw.chargingSpeed *= 1f;
                        dw.damage *= 1f;
                        //
                        dw.isDruid = false;
                        dw.rareLevel = rare1;
                        dw.wandSprite = dw.wandSpritesMenu.wand_defWand;
                        break;
                    case wandType.darkLordWand:
                        //
                        dw.additionalApScaler *= 0.7f;
                        dw.additionalCDR *= 0.7f;
                        dw.autoAbilityRange *= 1f;
                        dw.autoAbilitySpeed *= 1f;
                        dw.chargingSpeed *= 2f;
                        dw.damage *= 2f;
                        //
                        dw.isDruid = false;
                        dw.rareLevel = rare1;
                        dw.wandSprite = dw.wandSpritesMenu.wand_darkLordWand;
                        break;
                }
                if (dw.chargingSpeed < 0.1f) dw.chargingSpeed = 0.1f;
                return retWand;
            case WeaponType.bow:
                GameObject retBow = bowObject;
                DropBow db = retBow.GetComponent<DropBow>();
                bowType bt = (bowType)Random.Range(0, 3);
                int rare2 = ReturnRareLevel();
                db.bowDamage = Random.Range(menuScalers.damage_bow + rollLevelScaler * menuScalers.damage_bow * rare2, menuScalers.damage_bow * maxScaler + rollLevelScaler * menuScalers.damage_bow * rare2);
                db.arrowSpeed = Random.Range(menuScalers.arrowSpeed_bow + rollLevelScaler * menuScalers.arrowSpeed_bow * rare2, menuScalers.arrowSpeed_bow * maxScaler + rollLevelScaler * menuScalers.arrowSpeed_bow * rare2);
                db.arrowsSize = Random.Range(menuScalers.arrowsSize_bow + rollLevelScaler * menuScalers.arrowsSize_bow * rare2, menuScalers.arrowsSize_bow * maxScaler + rollLevelScaler * menuScalers.arrowsSize_bow * rare2);
                db.arrowStayTime = Random.Range(menuScalers.arrowStayTime_bow + rollLevelScaler * menuScalers.arrowStayTime_bow * rare2, menuScalers.arrowStayTime_bow * maxScaler + rollLevelScaler * menuScalers.arrowStayTime_bow * rare2);
                db.arrowRange = Random.Range(menuScalers.arrowRange_bow + rollLevelScaler * menuScalers.arrowRange_bow * rare2, menuScalers.arrowRange_bow * maxScaler + rollLevelScaler * menuScalers.arrowRange_bow * rare2);
                db.chargingAttackTime = Random.Range(menuScalers.chargingAttackTime_bow - rollLevelScaler * menuScalers.chargingAttackTime_bow * rare2, menuScalers.chargingAttackTime_bow * maxScaler - rollLevelScaler * menuScalers.chargingAttackTime_bow * rare2);
                switch (bt)
                {
                    case bowType.bow:
                        //
                        db.bowDamage *= 1.5f;
                        db.arrowSpeed *= 1f;
                        db.arrowsSize *= 1f;
                        db.arrowStayTime *= 1f;
                        db.arrowRange *= 1.5f;
                        db.chargingAttackTime *= 1f;
                        //
                        db.isCrossBow = false;
                        db.bowSprite = db.bowSpritesMenu.bow_defBow;
                        db.rareLevel = rare2;
                        break;
                    case bowType.crossBow:
                        //
                        db.bowDamage *= 0.7f;
                        db.arrowSpeed *= 2f;
                        db.arrowsSize *= 0.7f;
                        db.arrowStayTime *= 1.5f;
                        db.arrowRange *= 1f;
                        db.chargingAttackTime *= 0.7f;
                        //
                        db.isCrossBow = true;
                        db.bowSprite = db.bowSpritesMenu.bow_crossBow;
                        db.rareLevel = rare2;
                        break;
                    case bowType.longBow:
                        //
                        db.bowDamage *= 2f;
                        db.arrowSpeed *= 1.5f;
                        db.arrowsSize *= 1f;
                        db.arrowStayTime *= 1f;
                        db.arrowRange *= 2f;
                        db.chargingAttackTime *= 1.5f;
                        //
                        db.isCrossBow = false;
                        db.bowSprite = db.bowSpritesMenu.bow_longBow;
                        db.rareLevel = rare2;
                        break;
                }
                if (db.chargingAttackTime < 0.1f) db.chargingAttackTime = 0.1f;
                return retBow;
            case WeaponType.sword:
                GameObject retSword = swordObject;
                DropSword ds = retSword.GetComponent<DropSword>();
                swordType st = (swordType)Random.Range(0, 5);
                int rare3 = ReturnRareLevel();
                ds.attackRate = Random.Range(menuScalers.attackRate_sword + rollLevelScaler * menuScalers.attackRate_sword * rare3, menuScalers.attackRate_sword * maxScaler + rollLevelScaler * menuScalers.attackRate_sword * rare3);
                ds.swordDamage = Random.Range(menuScalers.damage_sword + rollLevelScaler * menuScalers.damage_sword * rare3, menuScalers.damage_sword * maxScaler + rollLevelScaler * menuScalers.damage_sword * rare3);
                ds.attackRadious = Random.Range(menuScalers.attackRadious_sword + rollLevelScaler * menuScalers.attackRadious_sword * rare3, menuScalers.attackRadious_sword * maxScaler + rollLevelScaler * menuScalers.attackRadious_sword * rare3);
                ds.passiveSwordCritChance = Random.Range(menuScalers.passiveSwordCritChance_sword + rollLevelScaler * menuScalers.passiveSwordCritChance_sword * rare3, menuScalers.passiveSwordCritChance_sword * maxScaler + rollLevelScaler * menuScalers.passiveSwordCritChance_sword * rare3);
                switch (st)
                {
                    case swordType.lightSaber:
                        //
                        ds.attackRate *= 1.5f;
                        ds.swordDamage *= 0.5f;
                        ds.attackRadious *= 0.7f;
                        ds.passiveSwordCritChance *= 2f;
                        //
                        ds.swordSprite = ds.swordSpritesMenu.sword_lightsaber;
                        ds.rareLevel = rare3;
                        ds.isRapier = false;
                        ds.bouncesBulletBack = true;
                        break;
                    case swordType.stealSword:
                        //
                        ds.attackRate *= 1f;
                        ds.swordDamage *= 1f;
                        ds.attackRadious *= 1f;
                        ds.passiveSwordCritChance *= 0.7f;
                        //
                        ds.swordSprite = ds.swordSpritesMenu.sword_stealSword;
                        ds.rareLevel = rare3;
                        ds.isRapier = false;
                        ds.bouncesBulletBack = false;
                        break;
                    case swordType.wideSword:
                        //
                        ds.attackRate *= 0.5f;
                        ds.swordDamage *= 2f;
                        ds.attackRadious *= 1.5f;
                        ds.passiveSwordCritChance *= 0.5f;
                        //
                        ds.swordSprite = ds.swordSpritesMenu.sword_wideSword;
                        ds.rareLevel = rare3;
                        ds.isRapier = false;
                        ds.bouncesBulletBack = false;
                        break;
                    case swordType.scythe:
                        //
                        ds.attackRate *= 0.7f;
                        ds.swordDamage *= 1.5f;
                        ds.attackRadious *= 2f;
                        ds.passiveSwordCritChance *= 1f;
                        //
                        ds.swordSprite = ds.swordSpritesMenu.sword_scythe;
                        ds.rareLevel = rare3;
                        ds.isRapier = false;
                        ds.bouncesBulletBack = false;
                        break;
                    case swordType.rapier:
                        //
                        ds.attackRate *= 2f;
                        ds.swordDamage *= 0.7f;
                        ds.attackRadious *= 0.7f;
                        ds.passiveSwordCritChance *= 1.5f;
                        //
                        ds.swordSprite = ds.swordSpritesMenu.sword_rapier;
                        ds.rareLevel = rare3;
                        ds.isRapier = true;
                        ds.bouncesBulletBack = false;
                        break;
                }
                return retSword;
        }
        return null;
    }
    public int ReturnRareLevel()
    {
        int pick = Random.Range(0, 101);
        switch (rollLevel)
        {
            case 0:
                if (pick <= 75) return 0;
                else if (pick <= 97) return 1;
                else return 2;
            case 1:
                if (pick <= 25) return 0;
                else if (pick <= 85) return 1;
                else if (pick <= 97) return 2;
                else return 3;
            case 2:
                if (pick <= 15) return 0;
                else if (pick <= 35) return 1;
                else if (pick <= 85) return 2;
                else if (pick <= 97) return 3;
                else return 4;
            case 3:
                if (pick <= 5) return 0;
                else if (pick <= 10) return 1;
                else if (pick <= 25) return 2;
                else if (pick <= 90) return 3;
                else return 4;
            case 4:
                if (pick <= 3) return 0;
                else if (pick <= 15) return 1;
                else if (pick <= 30) return 2;
                else if (pick <= 80) return 3;
                else return 4;
        }
        return 0;
    }
}
