using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pUIweapon : MonoBehaviour
{
    [HideInInspector] public UIManager uiMan;
    void Start()
    {
        uiMan = GameObject.FindGameObjectWithTag("uiMan").GetComponent<UIManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            uiMan.OnMouseHitUIweapon();
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
        }
    }
}
