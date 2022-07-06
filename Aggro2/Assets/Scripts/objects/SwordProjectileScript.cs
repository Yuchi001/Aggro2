using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectileScript : MonoBehaviour
{
    public ActiveWeaponScript.SoulTypes projectileSoulType;

    public GameObject miniBoom;
    public GameObject partSystem;
    public GameObject partSystem2;

    private Rigidbody2D rb2d;

    private Vector2 startPosition;
    private Vector2 startingScale;

    public float speed;
    public float range;
    public float damage;

    private bool isDead = false;
    public bool first = false;
    void Start()
    {
        startingScale = transform.localScale;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        rb2d.velocity = transform.up * speed;
        ParticleSystem.MainModule ps1 = partSystem.GetComponent<ParticleSystem>().main;
        ps1.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        ParticleSystem.MainModule ps2 = partSystem2.GetComponent<ParticleSystem>().main;
        ps2.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x + 1 * Time.deltaTime, transform.localScale.y + 1 * Time.deltaTime);
        if (Vector2.Distance(startPosition, transform.position) > range && !isDead) 
        {
            DestroyObject();
        }
    }
    void SpawnParticles()
    {
        GameObject go = Instantiate(miniBoom, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ps = go.GetComponent<ParticleSystem>().main;
        ps.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        ps.startSize = startingScale.x / 8;
        Destroy(go, 1f);
    }
    private void DestroyObject()
    {
        gameObject.GetComponent<Animator>().SetTrigger("die");
        Destroy(gameObject, 0.6f);
        isDead = true;
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            switch (projectileSoulType)
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
                    if (first) 
                    {
                        Color tempColor = gameObject.GetComponent<SpriteRenderer>().color;
                        tempColor.a = 255;
                        c.GetComponent<Enemy_Main>().StunFromAnotherObject(tempColor);
                    }
                    c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Mage, false);
                    break;
            }
            SpawnParticles();
            if (!isDead) DestroyObject();
        }
        else if (c.gameObject.CompareTag("wall")) DestroyObject();
    }
}
