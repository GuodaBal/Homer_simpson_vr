using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class OnlyOnePressedInGroup : MonoBehaviour
{
    public GameObject[] buttonGroup;
    public Renderer[] buttonVisualsGroup;
    public Material material_off;
    public Material material_on;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject button in buttonGroup)
        {
            button.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(delegate { ButtonPressed(button); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ButtonPressed(GameObject selectedButton)
    {
        for (int i = 0; i < buttonGroup.Length; i++)
        {
            if (buttonGroup[i] != selectedButton)
            {
                buttonVisualsGroup[i].material = material_off;
                //button.GetComponent<XRPokeFollowAffordance>().returnToInitialPosition = true;
            }
            else
            {
                buttonVisualsGroup[i].material = material_on;
                //button.GetComponent<XRPokeFollowAffordance>().returnToInitialPosition = false;
            }
        }
    }
}
