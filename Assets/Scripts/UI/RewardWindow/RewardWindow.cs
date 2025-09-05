using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardWindow : MonoBehaviour
{
    [Header("Game UI Controller Reference")]
    [SerializeField] private GameUIController gameUIController;
    [Header("Player Coin")]
    [SerializeField] private PlayerStats playerCoin;
    [Header("Reward UI Elements")]
    [SerializeField] private TMP_Text rewardNameText;
    private void OnEnable()
    {
        // Random amount of coin and update coin amount in Upgrade Window
        int randomCoinAmount = Random.Range(1, 4); // Adjust the max amount later
        playerCoin.coinAmount += randomCoinAmount;
        rewardNameText.text = "ตังค์ค่าขนม " + randomCoinAmount + " เหรียญ";
        PlayerDataHandler.instance.SavePlayerData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameUIController.OpenWindow(gameObject);
        }
    }
}
