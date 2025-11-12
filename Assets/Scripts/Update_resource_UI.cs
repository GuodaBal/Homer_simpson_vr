using UnityEngine;
using UnityEngine.UI;

public class Update_resource_UI : MonoBehaviour
{
    public FactoryResource factoryResource;
    public Slider displaySlider;
    //public SetGaugeRotation gaugeDisplay;
    //public ProgressBar progressDisplay;
    
    void Update()
    {
        if (displaySlider != null)
            displaySlider.value = factoryResource.GetCurrentValue();
        //gaugeDisplay?.SetValue(factoryResource.GetCurrentValue());
        //progressDisplay?.SetValue(factoryResource.GetCurrentValue());
    }
}
