using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon and Status")]
public class WeaponSO : ScriptableObject
{
    public WeaponUpgradeType upgradeType;
    public BulletType bulletType;
    public Sprite icon; // Weapon's upgrade icon
    public string upgradeName; // Weapon's upgrade name
    public string upgradeDescription; // Weapon's upgrade description
    public int upgradeCost; // Weapon's upgrade cost
    public int level; // Weapon's level
    public float aspd; // Player's atack speed
    public float travelSpeed; // Bullet travel speed
    public int spreadCount; // Bullet spawn count
    public int ultCount; // Ultimate amount
    public float ultChargingSpeed; // Ultimate charging speed
}
public enum WeaponUpgradeType
{
    bulletNormASPD,
    bulletNormSpeed,
    bulletSprdASPD,
    bulletSprdSprdCount,
    bulletLsrASPD,
    ultChargeTime,
    ultAmount
}
public enum BulletType
{
    normalBullet,
    spreadBullet,
    laser,
    ult
}
