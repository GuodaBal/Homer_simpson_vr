using UnityEngine;

public class FactoryResource : MonoBehaviour
{
    public string resourceName;
    public float increase_speed;
    public float max_value;
    public float starting_value;
    private float current_value;

    void Start()
    {
        current_value = starting_value;
    }

    void Update()
    {
        current_value += increase_speed * Time.deltaTime;
    }

    public float GetCurrentValue()
    {
        return current_value;
    }

   public void SetCurrentValue(float new_value)
    {
        current_value = new_value;
        if (current_value > max_value)
        {
            current_value = max_value;
        }
        if (current_value < 0)
        {
            current_value = 0;
        }
    }

    public void SetIncreaseSpeed(float new_speed)
    {
        increase_speed = new_speed;
    }

    public void IncreaseResource(float amount)
    {
        current_value += amount;
        if (current_value > max_value)
        {
            current_value = max_value;
        }
    }
    public void DecreaseResource(float amount)
    {
        current_value -= amount;
        if (current_value < 0)
        {
            current_value = 0;
        }
    }

}
