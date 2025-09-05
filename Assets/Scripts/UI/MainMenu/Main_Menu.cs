using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] private GameObject mainMenuWindow;
    [SerializeField] private GameObject albumWindow;
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private GameObject soundWindow;
    [SerializeField] private GameObject keymapWindow;

    [Header("Buttons")]
    [SerializeField] private GameObject continueBtn;

    [Header("Setting tabs")]
    [SerializeField] private Sprite[] tabImageArr; 
    [SerializeField] private Image[] tabImageComponentArr;

    [Header("Sound component")]
    [SerializeField] private AudioSource bgmAudio;
    private void Start()
    {
        PlayerData playerData = PlayerDataHandler.instance.LoadPlayerData();
        SettingData soundData = SettingHandler.instance.LoadSettingData();
        if (playerData != null)
        {
            continueBtn.SetActive(true);
        }
        else
        {
            continueBtn.SetActive(false);
        }
        if (soundData != null)
        {
            if (soundData.masterVolume >= soundData.bgmVolume)
            {
                bgmAudio.volume = soundData.bgmVolume;
            }
            else
            {
                bgmAudio.volume = soundData.masterVolume;
            }
        }
    }
    public void OpenMenu()
    {
        mainMenuWindow.SetActive(true);
        settingWindow.SetActive(false);
        albumWindow.SetActive(false);
    }
    public void OpenAlbum()
    {
        mainMenuWindow.SetActive(false);
        settingWindow.SetActive(false);
        albumWindow.SetActive(true);
    }
    public void OpenSetting()
    {
        mainMenuWindow.SetActive(false);
        settingWindow.SetActive(true);
        if(keymapWindow.activeSelf == true)
        {
            keymapWindow.SetActive(false);
        }
        soundWindow.SetActive(true);
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
        if(soundWindow.activeSelf == true)
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
}
