using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayArrowScript : MonoBehaviour
{
    public ActiveWeaponScript.SoulTypes arrowSoulType;
    private Enemy_Main enemyScript;
    public float lifeTime;
    void Start()
    {
        enemyScript = gameObject.transform.parent.GetComponent<Enemy_Main>();
        StartCoroutine(LifeTime());
    }
    void Update()
    {
        switch (arrowSoulType)
        {
            case ActiveWeaponScript.SoulTypes.Earth:
                enemyScript.earthEffectDuration = enemyScript.earthEffectDurationMax;
                break;
            case ActiveWeaponScript.SoulTypes.Poison:
                enemyScript.poisonDuration = enemyScript.poisonDurationMax;
                break;
            case ActiveWeaponScript.SoulTypes.Fire:
                enemyScript.burnDuration = enemyScript.burnDurationMax;
                break;
            case ActiveWeaponScript.SoulTypes.Ice:
                if (enemyScript.states_move == Enemy_Main.EnemyMoveStates.Slowed) enemyScript.slowDuration = enemyScript.slowDurationMax;
                else enemyScript.stunDuration = enemyScript.stunDurationMax;
                break;
            case ActiveWeaponScript.SoulTypes.Mage:
                enemyScript.stunDuration = enemyScript.stunDurationMax;
                break;
            case ActiveWeaponScript.SoulTypes.Sweet:
                if(enemyScript.charmStacksState == -1)enemyScript.charmDuration = 0;
                break;
        }
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime - 0.3f);
        gameObject.GetComponent<Animator>().SetTrigger("die");
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
