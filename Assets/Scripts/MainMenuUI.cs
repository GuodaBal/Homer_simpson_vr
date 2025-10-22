using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SettingsUI;

    public GameObject SettingsUI
    {
        get => m_SettingsUI;
        set => m_SettingsUI = value;
    }
    public void OpenSettings()
    {
        Debug.Log("Settings Menu Opened");
        SettingsUI.SetActive(true);
    }
}
