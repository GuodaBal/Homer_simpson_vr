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
    public FactoryResource coal_in_reactor;

    public GameOver gameOverScreen;

    float speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        electricity.SetIncreaseSpeed((coal_in_reactor.GetCurrentValue() * 3.0f + steam.GetCurrentValue() * 0.5f - electricity_consumption.GetCurrentValue() - 5) / speed);
        temperature.SetIncreaseSpeed((coal_in_reactor.GetCurrentValue() * 1.5f + steam.GetCurrentValue() * 0.25f - cooling_power.GetCurrentValue()) / speed);
        steam.SetIncreaseSpeed((coal_in_reactor.GetCurrentValue() * 1.5f - 3.5f) / speed);
        pressure.SetIncreaseSpeed((steam.GetCurrentValue() * 0.03f) / speed);
        trash.SetIncreaseSpeed((coal_in_reactor.GetCurrentValue() * 0.6f) / speed);

        //Game over checks
        if (temperature.GetCurrentValue() >= temperature.max_value)
        {
            gameOverScreen.TriggerGameOver("Reactor Overheated!");
        }
        else if (pressure.GetCurrentValue() >= pressure.max_value)
        {
            gameOverScreen.TriggerGameOver("Reactor Exploded from Excessive Pressure!");
        }
        else if (trash.GetCurrentValue() >= trash.max_value)
        {
            gameOverScreen.TriggerGameOver("Factory Shut Down due to Excessive Trash!");
        }
    }

    public void AddCoalToReactor(float amount)
    {
        if (coal.GetCurrentValue() < amount) amount = coal.GetCurrentValue();
        coal_in_reactor.SetCurrentValue(coal_in_reactor.GetCurrentValue() + amount); 
        coal.DecreaseResource(amount);
    }
    public void FanValueChanged(float fan_1, float fan_2, float fan_3)
    {
        float cooling = (fan_1 + fan_2 + fan_3)/3 * 100;
        cooling_power.SetCurrentValue(cooling);
        electricity_consumption.SetCurrentValue(cooling * 1.5f);
    }
    public void DumpTrash()
    {
        trash.SetCurrentValue(0);
    }
    public void ReleasePressure(float amount)
    {
        pressure.SetCurrentValue(pressure.GetCurrentValue() - amount);
        Debug.Log("Releasing pressure");
    }
}
