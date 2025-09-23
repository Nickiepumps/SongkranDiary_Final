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
                playerStats.currentPlayerHP = playerStats.playerHPStatsSOArr[playerData.hpLevel - 1];
            }
            if (playerData.ultChargeSO != null)
            {
                playerStats.currentPlayerUltCharge = playerStats.playerUltStatsSOArr[playerData.ultChargeLevel - 1];
            }
            if (playerData.bulletNormalASPDSO != null)
            {
                playerStats.currentNormalASPD = playerStats.normalBulletASPDStatsSOArr[playerData.bulletNormalASPDLevel - 1];
            }
            if (playerData.bulletNormalTSPDSO != null)
            {
                playerStats.currentWeaponTravelSpeed = playerStats.normalBulletTSPDStatsSOArr[playerData.bulletNormalTSPDLevel - 1];
            }
            if (playerData.bulletSpreadCountSO != null)
            {
                playerStats.currentWeaponSprdCount = playerStats.spreadBulletCountStatsSOArr[playerData.bulletSpreadCountLevel - 1];
            }
            if (playerData.bulletSpreadASPDSO != null)
            {
                playerStats.currentSprdBulletASPD = playerStats.spreadBulletASPDStatsSOArr[playerData.bulletSpreadASPDLevel - 1];
            }
            if (playerData.bulletLaserASPDSO != null)
            {
                playerStats.currentLsrBulletASPD = playerStats.laserBulletASPDStatsSOArr[playerData.bulletLaserASPDLevel - 1];
            }
            playerStats.spreadBulletUnlocked = playerData.bulletSpreadUnlocked;
            playerStats.laserBulletUnlocked = playerData.bulletLaserUnlocked;
            playerStats.coinAmount = playerData.Coin;
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
        if (Directory.Exists(Application.persistentDataPath) == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        string playerDataJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", playerDataJson);
    }
    public void UpdatePlayerData(int coinAmount)
    {
        PlayerData playerData = LoadPlayerData();
        if (playerData != null)
        {
            playerData.Coin += coinAmount;
        }
        if (Directory.Exists(Application.persistentDataPath) == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        string playerDataJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", playerDataJson);
    }
    public void SavePlayerData()
    {
        if (Directory.Exists(Application.persistentDataPath) == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath);
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
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", playerDataJson);
    }
    public PlayerData LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.json") == false)
        {
            return null;
        }
        string loadedPlayerDataJson = File.ReadAllText(Application.persistentDataPath + "/playerData.json");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(loadedPlayerDataJson);
        return loadedPlayerData;
    }
    public void ResetPlayerData()
    {
        PlayerData playerData = LoadPlayerData();
        if(playerData != null)
        {
            if (Directory.Exists(Application.persistentDataPath) == false)
            {
                Directory.CreateDirectory(Application.persistentDataPath);
            }
            //playerData = new PlayerData();
            playerData = null;
            string playerDataJson = JsonUtility.ToJson(playerData);
            File.WriteAllText(Application.persistentDataPath + "/playerData.json", playerDataJson);
        }
    }
}
