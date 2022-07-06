using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BasicAbilityScript : MonoBehaviour
{
    public Sprites menu_sprites;
    public Colors menu_colors;
    public ActiveWeaponScript weaponScript;
    public PlayerScript playerScript;
    public Scalers ScalersMenu;
    public PlayerDamageScaler pdScaler;

    public Image aImage; // ability cooldown image
    public Image frame; 
    public Image AbilityImage;  // real ability image lmao

    [HideInInspector] public bool canUseAbility = true;

    [HideInInspector] public float abilityCooldownMax;
    [HideInInspector] public float abilityCoolDownOnGoing = 0;
    [HideInInspector] public float swordCoolDownOnGoing = 0;

    public float chargingTime;
    public float swordCoolDownMax;

    public int[] multiShootValue = new int[5];
    public float[] swordRangeAttackTime = new float[5];

    public GameObject granadeObject;
    public GameObject arrowObject;
    public GameObject powerBallObject;

    public GameObject stillTextObject;
    public GameObject coolDownText;
    private void Update()
    {
        swordCoolDownMax = swordRangeAttackTime[playerScript.swordPower];
        chargingTime = 2f - 0.3f * playerScript.wandPower;
        AbilityImage.color = weaponScript.gameObject.GetComponent<SpriteRenderer>().color;
        abilityCooldownMax = playerScript.weaponCoolDown * (1 - playerScript.coolDownReduction);
        if(Input.GetMouseButton(1) && canUseAbility && !playerScript.inventoryIsOpen)
        {
            switch (playerScript.pWeapon)
            {
                case PlayerScript.WeaponTypes.Bow:
                    if (!weaponScript.isCrossBow) BowAbility();
                    else StartCoroutine(CrossBowAbility());
                    break;
                case PlayerScript.WeaponTypes.Pistol:
                    GunAbility();
                    break;
                case PlayerScript.WeaponTypes.Sword:
                    StartCoroutine(SwordAbility());
                    break;
                case PlayerScript.WeaponTypes.Wand:
                    if (weaponScript.canUseWand) StartCoroutine(WandAbility());
                    break;
            }
        }
        if(abilityCoolDownOnGoing > 0)
        {
            coolDownText.SetActive(true);
            coolDownText.GetComponent<TextMeshProUGUI>().text = ((int)abilityCoolDownOnGoing).ToString();
            canUseAbility = false;
            aImage.fillAmount = abilityCoolDownOnGoing / abilityCooldownMax;
            abilityCoolDownOnGoing -= Time.deltaTime;
            if(abilityCoolDownOnGoing < 1)
            {
                coolDownText.GetComponent<TextMeshProUGUI>().text = System.Math.Round(abilityCoolDownOnGoing, 1).ToString();
            }
            if(abilityCoolDownOnGoing <=0)
            {
                coolDownText.SetActive(false);
                aImage.fillAmount = 0;
                abilityCoolDownOnGoing = 0;
                canUseAbility = true;
            }
        }
    }
    public void OnWeaponChange()
    {
        swordCoolDownOnGoing = 0;
        frame.gameObject.SetActive(false);
        switch (playerScript.pWeapon)
        {
            case PlayerScript.WeaponTypes.Bow:
                AbilityImage.sprite = menu_sprites.ability_bow;
                break;
            case PlayerScript.WeaponTypes.Pistol:
                AbilityImage.sprite = menu_sprites.ability_gun;
                break;
            case PlayerScript.WeaponTypes.Sword:
                AbilityImage.sprite = menu_sprites.ability_sword;
                break;
            case PlayerScript.WeaponTypes.Wand:
                AbilityImage.sprite = menu_sprites.ability_wand;
                break;
        }
    }
    private IEnumerator CrossBowAbility()
    {
        abilityCoolDownOnGoing = abilityCooldownMax;
        int angle = 10 * multiShootValue[playerScript.bowPower];
        for (int i = 0; i <= multiShootValue[playerScript.bowPower]; i++)
        {
            ArrowScript ars = Instantiate(arrowObject, weaponScript.arrowShootPoint.position, Quaternion.Euler(0, 0, weaponScript.angle -90)).GetComponent<ArrowScript>();
            ars.canFly = true;
            ars.speed = weaponScript.arrowSpeed;
            ars.damage = pdScaler.bowAbilityDamage;
            ars.isAP = true;
            ars.range = weaponScript.arrowRange + multiShootValue[playerScript.bowPower];
            ars.stayTime = weaponScript.arrowStayTime * 1.3f;
            ars.arrowSoulType = weaponScript.activeSoulTypeAW;
            UseFullTools.ChangeColors_soulType_SpriteRenderer(menu_colors, ars.transform.GetChild(0).gameObject, weaponScript.activeSoulTypeAW);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void BowAbility()
    {
        abilityCoolDownOnGoing = abilityCooldownMax;
        int angle = 10 * multiShootValue[playerScript.bowPower];
        for(int i = angle / multiShootValue[playerScript.bowPower]; i<=angle; i+= angle / multiShootValue[playerScript.bowPower])
        {
            ArrowScript ars = Instantiate(arrowObject, weaponScript.arrowShootPoint.position, Quaternion.Euler(0, 0, weaponScript.angle - 85 + angle/2 - i)).GetComponent<ArrowScript>();
            ars.canFly = true;
            ars.speed = weaponScript.arrowSpeed;
            ars.damage = pdScaler.bowAbilityDamage;
            ars.isAP = true;
            ars.range = weaponScript.arrowRange + multiShootValue[playerScript.bowPower];
            ars.stayTime = weaponScript.arrowStayTime * 0.7f;
            ars.arrowSoulType = weaponScript.activeSoulTypeAW;
            ars.gameObject.transform.localScale = new Vector2(weaponScript.arrowsSize, weaponScript.arrowsSize);
            UseFullTools.ChangeColors_soulType_SpriteRenderer(menu_colors, ars.transform.GetChild(0).gameObject, weaponScript.activeSoulTypeAW);
        }
    }
    private void GunAbility()
    {
        abilityCoolDownOnGoing = abilityCooldownMax;
        GrenadeScript gs = Instantiate(granadeObject, weaponScript.transform.position, Quaternion.Euler(0,0, weaponScript.angle-90)).GetComponent<GrenadeScript>();
        gs.damage = pdScaler.grenadeAbilityDamage;
        gs.grenadeSoulType = weaponScript.activeSoulTypeAW;
        gs.endPosition = GameObject.FindGameObjectWithTag("Cursor").transform.position;
        gs.speed = 1 + Mathf.Log(Vector2.Distance(transform.position, gs.endPosition), 2);
        UseFullTools.ChangeColors_soulType_SpriteRenderer(menu_colors, gs.grenadeSprite, weaponScript.activeSoulTypeAW);
    }
    private IEnumerator SwordAbility()
    {
        canUseAbility = false;
        weaponScript.rangeSwordAttacks = true;
        swordCoolDownOnGoing = swordCoolDownMax;
        frame.gameObject.SetActive(true);
        while (swordCoolDownOnGoing > 0)
        {
            frame.fillAmount = swordCoolDownOnGoing / swordCoolDownMax;
            swordCoolDownOnGoing -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        weaponScript.rangeSwordAttacks = false;
        frame.gameObject.SetActive(false);
        abilityCoolDownOnGoing = abilityCooldownMax;
        weaponScript.firstUsed = false;
    }
    private IEnumerator WandAbility()
    {
        abilityCoolDownOnGoing = abilityCooldownMax;
        playerScript.canUseWeaponAfterPickUp = false;
        weaponScript.GetComponent<Animator>().enabled = false;
        weaponScript.GetComponent<Animator>().enabled = true;
        weaponScript.canUseWand = false;
        playerScript.canPickUp = false;
        weaponScript.weaponAnim.SetTrigger("UseWand");
        weaponScript.weaponAnim.speed = 1;
        weaponScript.weaponAnim.speed /= chargingTime;
        GameObject go = Instantiate(stillTextObject, playerScript.GetComponent<Transform>().position, Quaternion.identity, GameObject.FindGameObjectWithTag("c").transform);
        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = "Charging";
        float fullTimeCharge = 0;
        weaponScript.StartChargingParticles();
        while (Input.GetMouseButton(1) && !weaponScript.isChangingWeapon)
        {
            fullTimeCharge += 0.1f;
            yield return new WaitForSeconds(0.1f);
            if (fullTimeCharge >= chargingTime)
            {
                WandAbillityBullet wab = Instantiate(powerBallObject, weaponScript.wandShootPoint.position, Quaternion.Euler(0, 0, weaponScript.angle - 90)).GetComponent<WandAbillityBullet>();
                wab.bulletSoulType = weaponScript.activeSoulTypeAW;
                wab.damage = pdScaler.wandAbilityDamage;
                UseFullTools.ChangeColors_soulType_SpriteRenderer(menu_colors, wab.gameObject, weaponScript.activeSoulTypeAW);
                break;
            }
        }
        weaponScript.StopChargingParticles();
        weaponScript.weaponAnim.Play("DefAnim");
        Destroy(go);
        weaponScript.canUseWand = true;
        playerScript.canPickUp = true;
        playerScript.canUseWeaponAfterPickUp = true;
    }
}
