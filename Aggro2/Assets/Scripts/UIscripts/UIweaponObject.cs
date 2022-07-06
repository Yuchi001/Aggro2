using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIweaponObject : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI rare;
    public TextMeshProUGUI ad;
    public TextMeshProUGUI attackSpeed;
    public TextMeshProUGUI attackRange;
    public TextMeshProUGUI size;
    public TextMeshProUGUI bulletSpeed;
    public TextMeshProUGUI additionalCDR;
    public TextMeshProUGUI additionalAP;
    public TextMeshProUGUI ammo;
    public GameObject hand;

    public Image weaponImage;
    public Color colorObj;
    public Image[] sp;
    private void Update()
    {
        for(int i=0; i <sp.Length; i++)
        {
            sp[i].color = colorObj;
        }
    }
}
