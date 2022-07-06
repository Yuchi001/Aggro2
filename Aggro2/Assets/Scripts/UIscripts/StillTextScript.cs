using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StillTextScript : MonoBehaviour
{
    string oryginalText;

    Vector2 playerPos;

    IEnumerator Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        yield return new WaitForSeconds(0.1f);
        oryginalText = gameObject.GetComponent<TextMeshProUGUI>().text;
        StartCoroutine(animationPlay());
    }
    private void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        transform.position = new Vector2(playerPos.x, playerPos.y + 0.3f);
    }
    IEnumerator animationPlay()
    {
        while(true)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = oryginalText + '.';
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<TextMeshProUGUI>().text += '.';
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<TextMeshProUGUI>().text += '.';
            yield return new WaitForSeconds(0.5f);
        }
    }
}
