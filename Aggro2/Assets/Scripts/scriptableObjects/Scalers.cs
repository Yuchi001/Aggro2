using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scalers", menuName = "ScalersMenu")]
public class Scalers : ScriptableObject
{
    // eyeDrops stats
    [Space(20)]
    public int vitalED_armor;
    public int vitalED_magicRessists;
    public int vitalED_healthGain;

    [Space(20)]
    public int defED_armor;
    public int defED_magicRessists;

    [Space(20)]
    public int senative_armor;
    public int senative_magicRessists;

    [Space(20)]
    public int steelClad_armor;
    public int steelClad_magicRessists;

    [Space(20)]
    public int resistant_armor;
    public int resistant_magicRessists;


    // price scalers
    [Space(20)]
    public int weaponCost;

    // sword scalers
    [Space(20)]
    public float sword_ability_damageScaler;
    public float sword_ability_apScaler;

    // gun scalers
    [Space(20)]
    public float gun_ability_damageScaler;
    public float gun_ability_apScaler;

    // bow scalers
    [Space(20)]
    public float bow_ability_damageScaler;
    public float bow_ability_apScaler;

    // wand scalers
    [Space(20)]
    public float wand_ability_apScaler;


    // map bounds
    [Space(20)]
    public float xBoundsMax; // granice mapy X
    public float xBoundsMin; // granice mapy X
    public float yBoundsMax; // granice mapy Y
    public float yBoundsMin; // granice mapy Y

    //weapon stats 

    //gun stats
    [Space(20)]
    public float damage_gun;
    public float fireRate_gun;
    public float bulletSpeed_gun;
    public float bulletRange_gun;
    public float size_gun;

    //sword stats
    [Space(20)]
    public float damage_sword;
    public float attackRate_sword;
    public float attackRadious_sword;
    public float passiveSwordCritChance_sword;

    //wand stats
    [Space(20)]
    public float damage_wand;
    public float chargingSpeed_wand;
    public float autoAbilityRange_wand;
    public float additionalApScaler_wand;
    public float attackSpeed_wand;
    public float additionalCDR_wand;
    public float autoAbilitySpeed_wand;

    //bow stats
    [Space(20)]
    public float damage_bow;
    public float chargingAttackTime_bow;
    public float arrowsSize_bow;
    public float arrowStayTime_bow;
    public float arrowSpeed_bow;
    public float arrowRange_bow;
}
