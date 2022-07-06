using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    public float radious;
    public float speed;

    private bool isDead = false;
    void Update()
    {
        if(transform.localScale.x < radious)transform.localScale = new Vector2(transform.localScale.x + speed * Time.deltaTime, transform.localScale.y + speed * Time.deltaTime);
        else if(transform.localScale.x > radious - 0.1f && !isDead)
        {
            Destroy(gameObject, 0.3f);
            gameObject.GetComponent<Animator>().SetTrigger("die");
            isDead = true;
        }
    }
}
