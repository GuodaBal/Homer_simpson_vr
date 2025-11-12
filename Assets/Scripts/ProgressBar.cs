using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fill;
    public float minValue = 0f;
    public float maxValue = 100f;

    public FactoryResource resource;

    void Update()
    {
        float normalizedValue = (resource.GetCurrentValue() - minValue) / (maxValue - minValue);
        fill.fillAmount = normalizedValue;
    }

    public void SetValue(float progress)
    {
        Debug.Log("Setting progress bar value to: " + progress);
        float normalizedValue = (progress - minValue) / (maxValue - minValue);
        fill.fillAmount = normalizedValue;
        Debug.Log("Progress bar fill amount set to: " + normalizedValue);
    }
}
