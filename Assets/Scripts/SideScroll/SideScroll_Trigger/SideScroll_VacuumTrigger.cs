using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_VacuumTrigger : SideScroll_TriggerEvent
{
    [Header("Properties")]
    [SerializeField] private Rigidbody2D target;
    [SerializeField] private float vacuumValue;
    public Vector2 pullDirection;
    public void OnTriggerStay2D(Collider2D collision)
    {
        Trigger_Vacuum(target, vacuumValue, pullDirection);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        Trigger_Vacuum(target, 0, pullDirection);
    }
}
