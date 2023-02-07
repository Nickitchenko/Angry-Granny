using UnityEngine.UI;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public float progress;

    public void Start()
    {
        healthSlider.value = healthSlider.maxValue;
    }

    public void UpdateProgress(float newProgress)
    {
        healthSlider.value = newProgress;
    }

}
