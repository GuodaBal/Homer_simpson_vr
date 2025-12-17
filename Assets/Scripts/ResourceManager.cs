using UnityEngine;
using TMPro;

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
    public FactoryResource water;

    public PipeManager pipe_manager;
    public WaterManger water_manager;

    public GameOver gameOverScreen;
    public GameObject gameWinScreen;
    public LevelCleared levelClearedScreen;

    [SerializeField]
    public TextMeshProUGUI time_remaining_text;



    public float level_duration = 60.0f;

    public float speed = 1.0f;

    [SerializeField]
    AudioClip[] audioClips;

    private Transform transform;
    [SerializeField]
    private Transform outsideTransform;

    [SerializeField]
    private Transform pipeTransform;

    bool game_over_triggered = false;
    bool is_lockdown = false;
    bool lockdown_used = false;

    // Update is called once per frame
    void Update()
    {
        
        //Decreasing timer
        if (!game_over_triggered && level_duration > 0)
        {
            level_duration -= Time.deltaTime;
            time_remaining_text.text = "Time left: " + Mathf.CeilToInt(level_duration).ToString() + "s";

            if (is_lockdown)
            {
                electricity.SetIncreaseSpeed(0.0f);
                if (temperature)
                    temperature.SetIncreaseSpeed(0.0f);
                if (steam)
                    steam.SetIncreaseSpeed(0.0f);
                if (pressure)
                    pressure.SetIncreaseSpeed(0.0f);
                if (trash)
                    trash.SetIncreaseSpeed(0.0f);
                return;
            }
            if (pipe_manager && pipe_manager.are_pipes_dropped)
            {
                water.SetCurrentValue(0);
            }
            else if (water)
            {
                water.SetCurrentValue(water_manager.GetTotalWaterFlow());

            }
            electricity.SetIncreaseSpeed(GetElectricityIncrease() * speed);
            if (temperature)
                temperature.SetIncreaseSpeed(GetTemperatureIncrease() * speed);
            if (steam)
                steam.SetIncreaseSpeed(GetSteamIncrease() * speed);
            if (pressure)
                pressure.SetIncreaseSpeed(GetPressureIncrease() * speed);
            if (trash)
                trash.SetIncreaseSpeed(GetTrashIncrease() * speed);
            

            if (pipe_manager && Random.Range(0, 100000) <=  water.GetCurrentValue())
            {
                Debug.Log("Dropping Pipes");
                if (pipe_manager.are_pipes_dropped == false)
                {
                    pipe_manager.DropPipes();
                    AudioManager.instance.PlaySoundEffect(audioClips[8], pipeTransform, 1f, 1f);
                }
            }
            //Game win check
            if (level_duration <= 0)
            {
                AudioManager.instance.PlaySoundEffect(audioClips[4], transform, 1f, 1f);
                gameWinScreen.SetActive(true);
                levelClearedScreen.TriggerWin();
            }
            //Game over checks
            if (temperature && temperature.GetCurrentValue() >= temperature.max_value)
            {
                AudioManager.instance.PlaySoundEffect(audioClips[5], gameWinScreen.transform, 0.7f, 1f);
                gameOverScreen.TriggerGameOver("Reactor Overheated!");
                levelClearedScreen.TriggerGameOver();
                game_over_triggered = true;
            }
            else if (pressure && pressure.GetCurrentValue() >= pressure.max_value)
            {
                AudioManager.instance.PlaySoundEffect(audioClips[5], gameWinScreen.transform, 0.7f, 1f);
                gameOverScreen.TriggerGameOver("Reactor Exploded from Excessive Pressure!");
                levelClearedScreen.TriggerGameOver();
                game_over_triggered = true;
            }
            else if (trash && trash.GetCurrentValue() >= trash.max_value)
            {
                AudioManager.instance.PlaySoundEffect(audioClips[5], gameWinScreen.transform, 0.7f, 1f);
                gameOverScreen.TriggerGameOver("Factory Shut Down due to Excessive Trash!");
                levelClearedScreen.TriggerGameOver();
                game_over_triggered = true;
            }
            else if (electricity.GetCurrentValue() <= 0)
            {
                AudioManager.instance.PlaySoundEffect(audioClips[5], gameWinScreen.transform, 0.7f, 1f);
                gameOverScreen.TriggerGameOver("Factory Shut Down due to not meeting Demand for Electricity!");
                levelClearedScreen.TriggerGameOver();
                game_over_triggered = true;
            }
        }
        //Game win check
        if (!game_over_triggered && level_duration <= 0)
        {

            gameWinScreen.SetActive(true);
            electricity.SetIncreaseSpeed(0.0f);
            if (temperature)
                temperature.SetIncreaseSpeed(0.0f);
            if (steam)
                steam.SetIncreaseSpeed(0.0f);
            if (pressure)
                pressure.SetIncreaseSpeed(0.0f);
            if (trash)
                trash.SetIncreaseSpeed(0.0f);
        }
        if (game_over_triggered)
        {

            electricity.SetIncreaseSpeed(0.0f);
            if (temperature)
                temperature.SetIncreaseSpeed(0.0f);
            if (steam)
                steam.SetIncreaseSpeed(0.0f);
            if (pressure)
                pressure.SetIncreaseSpeed(0.0f);
            if (trash)
                trash.SetIncreaseSpeed(0.0f);
        }

     }
    public void SetTransformTo(Transform tr)
    {
        transform = tr;
    }
    public void AddCoalToReactor(float amount)
    {
        if (coal.GetCurrentValue() < amount) amount = coal.GetCurrentValue();
        coal_in_reactor.SetCurrentValue(coal_in_reactor.GetCurrentValue() + amount);
        coal.DecreaseResource(amount);
        AudioManager.instance.PlaySoundEffect(audioClips[0], transform, 1f, 1f);
        
        AudioManager.instance.PlaySoundEffect(audioClips[1], outsideTransform, 1f, Random.Range(0.9f, 1.1f));

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
        AudioManager.instance.PlaySoundEffect(audioClips[0], transform, 1f, 1f);
        AudioManager.instance.PlaySoundEffect(audioClips[6], outsideTransform, 1f, Random.Range(0.9f, 1.1f));
    }
    public void ReleasePressure(float amount)
    {
        pressure.SetCurrentValue(pressure.GetCurrentValue() - amount);
        Debug.Log("Releasing pressure");
    }
    public void LockdownEverything(float timer)
    {
        if (lockdown_used) return;
        is_lockdown = true;
        lockdown_used = true;
        Invoke("EndLockdown", timer);
    }
    public void EndLockdown()
    {
        is_lockdown = false;
        Debug.Log("Lockdown ended.");
    }


    //Resource calcluation functions
    float GetElectricityIncrease()
    {
        float increase = coal_in_reactor.GetCurrentValue() * 5.0f - electricity_demand.GetCurrentValue() * 0.7f;
        if (steam)
        {
            increase += steam.GetCurrentValue() * 0.5f;
        }
        if (electricity_consumption)
        {
            increase -= electricity_consumption.GetCurrentValue() * 0.45f;
        }
        if (water)
        {
            increase -= water.GetCurrentValue() * 0.2f;
        }
        return increase;
    }
    float GetTemperatureIncrease()
    {
        float increase = coal_in_reactor.GetCurrentValue() * 0.7f - cooling_power.GetCurrentValue() * 0.2f;
        if (steam)
        {
            increase += steam.GetCurrentValue() * 0.2f;
        }
        if (water)
        {
            increase -= water.GetCurrentValue() * 0.5f;
        }
        return increase;
    }
    float GetSteamIncrease()
    {
        float increase = coal_in_reactor.GetCurrentValue() * 0.2f - 3.0f;
        return increase;
    }
    float GetPressureIncrease()
    {
        float increase = steam.GetCurrentValue() * 0.03f;
        return increase;
    }
    float GetTrashIncrease()
    {
        float increase = coal_in_reactor.GetCurrentValue() * 0.15f;
        return increase;
    }

}
