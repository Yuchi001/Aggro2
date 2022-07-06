using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPistol : MonoBehaviour
{
    public bool isShootgun;
    public bool piercing;
    public float fireRate;
    public float damage;
    public float bulletSpeed;
    public float bulletRange;
    public float size;
    public int bulletQuanitity;
    public int rareLevel;
    public int magazineSizeMax;
    public int bulletsLeft;
    public Sprite gunSprite;
    public Sprite itemSprite;

    public Colors rareColorScheme;
    public Sprites gunSpritesMenu;
    public GameObject ob;
    private Rigidbody2D rb2d;

    public UIManager uiMan;
    private IEnumerator Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        Vector2 randomVec = new Vector2(Random.Range(-1, 2), Random.Range(-1.5f,-0.7f));
        while (randomVec == new Vector2(0, 0))
        {
            randomVec = new Vector2(Random.Range(-1, 2), Random.Range(-1.5f, -0.7f));
        }
        rb2d.velocity = new Vector2(Random.Range(2f, 2.5f) * randomVec.x, Random.Range(2f, 2.5f) * randomVec.y);
        uiMan = GameObject.FindGameObjectWithTag("uiMan").GetComponent<UIManager>();
        gameObject.GetComponent<SpriteRenderer>().sprite = gunSprite;

        // set sprites
        if (gunSprite == gunSpritesMenu.gun_Pistol) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = gunSpritesMenu.gun_PistolOut;
            itemSprite = gunSpritesMenu.gun_PistolItem;
        }
        else if (gunSprite == gunSpritesMenu.gun_MachineGun) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = gunSpritesMenu.gun_MachineGunOut;
            itemSprite = gunSpritesMenu.gun_MachineGunItem;
        }
        else if (gunSprite == gunSpritesMenu.gun_ShootGun) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = gunSpritesMenu.gun_ShootGunOut;
            itemSprite = gunSpritesMenu.gun_ShootGunItem;
        }
        else if (gunSprite == gunSpritesMenu.gun_Sniper) 
        {
            ob.GetComponent<SpriteRenderer>().sprite = gunSpritesMenu.gun_SniperOut;
            itemSprite = gunSpritesMenu.gun_SniperItem;
        }
        else 
        {
            ob.GetComponent<SpriteRenderer>().sprite = gunSpritesMenu.gun_UziOut;
            itemSprite = gunSpritesMenu.gun_UziItem;
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
        if(collision.gameObject.CompareTag("Cursor"))
        {
            uiMan.OnMouseHitWeapon(gameObject, ActiveWeaponScript.WeaponTypesAW.Pistol);
        }
        else if (collision.gameObject.CompareTag("wall")) rb2d.velocity = new Vector2(-rb2d.velocity.x, -rb2d.velocity.y);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            foreach(GameObject r in GameObject.FindGameObjectsWithTag("weaponUI"))
            {
                Destroy(r);
            }
        }
    }
}
