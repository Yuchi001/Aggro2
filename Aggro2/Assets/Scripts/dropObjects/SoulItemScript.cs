using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//skrypt wyświetlający statystyki gracza z poziomu itemu
public class SoulItemScript : MonoBehaviour
{
    public RollWeapon.SoulType soulType;

    public ParticleSystem soulEffect;

    public GameObject destroyFuzzyParticles;
    public GameObject destroyParticles;
    public GameObject chargeParticles;

    [HideInInspector] public GameObject uiObject;
    [HideInInspector] public GameObject canvas;

    public Colors menuColors;

    [HideInInspector]public UIManager uiMan;
    void Start()
    {
        uiMan = GameObject.FindGameObjectWithTag("uiMan").GetComponent<UIManager>();
        canvas = GameObject.FindGameObjectWithTag("c");
        ChangeColor(soulEffect.gameObject, true, soulType);
        ChangeColor(gameObject, false, soulType);
        soulEffect.Play();
    }
    public void ChangeColor(GameObject go, bool particles, RollWeapon.SoulType soulTypeON)
    {
        if(!particles)
        {
            switch (soulTypeON)
            {
                case RollWeapon.SoulType.earth:
                    go.GetComponent<SpriteRenderer>().color = menuColors.soulEarth;
                    break;
                case RollWeapon.SoulType.electric:
                    go.GetComponent<SpriteRenderer>().color = menuColors.soulElectric;
                    break;
                case RollWeapon.SoulType.ice:
                    go.GetComponent<SpriteRenderer>().color = menuColors.soulIce;
                    break;
                case RollWeapon.SoulType.fire:
                    go.GetComponent<SpriteRenderer>().color = menuColors.soulFire;
                    break;
                case RollWeapon.SoulType.poisson:
                    go.GetComponent<SpriteRenderer>().color = menuColors.soulPoison;
                    break;
                case RollWeapon.SoulType.dark:
                    go.GetComponent<SpriteRenderer>().color = menuColors.soulDark;
                    break;
                case RollWeapon.SoulType.sweet:
                    go.GetComponent<SpriteRenderer>().color = menuColors.soulSweet;
                    break;
                case RollWeapon.SoulType.mage:
                    go.GetComponent<SpriteRenderer>().color = menuColors.soulMage;
                    break;
                case RollWeapon.SoulType.empty:
                    go.GetComponent<SpriteRenderer>().color = Color.white;
                    break;
            }
        }
        else
        {
            ParticleSystem.MainModule ps = go.GetComponent<ParticleSystem>().main;
            switch (soulType)
            {
                case RollWeapon.SoulType.earth:
                    ps.startColor = menuColors.soulEarth;
                    break;
                case RollWeapon.SoulType.electric:
                    ps.startColor = menuColors.soulElectric;
                    break;
                case RollWeapon.SoulType.ice:
                    ps.startColor = menuColors.soulIce;
                    break;
                case RollWeapon.SoulType.fire:
                    ps.startColor = menuColors.soulFire;
                    break;
                case RollWeapon.SoulType.poisson:
                    ps.startColor = menuColors.soulPoison;
                    break;
                case RollWeapon.SoulType.dark:
                    ps.startColor = menuColors.soulDark;
                    break;
                case RollWeapon.SoulType.sweet:
                    ps.startColor = menuColors.soulSweet;
                    break;
                case RollWeapon.SoulType.mage:
                    ps.startColor = menuColors.soulMage;
                    break;
                case RollWeapon.SoulType.empty:
                    ps.startColor = Color.black;
                    break;
            }
        }
    }
    public IEnumerator OnPickUpSoul()
    {
        PlayerScript changeSoul = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        StartCoroutine(ChangeSoulItem(changeSoul.pSoul));
        GameObject go = Instantiate(chargeParticles, changeSoul.transform.position, Quaternion.identity, changeSoul.transform);
        go.transform.localScale = new Vector2(2, 2);
        ParticleSystem.MainModule goMain = go.GetComponent<ParticleSystem>().main;
        goMain.simulationSpace = ParticleSystemSimulationSpace.World;
        switch (soulType)
        {
            case RollWeapon.SoulType.earth:
                changeSoul.pSoul = PlayerScript.Souls.Earth;
                ChangeColor(go, true, RollWeapon.SoulType.earth);
                break;
            case RollWeapon.SoulType.electric:
                changeSoul.pSoul = PlayerScript.Souls.Electric;
                ChangeColor(go, true, RollWeapon.SoulType.electric);
                break;
            case RollWeapon.SoulType.ice:
                changeSoul.pSoul = PlayerScript.Souls.Ice;
                ChangeColor(go, true, RollWeapon.SoulType.ice);
                break;
            case RollWeapon.SoulType.fire:
                changeSoul.pSoul = PlayerScript.Souls.Fire;
                ChangeColor(go, true, RollWeapon.SoulType.fire);
                break;
            case RollWeapon.SoulType.dark:
                changeSoul.pSoul = PlayerScript.Souls.Dark;
                ChangeColor(go, true, RollWeapon.SoulType.dark);
                break;
            case RollWeapon.SoulType.sweet:
                changeSoul.pSoul = PlayerScript.Souls.Sweet;
                ChangeColor(go, true, RollWeapon.SoulType.sweet);
                break;
            case RollWeapon.SoulType.mage:
                changeSoul.pSoul = PlayerScript.Souls.Mage;
                ChangeColor(go, true, RollWeapon.SoulType.mage);
                break;
            case RollWeapon.SoulType.poisson:
                changeSoul.pSoul = PlayerScript.Souls.Poison;
                ChangeColor(go, true, RollWeapon.SoulType.poisson);
                break;
        }
        yield return new WaitForSeconds(0.7f);
        go.GetComponent<ParticleSystem>().Stop();
        Destroy(go, 2f);
        changeSoul.ChangePlayerSoul();
    }
    private IEnumerator ChangeSoulItem(PlayerScript.Souls ps)
    {
        soulEffect.Stop();
        gameObject.GetComponent<Collider2D>().enabled = false;
        GameObject part1 = Instantiate(destroyFuzzyParticles, transform.position, Quaternion.identity);
        Destroy(part1, 3f);
        ChangeColor(part1, true, soulType);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        yield return new WaitForSeconds(1.5f);
        GameObject go1 = Instantiate(chargeParticles, transform.position, Quaternion.identity, transform);
        go1.transform.localScale = new Vector2(2, 2);
        ParticleSystem.MainModule gopsmm = go1.GetComponent<ParticleSystem>().main;
        gopsmm.startColor = Color.black;
        yield return new WaitForSeconds(0.7f);

        go1.GetComponent<ParticleSystem>().Stop();
        GameObject dest = Instantiate(destroyParticles, transform.position, Quaternion.identity);
        ParticleSystem.MainModule psmm = dest.GetComponent<ParticleSystem>().main;
        psmm.startColor = Color.white;
        GameObject part2 = Instantiate(destroyFuzzyParticles, transform.position, Quaternion.identity);
        ParticleSystem.MainModule part2Main = part2.GetComponent<ParticleSystem>().main;
        part2Main.startColor = Color.black;
        Destroy(go1, 2f);
        Destroy(dest, 3f);
        Destroy(part2, 3f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cursor"))
        {
            switch (soulType)
            {
                case RollWeapon.SoulType.earth:
                    uiMan.OnMouseHitSoul(soulType, gameObject, "Earthen\n");
                    break;
                case RollWeapon.SoulType.electric:
                    uiMan.OnMouseHitSoul(soulType, gameObject, "Electric\n");
                    break;
                case RollWeapon.SoulType.ice:
                    uiMan.OnMouseHitSoul(soulType, gameObject, "Ice\n");
                    break;
                case RollWeapon.SoulType.fire:
                    uiMan.OnMouseHitSoul(soulType, gameObject, "Fiery\n");
                    break;
                case RollWeapon.SoulType.poisson:
                    uiMan.OnMouseHitSoul(soulType, gameObject, "Poisonous\n");
                    break;
                case RollWeapon.SoulType.dark:
                    uiMan.OnMouseHitSoul(soulType, gameObject, "Dark\n");
                    break;
                case RollWeapon.SoulType.sweet:
                    uiMan.OnMouseHitSoul(soulType, gameObject, "Sweet\n");
                    break;
                case RollWeapon.SoulType.mage:
                    uiMan.OnMouseHitSoul(soulType, gameObject, "Mage\n");
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            foreach(GameObject r in GameObject.FindGameObjectsWithTag("soulObj"))
            {
                Destroy(r);
            }
        }
    }
}
