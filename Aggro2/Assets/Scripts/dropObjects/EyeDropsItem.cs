using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeDropsItem : MonoBehaviour
{
    public enum EyeDropType { def, senative, steelClad, resistant, vital };

    public ArmorSO defED;
    public ArmorSO senativeED;
    public ArmorSO steelCladED;
    public ArmorSO resistantED;
    public ArmorSO vitalED;

    [HideInInspector] public ArmorSO eyeDropsBasicSC; // podstawowe zmienne i umiejętność

    public EyeDropType eyeDropType;
    public int rareLevel;
    [HideInInspector] public string eyeDropsName;
    [HideInInspector] public string eyeDropsPassiveText;

    public GameObject ob;

    private UIManager uiMan;
    private Rigidbody2D rb2d;
    private ActiveWeaponScript weaponScript;
    void Start()
    {
        eyeDropsBasicSC = vitalED;
        switch(eyeDropType)
        {
            case EyeDropType.def:
                eyeDropsBasicSC = defED;
                break;
            case EyeDropType.senative:
                eyeDropsBasicSC = senativeED;
                break;
            case EyeDropType.steelClad:
                eyeDropsBasicSC = steelCladED;
                break;
            case EyeDropType.resistant:
                eyeDropsBasicSC = resistantED;
                break;
            case EyeDropType.vital:
                eyeDropsBasicSC = vitalED;
                break;
        }
        rb2d = GetComponent<Rigidbody2D>();
        uiMan = GameObject.FindGameObjectWithTag("uiMan").GetComponent<UIManager>();
        weaponScript = GameObject.FindGameObjectWithTag("w").GetComponent<ActiveWeaponScript>();
        //weaponScript.ChangeColorRareLevels(rareLevel, ob.gameObject);
        UseFullTools.ChangeColors_rareLevel_SpriteRenderer(weaponScript.ColorsMenu, ob.gameObject, rareLevel);
        SetBasicStats();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            // show eye drops ui
        }
        else if (collision.gameObject.CompareTag("wall")) rb2d.velocity = new Vector2(-rb2d.velocity.x, -rb2d.velocity.y);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            foreach (GameObject r in GameObject.FindGameObjectsWithTag("eyeDropsUI"))
            {
                Destroy(r);
            }
        }
    }
    public void SetBasicStats()
    {
        eyeDropsName = eyeDropsBasicSC.armorName;
        eyeDropsPassiveText = eyeDropsBasicSC.armorPassive;
        GetComponent<SpriteRenderer>().sprite = eyeDropsBasicSC.eyeDropSprite;
    }
}
