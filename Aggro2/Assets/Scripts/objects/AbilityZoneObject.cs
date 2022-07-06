using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityZoneObject : MonoBehaviour
{
    public ActiveWeaponScript.SoulTypes soulType;

    public ParticleSystem mainSystem;

    public GameObject vanishParticle;

    public float stayTime;
    public float radious;
    IEnumerator Start()
    {
        ParticleSystem.MainModule ps = mainSystem.main;
        ParticleSystem.ShapeModule psSM = mainSystem.shape;
        ps.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        psSM.radius = radious / 3;
        transform.localScale = new Vector2(radious, radious);
        float tempRad = 0;

        //animacja pojawiania
        while(transform.localScale.x < radious *1.1f)
        {
            tempRad += radious/10;
            transform.localScale = new Vector2(tempRad, tempRad);
            yield return new WaitForSeconds(0.01f);
        }
        while(transform.localScale.x > radious)
        {
            transform.localScale = new Vector2(transform.localScale.x - radious/5, transform.localScale.y - radious/5);
            yield return new WaitForSeconds(0.01f);
        }
        //koniec animacji pojawiania

        ps.startSize = new ParticleSystem.MinMaxCurve(radious/15, radious/10);
        yield return new WaitForSeconds(stayTime - 0.15f);

        //animacja znikania
        tempRad = 0;
        while (transform.localScale.x < radious * 1.05f)
        {
            tempRad += radious / 5;
            transform.localScale = new Vector2(transform.localScale.x + tempRad, transform.localScale.y + tempRad);
            yield return new WaitForSeconds(0.01f);
        }
        while (transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(transform.localScale.x - radious / 10, transform.localScale.y - radious / 10);
            yield return new WaitForSeconds(0.01f);
        }
        //koniec animacji znikania

        VanishZone();
    }
    void VanishZone()
    {
        GameObject go = Instantiate(vanishParticle, transform.position, Quaternion.identity);
        ParticleSystem.MainModule psmm = go.GetComponent<ParticleSystem>().main;
        psmm.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        Destroy(go, 2f);
        Destroy(gameObject);
    }
}
