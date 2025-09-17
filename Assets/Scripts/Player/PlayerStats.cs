using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Delete this variable when finished prototype
    //[SerializeField] private TempPlayerDataSave tempData;

    // To do: Use this script for saving data
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
                currentPlayerHP = playerData.hpSO;
            }
            if(playerData.ultChargeSO != null)
            {
                currentPlayerUltCharge = playerData.ultChargeSO;
            }
            if(playerData.bulletNormalASPDSO != null)
            {
                currentNormalASPD = playerData.bulletNormalASPDSO;
            }
            if(playerData.bulletNormalTSPDSO != null)
            {
                currentWeaponTravelSpeed = playerData.bulletNormalTSPDSO;
            }
            if(playerData.bulletSpreadCountSO != null)
            {
                currentWeaponSprdCount = playerData.bulletSpreadCountSO;
            }
            if (playerData.bulletSpreadASPDSO != null)
            {
                currentSprdBulletASPD = playerData.bulletSpreadASPDSO;
            }
            if (playerData.bulletLaserASPDSO != null)
            {
                currentLsrBulletASPD = playerData.bulletLaserASPDSO;
            }
            spreadBulletUnlocked = playerData.bulletSpreadUnlocked;
            laserBulletUnlocked = playerData.bulletLaserUnlocked;
            coinAmount = playerData.Coin;
        }
    }
}
