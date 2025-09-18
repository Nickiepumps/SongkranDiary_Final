using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour
{
    [SerializeField] private bool isMainMenu = false;
    [SerializeField] PlayerStateController isoPlayerStateController;
    [SerializeField] PlayerStats playerStats;
    [HideInInspector] public bool hasPlayerData;

    public static PlayerDataHandler instance;
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
    private void Start()
    {
        PlayerData playerData = LoadPlayerData();
        if(playerData != null && isMainMenu == false)
        {
            hasPlayerData = true;
            if(isoPlayerStateController != null)
            {
                isoPlayerStateController.transform.position = playerData.playerISOPos;
            }
            if (playerData.hpSO != null)
            {
                playerStats.currentPlayerHP = playerData.hpSO;
            }
            if (playerData.ultChargeSO != null)
            {
                playerStats.currentPlayerUltCharge = playerData.ultChargeSO;
            }
            if (playerData.bulletNormalASPDSO != null)
            {
                playerStats.currentNormalASPD = playerData.bulletNormalASPDSO;
            }
            if (playerData.bulletNormalTSPDSO != null)
            {
                playerStats.currentWeaponTravelSpeed = playerData.bulletNormalTSPDSO;
            }
            if (playerData.bulletSpreadCountSO != null)
            {
                playerStats.currentWeaponSprdCount = playerData.bulletSpreadCountSO;
            }
            if (playerData.bulletSpreadASPDSO != null)
            {
                playerStats.currentSprdBulletASPD = playerData.bulletSpreadASPDSO;
            }
            if (playerData.bulletLaserASPDSO != null)
            {
                playerStats.currentLsrBulletASPD = playerData.bulletLaserASPDSO;
            }
            playerStats.spreadBulletUnlocked = playerData.bulletSpreadUnlocked;
            playerStats.laserBulletUnlocked = playerData.bulletLaserUnlocked;
            playerStats.coinAmount = playerData.Coin;
            /*playerStats.currentPlayerHP = playerData.hpSO;
            playerStats.currentPlayerUltCharge = playerData.ultChargeSO;
            playerStats.currentNormalASPD = playerData.bulletNormalASPDSO;
            playerStats.currentWeaponTravelSpeed = playerData.bulletNormalTSPDSO;
            playerStats.currentSprdBulletASPD = playerData.bulletSpreadASPDSO;
            playerStats.currentWeaponSprdCount = playerData.bulletSpreadCountSO;
            playerStats.currentLsrBulletASPD = playerData.bulletLaserASPDSO;
            playerStats.coinAmount = playerData.Coin;
            playerStats.spreadBulletUnlocked = playerData.bulletSpreadUnlocked;
            playerStats.laserBulletUnlocked = playerData.bulletLaserUnlocked;*/
        }
        else if (playerData != null && isMainMenu == true)
        {
            hasPlayerData = true;
        }
    }
    public void UpdatePlayerData(PlayerStats playerStat)
    {
        PlayerData playerData = LoadPlayerData();
        if (playerData != null)
        {
            playerData.Coin += playerStat.coinAmount;
        }
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        string playerDataJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/playerData.json", playerDataJson);
    }
    public void UpdatePlayerData(int coinAmount)
    {
        PlayerData playerData = LoadPlayerData();
        if (playerData != null)
        {
            playerData.Coin += coinAmount;
        }
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        string playerDataJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/playerData.json", playerDataJson);
    }
    public void SavePlayerData()
    {
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }

        PlayerData playerData = new PlayerData();
        playerData.playerISOPos = isoPlayerStateController.transform.position;
        playerData.hpLevel = playerStats.currentPlayerHP.level;
        playerData.ultChargeLevel = playerStats.currentPlayerUltCharge.level;
        playerData.ultAmountLevel = playerStats.currentPlayerUltAmount.level;
        playerData.bulletNormalASPDLevel = playerStats.currentNormalASPD.level;
        playerData.bulletNormalTSPDLevel = playerStats.currentWeaponTravelSpeed.level;
        playerData.bulletSpreadASPDLevel = playerStats.currentSprdBulletASPD.level;
        playerData.bulletSpreadCountLevel = playerStats.currentWeaponSprdCount.level;
        playerData.bulletLaserASPDLevel = playerStats.currentLsrBulletASPD.level;
        playerData.Coin = playerStats.coinAmount;
        playerData.bulletSpreadUnlocked = playerStats.spreadBulletUnlocked;
        playerData.bulletLaserUnlocked = playerStats.laserBulletUnlocked;
        playerData.hpSO = playerStats.currentPlayerHP;
        playerData.ultChargeSO = playerStats.currentPlayerUltCharge;
        playerData.bulletNormalASPDSO = playerStats.currentNormalASPD;
        playerData.bulletNormalTSPDSO = playerStats.currentWeaponTravelSpeed;
        playerData.bulletSpreadCountSO = playerStats.currentWeaponSprdCount;
        playerData.bulletSpreadASPDSO = playerStats.currentSprdBulletASPD;
        playerData.bulletLaserASPDSO = playerStats.currentLsrBulletASPD;

        string playerDataJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/playerData.json", playerDataJson);
    }
    public PlayerData LoadPlayerData()
    {
        if (File.Exists(Application.dataPath + "/playerData.json") == false)
        {
            return null;
        }
        string loadedPlayerDataJson = File.ReadAllText(Application.dataPath + "/playerData.json");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(loadedPlayerDataJson);
        return loadedPlayerData;
    }
    public void ResetPlayerData()
    {
        PlayerData playerData = LoadPlayerData();
        if(playerData != null)
        {
            if (Directory.Exists(Application.dataPath) == false)
            {
                Directory.CreateDirectory(Application.dataPath);
            }
            //playerData = new PlayerData();
            playerData = null;
            string playerDataJson = JsonUtility.ToJson(playerData);
            File.WriteAllText(Application.dataPath + "/playerData.json", playerDataJson);
        }
    }
}
