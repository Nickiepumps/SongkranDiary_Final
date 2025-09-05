using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideScrollGameController : GameSubject
{
    [Header("Side Scroll Game Mode")]
    [SerializeField] private GameType gameMode;
    [Header("Side Scroll Player Properties")]
    public PlayerSideScrollStateController sidescrollPlayer;
    [Header("Side Scroll Intro Reference")]
    [SerializeField] private SideScrollIntro sideScrollIntro;
    [Header("Observer References")]
    [SerializeField] private PlayerSubject sidescrollPlayerSubject;

    [Header("Side Scroll Game Properties")]
    [Header("Run n Gun Mode Properties")]
    [Header("Start and Goal Position")]
    [SerializeField] private Transform startPos; // Start pos in the world
    [SerializeField] private Transform goalPos; // Goal pos in the world
    [SerializeField] private int totalCoinAmount; // All call amount in the map
    [SerializeField] private SideScroll_Coin[] coinArr; // All coin in the map

    [Header("Boss Mode Properties")]
    //[SerializeField] BossHealthObserver bossHP;
    [SerializeField] BossHealth bossHP;

    [Header("Album Properties")]
    [Header("Side SCroll Album Image Reward")]
    [SerializeField] private AlbumSO albumImageSO;
    [Header("Side SCroll Album Controller")]
    [SerializeField] private SideScroll_AlbumController sideScroll_AlbumController;

    [HideInInspector] public bool isPaused = false;
    private float timer;
    private int minute;
    private int second;
    public string currentTime;
    public int coinCounter;
    private void Start()
    {
        if(gameMode != GameType.Tutorial && gameMode != GameType.Isometric)
        {
            StageClearData stageData = SideScroll_StageClearDataHandler.instance.LoadSideScrollStageClear();
            if (stageData != null)
            {
                for (int i = 0; i < coinArr.Length; i++)
                {
                    for (int j = 0; j < stageData.coinIDLists.Count; j++)
                    {
                        if (coinArr[i].coinID == stageData.coinIDLists[j])
                        {
                            coinArr[i].gameObject.SetActive(false);
                            break;
                        }
                    }
                }
            }
        }
    }
    private void Update()
    {
        if (sidescrollPlayer.isDead == false && sidescrollPlayer.isWinRunNGun == false && gameMode == GameType.RunNGun)
        {
            CountTime();
        }
        else if(sidescrollPlayer.isDead == false && sidescrollPlayer.isWinBoss == false && gameMode == GameType.Boss)
        {
            CountTime();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && sideScrollIntro.finishIntro == true)
        {
            if(isPaused == false)
            {
                NotifySideScrollGameObserver(SideScrollGameState.Paused);
                isPaused = true;
            }
            else
            {
                NotifySideScrollGameObserver(SideScrollGameState.Play);
                isPaused = false;
            }   
        }
    }
    public void AddCoin()
    {
        coinCounter++;
    }
    public int UpdatePlayerHPCount()
    {
        int healthCounter;
        healthCounter = sidescrollPlayer.playerCurrentHP;
        return healthCounter;
    }
    public float CheckGoalDistant(Image distanceUI)
    {
        // Find World distance Percentage between player's pos and goal's pos
        float result;
        float totalDistance = Vector2.Distance(startPos.position, goalPos.position); // World total distance
        float currentDistance = Vector2.Distance(sidescrollPlayer.transform.position, startPos.position); // World current distance
        float worldDistantPercentage = (currentDistance / totalDistance) * 100; // Convert the world distance values to percentage
        result = (worldDistantPercentage/100) * distanceUI.rectTransform.sizeDelta.x; // Using the worldDistantPercentage to calculate the percentage of UI progress bar width
        return result; // Output the position value as a result
    }
    public float CheckBossProgression(Image distanceUI)
    {
        // Find percentage of boss hp and convert it into a progress bar result
        float result;
        float bossCurrentHealth = bossHP.currentBossHP;
        float healthPercentage = (bossCurrentHealth / bossHP.bossMaxHP) * 100;
        result = (healthPercentage / 100) * distanceUI.rectTransform.sizeDelta.x;
        return distanceUI.rectTransform.sizeDelta.x - result;
    }
    private void CountTime()
    {
        timer += Time.deltaTime;
        minute = Mathf.FloorToInt(timer / 60);
        second = Mathf.FloorToInt(timer - (minute * 60));
        currentTime = string.Format("{0:00}:{1:00}", minute, second); // Timer format
    }
    public string Result()
    {
        // To Do: requirement are different depending on type of gamemode
        string grade;
        if(gameMode == GameType.RunNGun && sidescrollPlayer.playerCurrentHP >= 3 && minute <= 2 && coinCounter == totalCoinAmount)
        {
            grade = "A";
            AlbumDataHandler.instance.SaveAlbumData(albumImageSO); // Unlock the album image if Player got A rank
            PlayerDataHandler.instance.UpdatePlayerData(coinCounter);
        }
        else if(gameMode == GameType.Boss && sidescrollPlayer.playerCurrentHP >= 3 && minute <= 2)
        {
            grade = "A";
            AlbumDataHandler.instance.SaveAlbumData(albumImageSO); // Unlock the album image if Player got A rank
        }
        else
        {
            PlayerDataHandler.instance.UpdatePlayerData(coinCounter);
            grade = "B";
        }
        return grade;
    }
}
