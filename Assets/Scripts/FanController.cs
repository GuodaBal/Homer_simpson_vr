using UnityEngine;
using Unity.XRContent.Interaction;

public class FanController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;

    public void ControllerValueChanged(float value) {
        float value1 = slider1.GetComponent<XRSlider>().Value;
        float value2 = slider2.GetComponent<XRSlider>().Value;
        float value3 = slider3.GetComponent<XRSlider>().Value;
        resourceManager.FanValueChanged(value1, value2, value3);
    }
}
