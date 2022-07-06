using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWand : MonoBehaviour
{
    public bool isDruid;
    public float additionalApScaler;
    public float damage;
    public float attackSpeed;
    public float additionalCDR;
    public float autoAbilitySpeed;
    public float autoAbilityRange;
    public float chargingSpeed;
    public int rareLevel;
    public Sprite wandSprite;
    public Sprite itemSprite;

    public Colors rareColorScheme;
    public Sprites wandSpritesMenu;
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
        gameObject.GetComponent<SpriteRenderer>().sprite = wandSprite;
        //set sprites
        if (wandSprite == wandSpritesMenu.wand_defWand) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = wandSpritesMenu.wand_defWandOut;
            itemSprite = wandSpritesMenu.wand_defWandItem;
        }
        else if (wandSprite == wandSpritesMenu.wand_occultScepter) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = wandSpritesMenu.wand_occultScepterOut;
            itemSprite = wandSpritesMenu.wand_occultScepterItem;
        }
        else if (wandSprite == wandSpritesMenu.wand_druidWand) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = wandSpritesMenu.wand_druidWandOut;
            itemSprite = wandSpritesMenu.wand_druidWandItem;
        }
        else if (wandSprite == wandSpritesMenu.wand_crystalWand) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = wandSpritesMenu.wand_crystalWandOut;
            itemSprite = wandSpritesMenu.wand_crystalWandItem;
        }
        else 
        {
            ob.GetComponent<SpriteRenderer>().sprite = wandSpritesMenu.wand_darkLordWandOut;
            itemSprite = wandSpritesMenu.wand_darkLordWandItem;
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
            uiMan.OnMouseHitWeapon(gameObject, ActiveWeaponScript.WeaponTypesAW.Wand);
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
