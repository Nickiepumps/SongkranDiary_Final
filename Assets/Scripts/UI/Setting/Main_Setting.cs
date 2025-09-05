using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Main_Setting : MonoBehaviour
{
    [Header("Main menu")]
    [SerializeField] private Main_Menu mainMenu; // Use only in Main menu
    [SerializeField] private SettingController isoSettingController; // Use only in iso mode
    [Header("Button")]
    [SerializeField] private GameObject confirmBtn;
    [Header("Sounds")]
    [SerializeField] private AudioSource setting_BGMAudioSouce;
    [SerializeField] private AudioSource setting_SFXAudioSouce;
    [Header("Slider")]
    [SerializeField] private Slider setting_MasterSlider;
    [SerializeField] private Slider setting_BGMSlider;
    [SerializeField] private Slider setting_SFXSlider;
    [SerializeField] private TMP_Text setting_MasterValue;
    [SerializeField] private TMP_Text setting_BGMValue;
    [SerializeField] private TMP_Text setting_SFXValue;

    private float setting_OriginalMasterVolume;
    private float setting_OriginalBGMVolume;
    private float setting_OriginalSFXVolume;
    private void OnEnable()
    {
        // Load all setting from JSON
        SettingData soundData = SettingHandler.instance.LoadSettingData();
        if (soundData != null)
        {
            setting_OriginalMasterVolume = soundData.masterVolume;
            setting_OriginalBGMVolume = soundData.bgmVolume;
            setting_MasterSlider.value = soundData.masterVolume;
            setting_BGMSlider.value = soundData.bgmVolume;
            setting_SFXSlider.value = soundData.sfxVolume;
            //setting_OriginalSFXVolume = settingData.sfxVolume;
        }
        else
        {
            setting_OriginalMasterVolume = 1;
            setting_OriginalBGMVolume = 1;
            //setting_OriginalSFXVolume = 1;
        }
        // Master
        MasterSetting();
        // BGM
        BGMSetting();
        // SFX
        //SFXSetting();
        confirmBtn.GetComponent<Button>().onClick.AddListener(() => ConfirmSoundSetting());
        if(mainMenu != null)
        {
            mainMenu.TabHighlight();
        }
        else
        {
            isoSettingController.TabHighlight();
        }
    }
    private void OnDisable()
    {
        // Reset all setting to the original value if player doesn't confirm the setting
        setting_MasterSlider.value = setting_OriginalMasterVolume;
        setting_BGMSlider.value = setting_OriginalBGMVolume;
        setting_SFXSlider.value = setting_OriginalSFXVolume;
        confirmBtn.GetComponent<Button>().onClick.RemoveListener(() => ConfirmSoundSetting());
        confirmBtn.SetActive(false);

        // Master
        MasterSetting();
        // BGM
        BGMSetting();
        // SFX
        //SFXSetting();
    }
    public void MasterSetting()
    {
        setting_MasterValue.text = Convert.ToInt32(setting_MasterSlider.value * 100).ToString();
        if (setting_MasterSlider.value >= setting_BGMSlider.value)
        {
            setting_BGMAudioSouce.volume = setting_BGMSlider.value;
        }
        else
        {
            setting_BGMAudioSouce.volume = setting_MasterSlider.value;
        }
        if (setting_MasterSlider.value >= setting_SFXSlider.value)
        {
            //sfxAudioSouce.volume = sfxSlider.value;
        }
        else
        {
            //sfxAudioSouce.volume = masterSlider.value;
        }
        if (setting_MasterSlider.value != setting_OriginalMasterVolume)
        {
            confirmBtn.SetActive(true);
        }
    }
    public void BGMSetting()
    {
        if (setting_MasterSlider.value >= setting_BGMSlider.value)
        {
            setting_BGMAudioSouce.volume = setting_BGMSlider.value;
        }
        else
        {
            setting_BGMAudioSouce.volume = setting_MasterSlider.value;
        }
        if (setting_BGMSlider.value != setting_OriginalBGMVolume)
        {
            confirmBtn.SetActive(true);
        }
        setting_BGMValue.text = Convert.ToInt32(setting_BGMSlider.value * 100).ToString();
    }
    public void SFXSetting()
    {
        if (setting_MasterSlider.value >= setting_SFXSlider.value)
        {
            //sfxAudioSouce.volume = sfxSlider.value;
        }
        else
        {
            //sfxAudioSouce.volume = masterSlider.value;
        }
        if (setting_SFXSlider.value != setting_OriginalSFXVolume)
        {
            confirmBtn.SetActive(true);
        }
        setting_SFXValue.text = Convert.ToInt32(setting_SFXSlider.value * 100).ToString();
    }
    public void ConfirmSoundSetting()
    {
        SettingHandler.instance.SaveSetting_Sound(setting_MasterSlider.value, setting_BGMSlider.value, setting_SFXSlider.value);

        setting_OriginalMasterVolume = setting_MasterSlider.value;
        setting_OriginalBGMVolume = setting_BGMSlider.value;
        setting_OriginalSFXVolume = setting_SFXSlider.value;
        confirmBtn.SetActive(false);
    }
}
