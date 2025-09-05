using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossHealthObserver))]
public class BossHealth : MonoBehaviour
{
    public BossScriptableObject bossStats;
    [HideInInspector] public int bossMaxHP;
    [HideInInspector] public int bossMaxArmor;
    public int currentBossHP;
    public int currentBossArmor;
    void Start()
    {
        bossMaxHP = bossStats.hp;
        currentBossHP = bossMaxHP;
        bossMaxArmor = bossStats.armorHP;
        currentBossArmor = bossStats.armorHP;
    }
}
