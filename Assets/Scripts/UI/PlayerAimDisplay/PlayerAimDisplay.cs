using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimDisplay : MonoBehaviour, IShootingObserver
{
    [Header("Shooting Observer Reference")]
    [SerializeField] private ShootingSubject shootingSubject;
    [Header("Aim display components")]
    [SerializeField] Transform displayAimPivot;
    [SerializeField] Animation aimWheelAnim;
    private float currentPivotAngle, previousPivotAngle;
    private void OnEnable()
    {
        currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
        previousPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
        shootingSubject.AddShootingObserver(this);
    }
    private void OnDisable()
    {
        shootingSubject.RemoveShootingObserver(this);
    }
    private void Update()
    {
        if(currentPivotAngle != previousPivotAngle)
        {
            aimWheelAnim.Play();
            previousPivotAngle = currentPivotAngle;
        }
    }
    public void OnShootingNotify(ShootingAction shootingAction)
    {
        switch (shootingAction)
        {
            case (ShootingAction.aimright):
                displayAimPivot.localRotation = Quaternion.Euler(0, 0, -90);
                currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
                return;
            case (ShootingAction.aim45topright):
                displayAimPivot.localRotation = Quaternion.Euler(0, 0, -45);
                currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
                return;
            case (ShootingAction.aim45downright):
                displayAimPivot.localRotation = Quaternion.Euler(0, 0, -135);
                currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
                return;
            case (ShootingAction.aimleft):
                displayAimPivot.localRotation = Quaternion.Euler(0, 0, 90);
                currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
                return;
            case (ShootingAction.aim45topleft):
                displayAimPivot.localRotation = Quaternion.Euler(0, 0, 45);
                currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
                return;
            case (ShootingAction.aim45downleft):
                displayAimPivot.localRotation = Quaternion.Euler(0, 0, 135);
                currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
                return;
            case (ShootingAction.aimtop):
                displayAimPivot.localRotation = Quaternion.Euler(0, 0, 0);
                currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
                return;
            case (ShootingAction.aimdown):
                displayAimPivot.localRotation = Quaternion.Euler(0, 0, -180);
                currentPivotAngle = displayAimPivot.localRotation.eulerAngles.z;
                return;
        }
    }
}
