using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SideScrollIntro : GameSubject
{
    [Header("Intro Properties")]
    [SerializeField] private GameObject introGroup;
    [SerializeField] private RectMask2D introWipeMask;
    private float wipeMaskPaddingValue;
    [SerializeField] private TMP_Text introText;
    [SerializeField] private Image paperBG;
    [SerializeField] private Animation closingAnim;
    [SerializeField] private float introTransitionTime;
    [Header("Knouck out Properties")]
    public GameObject knouckOutGroup;
    [SerializeField] private Animation knockoutAnim;
    //[SerializeField] private RectMask2D knockOutwipeMask;
    //private float knockOutwipeMaskPaddingValue;
    [SerializeField] private Image knockOutImage;
    [SerializeField] private float knockOutTransitionTime;
    [Header("Reach Goal Properties")]
    public GameObject reachGoalGroup;
    [SerializeField] private Animation reachGoalAnim;
    [HideInInspector] public bool finishCoroutine = false;
    [HideInInspector] public bool finishIntro = false;

    private void Start()
    {
        wipeMaskPaddingValue = introWipeMask.GetComponent<RectTransform>().rect.width;
        //knockOutwipeMaskPaddingValue = knockOutImage.GetComponent<RectTransform>().rect.width;
        StartCoroutine(StartIntroWipeTransition());
    }
    private IEnumerator StartIntroWipeTransition()
    {
        finishCoroutine = false;
        closingAnim.Play("IntroOpening");
        float currentWipeValue = wipeMaskPaddingValue;
        yield return new WaitUntil(() => closingAnim.isPlaying == false);
        introText.text = "ลุย!!";
        closingAnim.Play("GoOpening");
        yield return new WaitForSeconds(0.5f);
        finishIntro = true;
        NotifySideScrollGameObserver(SideScrollGameState.StartRound);
        closingAnim.Play("Closing");
        yield return new WaitUntil(() => closingAnim.isPlaying == false);
        finishCoroutine = true;
        introGroup.gameObject.SetActive(false);
    }
    public IEnumerator StartKnockOutWipeTransition()
    {
        finishCoroutine = false;
        knockoutAnim.Play();
        yield return new WaitUntil(() => knockoutAnim.isPlaying == false);
        finishCoroutine = true;
        knouckOutGroup.gameObject.SetActive(false);
    }
    public IEnumerator StartReachGoalAnimation()
    {
        finishCoroutine = false;
        reachGoalAnim.Play();
        yield return new WaitUntil(() => reachGoalAnim.isPlaying == false);
        finishCoroutine = true;
        reachGoalGroup.SetActive(false);
    }
}
