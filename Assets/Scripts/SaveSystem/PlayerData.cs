using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector2 playerISOPos;
    public int Coin;
    public int hpLevel;
    public int ultChargeLevel;
    public int ultAmountLevel;
    public int bulletNormalTSPDLevel;
    public int bulletNormalASPDLevel;
    public int bulletSpreadCountLevel;
    public int bulletSpreadASPDLevel;
    public int bulletLaserASPDLevel;
    public bool bulletSpreadUnlocked;
    public bool bulletLaserUnlocked;
    public PlayerStatSO hpSO;
    public PlayerStatSO ultChargeSO;
    public WeaponSO bulletNormalTSPDSO;
    public WeaponSO bulletNormalASPDSO;
    public WeaponSO bulletSpreadCountSO;
    public WeaponSO bulletSpreadASPDSO;
    public WeaponSO bulletLaserASPDSO;
}
