using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SettingsUIRoot;

    public GameObject SettingsUIRoot
    {
        get => m_SettingsUIRoot;
        set => m_SettingsUIRoot = value;
    }

    [SerializeField]
    private GameObject m_AudioSrc;

    public GameObject AudioSrc
    {
        get => m_AudioSrc;
        set => m_AudioSrc = value;
    }

    [SerializeField]
    private Slider m_VolumeSlider;


    public Slider VolumeSlider
    {
        get => m_VolumeSlider;
        set => m_VolumeSlider = value;
    }

    [SerializeField]
    private Toggle m_MuteButton;


    public Toggle MuteButton
    {
        get => m_MuteButton;
        set => m_MuteButton = value;
    }

    public void CloseWindow()
    {
        Debug.Log("Settings Menu Opened");
        SettingsUIRoot.SetActive(false);
    }

    public void ChangeVolume()
    {
        AudioSrc.GetComponent<AudioSource>().volume = m_VolumeSlider.value;
    }

    public void MuteVolume()
    {
        AudioSrc.GetComponent<AudioSource>().mute = m_MuteButton.isOn;
    }
}
