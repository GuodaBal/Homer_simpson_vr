using UnityEngine;
using TMPro;

public class Level_4 : MonoBehaviour
{
    public FactoryResource electricity;
    public FactoryResource electricity_demand;
    public FactoryResource temperature;
    public FactoryResource coal;
    public FactoryResource trash;
    public FactoryResource electricity_consumption;
    public FactoryResource cooling_power;
    public FactoryResource coal_in_reactor;
    public FactoryResource water;

    public WaterManger water_manager;

    public GameOver gameOverScreen;
    public GameObject gameWinScreen;

    [SerializeField]
    public TextMeshProUGUI time_remaining_text;

    public float level_duration = 60.0f;

    public float speed = 10.0f;
    bool game_over_triggered = false;

    // Update is called once per frame
    void Update()
    {

        //Decreasing timer
        if (!game_over_triggered && level_duration > 0)
        {
            level_duration -= Time.deltaTime;
            time_remaining_text.text = "Time left: " + Mathf.CeilToInt(level_duration).ToString() + "s";

            water.SetCurrentValue(water_manager.GetTotalWaterFlow());
            electricity.SetIncreaseSpeed((coal_in_reactor.GetCurrentValue() * 3.0f - electricity_consumption.GetCurrentValue() * 0.5f - water.GetCurrentValue() * 0.2f - electricity_demand.GetCurrentValue() * 0.7f) / speed);
            temperature.SetIncreaseSpeed((coal_in_reactor.GetCurrentValue() * 2.5f - cooling_power.GetCurrentValue() - water.GetCurrentValue() * 3.0f) / speed);
            trash.SetIncreaseSpeed((coal_in_reactor.GetCurrentValue() * 1.2f) / speed);

            //Game win check
            if (level_duration <= 0)
            {
                gameWinScreen.SetActive(true);
            }
            //Game over checks
            if (temperature.GetCurrentValue() >= temperature.max_value)
            {
                gameOverScreen.TriggerGameOver("Reactor Overheated!");
                game_over_triggered = true;
            }
            else if (trash.GetCurrentValue() >= trash.max_value)
            {
                gameOverScreen.TriggerGameOver("Factory Shut Down due to Excessive Trash!");
                game_over_triggered = true;
            }
            else if (electricity.GetCurrentValue() <= 0)
            {
                gameOverScreen.TriggerGameOver("Factory Shut Down due to not meeting Demand for Electricity!");
                game_over_triggered = true;
            }
        }
        //Game win check
        if (!game_over_triggered && level_duration <= 0)
        {
            gameWinScreen.SetActive(true);
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
        float cooling = (fan_1 + fan_2 + fan_3) / 3 * 100;
        cooling_power.SetCurrentValue(cooling);
        electricity_consumption.SetCurrentValue(cooling * 1.5f);
    }
    public void DumpTrash()
    {
        trash.SetCurrentValue(0);
    }
}
