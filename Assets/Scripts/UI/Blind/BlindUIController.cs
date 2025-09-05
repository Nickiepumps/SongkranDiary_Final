using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindUIController : MonoBehaviour, IPlayerObserver
{
    [Header("Observer References")]
    [SerializeField] private PlayerSubject playerSubject;
    [Header("Blind Animator")]
    [SerializeField] private GameObject blindEffect;
    private void OnEnable()
    {
        playerSubject.AddPlayerObserver(this);
    }
    private void OnDisable()
    {
        playerSubject.RemovePlayerObserver(this);
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case(PlayerAction.Damaged):
                return;
            case (PlayerAction.Blind):
                StartCoroutine(Blind());
                return;
        }
    }
    private IEnumerator Blind()
    {
        blindEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        blindEffect.SetActive(false);
        yield return null;
    }
}
