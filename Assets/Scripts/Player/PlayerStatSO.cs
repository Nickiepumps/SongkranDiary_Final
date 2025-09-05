using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Status", menuName = "Player Status")]
public class PlayerStatSO : ScriptableObject
{
    public PlayerUpgradeType upgradeType;
    public Sprite icon;
    public string upgradeName;
    public string upgradeDescription;
    public int upgradeCost;
    public int level;
    public int hpPoint;
    public int ultChargeTime;
}
public enum PlayerUpgradeType
{
    playerHP,
    playerUlt,
}
