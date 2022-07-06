using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageScaler : MonoBehaviour
{
    public PlayerScript player;
    public ActiveWeaponScript weapon;
    public Scalers ScalersMenu;

    [HideInInspector] public float wandAbilityDamage;
    [HideInInspector] public float grenadeAbilityDamage;
    [HideInInspector] public float swordAbilityDamage;
    [HideInInspector] public float bowAbilityDamage;

    [HideInInspector] public float poisonDamage;
    [HideInInspector] public float iceDamage;
    [HideInInspector] public float fireDamage;
    [HideInInspector] public float electricDamage;
    [HideInInspector] public float earthDamage;
    [HideInInspector] public float sweetDamage;
    [HideInInspector] public float darkDamage;
    [HideInInspector] public float mageDamage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        poisonDamage = player.attackDamage * 0.1f + player.abilityPower * 0.2f;
        earthDamage = player.attackDamage * 0.2f + player.abilityPower * 0.3f;
        fireDamage = player.abilityPower * 0.01f + player.attackDamage * 0.05f;
        electricDamage = player.attackDamage * 0.2f + player.abilityPower * 0.1f;
        iceDamage = player.attackDamage * 0.1f + player.abilityPower * 0.2f;
        mageDamage = player.abilityPower * 0.4f;
        sweetDamage = player.abilityPower * 0.6f + player.attackDamage * 0.2f;

        bowAbilityDamage = player.attackDamage * ScalersMenu.bow_ability_damageScaler + (player.abilityPower * ScalersMenu.bow_ability_apScaler);
        wandAbilityDamage = player.abilityPower * ScalersMenu.wand_ability_apScaler;
        swordAbilityDamage = player.attackDamage * weapon.damage * ScalersMenu.sword_ability_damageScaler + (player.abilityPower * ScalersMenu.sword_ability_apScaler);
        grenadeAbilityDamage = weapon.damage * player.attackDamage * ScalersMenu.gun_ability_damageScaler + (player.abilityPower * ScalersMenu.gun_ability_apScaler);
    }
    void Update()
    {
        poisonDamage = player.attackDamage * 0.1f + player.abilityPower * 0.2f;
        earthDamage = player.attackDamage * 0.2f + player.abilityPower * 0.3f;
        fireDamage = player.abilityPower * 0.01f + player.attackDamage * 0.05f;
        electricDamage = player.attackDamage * 0.2f + player.abilityPower * 0.1f;
        iceDamage = player.attackDamage * 0.1f + player.abilityPower * 0.2f;
        mageDamage = player.abilityPower * 0.4f;
        sweetDamage = player.abilityPower * 0.6f + player.attackDamage * 0.2f;

        bowAbilityDamage = player.attackDamage * ScalersMenu.bow_ability_damageScaler + (player.abilityPower * ScalersMenu.bow_ability_apScaler);
        wandAbilityDamage = player.abilityPower * ScalersMenu.wand_ability_apScaler;
        swordAbilityDamage = player.attackDamage * weapon.damage * ScalersMenu.sword_ability_damageScaler + (player.abilityPower * ScalersMenu.sword_ability_apScaler);
        grenadeAbilityDamage = weapon.damage * player.attackDamage * ScalersMenu.gun_ability_damageScaler + (player.abilityPower * ScalersMenu.gun_ability_apScaler);
    }
}
