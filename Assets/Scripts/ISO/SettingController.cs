using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    [Header("Audio Slider Components")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Player state controller")]
    [SerializeField] private PlayerStateController playerStateController;
    [SerializeField] private PlayerSideScrollStateController playerSideScrollStateController;

    [Header("Windows")]
    [SerializeField] private GameObject soundWindow;
    [SerializeField] private GameObject keymapWindow;

    [Header("Keymap dropdown")]
    [SerializeField] private TMP_Dropdown keymapDropdown;
    [SerializeField] private GameObject confirmBtn;

    [Header("Keymap properties")]
    [SerializeField] private KeyMapSO[] keymapArr;

    [Header("Setting tabs")]
    [SerializeField] private Sprite[] tabImageArr;
    [SerializeField] private Image[] tabImageComponentArr;

    private void Start()
    {
        LoadSetting();
    }
    private void LoadSetting()
    {
        SettingData settingData = SettingHandler.instance.LoadSettingData();
        if (settingData != null)
        {
            masterSlider.value = settingData.masterVolume;
            bgmSlider.value = settingData.bgmVolume;
            sfxSlider.value = settingData.sfxVolume;

            keymapDropdown.value = settingData.keymapIndex;
            confirmBtn.SetActive(false);
            if (playerStateController != null)
            {
                playerStateController.keymapSO = keymapArr[settingData.keymapIndex];
            }
            else if (playerSideScrollStateController != null)
            {
                playerSideScrollStateController.keymapSO = keymapArr[settingData.keymapIndex];
            }
        }
        else
        {
            if (playerSideScrollStateController != null)
            {
                playerSideScrollStateController.keymapSO = keymapArr[0];
            }
            else if (playerStateController != null)
            {
                playerStateController.keymapSO = keymapArr[0];
            }
        }
    }
    public void OpenControllSetting()
    {
        soundWindow.SetActive(false);
        keymapWindow.SetActive(true);
    }
    public void OpenSoundSetting()
    {
        soundWindow.SetActive(true);
        keymapWindow.SetActive(false);
    }
    public void TabHighlight()
    {
        if (soundWindow.activeSelf == true)
        {
            tabImageComponentArr[0].sprite = tabImageArr[0];
            tabImageComponentArr[1].sprite = tabImageArr[3];
        }
        else if (keymapWindow.activeSelf == true)
        {
            tabImageComponentArr[0].sprite = tabImageArr[1];
            tabImageComponentArr[1].sprite = tabImageArr[2];
        }
    }
    public void UpdateSetting()
    {
        LoadSetting();
    }
}
