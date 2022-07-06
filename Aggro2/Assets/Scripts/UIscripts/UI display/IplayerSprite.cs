using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IplayerSprite : MonoBehaviour
{
    public Image playerImage;
    public Image playerImageEB;

    [HideInInspector] public Sprite pLookUpleft;
    [HideInInspector] public Sprite pLookUpRight;
    [HideInInspector] public Sprite pLookDownLeft;
    [HideInInspector] public Sprite pLookDownRight;
    [HideInInspector] public Sprite pLookMidLeft;
    [HideInInspector] public Sprite pLookMidRight;

    [HideInInspector] public Sprite pLookUpleftEB;
    [HideInInspector] public Sprite pLookUpRightEB;
    [HideInInspector] public Sprite pLookDownLeftEB;
    [HideInInspector] public Sprite pLookDownRightEB;
    [HideInInspector] public Sprite pLookMidLeftEB;
    [HideInInspector] public Sprite pLookMidRightEB;
    public void OnPlayerLookChange(ActiveWeaponScript aws)
    {
        pLookDownLeft = aws.pLookDownLeft;
        pLookDownRight = aws.pLookDownRight;
        pLookUpleft = aws.pLookUpleft;
        pLookUpRight = aws.pLookUpRight;
        pLookMidLeft = aws.pLookMidLeft;
        pLookMidRight = aws.pLookMidRight;

        pLookDownLeftEB = aws.pLookDownLeftEB;
        pLookDownRightEB = aws.pLookDownRightEB;
        pLookUpleftEB = aws.pLookUpleftEB;
        pLookUpRightEB = aws.pLookUpRightEB;
        pLookMidLeftEB = aws.pLookMidLeftEB;
        pLookMidRightEB = aws.pLookMidRightEB;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 awPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x -= awPosition.x;
        mousePosition.y -= awPosition.y;

        playerImageEB.color = GameObject.FindGameObjectWithTag("w").GetComponent<ActiveWeaponScript>().ebSprite.color;
        if (Mathf.Abs(mousePosition.y) - Mathf.Abs(transform.position.y) < 60 && mousePosition.x > transform.position.x)
        {
            playerImage.sprite = pLookMidRight;
            playerImageEB.sprite = pLookMidRightEB;
        }
        else if (Mathf.Abs(mousePosition.y) - Mathf.Abs(transform.position.y) < 60 && mousePosition.x < transform.position.x)
        {
            playerImage.sprite = pLookMidLeft;
            playerImageEB.sprite = pLookMidLeftEB;
        }
        else if (mousePosition.x > transform.position.x && mousePosition.y > transform.position.y)
        {
            playerImage.sprite = pLookUpRight;
            playerImageEB.sprite = pLookUpRightEB;
        }
        else if (mousePosition.x < transform.position.x && mousePosition.y > transform.position.y)
        {
            playerImage.sprite = pLookUpleft;
            playerImageEB.sprite = pLookUpleftEB;
        }
        else if (mousePosition.x > transform.position.x)
        {
            playerImage.sprite = pLookDownRight;
            playerImageEB.sprite = pLookDownRightEB;
        }
        else
        {
            playerImage.sprite = pLookDownLeft;
            playerImageEB.sprite = pLookDownLeftEB;
        }
    }
}
