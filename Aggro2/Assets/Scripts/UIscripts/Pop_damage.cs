using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pop_damage : MonoBehaviour
{
    public float moveSpeed;
    public float XmoveSpeed;
    public bool isState;
    private bool direction;
    private Rigidbody2D rb2d;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (isState) moveSpeed *= -0.5f;
        else gameObject.GetComponent<TextMeshProUGUI>().fontSize = 18;

        direction = Random.value < 0.5f;
    }
    void Update()
    {
        if(isState)
        {
            rb2d.velocity = Vector2.up * moveSpeed;
        }
        else
        {
            if (direction) rb2d.velocity = new Vector2(Random.Range(0.1f, XmoveSpeed), rb2d.velocity.y);
            else rb2d.velocity = new Vector2(Random.Range(-XmoveSpeed, -0.1f), rb2d.velocity.y);
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveSpeed);
            moveSpeed -= 0.05f;
            XmoveSpeed -= Time.deltaTime;
        }
    }
}
