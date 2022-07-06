using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSword : MonoBehaviour
{
    public bool bouncesBulletBack;
    public bool isRapier;
    public int rareLevel;
    public float attackRate;
    public float attackRadious;
    public float passiveSwordCritChance;
    public float swordDamage;
    public Sprite swordSprite;
    public Sprite itemSprite;

    public Colors rareColorScheme;
    public Sprites swordSpritesMenu;
    public GameObject ob;
    public UIManager uiMan;
    private Rigidbody2D rb2d;
    IEnumerator Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        Vector2 randomVec = new Vector2(Random.Range(-1, 2), Random.Range(-1.5f, -0.7f));
        while (randomVec == new Vector2(0, 0))
        {
            randomVec = new Vector2(Random.Range(-1, 2), Random.Range(-1.5f, -0.7f));
        }
        rb2d.velocity = new Vector2(Random.Range(2f, 3f) * randomVec.x, Random.Range(2f, 2.5f) * randomVec.y);
        uiMan = GameObject.FindGameObjectWithTag("uiMan").GetComponent<UIManager>();
        gameObject.GetComponent<SpriteRenderer>().sprite = swordSprite;
        // set sprites
        if (swordSprite == swordSpritesMenu.sword_stealSword) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = swordSpritesMenu.sword_stealSwordOut;
            itemSprite = swordSpritesMenu.sword_stealSwordItem;
        }
        else if (swordSprite == swordSpritesMenu.sword_lightsaber) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = swordSpritesMenu.sword_lightsaberOut;
            itemSprite = swordSpritesMenu.sword_lightsaberItem;
        }
        else if (swordSprite == swordSpritesMenu.sword_wideSword) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = swordSpritesMenu.sword_wideSwordOut;
            itemSprite = swordSpritesMenu.sword_wideSwordItem;
        }
        else if (swordSprite == swordSpritesMenu.sword_scythe) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = swordSpritesMenu.sword_scytheOut;
            itemSprite = swordSpritesMenu.sword_scytheItem;
        }
        else 
        {
            ob.GetComponent<SpriteRenderer>().sprite = swordSpritesMenu.sword_rapierOut;
            itemSprite = swordSpritesMenu.sword_rapierItem;
        }
        //
        switch (rareLevel)
        {
            case 0:
                ob.GetComponent<SpriteRenderer>().color = rareColorScheme.rareColor0;
                break;
            case 1:
                ob.GetComponent<SpriteRenderer>().color = rareColorScheme.rareColor1;
                break;
            case 2:
                ob.GetComponent<SpriteRenderer>().color = rareColorScheme.rareColor2;
                break;
            case 3:
                ob.GetComponent<SpriteRenderer>().color = rareColorScheme.rareColor3;
                break;
            case 4:
                ob.GetComponent<SpriteRenderer>().color = rareColorScheme.rareColor4;
                break;
        }
        yield return new WaitForSeconds(0.7f);
        rb2d.velocity = new Vector2(0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            uiMan.OnMouseHitWeapon(gameObject, ActiveWeaponScript.WeaponTypesAW.Sword);
        }
        else if (collision.gameObject.CompareTag("wall")) rb2d.velocity = new Vector2(-rb2d.velocity.x, -rb2d.velocity.y);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            foreach (GameObject r in GameObject.FindGameObjectsWithTag("weaponUI"))
            {
                Destroy(r);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadious);
    }
}
