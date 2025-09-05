using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour, IShootingObserver
{
    [SerializeField] private ShootingSubject shootingSubject;
    [SerializeField] private Image bulletDisplay;
    [SerializeField] private Image spreadBulletDisplay;
    [SerializeField] private Image laserBulletDisplay;
    [SerializeField] private Sprite[] bulletSprites;
    [SerializeField] private Sprite[] bulletKeySprites;
    [SerializeField] private TMP_Text ultAmountText;

    private void OnEnable()
    {
        shootingSubject.AddShootingObserver(this);
    }
    private void OnDisable()
    {
        shootingSubject.RemoveShootingObserver(this);
    }
    public void OnShootingNotify(ShootingAction shootingAction)
    {
        switch (shootingAction)
        {
            case (ShootingAction.switchtonormal):
                bulletDisplay.sprite = bulletSprites[0];
                //spreadBulletDisplay.sprite = bulletKeySprites[0];
                laserBulletDisplay.sprite = bulletKeySprites[2];
                return;
            case (ShootingAction.switchtospread):
                bulletDisplay.sprite = bulletSprites[1];
                //spreadBulletDisplay.sprite = bulletKeySprites[1];
                laserBulletDisplay.sprite = bulletKeySprites[2];
                return;
            case (ShootingAction.switchtolaser):
                bulletDisplay.sprite = bulletSprites[2];
                //spreadBulletDisplay.sprite = bulletKeySprites[0];
                laserBulletDisplay.sprite = bulletKeySprites[3];
                return;
            case (ShootingAction.chargeult):
                ultAmountText.text = shootingSubject.GetComponent<PlayerSideScrollStateController>().playerUltAmount.ToString();
                return;
            case(ShootingAction.useult):
                ultAmountText.text = shootingSubject.GetComponent<PlayerSideScrollStateController>().playerUltAmount.ToString();
                return;
        }
    }
    public void UpdateBulletDisplay(bool coolDownStatus, float currentTimer, float cooldownTime)
    {
        if (coolDownStatus == true)
        {
            currentTimer -= Time.deltaTime;
            //spreadBulletDisplay.fillAmount = 1 - (currentTimer / cooldownTime);
            laserBulletDisplay.fillAmount = 1 - (currentTimer / cooldownTime);
            if (currentTimer <= 0)
            {
                currentTimer = cooldownTime;
            }
        }
        else
        {
            //spreadBulletDisplay.fillAmount = 1;
            laserBulletDisplay.fillAmount = 1;
        }
    }
}
