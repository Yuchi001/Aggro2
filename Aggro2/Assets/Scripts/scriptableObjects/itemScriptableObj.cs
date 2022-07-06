using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootItem", menuName = "LootItemMenu")]
public class itemScriptableObj : ScriptableObject
{
    public ValueItemScript.LootType lootType;
    public Sprite itemSprite;
    public Sprite itemSpriteUI;
    public Sprite itemSpriteOb;
    public string itemName;
    public int baseValue;
}
