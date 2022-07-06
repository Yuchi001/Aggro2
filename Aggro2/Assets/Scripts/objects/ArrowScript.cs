using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public ActiveWeaponScript.SoulTypes arrowSoulType;

    public GameObject stayArrowObject;
    public GameObject miniBoom;

    private Rigidbody2D rb2d;

    private Vector2 startPosition;

    public bool canFly = false;
    public bool isAP = false;
    public bool canCheck = false;

    public float speed;
    public float range;
    public float damage;
    public float additionalDamage = 1;
    public float stayTime;

    public GameObject trail;
    IEnumerator Start()
    {
        trail.SetActive(false);
        trail.GetComponent<TrailRenderer>().startColor = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        yield return new WaitUntil(() => canFly);
        gameObject.transform.parent = null;
        rb2d.velocity = transform.up * speed;
        startPosition = transform.position;
        canCheck = true;
        trail.SetActive(true);
    }
    void Update()
    {
        if (Vector2.Distance(startPosition, transform.position) > range && canCheck) DestroyArrow();
    }
    void DestroyArrow()
    {
        GameObject go = Instantiate(miniBoom, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ps = go.GetComponent<ParticleSystem>().main;
        ps.startColor = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        ps.startSize = gameObject.transform.localScale.x / 8;
        Destroy(go, 1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            int s = 1; //mnożnik 
            foreach(GameObject r in GameObject.FindGameObjectsWithTag("StayArrow"))
            {
                if(GameObject.ReferenceEquals(r.transform.parent, c.transform))
                {
                    additionalDamage += Mathf.Ceil(damage / 5) + s;
                    s += (int)Mathf.Ceil(damage / 100);
                    r.GetComponent<StayArrowScript>().lifeTime /= 2;
                }
            }
            if (additionalDamage == 0) additionalDamage = 1;
            Enemy_Main.DamageType dmType = Enemy_Main.DamageType.attackDamage;
            if (isAP) 
            {
                dmType = Enemy_Main.DamageType.abilityPower;
                if(arrowSoulType == ActiveWeaponScript.SoulTypes.Mage)c.GetComponent<Enemy_Main>().StunFromAnotherObject(c.GetComponent<Enemy_Main>().ColorsMenu.soulMage);
            }
            switch (arrowSoulType)
            {
                case ActiveWeaponScript.SoulTypes.Earth:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Earth, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Poison:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Poison, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Fire:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Fire, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Ice:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Ice, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Electric:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Electric, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Empty:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Empty, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Sweet:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Sweet, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Dark:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Dark, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Mage:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, dmType, Enemy_Main.SoulTypes.Mage, false);
                    break;
            }
            c.GetComponent<Enemy_Main>().DamageEnemy(Mathf.Ceil(additionalDamage), Enemy_Main.DamageType.trueDamage, Enemy_Main.SoulTypes.Empty, false);
            StayArrowScript sas = Instantiate(stayArrowObject, transform.position, transform.rotation, c.transform).GetComponent<StayArrowScript>();
            sas.arrowSoulType = arrowSoulType;
            sas.lifeTime = stayTime;
            sas.gameObject.transform.localScale = gameObject.transform.localScale;
            sas.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            DestroyArrow();
        }
    }
}
