using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public ActiveWeaponScript.SoulTypes bulletSoulType;
    public bool piercing;
    public float speed;
    public float range;
    public float damage;
    Vector2 startLocation;
    public GameObject miniboom;
    private Rigidbody2D rb2d;
    private TrailRenderer trail;

    void Start()
    {
        trail = gameObject.GetComponent<TrailRenderer>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        startLocation = transform.position;
        trail.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (Vector2.Distance(startLocation, transform.position) > range) DestroyBullet();
        rb2d.velocity = transform.up * speed;
    }
    void DestroyBullet()
    {
        GameObject go = Instantiate(miniboom, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ps = go.GetComponent<ParticleSystem>().main;
        ps.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        ps.startSize = gameObject.transform.localScale.x / 8;
        Destroy(go, 1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.CompareTag("Enemy"))
        {
            switch (bulletSoulType)
            {
                case ActiveWeaponScript.SoulTypes.Earth:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Earth, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Poison:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Poison, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Fire:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Fire, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Ice:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Ice, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Electric:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Electric, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Empty:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Empty, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Sweet:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Sweet, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Dark:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Dark, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Mage:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Mage, false);
                    break;
            }
            if (!piercing) DestroyBullet();
        }
    }
}
