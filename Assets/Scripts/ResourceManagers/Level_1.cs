using UnityEngine;

public class Level_1 : MonoBehaviour
{
    public FactoryResource electricity;
    public FactoryResource electricity_demand;
    public FactoryResource coal;
    public FactoryResource coal_in_reactor;

    public GameOver gameOverScreen;
    public GameObject gameWinScreen;

    public float level_duration = 60.0f;

    bool game_over_triggered = false;

    void Start()
    {
        gameWinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        electricity.SetIncreaseSpeed(coal_in_reactor.GetCurrentValue() * 3.0f - 2.0f);
        //Decreasing timer
        if (!game_over_triggered && level_duration > 0)
        {
            level_duration -= Time.deltaTime;
            //Game win check
            if (level_duration <= 0)
            {
                gameWinScreen.SetActive(true);
            }
            //Game over checks
            if (electricity.GetCurrentValue() <= electricity_demand.GetCurrentValue() - 10)
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
}
