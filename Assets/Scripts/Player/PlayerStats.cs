using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Delete this variable when finished prototype
    //[SerializeField] private TempPlayerDataSave tempData;

    // This container use for storing data from Scriptable object to use them when reload the game
    [Header("Player and Weapon Stats Container")]
    public PlayerStatSO[] playerHPStatsSOArr;
    public PlayerStatSO[] playerUltStatsSOArr;
    public WeaponSO[] normalBulletASPDStatsSOArr;
    public WeaponSO[] normalBulletTSPDStatsSOArr;
    public WeaponSO[] spreadBulletCountStatsSOArr;
    public WeaponSO[] spreadBulletASPDStatsSOArr;
    public WeaponSO[] laserBulletASPDStatsSOArr;

    [Header("Player Stats")]
    public PlayerStatSO maxPlayerHP;
    public PlayerStatSO currentPlayerHP;
    public PlayerStatSO maxPlayerUltCharge;
    public PlayerStatSO currentPlayerUltCharge;
    public PlayerStatSO currentPlayerUltAmount;

    [Space]
    [Header("Weapon Stats")]
    [Header("Normal Bullet")]
    public WeaponSO currentNormalASPD;
    public WeaponSO maxNormalASPD;
    public WeaponSO currentWeaponTravelSpeed;
    public WeaponSO maxWeaponTravelSpeed;

    [Space]
    [Header("Spread Bullet")]
    public bool spreadBulletUnlocked;
    public WeaponSO currentSprdBulletASPD;
    public WeaponSO maxSprdBulletASPD;
    public WeaponSO currentWeaponSprdCount;
    public WeaponSO maxWeaponSprdCount;

    [Space]
    [Header("Laser Bullet")]
    public bool laserBulletUnlocked;
    public WeaponSO currentLsrBulletASPD;
    public WeaponSO maxLsrBulletASPD;

    [Space]
    [Header("Ultimate")]
    public WeaponSO maxPlayerUltAmount;
    public WeaponSO maxUltimateTravelSpeed;

    [Space]
    [Header("Coin")]
    public int coinAmount;

    private void Start()
    {
        PlayerData playerData = PlayerDataHandler.instance.LoadPlayerData();
        if(playerData != null)
        {
            if(playerData.hpSO != null)
            {
                currentPlayerHP = playerHPStatsSOArr[playerData.hpLevel - 1];
            }
            if(playerData.ultChargeSO != null)
            {
                currentPlayerUltCharge = playerUltStatsSOArr[playerData.ultChargeLevel - 1];
            }
            if(playerData.bulletNormalASPDSO != null)
            {
                currentNormalASPD = normalBulletASPDStatsSOArr[playerData.bulletNormalASPDLevel - 1];
            }
            if(playerData.bulletNormalTSPDSO != null)
            {
                currentWeaponTravelSpeed = normalBulletTSPDStatsSOArr[playerData.bulletNormalTSPDLevel - 1];
            }
            if(playerData.bulletSpreadCountSO != null)
            {
                currentWeaponSprdCount = spreadBulletCountStatsSOArr[playerData.bulletSpreadCountLevel - 1];
            }
            if (playerData.bulletSpreadASPDSO != null)
            {
                currentSprdBulletASPD = spreadBulletASPDStatsSOArr[playerData.bulletSpreadASPDLevel - 1];
            }
            if (playerData.bulletLaserASPDSO != null)
            {
                currentLsrBulletASPD = laserBulletASPDStatsSOArr[playerData.bulletLaserASPDLevel - 1];
            }
            spreadBulletUnlocked = playerData.bulletSpreadUnlocked;
            laserBulletUnlocked = playerData.bulletLaserUnlocked;
            coinAmount = playerData.Coin;
        }
    }
}
