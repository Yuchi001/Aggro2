using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSO : ScriptableObject
{
    public EyeDropsItem.EyeDropType eyeDropType;
    public string armorName;
    public string armorPassive;
    public Sprite eyeDropSprite;
    public Sprite eyeDropSprite_OB;
    public Sprite eyeDropSprite_UI;
    public Sprite armorSprite_Up_Left;
    public Sprite armorSprite_Midle_Left;
    public Sprite armorSprite_Down_Left;
    public Sprite armorSprite_Up_Right;
    public Sprite armorSprite_Middle_Right;
    public Sprite armorSprite_Down_Right;

    public virtual void PassiveArmorAbility(PlayerScript playerScript)
    {
        //null
    }
    public virtual void StopPassiveAbility(PlayerScript playerScript)
    {
        // drop item mechanics 
    }
}
