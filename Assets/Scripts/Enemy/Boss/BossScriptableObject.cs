using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Boss")]
public class BossScriptableObject : ScriptableObject
{
    [Header("Boss Properties")]
    public BossList bossName; // To Do: Change to something else that can make any boss have unique attack without making another state machine
    public int hp;
    public int armorHP;
    public int damage;
    public float ultCooldown;
    public float idleTime;
    public float initialIdleTime;
    public float aspd;
    public int healAmount;
    public float bossNormalMovementSpeed;
    public float bossUltMovementSpeed;

    [Header("Boss Animations")]
    public Sprite ultimateSprite; // Temp ult sprite
    public Sprite idleSprite; // Temp idle sprite
    public AnimationClip ultimateAnimation; // Use this when have ult anim
    public AnimationClip normalAtkAnimation; // Use this when have normal attack anim
    public AnimationClip idleAnimation; // Use this when have idle anim
    public AnimationClip loseAnimation; // Use this when have lose anim
}
