using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseFullTools
{
    public static void ChangeColors_rareLevel_SpriteRenderer(Colors colorScheme, GameObject go, int rareLevel)
    {
        SpriteRenderer change = go.GetComponent<SpriteRenderer>();
        switch(rareLevel)
        {
            case 0:
                change.color = colorScheme.rareColor0;
                break;
            case 1:
                change.color = colorScheme.rareColor1;
                break;
            case 2:
                change.color = colorScheme.rareColor2;
                break;
            case 3:
                change.color = colorScheme.rareColor3;
                break;
            case 4:
                change.color = colorScheme.rareColor4;
                break;
        }
    }
    public static void ChangeColors_rareLevel_Image(Colors colorScheme, GameObject go, int rareLevel)
    {
        Image change = go.GetComponent<Image>();
        switch (rareLevel)
        {
            case 0:
                change.color = colorScheme.rareColor0;
                break;
            case 1:
                change.color = colorScheme.rareColor1;
                break;
            case 2:
                change.color = colorScheme.rareColor2;
                break;
            case 3:
                change.color = colorScheme.rareColor3;
                break;
            case 4:
                change.color = colorScheme.rareColor4;
                break;
        }
    }
    public static void ChangeColors_soulType_SpriteRenderer(Colors colorScheme, GameObject go, ActiveWeaponScript.SoulTypes soulType)
    {
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        switch (soulType)
        {
            case ActiveWeaponScript.SoulTypes.Earth:
                sr.color = colorScheme.soulEarth;
                break;
            case ActiveWeaponScript.SoulTypes.Electric:
                sr.color = colorScheme.soulElectric;
                break;
            case ActiveWeaponScript.SoulTypes.Ice:
                sr.color = colorScheme.soulIce;
                break;
            case ActiveWeaponScript.SoulTypes.Fire:
                sr.color = colorScheme.soulFire;
                break;
            case ActiveWeaponScript.SoulTypes.Poison:
                sr.color = colorScheme.soulPoison;
                break;
            case ActiveWeaponScript.SoulTypes.Dark:
                sr.color = colorScheme.soulDark;
                break;
            case ActiveWeaponScript.SoulTypes.Sweet:
                sr.color = colorScheme.soulSweet;
                break;
            case ActiveWeaponScript.SoulTypes.Mage:
                sr.color = colorScheme.soulMage;
                break;
            case ActiveWeaponScript.SoulTypes.Empty:
                sr.color = Color.white;
                break;
        }
    }
    public static void ChangeColors_soulType_ParticleSystem(Colors colorScheme, GameObject go, ActiveWeaponScript.SoulTypes soulType)
    {
        ParticleSystem.MainModule ps = go.GetComponent<ParticleSystem>().main;
        switch (soulType)
        {
            case ActiveWeaponScript.SoulTypes.Earth:
                ps.startColor = colorScheme.soulEarth;
                break;
            case ActiveWeaponScript.SoulTypes.Electric:
                ps.startColor = colorScheme.soulElectric;
                break;
            case ActiveWeaponScript.SoulTypes.Ice:
                ps.startColor = colorScheme.soulIce;
                break;
            case ActiveWeaponScript.SoulTypes.Fire:
                ps.startColor = colorScheme.soulFire;
                break;
            case ActiveWeaponScript.SoulTypes.Poison:
                ps.startColor = colorScheme.soulPoison;
                break;
            case ActiveWeaponScript.SoulTypes.Dark:
                ps.startColor = colorScheme.soulDark;
                break;
            case ActiveWeaponScript.SoulTypes.Sweet:
                ps.startColor = colorScheme.soulSweet;
                break;
            case ActiveWeaponScript.SoulTypes.Mage:
                ps.startColor = colorScheme.soulMage;
                break;
            case ActiveWeaponScript.SoulTypes.Empty:
                ps.startColor = Color.black;
                break;
        }
    }
    public static void ChangeColors_soulType_Image(Colors colorScheme, GameObject go, ActiveWeaponScript.SoulTypes soulType)
    {
        Image im = go.GetComponent<Image>();
        switch (soulType)
        {
            case ActiveWeaponScript.SoulTypes.Earth:
                im.color = colorScheme.soulEarth;
                break;
            case ActiveWeaponScript.SoulTypes.Electric:
                im.color = colorScheme.soulElectric;
                break;
            case ActiveWeaponScript.SoulTypes.Ice:
                im.color = colorScheme.soulIce;
                break;
            case ActiveWeaponScript.SoulTypes.Fire:
                im.color = colorScheme.soulFire;
                break;
            case ActiveWeaponScript.SoulTypes.Poison:
                im.color = colorScheme.soulPoison;
                break;
            case ActiveWeaponScript.SoulTypes.Dark:
                im.color = colorScheme.soulDark;
                break;
            case ActiveWeaponScript.SoulTypes.Sweet:
                im.color = colorScheme.soulSweet;
                break;
            case ActiveWeaponScript.SoulTypes.Mage:
                im.color = colorScheme.soulMage;
                break;
            case ActiveWeaponScript.SoulTypes.Empty:
                im.color = Color.black;
                break;
        }
    }
}
