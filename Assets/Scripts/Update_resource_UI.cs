using UnityEngine;
using UnityEngine.UI;

public class Update_resource_UI : MonoBehaviour
{
    public FactoryResource factoryResource;
    public Slider displaySlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displaySlider.value = factoryResource.GetCurrentValue();
    }
}
