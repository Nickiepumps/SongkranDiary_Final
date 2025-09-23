using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingHandler : MonoBehaviour
{
    public static SettingHandler instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveSetting_Sound(float masterValue, float bgmValue, float sfxValue)
    {
        SettingData settingData = LoadSettingData();
        if(settingData != null)
        {
            settingData.masterVolume = masterValue;
            settingData.bgmVolume = bgmValue;
            settingData.sfxVolume = sfxValue;
        }
        else
        {
            settingData = new SettingData();
            settingData.masterVolume = masterValue;
            settingData.bgmVolume = bgmValue;
            settingData.sfxVolume = sfxValue;
        }
        if (Directory.Exists(Application.persistentDataPath) == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        string settingJson = JsonUtility.ToJson(settingData);
        File.WriteAllText(Application.persistentDataPath + "/setting.json", settingJson);
    }
    public void SaveSetting_Keymap(KeyMapSO keymap)
    {
        SettingData settingData = LoadSettingData();
        if(settingData != null)
        {
            settingData.keymapSO = keymap;
        }
        else
        {
            settingData = new SettingData();
            settingData.keymapSO = keymap;
        }
        if (Directory.Exists(Application.persistentDataPath) == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        string settingJson = JsonUtility.ToJson(settingData);
        File.WriteAllText(Application.persistentDataPath + "/setting.json", settingJson);
    }
    public SettingData LoadSettingData()
    {
        if(File.Exists(Application.persistentDataPath + "/setting.json") == false)
        {
            return null;
        }
        string loadedSettingJson = File.ReadAllText(Application.persistentDataPath + "/setting.json");
        SettingData loadedSetting = JsonUtility.FromJson<SettingData>(loadedSettingJson);
        return loadedSetting;
    }
}
