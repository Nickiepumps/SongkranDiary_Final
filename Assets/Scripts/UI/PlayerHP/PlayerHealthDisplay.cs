using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour, IPlayerObserver
{
    [Header("Observer References")]
    [SerializeField] private PlayerSubject sideScrollPlayerObserver;
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [Header("Player Health UI")]
    [SerializeField] private GameObject playerHealthHUDGroup;
    [SerializeField] private Sprite healthActiveIcon; // Actived health icon, Changed from depleated icon when player is healed
    [SerializeField] private Sprite healthDepletedIcon; // Depleated health icon, Changed from actived icon when player is damaged
    [SerializeField] private Image healthUI;
    [SerializeField] private Transform playerHealthGroup;
    [SerializeField] private Image playerEmotionDisplay;
    [SerializeField] private List<Sprite> playerEmotionIconLists;
    private List<Image> healthIconList = new List<Image>();
    private int currentHealthPoint;
    private int maxHealthPoint;
    [HideInInspector] public Sprite currentPlayerEmotionIcon;

    private void OnEnable()
    {
        sideScrollPlayerObserver.AddPlayerObserver(this);   
    }
    private void OnDisable()
    {
        sideScrollPlayerObserver.RemovePlayerObserver(this);
    }
    private void Start()
    {
        maxHealthPoint = playerStats.currentPlayerHP.hpPoint;
        currentHealthPoint = maxHealthPoint;
        UpdateEmotionIcon(maxHealthPoint, currentHealthPoint);
        for(int i = 0; i < maxHealthPoint; i++)
        {
            Image hp = Instantiate(healthUI, playerHealthGroup);
            healthUI.rectTransform.sizeDelta = new Vector2(38f, 38f);
            healthUI.sprite = healthActiveIcon;
            healthIconList.Add(hp);
        }
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case (PlayerAction.Damaged):
                //UpdateEmotionIcon(maxHealthPoint, sideScrollPlayerObserver.GetComponent<PlayerSideScrollStateController>().playerCurrentHP); // This doesn't work except boss2. Don't know why
                currentHealthPoint--;
                UpdateEmotionIcon(maxHealthPoint, currentHealthPoint);
                playerHealthHUDGroup.GetComponent<Animation>().Play();
                return;
            case (PlayerAction.Heal):
                if(currentHealthPoint < maxHealthPoint)
                {
                    currentHealthPoint++;
                }
                UpdateEmotionIcon(maxHealthPoint, currentHealthPoint);
                return;
            case (PlayerAction.Blind):
                currentHealthPoint--;
                UpdateEmotionIcon(maxHealthPoint, currentHealthPoint);
                playerHealthHUDGroup.GetComponent<Animation>().Play();
                return;
            case (PlayerAction.Dead):
                currentHealthPoint = 0;
                UpdateEmotionIcon(maxHealthPoint, currentHealthPoint);
                playerHealthHUDGroup.GetComponent<Animation>().Play();
                return;
        }
    }
    public void IncreaseHealth(int currentHP)
    {
        healthIconList[currentHP - 1].sprite = healthActiveIcon;
    }
    public void DecreaseHealth(int currentHP)
    {
        healthIconList[currentHP].sprite = healthDepletedIcon;
    }
    public void UpdateEmotionIcon(float maxHP, float currentHP)
    {
        float healthPercentage = (currentHP / maxHP) * 100;
        healthPercentage = Mathf.RoundToInt(healthPercentage); // Round the decimal to the nearest int value
        if (healthPercentage <= 34)
        {
            playerEmotionDisplay.sprite = playerEmotionIconLists[2]; // Angry
            currentPlayerEmotionIcon = playerEmotionIconLists[2];
        }
        else if(healthPercentage <= 67)
        {
            playerEmotionDisplay.sprite = playerEmotionIconLists[1]; // Nervous
            currentPlayerEmotionIcon = playerEmotionIconLists[1];
        }
        else
        {
            playerEmotionDisplay.sprite = playerEmotionIconLists[0]; // Happy
            currentPlayerEmotionIcon = playerEmotionIconLists[0];
        }
    }
}
