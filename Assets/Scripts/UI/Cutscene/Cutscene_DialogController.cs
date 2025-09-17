using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Cutscene_DialogController : MonoBehaviour
{
    private bool finishedDialog;
    private int dialogSeqNumber = 0;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private float dialogSpeed = 0.05f;
    [Header("Transition Animation")]
    [SerializeField] private GameObject transitionCanvas;
    [SerializeField] private Animator transitionAnimator;

    [TextArea]
    [SerializeField] private string[] dialogArr;

    private void Start()
    {
        StartCoroutine(CutsceneDialogSequence());  
    }
    public void SkipOutro()
    {
        StartCoroutine(PlaySkipOutroSequence());
    }
    private IEnumerator PlaySkipOutroSequence()
    {
        transitionCanvas.SetActive(true);
        transitionAnimator.SetFloat("FadeVariant", 1);
        transitionAnimator.SetInteger("Transition", 0);
        transitionAnimator.SetBool("IsISO", false);
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator CutsceneDialogSequence()
    {
        // Play dialog sequence
        if (dialogSeqNumber < dialogArr.Length)
        {
            StartCoroutine(TypeWriterAnimation(dialogArr[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            yield return new WaitForSeconds(5f);
            dialogSeqNumber++;
            StartCoroutine(CutsceneDialogSequence());

        }
        // stop dialog sequence when npc ran through all dialogs in the list
        else
        {
            transitionCanvas.SetActive(true);
            transitionAnimator.SetFloat("FadeVariant", 1);
            transitionAnimator.SetInteger("Transition", 0);
            transitionAnimator.SetBool("IsISO", false);
            yield return new WaitForSeconds(1.5f);
            
            SceneManager.LoadScene("MainMenu");
        }
    }
    private IEnumerator TypeWriterAnimation(string dialog)
    {
        finishedDialog = false;
        float timeBtwChar = dialogSpeed; // Time between character
        dialogText.text = ""; // Clear all previous text
        foreach (char line in dialog.ToCharArray())
        {
            dialogText.text += line; // Add a character
            yield return new WaitForSeconds(timeBtwChar);
        }
        finishedDialog = true;
    }
}
