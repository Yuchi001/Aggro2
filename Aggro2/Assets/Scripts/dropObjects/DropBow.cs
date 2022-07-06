using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropBow : MonoBehaviour
{
    public bool isCrossBow;
    public float bowDamage;
    public float chargingAttackTime;
    public float arrowsSize;
    public float arrowStayTime;
    public float arrowSpeed;
    public float arrowRange;
    public int rareLevel;
    public Sprite bowSprite;
    public Sprite itemSprite;

    public Colors rareColorScheme;
    public Sprites bowSpritesMenu;
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
        rb2d.velocity = new Vector2(Random.Range(2f, 2.5f) * randomVec.x, Random.Range(2f, 2.5f) * randomVec.y);
        uiMan = GameObject.FindGameObjectWithTag("uiMan").GetComponent<UIManager>();
        gameObject.GetComponent<SpriteRenderer>().sprite = bowSprite;
        // set sprites
        if (bowSprite == bowSpritesMenu.bow_defBow) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = bowSpritesMenu.bow_defBowOut;
            itemSprite = bowSpritesMenu.bow_defBowItem;
        }
        else if (bowSprite == bowSpritesMenu.bow_longBow) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = bowSpritesMenu.bow_longBowOut;
            itemSprite = bowSpritesMenu.bow_longBowItem;
        }
        else 
        {
            ob.GetComponent<SpriteRenderer>().sprite = bowSpritesMenu.bow_crossBowOut;
            itemSprite = bowSpritesMenu.bow_crossBowItem;
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
            uiMan.OnMouseHitWeapon(gameObject, ActiveWeaponScript.WeaponTypesAW.Bow);
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
}

