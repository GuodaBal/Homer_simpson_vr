using UnityEngine;

public class SetGaugeRotation : MonoBehaviour
{
    public float minRotation = -90;
    public float maxRotation = 90;
    public float gaugeValue = 0;
    public float minGaugeValue = 0;
    public float maxGaugeValue = 100;

    public Transform rotationObject;
    public FactoryResource resource;

    void Update()
    {
        if (resource != null)
        {
            gaugeValue = Mathf.Clamp(resource.GetCurrentValue(), minGaugeValue, maxGaugeValue);
            float normalizedValue = (gaugeValue - minGaugeValue) / (maxGaugeValue - minGaugeValue);
            float targetRotation = Mathf.Lerp(minRotation, maxRotation, normalizedValue);
            rotationObject.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, targetRotation);
        }
    }

    public void SetValue(float value)
    {
        Debug.Log("Setting gauge value to: " + value);
        gaugeValue = Mathf.Clamp(value, minGaugeValue, maxGaugeValue);
        UpdateGaugeRotation();
    }

    public void UpdateGaugeRotation()
    {
        float normalizedValue = (gaugeValue - minGaugeValue) / (maxGaugeValue - minGaugeValue);
        float targetRotation = Mathf.Lerp(minRotation, maxRotation, normalizedValue);
        rotationObject.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, targetRotation);
        Debug.Log("Gauge rotation set to: " + targetRotation);
    }
}
