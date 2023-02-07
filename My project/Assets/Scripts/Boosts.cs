using System.Collections;
using UnityEngine;

public class Boosts : MonoBehaviour
{
    [Header("Stats")]
    public string boosterName;
    public float boosterTime;
    public float value;
    public GameObject effetsList;

    [SerializeField] private GameObject GFX;

    private GameObject target;
    PlayerController pc;

    //Select target who can take booster
    private void Start()
    {
        target = GameObject.Find("Player");
        pc = target.GetComponent<PlayerController>();
    }

    //Take booster
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject==target)
        {
            Debug.Log(boosterName);
            BoosterCheck();
        }
    }

    //What boooster take
    private void BoosterCheck()
    {
        switch(boosterName)
        {
            case "BoosterRegeneration": BoosterRegeneration(); Destroy(gameObject); break;
            case "BoosterHealthPlus": BoosterHealthPlus(); Destroy(gameObject); break;
            case "BoosterShield": BoosterShield(); break;
            case "BoosterRun": BoosterRun();  break;
            default: break;
        }
    }

    //BoosterRun
    private void BoosterRun()
    {
        if(pc.moveSpeedCurrent!=pc.moveSpeedStart+value)
        {
            CheckUIForBooster("BoosterRun");
            //GameObject obj = GameObject.FindGameObjectWithTag("BoosterRunUI");
            //obj.GetComponent<BoostsUI>().isActive = true;
            //obj.GetComponent<BoostsUI>().timeMax = boosterTime;
            //obj.GetComponent<BoostsUI>().isNow = true;
            pc.moveSpeedCurrent += value;
            GFX.SetActive(false);
            StartCoroutine(NormalSpeed());
        }
    }

    IEnumerator NormalSpeed()
    {
        yield return new WaitForSeconds(boosterTime);
        pc.moveSpeedCurrent = pc.moveSpeedStart;
        Destroy(gameObject);
    }

    //BoosterRegeneration
    private void BoosterRegeneration()
    {
        if(pc.healthCurrent+value>=pc.healthMax)
        {
            pc.healthCurrent = pc.healthMax;
            pc.ChangeHP();
        }
        else
        {
            pc.healthCurrent += value;
            pc.ChangeHP();
        }
    }

    //BoosterShield
    private void BoosterShield()
    {
        CheckUIForBooster("BoosterShield");
        pc.isCanTakeDamage = false;
        GFX.SetActive(false);
        StartCoroutine(ChangeCanTakeDamage());
    }

    IEnumerator ChangeCanTakeDamage()
    {
        yield return new WaitForSeconds(boosterTime);
        pc.isCanTakeDamage = true;
        Destroy(gameObject);
    }

    //Booster plus HP
    private void BoosterHealthPlus()
    {
        pc.healthCurrent +=value;
        pc.healthMax += value;
        pc.ChangeHP();
    }

    private void CheckUIForBooster(string uiName)
    {
        GameObject obj = GameObject.FindGameObjectWithTag(uiName);
        obj.GetComponent<BoostsUI>().isActive = true;
        obj.GetComponent<BoostsUI>().timeMax = boosterTime;
        obj.GetComponent<BoostsUI>().isNow = true;
    }

}
