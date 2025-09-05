using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour, IGameObserver, IPlayerObserver
{
    [Header("Level Type")]
    [SerializeField] private LevelType levelType;

    [Header("Observer References")]
    [SerializeField] private GameSubject gameControllerSubject; // Game controller subject
    [SerializeField] private GameSubject gameUIControllerSubject; // Game UI Controller subject
    [SerializeField] private GameSubject goalSubject; // Run n Gun destination subject
    [SerializeField] private PlayerSubject ISOPlayerSubject; // Isometric player subject
    [SerializeField] private GameSubject sideScrollIntroSubject; // Side scroll intro/outro subject

    [Header("Game Controller Reference")]
    [SerializeField] private SideScrollGameController sidescrollGameController;

    [Header("Enemy Spawn Controller Reference")]
    [SerializeField] private NormalEnemySpawnerController enemySpawnController;
    [Space]
    [Space]
    [Header("Windows UI References")]
    [Header("Isometric mode")]
    [Space]
    [Space]
    [Header("Diary Audio")]
    public AudioSource diaryAudioPlayer;
    public AudioClip[] diaryAudioClipArr;
    [Header("Player HUD")]
    [SerializeField] private GameObject ISOPlayerHud; // Isometric player HUD
    [SerializeField] private GameObject diaryNotifImage; // Diary notification image
    [Header("Reward Window")]
    [SerializeField] private GameObject rewardWindow; // Player reward window
    [Header("Diary Main Window")]
    [SerializeField] private GameObject diaryMainWindow; // Player upgrade window
    [Header("Upgrade Window")]
    [SerializeField] private GameObject upgradeWindow; // Player upgrade window
    [Header("Album Window")]
    [SerializeField] private GameObject albumWindow; // Player album window
    [Header("Postcard Window")]
    [SerializeField] private GameObject postcardWindow; // Postcard window
    [Space]
    [Space]
    [Header("Side-Scroll mode")]
    [Header("Scoreboard Window")]
    [SerializeField] private GameObject scoreBoard;
    [SerializeField] private GameObject scoreBoardBG;
    [SerializeField] private GameObject winScoreBoard;
    [SerializeField] private GameObject loseScoreBoard;
    [SerializeField] private Image scoreBoardDistantUI;
    [SerializeField] private Image currentProgreesionIcon;
    [SerializeField] private TMP_Text loseText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private TMP_Text coinAmountText;
    [Space]
    [Header("Pause Window")]
    [SerializeField] private GameObject pauseWindow; // Pause window
    [Space]
    [Header("Side Scroll Outro Window")]
    [SerializeField] private SideScrollIntro sideScrollIntroWindow;
    [Space]
    [Header("Tutorial Exit Window")]
    [SerializeField] private GameObject tutorialExitWindow;
    [Space]
    [Header("Transition Window")]
    [SerializeField] private GameObject transitionWindow;
    [SerializeField] private Animator transitionAnimator;

    [SerializeField] private GameObject[] allISOWindowArr;
    [HideInInspector] public bool isNPCTalking = false;
    private void OnEnable()
    {
        if (levelType == LevelType.IsoLevel)
        {
            gameUIControllerSubject.AddGameObserver(this);
            //gameControllerSubject.AddGameObserver(this);
            ISOPlayerSubject.AddPlayerObserver(this);
        }
        else if(levelType == LevelType.RunNGunLevel || levelType == LevelType.TutorialLevel)
        {
            gameUIControllerSubject.AddGameObserver(this);
            gameUIControllerSubject.AddSideScrollGameObserver(this);
            gameControllerSubject.AddGameObserver(this);
            gameControllerSubject.AddSideScrollGameObserver(this);
            goalSubject.AddSideScrollGameObserver(this);
            sideScrollIntroSubject.AddSideScrollGameObserver(this);
        }
        else
        {
            gameUIControllerSubject.AddGameObserver(this);
            gameControllerSubject.AddGameObserver(this);
            gameControllerSubject.AddSideScrollGameObserver(this);
            gameUIControllerSubject.AddSideScrollGameObserver(this);
            sideScrollIntroSubject.AddSideScrollGameObserver(this);
        }
    }
    private void OnDisable()
    {
        if (levelType == LevelType.IsoLevel)
        {
            gameUIControllerSubject.RemoveGameObserver(this);
            //gameControllerSubject.RemoveGameObserver(this);
            ISOPlayerSubject.RemovePlayerObserver(this);
        }
        else if (levelType == LevelType.RunNGunLevel)
        {
            gameUIControllerSubject.RemoveGameObserver(this);
            gameUIControllerSubject.RemoveSideScrollGameObserver(this);
            gameControllerSubject.RemoveSideScrollGameObserver(this);
            goalSubject.RemoveSideScrollGameObserver(this);
            sideScrollIntroSubject.RemoveSideScrollGameObserver(this);
        }
        else
        {
            gameUIControllerSubject.RemoveGameObserver(this);
            gameUIControllerSubject.RemoveSideScrollGameObserver(this);
            gameControllerSubject.RemoveSideScrollGameObserver(this);
            sideScrollIntroSubject.RemoveSideScrollGameObserver(this);
        }
    }
    private void Update()
    {
        if(levelType == LevelType.IsoLevel)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isNPCTalking == false && CheckAnyOpenedISOWindow() == false)
            {
                pauseWindow.SetActive(true);
                GetComponent<IsometricGameObserverContoller>().NotifyGameObserver(IsometricGameState.Paused);
            }
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        switch (isoGameState)
        {
            case (IsometricGameState.Play):
                ISOPlayerHud.SetActive(true);
                return;
            case (IsometricGameState.Paused):
                ISOPlayerHud.SetActive(false);
                return;
            case (IsometricGameState.Reward):
                rewardWindow.SetActive(true);
                return;
        }
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        switch (sidescrollGameState)
        {
            case(SideScrollGameState.Play):
                pauseWindow.SetActive(false);
                sidescrollGameController.sidescrollPlayer.playerAnimator.enabled = true;
                sidescrollGameController.sidescrollPlayer.playerBulletShooting.enabled = true;
                sidescrollGameController.sidescrollPlayer.enabled = true;
                Time.timeScale = 1; // To Do: Find a better way to pause the game
                return;
            case (SideScrollGameState.StartRound):
                return;
            case (SideScrollGameState.Paused):
                pauseWindow.SetActive(true);
                pauseWindow.GetComponent<PauseWindow>().DisplayCurrentStatus(levelType);
                Time.timeScale = 0; // To Do: Find a better way to pause the game
                return;
            case (SideScrollGameState.FinishTutorial):
                //OpenTutorialWindow();
                sidescrollGameController.sidescrollPlayer.playerAnimator.enabled = false;
                sidescrollGameController.sidescrollPlayer.playerBulletShooting.enabled = false;
                sidescrollGameController.sidescrollPlayer.enabled = false;
                tutorialExitWindow.SetActive(true);
                return;
            case (SideScrollGameState.WinRunNGun):
                StartCoroutine(ReachGoal());
                return;
            case (SideScrollGameState.WinBoss):
                StartCoroutine(BossKnockOut());
                return;
            case (SideScrollGameState.Lose):
                // show lose scoreboard
                scoreBoard.SetActive(true);
                winScoreBoard.SetActive(false);
                if(levelType == LevelType.RunNGunLevel)
                {
                    Vector2 progressValue = new Vector2(sidescrollGameController.CheckGoalDistant(scoreBoardDistantUI),
                        currentProgreesionIcon.rectTransform.anchoredPosition.y);
                    StartCoroutine(StartPlayerLoseProgression(progressValue));
                    enemySpawnController.enabled = false;
                }
                else if(levelType == LevelType.BossLevel)
                {
                    Vector2 progressValue = new Vector2(sidescrollGameController.CheckBossProgression(scoreBoardDistantUI),
                        currentProgreesionIcon.rectTransform.anchoredPosition.y);
                    StartCoroutine(StartPlayerLoseProgression(progressValue));
                }
                loseScoreBoard.SetActive(true);
                return;
        }
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case (PlayerAction.Idle):
                if (ISOPlayerSubject.GetComponent<PlayerStats>().coinAmount != 0)
                {
                    diaryNotifImage.SetActive(true);
                }
                else
                {
                    diaryNotifImage.SetActive(false);
                }
                isNPCTalking = false;
                return;
            case (PlayerAction.Talk):
                ISOPlayerHud.SetActive(false);
                isNPCTalking = true;
                return;
        }
    }
    private bool CheckAnyOpenedISOWindow()
    {
        foreach(GameObject window in allISOWindowArr)
        {
            if(window.activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }
    public void OpenDiaryWindow()
    {
        upgradeWindow.SetActive(false);
        albumWindow.SetActive(false);
    }
    public void CloseDiaryWindow()
    {
        albumWindow.SetActive(false);
        upgradeWindow.SetActive(true);
        OpenWindow(diaryMainWindow);
    }
    public void OpenTutorialWindow()
    {
        if(tutorialExitWindow.activeSelf == true)
        {
            tutorialExitWindow.SetActive(false);
            sidescrollGameController.NotifySideScrollGameObserver(SideScrollGameState.Play);
        }
        else
        {
            tutorialExitWindow.SetActive(true);
        }
    }
    public void OpenWindow(GameObject windowObj)
    {
        // Use for open any window in Isometric mode
        if(windowObj.activeSelf == true)
        {
            windowObj.SetActive(false);
            if (isNPCTalking == false)
            {
                GetComponent<IsometricGameObserverContoller>().NotifyGameObserver(IsometricGameState.Play);
            }
        }
        else
        {
            windowObj.SetActive(true);
            if(isNPCTalking == false)
            {
                GetComponent<IsometricGameObserverContoller>().NotifyGameObserver(IsometricGameState.Paused);
            }
        }
    }
    public IEnumerator OpenObjective()
    {
        yield return null;
    }
    public IEnumerator StartTransitionISOScene(Transform startPos)
    {
        Time.timeScale = 1; // Reset timescale to 1 in case the player paused the game
        transitionWindow.SetActive(true);
        transitionAnimator.SetInteger("Transition", 0);
        transitionAnimator.SetBool("IsISO", true);
        yield return new WaitForSeconds(0.5f);
        ISOPlayerSubject.gameObject.transform.position = startPos.position;
        postcardWindow.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        transitionAnimator.SetInteger("Transition", 1);
        transitionAnimator.SetBool("IsISO", true);
        GetComponent<IsometricGameObserverContoller>().NotifyGameObserver(IsometricGameState.Play);
        yield return new WaitForSeconds(0.2f);
        transitionWindow.SetActive(false);
    }
    private IEnumerator StartPlayerLoseProgression(Vector2 progressValue)
    {
        loseText.GetComponent<Animation>().Play();
        yield return new WaitUntil(() => loseText.GetComponent<Animation>().isPlaying == false);
        scoreBoardBG.SetActive(true);
        loseScoreBoard.SetActive(true);
        while(currentProgreesionIcon.rectTransform.anchoredPosition.x < progressValue.x)
        {
            currentProgreesionIcon.rectTransform.anchoredPosition = new Vector2(currentProgreesionIcon.rectTransform.anchoredPosition.x + 1, currentProgreesionIcon.rectTransform.anchoredPosition.y);
            yield return null;
        }
        if(currentProgreesionIcon.rectTransform.anchoredPosition.x > progressValue.x)
        {
            currentProgreesionIcon.rectTransform.anchoredPosition = progressValue;
        }
        yield return null;
    }
    private IEnumerator ReachGoal()
    {
        // Wait for player to finish win animation
        //yield return new WaitForSeconds(1f); Uncomment this when we have player win anim
        scoreBoardBG.SetActive(true);
        sideScrollIntroWindow.reachGoalGroup.SetActive(true);
        StartCoroutine(sideScrollIntroWindow.StartReachGoalAnimation());
        yield return new WaitUntil(() => sideScrollIntroWindow.finishCoroutine == true); // Change this when we have player win anim
        transitionWindow.SetActive(true);
        transitionWindow.GetComponentInChildren<Animator>().SetInteger("Transition", 0);
        yield return new WaitForSeconds(1f);
        transitionWindow.GetComponentInChildren<Animator>().SetInteger("Transition", 1);

        // Show win scoreboard
        enemySpawnController.enabled = false;
        scoreBoard.SetActive(true);
        winScoreBoard.SetActive(true);
        scoreBoardBG.SetActive(true);
        loseScoreBoard.SetActive(false);
        timerText.text = sidescrollGameController.currentTime;
        hpText.text = sidescrollGameController.UpdatePlayerHPCount().ToString();
        coinAmountText.text = sidescrollGameController.coinCounter.ToString();
        gradeText.text = sidescrollGameController.Result();
        yield return null;
    }
    private IEnumerator BossKnockOut()
    {
        sideScrollIntroWindow.knouckOutGroup.SetActive(true);
        StartCoroutine(sideScrollIntroWindow.StartKnockOutWipeTransition());
        yield return new WaitUntil(() => sideScrollIntroWindow.finishCoroutine == true);
        // Start transition window
        transitionWindow.SetActive(true);
        transitionWindow.GetComponentInChildren<Animator>().SetInteger("Transition", 0);
        yield return new WaitForSeconds(1f);
        transitionWindow.GetComponentInChildren<Animator>().SetInteger("Transition", 1);
        // Show win scoreboard
        scoreBoard.SetActive(true);
        winScoreBoard.SetActive(true);
        scoreBoardBG.SetActive(true);
        loseScoreBoard.SetActive(false);
        timerText.text = sidescrollGameController.currentTime;
        hpText.text = sidescrollGameController.UpdatePlayerHPCount().ToString();
        gradeText.text = sidescrollGameController.Result();
        yield return null;
    }
}
