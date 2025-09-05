using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKeymap", menuName = "Keymap")]
public class KeyMapSO : ScriptableObject
{
    [Header("Keymap ID")]
    public int id;

    [Header("Movement")]
    public KeyCode left;
    public KeyCode right;
    public KeyCode top;
    public KeyCode bottom;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode dash;

    [Header("Combat")]
    public KeyCode shoot;
    public KeyCode ult;
    public KeyCode aim;
    public KeyCode weaponSwitch;

    [Header("Interaction")]
    public KeyCode interact;
}
