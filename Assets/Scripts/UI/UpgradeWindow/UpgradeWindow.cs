using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour, IGameObserver
{
    [Header("Temp PlayerDataSO")]
    [SerializeField] private TempPlayerDataSave tempDataSO;

    [Header("Observer Reference")]
    [SerializeField] private GameSubject gameUIControllerSubject;

    [Header("Player's Stat Reference")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Coin Bar")]
    [SerializeField] private TMP_Text coinAmountText;
    [SerializeField] private int coinAmount;

    [Header("Upgrade Bar Images")]
    [SerializeField] private Image p_HP_UpgradeBar;
    [SerializeField] private Image p_Ult_UpgradeBar;
    [SerializeField] private Image bl_norm_ASPD_UpgradeBar;
    [SerializeField] private Image bl_norm_bulletSPD_UpgradeBar;
    [SerializeField] private Image bl_Sprd_SprdCount_UpgradeBar;
    [SerializeField] private Image bl_Sprd_ASPD_UpgradeBar;
    [SerializeField] private Image bl_Lsr_ASPD_UpgradeBar;

    [Header("UpgradeBar Sprites Variants")]
    [SerializeField] private List<Sprite> p_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List<Sprite> bl_norm_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List<Sprite> bl_Sprd_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List<Sprite> bl_Lsr_UpgradeBarLevel = new List<Sprite>();

    [Header("Locked Upgrade Sprites")]
    [SerializeField] private GameObject bl_Sprd_LockedGO;
    [SerializeField] private GameObject bl_Lsr_LockedGO;

    [Header("Button Sprites")]
    [SerializeField] private Sprite[] btn_ConfirmUpgrade;

    [Header("Button Objects")]
    [SerializeField] private GameObject p_HP_UpgradeBtn;
    [SerializeField] private GameObject p_Ult_UpgradeBarBtn;
    [SerializeField] private GameObject bl_norm_ASPD_UpgradeBtn;
    [SerializeField] private GameObject bl_norm_bulletSPD_UpgradeBtn;
    [SerializeField] private GameObject bl_Sprd_SprdCount_UpgradeBtn;
    [SerializeField] private GameObject bl_Sprd_ASPD_UpgradeBtn;
    [SerializeField] private GameObject bl_Lsr_ASPD_UpgradeBtn;
    [SerializeField] private GameObject confirmUpgradeBtn;
    [SerializeField] private GameObject[] UpgradeBtnArr = new GameObject[7];

    [Space]
    [Header("Player Stats Scriptable Objects")]
    [SerializeField] private List<PlayerStatSO> playerHPLists = new List<PlayerStatSO>();
    [SerializeField] private List<PlayerStatSO> playerUltChargeLists = new List<PlayerStatSO>();
    [Space]
    [Header("Weapon Stats Scriptable Objects")]
    [Header("Normal Bullet Stats")]
    [SerializeField] private List<WeaponSO> normalBulletASPDLists = new List<WeaponSO>();
    [SerializeField] private List<WeaponSO> normalBulletTASPDLists = new List<WeaponSO>();
    [Header("Spread Bullet Stats")]
    [SerializeField] private List<WeaponSO> spreadBulletASPDLists = new List<WeaponSO>();
    [SerializeField] private List<WeaponSO> spreadBulletCountLists = new List<WeaponSO>();
    [Header("Laser Bullet Stats")]
    [SerializeField] private List<WeaponSO> laserBulletASPDLists = new List<WeaponSO>();

    [Space]
    [Header("Unlock Costs and Status")]
    [SerializeField] private int sprdBulletUnlockCost = 5;
    [SerializeField] private int laserBulletUnlockCost = 5;
    [SerializeField] private bool sprdBulletUnlocked = false;
    [SerializeField] private bool laserBulletUnlocked = false;

    private List<PlayerStatSO> allSelectedPlayerStatLists = new List<PlayerStatSO>(); // All selected player's abilities waiting to upgrade
    private List<WeaponSO> allSelectedWeaponStatLists = new List<WeaponSO>(); // All selected weapons' abilities waiting to upgrade
    private List<GameObject> allUpgradeToUnlockLists = new List<GameObject>(); // All upgrade waiting to unlock
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {

    }
    private void OnEnable()
    {
        coinAmount = playerStats.coinAmount;
        coinAmountText.text = coinAmount.ToString() + " เหรียญ";
        UpdateUpgradableDisplay();
        gameUIControllerSubject.AddGameObserver(this);
    }
    private void OnDisable()
    {
        CancleUpgrade();
        gameUIControllerSubject.RemoveGameObserver(this);
    }
    private void Start()
    {
        PlayerData playerStats = PlayerDataHandler.instance.LoadPlayerData();
        if(playerStats != null)
        {
            bl_Sprd_LockedGO.SetActive(!playerStats.bulletSpreadUnlocked);
            bl_Lsr_LockedGO.SetActive(!playerStats.bulletLaserUnlocked);
            p_HP_UpgradeBar.sprite = p_UpgradeBarLevel[playerStats.hpLevel - 1];
            p_Ult_UpgradeBar.sprite = p_UpgradeBarLevel[playerStats.ultChargeLevel - 1];
            bl_norm_ASPD_UpgradeBar.sprite = bl_norm_UpgradeBarLevel[playerStats.bulletNormalASPDLevel - 1];
            bl_norm_bulletSPD_UpgradeBar.sprite = bl_norm_UpgradeBarLevel[playerStats.bulletNormalTSPDLevel - 1];
            bl_Sprd_ASPD_UpgradeBar.sprite = bl_Sprd_UpgradeBarLevel[playerStats.bulletSpreadASPDLevel - 1];
            bl_Sprd_SprdCount_UpgradeBar.sprite = bl_Sprd_UpgradeBarLevel[playerStats.bulletSpreadCountLevel - 1];
            bl_Lsr_ASPD_UpgradeBar.sprite = bl_Lsr_UpgradeBarLevel[playerStats.bulletLaserASPDLevel - 1];
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check current player's HP/Ult stat
            if(playerStats.currentPlayerHP.level != 3)
            {
                p_HP_UpgradeBtn.SetActive(true);
            }
            if(playerStats.currentPlayerUltCharge.level != 3)
            {
                p_Ult_UpgradeBarBtn.SetActive(true);
            }
            gameObject.SetActive(false);
            gameUIControllerSubject.NotifyGameObserver(IsometricGameState.Play);
        }
        if(coinAmount == 0)
        {
            foreach(GameObject btn in UpgradeBtnArr)
            {
                btn.SetActive(false);
            }
        }
    }
    public void AddPlayerAbility(PlayerStatSO playerStatType)
    {
        if(playerStatType.upgradeType == PlayerUpgradeType.playerHP && playerStats.coinAmount >= playerStatType.upgradeCost)
        {
            int currentLevel = playerStats.currentPlayerHP.level;
            UpdateCoinAmount(playerHPLists[currentLevel]);
            allSelectedPlayerStatLists.Add(playerHPLists[currentLevel]);
            p_HP_UpgradeBar.sprite = p_UpgradeBarLevel[currentLevel];
            playerStats.currentPlayerHP = playerHPLists[currentLevel];
            Debug.Log("Add player HP level" + playerHPLists[currentLevel].level);
            UpdateUpgradableDisplay(playerStatType, p_HP_UpgradeBtn);
        }
        if (playerStatType.upgradeType == PlayerUpgradeType.playerUlt && playerStats.coinAmount >= playerStatType.upgradeCost)
        {
            int currentLevel = playerStats.currentPlayerUltCharge.level;
            UpdateCoinAmount(playerUltChargeLists[currentLevel]);
            allSelectedPlayerStatLists.Add(playerUltChargeLists[currentLevel]);
            p_Ult_UpgradeBar.sprite = p_UpgradeBarLevel[currentLevel];
            playerStats.currentPlayerUltCharge = playerUltChargeLists[currentLevel];
            Debug.Log("Add player Ult level" + playerUltChargeLists[currentLevel].level);
            UpdateUpgradableDisplay(playerStatType, p_Ult_UpgradeBarBtn);
        }
        confirmUpgradeBtn.SetActive(true);
    }
    public void AddWeaponAbility(WeaponSO weaponStat)
    {
        if(weaponStat.bulletType == BulletType.normalBullet)
        {
            if(weaponStat.upgradeType == WeaponUpgradeType.bulletNormASPD)
            {
                int currentLevel = playerStats.currentNormalASPD.level;
                UpdateCoinAmount(normalBulletASPDLists[currentLevel]);
                allSelectedWeaponStatLists.Add(normalBulletASPDLists[currentLevel]);
                bl_norm_ASPD_UpgradeBar.sprite = bl_norm_UpgradeBarLevel[currentLevel];
                playerStats.currentNormalASPD = normalBulletASPDLists[currentLevel];
                UpdateUpgradableDisplay(weaponStat, bl_norm_ASPD_UpgradeBtn);
            }
            if (weaponStat.upgradeType == WeaponUpgradeType.bulletNormSpeed)
            {
                int currentLevel = playerStats.currentWeaponTravelSpeed.level;
                UpdateCoinAmount(normalBulletTASPDLists[currentLevel]);
                allSelectedWeaponStatLists.Add(normalBulletTASPDLists[currentLevel]);
                bl_norm_bulletSPD_UpgradeBar.sprite = bl_norm_UpgradeBarLevel[currentLevel];
                playerStats.currentWeaponTravelSpeed = normalBulletTASPDLists[currentLevel];
                UpdateUpgradableDisplay(weaponStat, bl_norm_bulletSPD_UpgradeBtn);
            }
        }
        if(weaponStat.bulletType == BulletType.spreadBullet)
        {
            if(weaponStat.upgradeType == WeaponUpgradeType.bulletSprdSprdCount)
            {
                int currentLevel = playerStats.currentWeaponSprdCount.level;
                UpdateCoinAmount(spreadBulletCountLists[currentLevel]);
                allSelectedWeaponStatLists.Add(spreadBulletCountLists[currentLevel]);
                bl_Sprd_SprdCount_UpgradeBar.sprite = bl_Sprd_UpgradeBarLevel[currentLevel];
                playerStats.currentWeaponSprdCount = spreadBulletCountLists[currentLevel];
                UpdateUpgradableDisplay(weaponStat, bl_Sprd_SprdCount_UpgradeBtn);
            }
            if(weaponStat.upgradeType == WeaponUpgradeType.bulletSprdASPD)
            {
                int currentLevel = playerStats.currentSprdBulletASPD.level;
                UpdateCoinAmount(spreadBulletASPDLists[currentLevel]);
                allSelectedWeaponStatLists.Add(spreadBulletASPDLists[currentLevel]);
                bl_Sprd_ASPD_UpgradeBar.sprite = bl_Sprd_UpgradeBarLevel[currentLevel];
                playerStats.currentSprdBulletASPD = spreadBulletASPDLists[currentLevel];
                UpdateUpgradableDisplay(weaponStat, bl_Sprd_ASPD_UpgradeBtn);
            }
        }
        if(weaponStat.bulletType == BulletType.laser)
        {
            if (weaponStat.upgradeType == WeaponUpgradeType.bulletLsrASPD)
            {
                int currentLevel = playerStats.currentLsrBulletASPD.level;
                UpdateCoinAmount(laserBulletASPDLists[currentLevel]);
                allSelectedWeaponStatLists.Add(laserBulletASPDLists[currentLevel]);
                bl_Lsr_ASPD_UpgradeBar.sprite = bl_Lsr_UpgradeBarLevel[currentLevel];
                playerStats.currentLsrBulletASPD = laserBulletASPDLists[currentLevel];
                UpdateUpgradableDisplay(weaponStat, bl_Lsr_ASPD_UpgradeBtn);
            }
        }
        confirmUpgradeBtn.SetActive(true);
    }
    private void UpdateUpgradableDisplay()
    {
        // Disable upgrade button if player upgrade to the max level or not enough coin
        // Call this method when
        // 1. after purchase one of the upgrade type
        // 2. when player open the upgrade window
        //**********************************************************************************//
        if(playerStats.currentPlayerHP.level == 3)
        {
            p_HP_UpgradeBtn.SetActive(false);
        }
        else
        {
            p_HP_UpgradeBtn.SetActive(true);
        }
        if(playerStats.currentPlayerUltCharge.level == 3)
        {
            p_Ult_UpgradeBarBtn.SetActive(false);
        }
        else
        {
            p_Ult_UpgradeBarBtn.SetActive(true);
        }
        if (playerStats.currentNormalASPD.level == 3)
        {
            bl_norm_ASPD_UpgradeBtn.SetActive(false);
        }
        else
        {
            bl_norm_ASPD_UpgradeBtn.SetActive(true);
        }
        if (playerStats.currentWeaponTravelSpeed.level == 3)
        {
            bl_norm_bulletSPD_UpgradeBtn.SetActive(false);
        }
        else
        {
            bl_norm_bulletSPD_UpgradeBtn.SetActive(true);
        }
        if (playerStats.currentWeaponSprdCount.level == 3)
        {
            bl_Sprd_SprdCount_UpgradeBtn.SetActive(false);
        }
        else
        {
            bl_Sprd_SprdCount_UpgradeBtn.SetActive(true);
        }
        if (playerStats.currentSprdBulletASPD.level == 3)
        {
            bl_Sprd_ASPD_UpgradeBtn.SetActive(false);
        }
        else
        {
            bl_Sprd_ASPD_UpgradeBtn.SetActive(true);
        }
        if (playerStats.currentLsrBulletASPD.level == 3)
        {
            bl_Lsr_ASPD_UpgradeBtn.SetActive(false);
        }
        else
        {
            bl_Lsr_ASPD_UpgradeBtn.SetActive(true);
        }
    }
    private void UpdateUpgradableDisplay(PlayerStatSO playerStat, GameObject upgradeBtn)
    {
        if (playerStat.upgradeType == PlayerUpgradeType.playerHP)
        {
            if(playerStats.coinAmount < playerStat.upgradeCost || playerStats.currentPlayerHP.level == 3)
            {
                upgradeBtn.SetActive(false);
            }
            else
            {
                upgradeBtn.SetActive(true);
            }
        }
        else
        {
            if (playerStats.coinAmount < playerStat.upgradeCost || playerStats.currentPlayerUltCharge.level == 3)
            {
                upgradeBtn.SetActive(false);
            }
            else
            {
                upgradeBtn.SetActive(true);
            }
        }
    }
    private void UpdateUpgradableDisplay(WeaponSO weaponStat, GameObject upgradeBtn)
    {

    }
    private void UpdateCoinAmount(PlayerStatSO playerStat)
    {
        coinAmount -= playerStat.upgradeCost;
        coinAmountText.text = coinAmount.ToString() + " เหรียญ";
    }
    private void UpdateCoinAmount(WeaponSO weaponStat)
    {
        coinAmount -= weaponStat.upgradeCost;
        coinAmountText.text = coinAmount.ToString() + " เหรียญ";
    }
    public void ConfirmUpgrade()
    {
        foreach(var playerConfirmStats in allSelectedPlayerStatLists)
        {
            if(playerConfirmStats.upgradeType == PlayerUpgradeType.playerHP)
            {
                playerStats.currentPlayerHP = playerConfirmStats;
            }
            if(playerConfirmStats.upgradeType == PlayerUpgradeType.playerUlt)
            {
                playerStats.currentPlayerUltCharge = playerConfirmStats;
            }
        }
        foreach(var weaponConfirmStats in allSelectedWeaponStatLists)
        {
            if(weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletNormASPD)
            {
                playerStats.currentNormalASPD = weaponConfirmStats;
            }
            if(weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletNormSpeed)
            {
                playerStats.currentWeaponTravelSpeed = weaponConfirmStats;
            }
            if (weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletSprdSprdCount)
            {
                playerStats.currentWeaponSprdCount = weaponConfirmStats;
            }
            if (weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletSprdASPD)
            {
                playerStats.currentSprdBulletASPD = weaponConfirmStats;
            }
            if (weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletLsrASPD)
            {
                playerStats.currentLsrBulletASPD = weaponConfirmStats;
            }
        }
        allSelectedPlayerStatLists.Clear();
        allSelectedWeaponStatLists.Clear();
        allUpgradeToUnlockLists.Clear();
        playerStats.coinAmount = coinAmount;
        PlayerDataHandler.instance.SavePlayerData();
        confirmUpgradeBtn.SetActive(false);
    }
    private void CancleUpgrade()
    {
        coinAmount = playerStats.coinAmount;
        coinAmountText.text = coinAmount.ToString();

        // Reset player stat to the previous level
        if (allSelectedPlayerStatLists != null)
        {
            foreach(var stat in allSelectedPlayerStatLists)
            {
                if(stat.upgradeType == PlayerUpgradeType.playerHP)
                {
                    p_HP_UpgradeBar.sprite = p_UpgradeBarLevel[playerStats.currentPlayerHP.level - 2];
                    playerStats.currentPlayerHP = playerHPLists[playerStats.currentPlayerHP.level - 2];
                }
                else if(stat.upgradeType == PlayerUpgradeType.playerUlt)
                {
                    p_Ult_UpgradeBar.sprite = p_UpgradeBarLevel[playerStats.currentPlayerUltCharge.level - 2];
                    playerStats.currentPlayerUltCharge = playerUltChargeLists[playerStats.currentPlayerUltCharge.level - 2];
                }
            }
        }
        if(allSelectedWeaponStatLists != null)
        {
            foreach(var stat in allSelectedWeaponStatLists)
            {
                if (stat.upgradeType == WeaponUpgradeType.bulletNormASPD)
                {
                    bl_norm_ASPD_UpgradeBar.sprite = bl_norm_UpgradeBarLevel[playerStats.currentNormalASPD.level - 2];
                    playerStats.currentNormalASPD = normalBulletASPDLists[playerStats.currentNormalASPD.level - 2];
                }
                else if(stat.upgradeType == WeaponUpgradeType.bulletNormSpeed)
                {
                    bl_norm_bulletSPD_UpgradeBar.sprite = bl_norm_UpgradeBarLevel[playerStats.currentWeaponTravelSpeed.level - 2];
                    playerStats.currentWeaponTravelSpeed = normalBulletTASPDLists[playerStats.currentWeaponTravelSpeed.level - 2];
                }
                else if(stat.upgradeType == WeaponUpgradeType.bulletSprdASPD)
                {
                    bl_Sprd_ASPD_UpgradeBar.sprite = bl_Sprd_UpgradeBarLevel[playerStats.currentSprdBulletASPD.level - 2];
                    playerStats.currentSprdBulletASPD = spreadBulletASPDLists[playerStats.currentSprdBulletASPD.level - 2];
                }
                else if(stat.upgradeType == WeaponUpgradeType.bulletSprdSprdCount)
                {
                    bl_Sprd_SprdCount_UpgradeBar.sprite = bl_Sprd_UpgradeBarLevel[playerStats.currentWeaponSprdCount.level - 2];
                    playerStats.currentWeaponSprdCount = spreadBulletCountLists[playerStats.currentWeaponSprdCount.level - 2];
                }
                else if(stat.upgradeType == WeaponUpgradeType.bulletLsrASPD)
                {
                    bl_Lsr_ASPD_UpgradeBar.sprite = bl_Lsr_UpgradeBarLevel[playerStats.currentLsrBulletASPD.level - 2];
                    playerStats.currentLsrBulletASPD = laserBulletASPDLists[playerStats.currentLsrBulletASPD.level - 2];
                }
            }
        }

        // Clear all selected upgrade lists
        allSelectedPlayerStatLists.Clear();
        allSelectedWeaponStatLists.Clear();
        allUpgradeToUnlockLists.Clear();

        confirmUpgradeBtn.SetActive(false);
    }
    public void UnlockUpgrade(GameObject lockedUpgradePanel)
    {
        switch (lockedUpgradePanel.name)
        {
            case("SpreadLockPanel"):
                if(coinAmount >= sprdBulletUnlockCost)
                {
                    playerStats.coinAmount -= sprdBulletUnlockCost;
                    coinAmount -= sprdBulletUnlockCost;
                    coinAmountText.text = "X " + coinAmount.ToString();
                    sprdBulletUnlocked = true;
                    playerStats.spreadBulletUnlocked = true;
                    PlayerDataHandler.instance.SavePlayerData();
                    lockedUpgradePanel.SetActive(false);
                }
                break;
            case ("LaserLockPanel"):
                if (coinAmount >= laserBulletUnlockCost)
                {
                    playerStats.coinAmount -= laserBulletUnlockCost;
                    coinAmount -= laserBulletUnlockCost;
                    coinAmountText.text = "X " + coinAmount.ToString();
                    laserBulletUnlocked = true;
                    playerStats.laserBulletUnlocked = true;
                    PlayerDataHandler.instance.SavePlayerData();
                    lockedUpgradePanel.SetActive(false);
                }
                break;
        }
    }
}
