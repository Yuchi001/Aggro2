using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueItemScript : MonoBehaviour
{
    public itemScriptableObj[] vItems;
    public enum LootType {coin, coin2, manyCoins, sellItem}
    public LootType lootType;
    public string itemName;
    public int itemValue;
    public int rareLevel;
    public GameObject ob;
    public Sprite itemSprite;
    public Sprite uiSprite;

    public Sprites menuSprites;
    public Colors rareColorScheme;

    private Rigidbody2D rb2d;
    private UIManager uiMan;
    public bool canPickUp = false;
    public bool justDroped = false;
    IEnumerator Start()
    {
        uiMan = GameObject.FindGameObjectWithTag("uiMan").GetComponent<UIManager>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        Vector2 randomVec = new Vector2(Random.Range(-1, 2), Random.Range(-1.5f, -0.7f));
        while (randomVec == new Vector2(0, 0))
        {
            randomVec = new Vector2(Random.Range(-1, 2), Random.Range(-1.5f,- 0.7f));
        }
        rb2d.velocity = new Vector2(Random.Range(2f, 2.5f) * randomVec.x, Random.Range(2f, 2.5f) * randomVec.y);
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
        switch (lootType)
        {
            case LootType.coin:
                ob.GetComponent<SpriteRenderer>().sprite = menuSprites.coin_vItem_OB;
                uiSprite = menuSprites.coin2_vItem_UI;
                itemName = "Coin";
                break;
            case LootType.coin2:
                ob.GetComponent<SpriteRenderer>().sprite = menuSprites.coin2_vItem_OB;
                uiSprite = menuSprites.coin2_vItem_UI;
                itemName = "Coin2";
                break;
            case LootType.manyCoins:
                ob.GetComponent<SpriteRenderer>().sprite = menuSprites.manyCoins_vItem_OB;
                uiSprite = menuSprites.manyCoins_vItem_UI;
                itemName = "ManyCoins";
                break;
            case LootType.sellItem:
                if (justDroped) break;
                int randomIndex = Random.Range(0, vItems.Length);
                itemScriptableObj vItemSpec = vItems[randomIndex];
                ob.GetComponent<SpriteRenderer>().sprite = vItemSpec.itemSpriteOb;
                GetComponent<SpriteRenderer>().sprite = vItemSpec.itemSprite;
                uiSprite = vItemSpec.itemSpriteUI;
                itemName = vItemSpec.itemName;
                itemValue = vItemSpec.baseValue + (int)(vItemSpec.baseValue * Mathf.Pow(rareLevel, 2));
                break;
        }
        yield return new WaitForSeconds(0.7f);
        rb2d.velocity = new Vector2(0, 0);
        canPickUp = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            //show loot info
        }
        else if (collision.gameObject.CompareTag("wall")) rb2d.velocity = new Vector2(-rb2d.velocity.x, -rb2d.velocity.y);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            foreach (GameObject r in GameObject.FindGameObjectsWithTag("lootUI"))
            {
                Destroy(r);
            }
        }
    }
}
