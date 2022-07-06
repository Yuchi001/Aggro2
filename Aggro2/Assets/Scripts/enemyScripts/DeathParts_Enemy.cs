using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParts_Enemy : MonoBehaviour
{
    [HideInInspector] public int rotation;

    public float speed;
    public float rotationSpeed;

    private Rigidbody2D rb2d;

    public GameObject blood;
    IEnumerator Start()
    {
        rotationSpeed = Random.Range(-rotationSpeed, rotationSpeed);
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        switch (rotation)
        {
            case 0:
                rb2d.velocity = new Vector2(Random.Range(speed/2, speed), Random.Range(speed / 2, speed));
                break;
            case 1:
                rb2d.velocity = new Vector2(Random.Range(speed / 2, speed), -Random.Range(speed / 2, speed));
                break;
            case 2:
                rb2d.velocity = new Vector2(-Random.Range(speed / 2, speed), -Random.Range(speed / 2, speed));
                break;
            case 3:
                rb2d.velocity = new Vector2(-Random.Range(speed / 2, speed), Random.Range(speed / 2, speed));
                break;
        }
        yield return new WaitForSeconds(0.4f);
        GameObject go = Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(go, 2f);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
}
