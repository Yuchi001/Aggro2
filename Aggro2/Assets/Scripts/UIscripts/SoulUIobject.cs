using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//skrypt kontrolujący wyświetlanie statystyk duszy gracza jako obiekt (innymi słowy ten skrypt przypięty jest do obiektu)
public class SoulUIobject : MonoBehaviour
{
    public TextMeshProUGUI soulName;
    public TextMeshProUGUI soulText;

    public Image[] sp;
    public Color colorObj;
    private void Update()
    {
        for (int i = 0; i < sp.Length; i++)
        {
            sp[i].color = colorObj;
        }
    }
}
