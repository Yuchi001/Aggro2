using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public ActiveWeaponScript.SoulTypes grenadeSoulType;

    [HideInInspector] public PlayerScript player;
    public Scalers menuScalers;

    public GameObject miniBoom;
    public GameObject smoke;
    public GameObject circle;
    public GameObject preSmoke;
    public GameObject grenadeSprite;

    private Rigidbody2D rb2d;

    private Vector2 startPosition;

    public float rotateSpeed;

    [HideInInspector] public Vector3 endPosition;

    [HideInInspector] public bool move;
    [HideInInspector] public bool bounce;

    private bool antyGlitch = false;
    private bool isCheckForEnemiesRunning = false;

    public float speed;
    [HideInInspector] public float damage;
    public float[] detonateRadious;

    void Start()
    {
        startPosition = transform.position;
        bounce = true;
        rotateSpeed += 0.1f * speed;
        rotateSpeed = Random.Range(rotateSpeed /2, rotateSpeed);
        endPosition.z = 0;
        move = true;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        StartCoroutine(CheckForEnemies());
    }
    void Update()
    {
        if (move)
        {
            if (speed > 0) speed -= speed / 2 * Time.deltaTime;
            if (rotateSpeed > 0) rotateSpeed -= rotateSpeed / 2 * Time.deltaTime;
            rb2d.velocity = transform.up * speed;
            grenadeSprite.transform.Rotate(0, 0, rotateSpeed);
        }
        else rb2d.velocity = new Vector2(0, 0);
        if(transform.position == endPosition && move)
        {
            move = false;
            StartCoroutine(CheckForEnemies());
        }
        if ((transform.position.y < menuScalers.yBoundsMin - 0.1f || transform.position.x > menuScalers.xBoundsMax + 0.1f) && !antyGlitch) 
        {
            antyGlitch = true;
            speed = -speed;
        }
    }
    void DestroyGrenade()
    {
        GameObject smoke1 = Instantiate(smoke, transform.position, Quaternion.identity);
        ParticleSystem.MainModule smokeCopy = smoke1.GetComponent<ParticleSystem>().main;
        smokeCopy.startColor = grenadeSprite.gameObject.GetComponent<SpriteRenderer>().color;
        Destroy(smoke1, 1f);
        GameObject go = Instantiate(miniBoom, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ps = go.GetComponent<ParticleSystem>().main;
        ps.startColor = grenadeSprite.gameObject.GetComponent<SpriteRenderer>().color;
        ps.startSize = gameObject.transform.localScale.x / 8;
        Destroy(go, 1f);
        Destroy(gameObject);
    }
    public IEnumerator CheckForEnemies()
    {
        isCheckForEnemiesRunning = true;
        yield return new WaitForSeconds(2);
        GameObject preS = Instantiate(preSmoke, transform.position, Quaternion.identity, transform);
        ParticleSystem.MainModule presPS = preS.GetComponent<ParticleSystem>().main;
        if (grenadeSoulType != ActiveWeaponScript.SoulTypes.Empty) presPS.startColor = grenadeSprite.gameObject.GetComponent<SpriteRenderer>().color;
        else presPS.startColor = Color.black;
        Destroy(preS, 5f);
        yield return new WaitForSeconds(1);
        MakeGrenadeExplode();
        
    }
    public void MakeGrenadeExplode()
    {
        CircleScript cs = Instantiate(circle, transform.position, Quaternion.identity).GetComponent<CircleScript>();
        cs.GetComponent<SpriteRenderer>().color *= grenadeSprite.gameObject.GetComponent<SpriteRenderer>().color;
        cs.radious = 2 * detonateRadious[player.grenadePower];
        cs.speed = cs.radious * cs.radious;
        damage += damage * 0.1f * player.grenadePower;
        foreach (Collider2D c in Physics2D.OverlapCircleAll(transform.position, detonateRadious[player.grenadePower] / 2))
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                switch (grenadeSoulType)
                {
                    case ActiveWeaponScript.SoulTypes.Earth:
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
                        c.GetComponent<Enemy_Main>().StunFromAnotherObject(c.GetComponent<Enemy_Main>().ColorsMenu.soulMage);
                        c.GetComponent<Enemy_Main>().DamageEnemy(damage, Enemy_Main.DamageType.abilityPower, Enemy_Main.SoulTypes.Mage, false);
                        break;
                }
            }
        }
        DestroyGrenade();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Enemy":
                if(bounce)
                {
                    Vector2 collisionPoint = collision.ClosestPoint(transform.position);
                    Vector2 enemyCenter = collision.bounds.center;
                    Vector2 resultVector = collisionPoint - enemyCenter;
                    if (transform.position.x > collision.transform.position.x) resultVector = -resultVector;
                    if (GameObject.FindGameObjectWithTag("Player").transform.position.y > collision.transform.position.y)
                    {
                        transform.Rotate(0, 0, -Vector2.Angle(transform.position, resultVector));
                        grenadeSprite.transform.Rotate(0, 0, Vector2.Angle(transform.position, resultVector));
                    }
                    else
                    {
                        transform.Rotate(0, 0, Vector2.Angle(transform.position, resultVector));
                        grenadeSprite.transform.Rotate(0, 0, -Vector2.Angle(transform.position, resultVector));
                    }
                    collision.GetComponent<Enemy_Main>().DamageEnemy(1, Enemy_Main.DamageType.attackDamage, Enemy_Main.SoulTypes.Empty, true);
                }
                else
                {
                    move = false;
                    transform.parent = collision.transform;
                    StartCoroutine(CheckForEnemies());
                }
                break;
            case "wall":
                if (bounce)
                {
                    Quaternion rot = grenadeSprite.transform.rotation;
                    if (collision.GetComponent<Collider2D>().bounds.size.x > collision.GetComponent<Collider2D>().bounds.size.y)
                    {
                        if(transform.position.y < collision.transform.position.y)
                        {
                            Vector2 resultVector = Vector3.Cross(startPosition, new Vector3(1, 0, 0));
                            float angle = Mathf.Atan2(resultVector.y, resultVector.x) * Mathf.Rad2Deg - 180;
                            transform.Rotate(0, 0, angle);
                            transform.rotation = Quaternion.Inverse(transform.rotation);
                        }
                        else
                        {
                            Vector2 resultVector = Vector3.Cross(startPosition, new Vector3(-1, 0, 0));
                            float angle = Mathf.Atan2(resultVector.y, resultVector.x) * Mathf.Rad2Deg;
                            transform.Rotate(0, 0, angle);
                            transform.rotation = Quaternion.Inverse(transform.rotation);
                        }
                    }
                    else
                    {
                        if(transform.position.x < collision.transform.position.x)
                        {
                            Vector2 resultVector = Vector3.Cross(startPosition, new Vector3(0, 1, 0));
                            float angle = Mathf.Atan2(resultVector.y, resultVector.x) * Mathf.Rad2Deg - 180;
                            transform.Rotate(0, 0, -angle);
                            transform.rotation = Quaternion.Inverse(transform.rotation);
                        }
                        else
                        {
                            Vector2 resultVector = Vector3.Cross(startPosition, new Vector3(0, -1, 0));
                            float angle = Mathf.Atan2(resultVector.y, resultVector.x) * Mathf.Rad2Deg;
                            transform.Rotate(0, 0, -angle);
                            transform.rotation = Quaternion.Inverse(transform.rotation);
                        }
                    }
                    grenadeSprite.transform.rotation = rot;
                }
                else
                {
                    rb2d.velocity = new Vector2(0, 0);
                }
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && !bounce && !isCheckForEnemiesRunning)
        {
            StartCoroutine(CheckForEnemies());
        }
    }
}
