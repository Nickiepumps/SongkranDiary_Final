using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data")]
public class TempPlayerDataSave : ScriptableObject
{
    // Delete this when finished prototype
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
    public WeaponSO currentSprdBulletASPD;
    public WeaponSO maxSprdBulletASPD;
    public WeaponSO currentWeaponSprdCount;
    public WeaponSO maxWeaponSprdCount;

    [Space]
    [Header("Laser Bullet")]
    public WeaponSO currentLsrBulletASPD;
    public WeaponSO maxLsrBulletASPD;

    [Space]
    [Header("Ultimate")]
    public WeaponSO maxPlayerUltAmount;
    public WeaponSO maxUltimateTravelSpeed;

    [Space]
    [Header("Coin")]
    public int coinAmount;
}
