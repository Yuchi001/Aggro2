using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotScript : MonoBehaviour
{
    public InventoryScript.ItemTypeEnum itemType;

    public Image itemImage;
    public Image lootImage;

    public Sprite emptySprite;
    public Sprite backGround;

    public TextMeshProUGUI rareText;
    public TextMeshProUGUI quantityText;

    public Transform costSpawnPos;

    public GameObject itemObject = null;
    public GameObject x;

    [HideInInspector] public PlayerScript player;
    [HideInInspector] public ActiveWeaponScript weaponScript;
    [HideInInspector] public InventoryScript inventory;
    [HideInInspector] public UIManager uiMan;

    public int id;
    public int itemCost;
    public int itemsValue = 0;
    public int rareLevel;

    public string lootItemName = "";

    private void Start()
    {
        rareText.text = "";
        itemObject = new GameObject();
        uiMan = GameObject.FindGameObjectWithTag("uiMan").GetComponent<UIManager>();
        inventory = GameObject.FindGameObjectWithTag("inventory").GetComponent<InventoryScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        weaponScript = GameObject.FindGameObjectWithTag("w").GetComponent<ActiveWeaponScript>();
    }
    public void OnDropItem()
    {
        inventory.inventoryValue -= itemCost;
        switch (itemType)
        {
            case InventoryScript.ItemTypeEnum.weapon:
                if (weaponScript.CheckPos(weaponScript.transform)) Instantiate(itemObject, weaponScript.transform.position, Quaternion.identity);
                else Instantiate(itemObject, weaponScript.reflectionSpawnPoint.position, Quaternion.identity);
                x.SetActive(false);
                itemImage.sprite = emptySprite;
                itemObject = null;
                inventory.isFull[id] = false;
                gameObject.GetComponent<Image>().color = Color.white;
                rareText.text = "";
                foreach (GameObject r in GameObject.FindGameObjectsWithTag("weaponUI"))
                {
                    Destroy(r);
                }
                break;
            case InventoryScript.ItemTypeEnum.loot:
                if (weaponScript.CheckPos(weaponScript.transform))
                {
                    ValueItemScript vis = Instantiate(itemObject, weaponScript.transform.position, Quaternion.identity).GetComponent<ValueItemScript>();
                    vis.justDroped = true;
                }
                else
                {
                    ValueItemScript vis = Instantiate(itemObject, weaponScript.reflectionSpawnPoint.position, Quaternion.identity).GetComponent<ValueItemScript>();
                    vis.justDroped = true;
                }
                
                if (itemsValue > 1)
                {
                    itemsValue--;
                    quantityText.text = itemsValue.ToString();
                }
                else
                {
                    itemsValue = 0;
                    x.SetActive(false);
                    itemImage.sprite = emptySprite;
                    itemObject = null;
                    inventory.isFull[id] = false;
                    gameObject.GetComponent<Image>().color = Color.white;
                    rareText.text = "";
                    lootItemName = "";
                    foreach (GameObject r in GameObject.FindGameObjectsWithTag("lootUI"))
                    {
                        Destroy(r);
                    }
                    quantityText.gameObject.SetActive(false);
                    lootImage.gameObject.SetActive(false);
                }
                break;
        }
    }
    public void OnUseItem()
    {
        inventory.inventoryValue -= itemCost;
        player.canPickUp = true;
        switch(itemType)
        {
            case InventoryScript.ItemTypeEnum.weapon:
                if(player.canPickUp)
                {
                    player.PickUpWeapon(itemObject.tag, itemObject);
                    x.SetActive(false);
                    itemImage.sprite = emptySprite;
                    itemObject = null;
                    inventory.isFull[id] = false;
                    gameObject.GetComponent<Image>().color = Color.white;
                }
                break;
            case InventoryScript.ItemTypeEnum.eyeDrops:
                break;
            case InventoryScript.ItemTypeEnum.slime:
                break;
        }
        rareText.text = "";
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("weaponUI"))
        {
            Destroy(r);
        }
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("lootUI"))
        {
            Destroy(r);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor") && itemObject != null)
        {
            switch (itemType)
            {
                case InventoryScript.ItemTypeEnum.weapon:
                    switch (itemObject.tag)
                    {
                        case "dropWeaponSword":
                            uiMan.OnMouseHitSlot(itemType, itemCost, rareLevel, costSpawnPos, "Sword");
                            uiMan.OnMouseHitWeapon(itemObject, ActiveWeaponScript.WeaponTypesAW.Sword);
                            break;
                        case "dropWeaponPistol":
                            uiMan.OnMouseHitSlot(itemType, itemCost, rareLevel, costSpawnPos, "Gun");
                            uiMan.OnMouseHitWeapon(itemObject, ActiveWeaponScript.WeaponTypesAW.Pistol);
                            break;
                        case "dropWeaponWand":
                            uiMan.OnMouseHitWeapon(itemObject, ActiveWeaponScript.WeaponTypesAW.Wand);
                            uiMan.OnMouseHitSlot(itemType, itemCost, rareLevel, costSpawnPos, "Wand");
                            break;
                        case "dropWeaponBow":
                            uiMan.OnMouseHitWeapon(itemObject, ActiveWeaponScript.WeaponTypesAW.Bow);
                            uiMan.OnMouseHitSlot(itemType, itemCost, rareLevel, costSpawnPos, "Bow");
                            break;
                    }
                    break;
                case InventoryScript.ItemTypeEnum.loot:
                    uiMan.OnMouseHitSlot(itemType, itemCost, rareLevel, costSpawnPos, lootItemName);
                    break;
                case InventoryScript.ItemTypeEnum.eyeDrops:
                    break;
                case InventoryScript.ItemTypeEnum.slime:
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            foreach (GameObject r in GameObject.FindGameObjectsWithTag("weaponUI"))
            {
                Destroy(r);
            }
            foreach (GameObject r in GameObject.FindGameObjectsWithTag("lootUI"))
            {
                Destroy(r);
            }
        }
    }
    public string RareLevelName(int r)
    {
        switch (r)
        {
            case 0:
                return "I";
            case 1:
                return "II";
            case 2:
                return "III";
            case 3:
                return "IV";
            case 4:
                return "V";
            default:
                return "something went wrong UwU";
        }
    }
}
