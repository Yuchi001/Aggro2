using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryScript : MonoBehaviour
{
    public enum ItemTypeEnum {weapon, loot, eyeDrops, slime}
    public bool[] isFull;
    public SlotScript[] slots;
    public Scalers menuScalers;
    public Colors menuColors;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI valueText;

    public int coinQuantity = 0;
    public int inventoryValue = 0;
    private void Update()
    {
        if(coinQuantity < 1000)
        {
            coinText.text = ": $" + coinQuantity.ToString();
        }
        else
        {
            coinText.text = ": $" + ((float)coinQuantity / 1000).ToString() + ".k";
        }
        if (inventoryValue < 1000)
        {
            valueText.text = ": $" + inventoryValue.ToString();
        }
        else
        {
            valueText.text = ": $" + ((float)inventoryValue / 1000).ToString() + " k";
        }
    }
    public bool AddToInventory(GameObject item, Sprite itemSprite, ItemTypeEnum itemType, int rareLevel, int itemCost)
    {
        if (itemType == ItemTypeEnum.loot)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].rareLevel == rareLevel && slots[i].lootItemName == item.GetComponent<ValueItemScript>().itemName && slots[i].itemsValue < 16)
                {
                    slots[i].itemsValue++;
                    slots[i].quantityText.text = slots[i].itemsValue.ToString();
                    if (slots[i].itemsValue == 16) isFull[i] = true;
                    inventoryValue += slots[i].itemCost;
                    return true;
                }
            }
        }
        for (int i = 0; i< slots.Length; i++)
        {
            if(!isFull[i])
            {
                switch(itemType)
                {
                    case ItemTypeEnum.weapon:
                        if(slots[i].itemsValue == 0)
                        {
                            isFull[i] = true;
                            slots[i].itemCost = itemCost;
                            slots[i].itemObject = item;
                            slots[i].rareLevel = rareLevel;
                            slots[i].itemImage.sprite = itemSprite;
                            slots[i].itemType = itemType;
                            slots[i].rareText.text = slots[i].RareLevelName(rareLevel);
                            slots[i].x.SetActive(true);
                            UseFullTools.ChangeColors_rareLevel_Image(menuColors, slots[i].itemImage.gameObject, rareLevel);
                            if (rareLevel == 0) slots[i].itemImage.color = Color.white;
                            inventoryValue += slots[i].itemCost;
                            return true;
                        }
                        break;
                    case ItemTypeEnum.loot:
                        if (slots[i].itemsValue == 0)
                        {
                            slots[i].itemCost = itemCost;
                            slots[i].lootImage.gameObject.SetActive(true);
                            slots[i].lootImage.sprite = itemSprite;
                            slots[i].quantityText.gameObject.SetActive(true);
                            slots[i].itemsValue++;
                            slots[i].quantityText.text = slots[i].itemsValue.ToString();
                            slots[i].itemObject = item;
                            slots[i].rareLevel = rareLevel;
                            slots[i].itemImage.sprite = slots[i].backGround;
                            slots[i].itemType = itemType;
                            slots[i].lootItemName = item.GetComponent<ValueItemScript>().itemName;
                            slots[i].rareText.text = slots[i].RareLevelName(rareLevel);
                            slots[i].x.SetActive(true);
                            UseFullTools.ChangeColors_rareLevel_Image(menuColors, slots[i].itemImage.gameObject, rareLevel);
                            if (rareLevel == 0) slots[i].itemImage.color = Color.white;
                            slots[i].lootImage.color = slots[i].itemImage.color;
                            inventoryValue += slots[i].itemCost;
                            return true;
                        }
                        else if(slots[i].rareLevel == rareLevel && slots[i].lootItemName == item.GetComponent<ValueItemScript>().itemName)
                        {
                            slots[i].itemsValue++;
                            slots[i].quantityText.text = slots[i].itemsValue.ToString();
                            if (slots[i].itemsValue == 16) isFull[i] = true;
                            inventoryValue += slots[i].itemCost;
                            return true;
                        }
                        break;
                    case ItemTypeEnum.eyeDrops:
                        break;
                }
            }
        }
        return false;
    }
}
