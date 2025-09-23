using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public Vector2 ultDirection;
    public float ultTravelSpeed;
    public Animator ultAnimator;
    Camera camera;
    [SerializeField] private AudioSource ultAudioSource;
    [SerializeField] private AudioClip[] ultAudioClipArr;
    private bool isExploding;
    private int maxHit = 3;
    private int currentHIt = 0;
    private void Awake()
    {
        camera = Camera.main;   
    }
    private void OnEnable()
    {
        isExploding = false;
        currentHIt = 0;
        ultAnimator.SetBool("isHit", false);
        ultAudioSource.clip = ultAudioClipArr[0];
        ultAudioSource.Play();
    }
    private void Update()
    {
        Vector2 bulletPosition = transform.position;
        bulletPosition += ultDirection * ultTravelSpeed * Time.deltaTime;
        transform.position = bulletPosition;
        Vector2 bulletPos = camera.WorldToViewportPoint(transform.position);
        if (bulletPos.x < 0f || bulletPos.x > 1f || bulletPos.y < 0f || bulletPos.y > 1f)
        {
            if (isExploding == false)
            {
                StartCoroutine(StartDeactivateUltAnim());
                isExploding = true;
            }
        }
        else if(currentHIt >= maxHit && isExploding == false)
        {
            StartCoroutine(StartDeactivateUltAnim());
            isExploding = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "B_Boundary")
        {
            gameObject.SetActive(false);
        }*/
        if (collision.gameObject.tag == "EnemyHitBox")
        {
            if (currentHIt <= 3 && collision.gameObject.GetComponent<BossHealthObserver>() == null)
            {
                currentHIt++;
                StartCoroutine(StartDeactivateUltAnim());
            }
            else if(collision.gameObject.GetComponent<BossHealthObserver>() != null)
            {
                isExploding = true;
                StartCoroutine(StartDeactivateUltAnim());
            }
        }
    }
    private IEnumerator StartDeactivateUltAnim()
    {
        ultAnimator.SetBool("isHit", true);
        ultAudioSource.clip = ultAudioClipArr[1];
        ultAudioSource.Play();
        yield return new WaitForSeconds(0.8f);
        ultAnimator.SetBool("isHit", false);
        gameObject.SetActive(false);
    }
}
