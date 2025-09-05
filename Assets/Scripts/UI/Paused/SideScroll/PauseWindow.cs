using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseWindow : MonoBehaviour
{
    [Header("SideScroll Game Controller")]
    [SerializeField] private SideScrollGameController sideScrollGameController;
    [Header("Player Health Display")]
    [SerializeField] private PlayerHealthDisplay playerHealthDisplay;
    [Header("Progress Bar")]
    [SerializeField] private Image progressionBar;
    [SerializeField] private Image currentProgressIcon;
    [Header("Emontion Icon")]
    [SerializeField] private Image playerEmotionIcon;
    [Header("Progression Text")]
    [SerializeField] private TMP_Text progressionText;

    [Header("Pause Window")]
    [SerializeField] private GameObject pauseWindow;

    [Header("Setting Windows")]
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private GameObject soundWindow;
    [SerializeField] private GameObject keymapWindow;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        pauseWindow.SetActive(true);
        settingWindow.SetActive(false);
        soundWindow.SetActive(true);
        keymapWindow.SetActive(false);
    }
    public void DisplayCurrentStatus(LevelType levelType)
    {
        if(levelType == LevelType.RunNGunLevel)
        {
            float currentProgress = sideScrollGameController.CheckGoalDistant(progressionBar);
            float currentProgressPercentage = DistanceToPercentage(currentProgress);
            currentProgressIcon.rectTransform.anchoredPosition = new Vector2(currentProgress, currentProgressIcon.rectTransform.anchoredPosition.y); // Update current game progression.
            if(currentProgressPercentage >= 80f)
            {
                progressionText.text = "อีกนิดเดียวเเล้ว!><";
            }
            else if(currentProgressPercentage >= 79f)
            {
                progressionText.text = "ผ่านมาครึ่งทางเเล้ว!><";
            }
            else if(currentProgressPercentage >= 49f)
            {
                progressionText.text = "ขอพักก่อนน้า:D";
            }
        }
        else if(levelType == LevelType.BossLevel)
        {
            float currentProgress = sideScrollGameController.CheckBossProgression(progressionBar);
            float currentProgressPercentage = DistanceToPercentage(currentProgress);
            currentProgressIcon.rectTransform.anchoredPosition = new Vector2(currentProgress, currentProgressIcon.rectTransform.anchoredPosition.y); // Update current game progression.
            if (currentProgressPercentage <= 49f)
            {
                progressionText.text = "ขอพักก่อนน้า:D";
            }
            else if (currentProgressPercentage <= 79f)
            {
                progressionText.text = "หัวหน้าเริ่มเหนื่อยเเล้ว เอามันเลย!><";
            }
            else if (currentProgressPercentage >= 80f)
            {
                progressionText.text = "อีกนิดเดียวเเล้ว!><";
            }
        }
        playerEmotionIcon.sprite = playerHealthDisplay.currentPlayerEmotionIcon; // Update player's emotion icon.
    }
    private float DistanceToPercentage(float currentDistance)
    {
        float result = (currentDistance / progressionBar.rectTransform.sizeDelta.x) * 100;
        return result;
    }
    public void ResumeGame()
    {
        sideScrollGameController.NotifySideScrollGameObserver(SideScrollGameState.Play);
        sideScrollGameController.isPaused = false;
    }
    public void SettingToggle()
    {
        if (settingWindow.activeSelf == true)
        {
            settingWindow.SetActive(false);
            pauseWindow.SetActive(true);
        }
        else
        {
            settingWindow.SetActive(true);
            pauseWindow.SetActive(false);
        }
    }
}
