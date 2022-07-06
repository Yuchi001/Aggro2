using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandBullet : MonoBehaviour
{
    public ActiveWeaponScript.SoulTypes bulletSoulType;
    public float speed;
    public float range;
    public float damage;
    Vector2 startLocation;
    public GameObject miniboom;
    public GameObject particles;
    public GameObject wandBulletSprite;
    private Rigidbody2D rb2d;
    private bool oneTimeAction = false;
    private bool dead = false;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        startLocation = transform.position;
        wandBulletSprite.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        ParticleSystem.MainModule psMM = particles.GetComponent<ParticleSystem>().main;
        psMM.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        if (bulletSoulType == ActiveWeaponScript.SoulTypes.Empty) psMM.startColor = Color.black;
    }
    void Update()
    {
        if (Vector2.Distance(startLocation, transform.position) > range && !oneTimeAction && !dead) 
        {
            DestroyBullet();
            oneTimeAction = true;
        }
        wandBulletSprite.transform.Rotate(0, 0, speed/2);
        rb2d.velocity = transform.up * speed;
    }
    void DestroyBullet()
    {
        dead = true;
        GameObject go = Instantiate(miniboom, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ps = go.GetComponent<ParticleSystem>().main;
        ps.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        ps.startSize = gameObject.transform.localScale.x / 8;
        go.transform.parent = null;
        particles.transform.localScale = new Vector2(4 * transform.localScale.x * particles.transform.localScale.x, 4 * transform.localScale.x * particles.transform.localScale.x);
        particles.transform.parent = null;
        Destroy(go, 1f);
        rb2d.velocity = new Vector2(0,0);
        particles.GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<Collider2D>().enabled = false;
        wandBulletSprite.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            switch (bulletSoulType)
            {
                case ActiveWeaponScript.SoulTypes.Earth:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Earth, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Poison:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Poison, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Fire:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Fire, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Ice:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Ice, false);
                    break;
                case ActiveWeaponScript.SoulTypes.Electric:
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Electric, false);
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
            DestroyBullet();
        }
        else if (c.gameObject.CompareTag("wall")) DestroyBullet();
    }
}
