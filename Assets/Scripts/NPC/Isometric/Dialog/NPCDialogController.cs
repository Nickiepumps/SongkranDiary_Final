using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class NPCDialogController : MonoBehaviour, INPCObserver, IPlayerObserver, IGameObserver, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Dialog Type")]
    [SerializeField] private NPCDialogType dialogType;

    [Header("Observer References")]
    [SerializeField] private NPCSubject npcSubject; // NPC
    [SerializeField] private PlayerSubject playerSubject; // Player
    [SerializeField] private GameSubject isometricGameSubject; // Isometric Game subject To Do: find gameSubject object in the scene and reference it when turned into prefab

    [Header("GameUIController Reference")]
    [SerializeField] private GameUIController gameUIController;

    [Header("Audio References")]
    [SerializeField] private AudioSource npcAudioSouce;
    [SerializeField] private AudioClip[] npcDialogClipArr;

    [Header("Dialog box and Notif References")]
    [SerializeField] private Color correctColor;// Correct color (green) 
    [SerializeField] private Color wrongColor; // Correct color (red) 
    [SerializeField] private Color nonSelectColor; // Non-Selected choice color (dark grey) 
    [SerializeField] private GameObject interactNotif; // Interact notification popup
    [SerializeField] private GameObject dialogBox; // NPC dialog box GameObject
    [SerializeField] private Image dialogBoxImg; // NPC dialog box Image
    [SerializeField] private Image[] choiceImgArr; // NPC dialog box Image
    [SerializeField] private TextMeshProUGUI dialogText; // Dialog text
    [SerializeField] private GameObject nextDialogNotif;
    private int dialogSeqNumber = 0;
    private GameObject player; // Player to NPC status

    [Header("Dialog and Choices")]
    [SerializeField] private List<string> dialogLists = new List<string>(); // NPC's normal dialog lists
    [SerializeField] private List<string> correctAnswerDialogLists = new List<string>();
    [SerializeField] private List<string> wrongAnswerDialogLists = new List<string>();
    [SerializeField] private List<string> choiceLists = new List<string>(); // Player's choice iists
    [SerializeField] private string outroDialog;
    [SerializeField] private GameObject choicesGroup; // Player's choices group gameObject
    [SerializeField] private GameObject correctChoice; // Player's correct choice
    [SerializeField] private List<GameObject> choiceButtonLists = new List<GameObject>(); // Player's choices list

    [Header("Reward Window Reference (For NPC with choice only)")]
    [SerializeField] private GameObject rewardWindow;

    [Header("Player Status")]
    [SerializeField] private bool isPlayerAnswered = false;
    public bool isPlayerUpgrade = false;

    [Header("NPC Save ID")]
    [SerializeField] private string npcID;

    private bool finishedDialog = false; // Check status of dialog typewriter effect
    private bool startChoosing = false; // Player's choosing dialogs
    private void OnEnable()
    {
        if(dialogType == NPCDialogType.Choices)
        {
            int choiceTextOrder = 0;
            foreach(GameObject choice in choiceButtonLists)
            {
                choice.GetComponentInChildren<TMP_Text>().text = choiceLists[choiceTextOrder];
                choiceTextOrder++;
            }
        }
        npcSubject.AddNPCObserver(this);
        playerSubject.AddPlayerObserver(this);
        isometricGameSubject.AddGameObserver(this);
    }
    private void OnDisable()
    {
        npcSubject.RemoveNPCObserver(this);
        playerSubject.RemovePlayerObserver(this);
        isometricGameSubject?.RemoveGameObserver(this);
    }
    private void Start()
    {
        ISOStageData isoStageData = ISOStageDataHandler.instance.LoadISOStageData();
        if(isoStageData != null)
        {
            for(int i = 0; i < isoStageData.npcIDLists.Count; i++)
            {
                if (npcID == isoStageData.npcIDLists[i])
                {
                    isPlayerAnswered = true;
                    interactNotif.transform.GetChild(0).gameObject.SetActive(true);
                    interactNotif.transform.GetChild(1).gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
    public void OnNPCNotify(NPCAction npcAction)
    {
        
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        if(player != null)
        {
            switch (playerAction)
            {
                case (PlayerAction.Idle):
                    if (isPlayerAnswered == false)
                    {
                        interactNotif.transform.GetChild(0).gameObject.SetActive(true);
                        interactNotif.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    return;
                case (PlayerAction.Talk):

                    if (dialogType == NPCDialogType.Normal)
                    {
                        interactNotif.SetActive(false);
                        dialogBox.SetActive(true);
                        StartCoroutine(NormalDialogSequence());
                    }
                    else if (isPlayerAnswered == false && dialogType == NPCDialogType.Choices)
                    {
                        interactNotif.SetActive(false);
                        dialogBox.SetActive(true);
                        StartCoroutine(ChoiceDialogSequence());
                    }
                    else
                    {
                        interactNotif.SetActive(false);
                        dialogBox.SetActive(true);
                        StartCoroutine(OutroDialogSequence());
                    }
                    return;
                case (PlayerAction.Walk):
                    interactNotif.transform.GetChild(0).gameObject.SetActive(true);
                    interactNotif.transform.GetChild(1).gameObject.SetActive(false);
                    return;
            }
        }
        else
        {
            if (isPlayerAnswered == false)
            {
                interactNotif.transform.GetChild(0).gameObject.SetActive(false);
                interactNotif.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        switch (isoGameState)
        {
            case (IsometricGameState.Play):
                interactNotif.SetActive(true);
                return;
        }
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {

    }
    public void OnSideScrollGameNotify(IsometricGameState isoGameState)
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        /*if(eventData.pointerEnter.gameObject == choiceButtonLists[0])
        {
            choiceImgArr[0].color = Color.white;
            choiceImgArr[1].color = nonSelectColor;
            choiceImgArr[2].color = nonSelectColor;
        }
        else if(eventData.pointerEnter.gameObject == choiceButtonLists[1])
        {
            choiceImgArr[0].color = nonSelectColor;
            choiceImgArr[1].color = Color.white;
            choiceImgArr[2].color = nonSelectColor;
        }
        else if (eventData.pointerEnter.gameObject == choiceButtonLists[2])
        {
            choiceImgArr[0].color = nonSelectColor;
            choiceImgArr[1].color = nonSelectColor;
            choiceImgArr[2].color = Color.white;
        }*/
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        /*choiceImgArr[0].color = Color.white;
        choiceImgArr[1].color = Color.white;
        choiceImgArr[2].color = Color.white;*/
    }
    private void Update()
    {
        if(dialogType == NPCDialogType.Choices && startChoosing == true)
        {
            ChoosingDialog();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
        if(isPlayerAnswered == false && dialogType == NPCDialogType.Choices)
        {
            interactNotif.transform.GetChild(0).gameObject.SetActive(false);
            interactNotif.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            interactNotif.transform.GetChild(0).gameObject.SetActive(false);
            interactNotif.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    private IEnumerator OutroDialogSequence()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        StartCoroutine(TypeWriterAnimation(outroDialog));
        yield return new WaitUntil(() => finishedDialog == true);
        nextDialogNotif.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space));
        isometricGameSubject.NotifyGameObserver(IsometricGameState.Play);
        dialogBox.SetActive(false);
        interactNotif.SetActive(true);
        yield return null;
    }
    private IEnumerator NormalDialogSequence()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        // Play dialog sequence
        if(dialogSeqNumber < dialogLists.Count)
        {
            StartCoroutine(TypeWriterAnimation(dialogLists[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            nextDialogNotif.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space));
            dialogSeqNumber++;
            StartCoroutine(NormalDialogSequence());
        }
        // stop dialog sequence when npc ran through all dialogs in the list
        else
        {
            isometricGameSubject.NotifyGameObserver(IsometricGameState.Play);
            dialogSeqNumber = 0;
            isPlayerAnswered = true;
            dialogBox.SetActive(false);
            interactNotif.SetActive(true);
            ISOStageDataHandler.instance.UpdateIsoNPCInteraction(npcID);
            yield return null;
        }
    }
    private IEnumerator ChoiceDialogSequence()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        // Play dialog sequence
        if(dialogSeqNumber == dialogLists.Count - 1)
        {
            StartCoroutine(TypeWriterAnimation(dialogLists[dialogSeqNumber].ToString()));
            dialogSeqNumber = 0;
            yield return new WaitUntil(() => finishedDialog == true);
            startChoosing = true;
            choicesGroup.SetActive(true); // Show choices
            yield return null;
        }
        else if (dialogSeqNumber < dialogLists.Count)
        {
            StartCoroutine(TypeWriterAnimation(dialogLists[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            nextDialogNotif.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space));
            dialogSeqNumber++;
            StartCoroutine(ChoiceDialogSequence());
        }
    }
    private IEnumerator CorrectResultDialog()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        choicesGroup.SetActive(false);
        // Play dialog sequence
        if (dialogSeqNumber < correctAnswerDialogLists.Count)
        {
            StartCoroutine(TypeWriterAnimation(correctAnswerDialogLists[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            nextDialogNotif.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space));
            dialogSeqNumber++;
            StartCoroutine(CorrectResultDialog());
        }
        // stop dialog sequence when npc ran through all dialogs in the list
        else
        {
            gameUIController.isNPCTalking = false;
            isometricGameSubject.NotifyGameObserver(IsometricGameState.Paused);
            rewardWindow.SetActive(true);
            dialogSeqNumber = 0;
            choicesGroup.SetActive(false);
            dialogBox.SetActive(false);
            ISOStageDataHandler.instance.UpdateIsoNPCInteraction(npcID);
            yield return null;
        }
    }
    private IEnumerator WrongResultDialog()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        choicesGroup.SetActive(false);
        // Play dialog sequence
        if (dialogSeqNumber < wrongAnswerDialogLists.Count)
        {
            StartCoroutine(TypeWriterAnimation(wrongAnswerDialogLists[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            nextDialogNotif.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space));
            dialogSeqNumber++;
            StartCoroutine(WrongResultDialog());
        }
        // stop dialog sequence when npc ran through all dialogs in the list
        else
        {
            isometricGameSubject.NotifyGameObserver(IsometricGameState.Play); // Notify gamestate observer to change the state to play state
            dialogSeqNumber = 0;
            dialogBox.SetActive(false);
            interactNotif.SetActive(true);
            isPlayerAnswered = false;
            yield return null;
        }
    }
    private void ChoosingDialog()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            /*choiceImgArr[0].color = Color.white;
            choiceImgArr[1].color = nonSelectColor;
            choiceImgArr[2].color = nonSelectColor;
            confirmedChoice = choiceButtonLists[0];*/
            CheckAnswer(choiceButtonLists[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            /*choiceImgArr[0].color = nonSelectColor;
            choiceImgArr[1].color = Color.white;
            choiceImgArr[2].color = nonSelectColor;
            confirmedChoice = choiceButtonLists[1];*/
            CheckAnswer(choiceButtonLists[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            /*choiceImgArr[0].color = nonSelectColor;
            choiceImgArr[1].color = nonSelectColor;
            choiceImgArr[2].color = Color.white;
            confirmedChoice = choiceButtonLists[2];*/
            CheckAnswer(choiceButtonLists[2]);
        }
        /*else if (Input.GetKeyDown(KeyCode.Return) && confirmedChoice != null)
        {
            foreach (Image img in choiceImgArr)
            {
                img.color = Color.white;
            }
            startChoosing = false;
            CheckAnswer(confirmedChoice);
            confirmedChoice = null;
        }*/
    }
    public void CheckAnswer(GameObject choice)
    {
        startChoosing = false;
        if(choice == correctChoice)
        {
            isPlayerAnswered = true;
            npcAudioSouce.clip = npcDialogClipArr[0];
            npcAudioSouce.Play();
            dialogBoxImg.color = correctColor;
            StartCoroutine(AnswerIndicator(correctColor));
            StartCoroutine(CorrectResultDialog());
        }
        else
        {
            isPlayerAnswered = true;
            npcAudioSouce.clip = npcDialogClipArr[1];
            npcAudioSouce.Play();
            dialogBoxImg.color = wrongColor;
            StartCoroutine(AnswerIndicator(wrongColor));
            StartCoroutine(WrongResultDialog());
        }
    }
    private IEnumerator AnswerIndicator(Color color)
    {
        while (dialogBoxImg.color != Color.white)
        {
            dialogBoxImg.color = Vector4.Lerp(dialogBoxImg.color, Color.white, 5 * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator TypeWriterAnimation(string dialog)
    {
        finishedDialog = false;
        float timeBtwChar = 0.05f; // Time between character
        dialogText.text = ""; // Clear all previous text
        foreach(char line in dialog.ToCharArray())
        {
            dialogText.text += line; // Add a character
            yield return new WaitForSeconds(timeBtwChar);
        }
        finishedDialog = true;
    }
}
