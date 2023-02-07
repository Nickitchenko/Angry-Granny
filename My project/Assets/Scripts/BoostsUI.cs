using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostsUI : MonoBehaviour
{
    private float timeCurrent;
    public float timeMax = 0;
    public bool isActive = false;
    public bool isNow = false;

    public Image coolldownImage;

    private void FixedUpdate()
    {
        if(isNow)
        {
            timeMax *= 50;
            timeCurrent = timeMax;
            isNow = false;
        }
        if(timeCurrent>0 && timeCurrent<=timeMax)
        {
            timeCurrent--;
        }
        else
        {
            isActive = false;
        }
    }

    private void Update()
    {
        if (isActive)
        {
            coolldownImage.fillAmount = timeCurrent / timeMax;
        }
        else
        {
            coolldownImage.fillAmount = 0;
        }
    }
}
