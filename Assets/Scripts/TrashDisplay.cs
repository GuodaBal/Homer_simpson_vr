using UnityEngine;
using UnityEngine.UI;

public class TrashDisplay : MonoBehaviour
{
    public Image stage0;
    public Image stage1;
    public Image stage2;
    public Image stage3;

    public FactoryResource resource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stage0.enabled = true;
        stage1.enabled = false;
        stage2.enabled = false;
        stage3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float value = resource.GetCurrentValue();
        if (value < resource.max_value / 4)
        {
            stage0.enabled = true;
            stage1.enabled = false;
            stage2.enabled = false;
            stage3.enabled = false;
        }
        else if (value < resource.max_value / 2)
        {
            stage0.enabled = false;
            stage1.enabled = true;
            stage2.enabled = false;
            stage3.enabled = false;
        }
        else if (value < 3 * resource.max_value / 4)
        {
            stage0.enabled = false;
            stage1.enabled = false;
            stage2.enabled = true;
            stage3.enabled = false;
        }
        else
        {
            stage0.enabled = false;
            stage1.enabled = false;
            stage2.enabled = false;
            stage3.enabled = true;
        }
    }
}
