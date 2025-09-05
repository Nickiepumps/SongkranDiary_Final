using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_BoostPad : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator boostPadAnimator;

    [Header("Properties")]
    [SerializeField] private float boostCooldownTime = 3f;
    [SerializeField] private float boostAmount = 10f;
    private float currentBoostCooldownTime;
    private Rigidbody2D playerRB;
    private bool isLaunch = false;
    private void Start()
    {
        currentBoostCooldownTime = boostCooldownTime;
    }
    private void Update()
    {
        currentBoostCooldownTime -= Time.deltaTime;
        if(currentBoostCooldownTime <= 0 && isLaunch == false)
        {
            if (playerRB != null)
            {
                playerRB.AddForce(Vector2.up * boostAmount, ForceMode2D.Impulse);
            }
            StartCoroutine(PlayLaunchAnim());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerRB = collision.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerRB = null;
        }
    }
    private IEnumerator PlayLaunchAnim()
    {
        isLaunch = true;
        boostPadAnimator.SetBool("isLaunch", true);
        yield return new WaitForSeconds(0.6f);
        isLaunch = false;
        currentBoostCooldownTime = boostCooldownTime;
        boostPadAnimator.SetBool("isLaunch", false);
    }
}
