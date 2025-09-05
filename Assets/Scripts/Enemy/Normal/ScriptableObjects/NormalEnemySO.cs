using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "normalEnemy", menuName = "Enemy/Normal enemy")]
public class NormalEnemySO : ScriptableObject
{
    [Header("Properties")]
    public NormalEnemyType NormalEnemyType;
    public int hp;
    public int damage;
    public float aspd;
    public int movementSpeed;

    [Header("Sprite")]
    public Sprite normalSprite;
    public Sprite damagedSprite;
    public Sprite deadSprite;
}
