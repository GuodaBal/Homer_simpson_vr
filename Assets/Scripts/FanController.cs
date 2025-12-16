using UnityEngine;
using Unity.XRContent.Interaction;

public class FanController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;
    public AudioSource fan1;
    public AudioSource fan2;
    public AudioSource fan3;

    public void ControllerValueChanged(float value) {
        float value1 = slider1.GetComponent<XRSlider>().Value;
        fan1.volume = value1;
        float value2 = slider2.GetComponent<XRSlider>().Value;
        fan2.volume = value2;
        float value3 = slider3.GetComponent<XRSlider>().Value;
        fan3.volume = value3;
        resourceManager.FanValueChanged(value1, value2, value3);
    }
}
