using UnityEngine;
using TMPro;

public class Level_1 : MonoBehaviour
{
    public FactoryResource electricity;
    public FactoryResource electricity_demand;
    public FactoryResource coal;
    public FactoryResource coal_in_reactor;

    public GameOver game_over_screen;
    public GameObject game_win_screen;
    [SerializeField]
    public TextMeshProUGUI time_remaining_text;

    public float level_duration = 60.0f;

    bool game_over_triggered = false;

    void Start()
    {
        game_win_screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Decreasing timer
        if (!game_over_triggered && level_duration > 0)
        {
            level_duration -= Time.deltaTime;
            time_remaining_text.text = "Time left: " + Mathf.CeilToInt(level_duration).ToString() + "s";
            electricity.SetIncreaseSpeed(coal_in_reactor.GetCurrentValue() * 2.0f - 2.0f);
            //Game win check
            if (level_duration <= 0)
            {
                game_win_screen.SetActive(true);
            }
            //Game over checks
            if (electricity.GetCurrentValue() <= electricity_demand.GetCurrentValue() - 10)
            {
                game_over_screen.TriggerGameOver("Factory Shut Down due to not meeting Demand for Electricity!");
                game_over_triggered = true;
            }
        }
    }

    public void AddCoalToReactor(float amount)
    {
        if (coal.GetCurrentValue() < amount) amount = coal.GetCurrentValue();
        coal_in_reactor.SetCurrentValue(coal_in_reactor.GetCurrentValue() + amount);
        coal.DecreaseResource(amount);
    }
}
