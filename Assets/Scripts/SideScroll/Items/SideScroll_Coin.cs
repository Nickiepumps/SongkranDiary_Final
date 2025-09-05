using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_Coin : MonoBehaviour
{
    [Header("Coin Components")]
    [SerializeField] private AudioSource coinAudioPlayer;
    [SerializeField] private SpriteRenderer coinSpriteRenderer;
    [Header("Side-Scroll Game Controller")]
    [SerializeField] private SideScrollGameController sidescrollGameController;
    [Header("Stage Clear Save System")]
    [SerializeField] private SideScroll_StageClearDataHandler stageClearDataHandler;
    [Header("Properties")]
    public string coinID;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sidescrollGameController.AddCoin();
            stageClearDataHandler.coinIDLists.Add(coinID);
            StartCoroutine(CollectCoin());
        }
    }
    private IEnumerator CollectCoin()
    {
        coinSpriteRenderer.enabled = false;
        coinAudioPlayer.Play();
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
