using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VitalEyeDrop", menuName = "VitalEyeDropMenu")]
public class VitalEyeDrop : ArmorSO
{
    public Scalers menuScalers;

    private bool passiveIsOnGoing = false;
    public override void PassiveArmorAbility(PlayerScript playerScript)
    {
        passiveIsOnGoing = true;
        playerScript.maxHp += menuScalers.vitalED_healthGain;
        playerScript.StartCoroutine(RealPassiveAbility(playerScript));
    }
    IEnumerator RealPassiveAbility(PlayerScript playerScript)
    {
        int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        while(passiveIsOnGoing)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < enemies)
            {
                playerScript.Vampirism((int)Mathf.Ceil(playerScript.maxHp / 100));
                enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    public override void StopPassiveAbility(PlayerScript playerScript)
    {
        playerScript.maxHp -= menuScalers.vitalED_healthGain;
        passiveIsOnGoing = false;
        base.StopPassiveAbility(playerScript);
    }
}
