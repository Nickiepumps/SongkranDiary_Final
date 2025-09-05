using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    [Header("All audio players")]
    [SerializeField] private AudioSource bgmAudioPlayer;
    [SerializeField] private AudioSource[] sfxAudioPlayer;

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
    [SerializeField] private KeyMapSO defaultKeymap;

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
            if(settingData.keymapSO != null)
            {
                keymapDropdown.value = settingData.keymapSO.id;
                confirmBtn.SetActive(false);
                if(playerStateController != null)
                {
                    playerStateController.keymapSO = settingData.keymapSO;
                }
                else if(playerSideScrollStateController != null)
                {
                    playerSideScrollStateController.keymapSO = settingData.keymapSO;
                }
            }
            if (settingData.bgmVolume > settingData.masterVolume)
            {
                bgmAudioPlayer.volume = settingData.masterVolume;
            }
            else
            {
                bgmAudioPlayer.volume = settingData.bgmVolume;
            }
            if (settingData.sfxVolume > settingData.masterVolume)
            {
                foreach (AudioSource sfxSource in sfxAudioPlayer)
                {
                    sfxSource.volume = settingData.masterVolume;
                }
            }
            else
            {
                foreach (AudioSource sfxSource in sfxAudioPlayer)
                {
                    sfxSource.volume = settingData.sfxVolume;
                }
            }
        }
        else
        {
            if (playerSideScrollStateController != null)
            {
                playerSideScrollStateController.keymapSO = defaultKeymap;
            }
            else if (playerStateController != null)
            {
                playerStateController.keymapSO = defaultKeymap;
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
