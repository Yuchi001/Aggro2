using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// skrypt który kontroluje wyświetlanie statystyk duszy gracza z poziomu UI
public class pUIsoul : MonoBehaviour
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
            uiMan.OnMouseHitUIsoul();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            foreach (GameObject r in GameObject.FindGameObjectsWithTag("soulObj"))
            {
                Destroy(r);
            }
        }
    }
}
