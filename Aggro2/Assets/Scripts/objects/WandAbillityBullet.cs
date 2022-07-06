using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAbillityBullet : MonoBehaviour
{
    public ActiveWeaponScript.SoulTypes bulletSoulType;

    public float[] speed = new float[5];
    public float[] radious = new float[5];
    public float[] zoneRadious = new float[5];
    public float[] zoneStayTime = new float[5];
    public float damage;

    public GameObject miniboom;
    public GameObject particles;
    public GameObject wandBulletSprite;
    public GameObject zoneObject;
    public GameObject circle;

    private Rigidbody2D rb2d;

    private PlayerScript player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        wandBulletSprite.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        ParticleSystem.MainModule psMM = particles.GetComponent<ParticleSystem>().main;
        psMM.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        if (bulletSoulType == ActiveWeaponScript.SoulTypes.Empty) psMM.startColor = Color.black;
        if (bulletSoulType == ActiveWeaponScript.SoulTypes.Poison) StartCoroutine(SpawnPoisonZone());
    }
    void Update()
    {
        wandBulletSprite.transform.Rotate(0, 0, speed[player.wandPower] / 2);
        rb2d.velocity = transform.up * speed[player.wandPower];
    }
    void Explode()
    {
        StopCoroutine(SpawnPoisonZone());
        float r = radious[player.wandPower];
        if (bulletSoulType == ActiveWeaponScript.SoulTypes.Electric) r *= 2;
        if(bulletSoulType != ActiveWeaponScript.SoulTypes.Fire)
        {
            CircleScript cs = Instantiate(circle, transform.position, Quaternion.identity).GetComponent<CircleScript>();
            cs.GetComponent<SpriteRenderer>().color *= wandBulletSprite.gameObject.GetComponent<SpriteRenderer>().color;
            cs.radious = 2 * r;
            cs.speed = cs.radious * cs.radious;
        }
        foreach (Collider2D c in Physics2D.OverlapCircleAll(transform.position, r/2))
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                switch (bulletSoulType)
                {
                    case ActiveWeaponScript.SoulTypes.Earth:
                        c.GetComponent<Enemy_Main>().shieldStacksState = 1;
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Earth, false);
                        break;
                    case ActiveWeaponScript.SoulTypes.Poison:
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Poison, false);
                        break;
                    case ActiveWeaponScript.SoulTypes.Fire:
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Fire, false);
                        c.GetComponent<Enemy_Main>().BurningCumulator += damage / 2;
                        break;
                    case ActiveWeaponScript.SoulTypes.Ice:
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Ice, false);
                        break;
                    case ActiveWeaponScript.SoulTypes.Electric:
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage * 2, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Electric, false);
                        break;
                    case ActiveWeaponScript.SoulTypes.Empty:
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Empty, false);
                        break;
                    case ActiveWeaponScript.SoulTypes.Sweet:
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Sweet, false);
                        break;
                    case ActiveWeaponScript.SoulTypes.Dark:
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Dark, false);
                        break;
                    case ActiveWeaponScript.SoulTypes.Mage:
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Mage, false);
                        break;
                }
            }
        }
        GameObject go = Instantiate(miniboom, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ps = go.GetComponent<ParticleSystem>().main;
        ps.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        ps.startSize = gameObject.transform.localScale.x / 8;
        go.transform.parent = null;
        particles.transform.localScale = new Vector2(transform.localScale.x * particles.transform.localScale.x, transform.localScale.x * particles.transform.localScale.x);
        particles.transform.parent = null;
        Destroy(go, 1f);
        rb2d.velocity = new Vector2(0, 0);
        particles.GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<Collider2D>().enabled = false;
        wandBulletSprite.GetComponent<SpriteRenderer>().enabled = false;
        switch(bulletSoulType)
        {
            case ActiveWeaponScript.SoulTypes.Earth:
                AbilityZoneObject zoneObj = Instantiate(zoneObject, transform.position, Quaternion.identity).GetComponent<AbilityZoneObject>();
                zoneObj.soulType = bulletSoulType;
                zoneObj.stayTime = zoneStayTime[player.wandPower];
                zoneObj.radious = zoneRadious[player.wandPower] * 1.2f;
                zoneObj.GetComponent<SpriteRenderer>().color = wandBulletSprite.GetComponent<SpriteRenderer>().color;
                break;
            case ActiveWeaponScript.SoulTypes.Fire:
                foreach (Collider2D c in Physics2D.OverlapCircleAll(transform.position, r))
                {
                    if (c.gameObject.CompareTag("Enemy"))
                    {
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Fire, false);
                        c.GetComponent<Enemy_Main>().BurningCumulator += damage / 2;
                        CircleScript cs1 = Instantiate(circle, c.transform.position, Quaternion.identity).GetComponent<CircleScript>();
                        cs1.GetComponent<SpriteRenderer>().color *= wandBulletSprite.gameObject.GetComponent<SpriteRenderer>().color;
                        cs1.radious = 2 * r;
                        cs1.speed = cs1.radious * cs1.radious;
                    }
                }
                break;
            case ActiveWeaponScript.SoulTypes.Ice:
                AbilityZoneObject zoneObj1 = Instantiate(zoneObject, transform.position, Quaternion.identity).GetComponent<AbilityZoneObject>();
                zoneObj1.soulType = bulletSoulType;
                zoneObj1.stayTime = zoneStayTime[player.wandPower];
                zoneObj1.radious = zoneRadious[player.wandPower] * 2;
                zoneObj1.GetComponent<SpriteRenderer>().color = wandBulletSprite.GetComponent<SpriteRenderer>().color;
                break;
            case ActiveWeaponScript.SoulTypes.Mage:
                AbilityZoneObject zoneObj2 = Instantiate(zoneObject, transform.position, Quaternion.identity).GetComponent<AbilityZoneObject>();
                zoneObj2.soulType = bulletSoulType;
                zoneObj2.stayTime = zoneStayTime[player.wandPower];
                zoneObj2.radious = zoneRadious[player.wandPower] * 3;
                zoneObj2.GetComponent<SpriteRenderer>().color = wandBulletSprite.GetComponent<SpriteRenderer>().color;
                break;
        }
        Destroy(gameObject);
    }
    IEnumerator SpawnPoisonZone()
    {
        for(int i=0; true; i++)
        {
            AbilityZoneObject zoneObj2 = Instantiate(zoneObject, transform.position, Quaternion.identity).GetComponent<AbilityZoneObject>();
            zoneObj2.soulType = bulletSoulType;
            zoneObj2.stayTime = zoneStayTime[player.wandPower] / 2;
            zoneObj2.radious = zoneRadious[player.wandPower] / 2;
            zoneObj2.GetComponent<SpriteRenderer>().color = wandBulletSprite.GetComponent<SpriteRenderer>().color;
            zoneObj2.GetComponent<SpriteRenderer>().sortingOrder = i;
            yield return new WaitForSeconds(1/ (speed[player.wandPower] * 1.5f));
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("wall"))
        {
            Explode();
        }
    }
}
