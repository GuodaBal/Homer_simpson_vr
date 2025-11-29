using UnityEngine;
using TMPro;

public class Level_2 : MonoBehaviour
{
    public FactoryResource electricity;
    public FactoryResource electricity_demand;
    public FactoryResource coal;
    public FactoryResource trash;
    public FactoryResource coal_in_reactor;

    public GameOver gameOverScreen;
    public GameObject gameWinScreen;

    [SerializeField]
    public TextMeshProUGUI time_remaining_text;

    public float level_duration = 60.0f;

    bool game_over_triggered = false;

    // Update is called once per frame
    void Update()
    {
        //Decreasing timer
        if (!game_over_triggered && level_duration > 0)
        {
            level_duration -= Time.deltaTime;
            time_remaining_text.text = "Time left: " + Mathf.CeilToInt(level_duration).ToString() + "s";

            electricity.SetIncreaseSpeed(coal_in_reactor.GetCurrentValue() * 2.0f - electricity_demand.GetCurrentValue() * 0.5f);
            trash.SetIncreaseSpeed(coal_in_reactor.GetCurrentValue() * 1.2f);

            //Game win check
            if (level_duration <= 0)
            {
                gameWinScreen.SetActive(true);
            }
            //Game over checks
            if (trash.GetCurrentValue() >= trash.max_value)
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
    public void DumpTrash()
    {
        trash.SetCurrentValue(0);
    }
}
