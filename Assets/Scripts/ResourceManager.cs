using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public FactoryResource electricity;
    public FactoryResource electricity_demand;
    public FactoryResource temperature;
    public FactoryResource coal;
    public FactoryResource trash;
    public FactoryResource steam;
    public FactoryResource pressure;
    public FactoryResource electricity_consumption;
    public FactoryResource cooling_power;

    private float coal_in_reactor = 0;

    // Update is called once per frame
    void Update()
    {
        electricity.SetCurrentValue(coal_in_reactor * 3.0f + steam.GetCurrentValue() - electricity_consumption.GetCurrentValue());
        temperature.SetCurrentValue(coal_in_reactor * 2.0f - cooling_power.GetCurrentValue());
        steam.SetIncreaseSpeed(coal_in_reactor * 1.5f);
        pressure.SetIncreaseSpeed(steam.GetCurrentValue() * 0.2f);
        trash.SetIncreaseSpeed(coal_in_reactor * 0.1f);
    }

    public void AddCoalToReactor(float amount)
    {
        coal_in_reactor += amount; 
        coal.DecreaseResource(amount);
    }
    public void FanValueChanged(float fan_1, float fan_2, float fan_3)
    {
        float cooling = (fan_1 + fan_2 + fan_3)/3 * 100;
        cooling_power.SetCurrentValue(cooling);
        electricity_consumption.SetCurrentValue(cooling * 1.5f);
    }
}
