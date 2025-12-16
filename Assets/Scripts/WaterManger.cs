using UnityEngine;

public class WaterManger : MonoBehaviour
{
    float pump_1_speed = 30f;
    float pump_2_speed = 30f;
    float pump_3_speed = 30f;

    [SerializeField]
    AudioClip audio;

    [SerializeField]
    Transform trans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetPumpSpeed(string args)
    {
        string[] split_args = args.Split(',');
        int pump_number = int.Parse(split_args[0]);
        float speed = float.Parse(split_args[1]);
        if (pump_number == 1)
        {
            pump_1_speed = speed;
        }
        else if (pump_number == 2)
        {
            pump_2_speed = speed;
        }
        else if (pump_number == 3)
        {
            pump_3_speed = speed;
        }
        Debug.Log("Pump Speeds: " + pump_1_speed + ", " + pump_2_speed + ", " + pump_3_speed);
        AudioManager.instance.PlaySoundEffect(audio, trans, 1f, Random.RandomRange(0.8f, 1.2f));
    }

    public float GetTotalWaterFlow()
    {
        return (pump_1_speed + pump_2_speed + pump_3_speed)/3.0f;
    }
}
