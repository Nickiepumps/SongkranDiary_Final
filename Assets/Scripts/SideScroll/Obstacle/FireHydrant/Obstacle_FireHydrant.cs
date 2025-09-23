using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_FireHydrant : MonoBehaviour
{
    [Header("Fire Hydrant GameObject")]
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject waterSplash;
    [SerializeField] private Animator waterAnimator;
    [SerializeField] private BoxCollider2D waterTrigger;

    [Header("Properties")]
    public bool isShootingIndefinitely; // Is true, The water will continue shooting forever until the player shoot the switch
    [SerializeField] private bool isMovingPipe = false; // Can this pipe move
    public GameObject waterStopper; // Use this when The player need to shoot the lever to stop the hydrant from shooting the water
    public float waterCooldownTime = 3f; // normal cooldown
    public float waterInitialCooldownTime = 3f; // Cooldown time when first start the game
    private float currentCooldownTime;
    public bool isShoot = false;
    public bool usedByBoss = false;
    [SerializeField] private float waterSpriteAnimVariant = 0f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform[] moveTargetArr;
    private Transform currentTarget;

    [Header("Audio reference")]
    [SerializeField] private AudioSource waterpipeAudioSource;
    [SerializeField] private AudioClip[] waterPipeAudioClipArr;
    private bool isPlayingWarningSound = false;
    private void OnEnable()
    {
        waterAnimator.enabled = true;
        currentCooldownTime = waterInitialCooldownTime;
        if(waterpipeAudioSource.enabled == true)
        {
            waterpipeAudioSource.clip = waterPipeAudioClipArr[0];
            waterpipeAudioSource.loop = true;
            waterpipeAudioSource.Play();
        }
    }
    private void OnDisable()
    {
        waterpipeAudioSource.enabled = false;
        waterAnimator.enabled = false;
    }
    private void Start()
    {
        waterTrigger.enabled = false;
        currentCooldownTime = waterInitialCooldownTime;
        waterAnimator.SetBool("WaterIdle", true);
        waterAnimator.SetFloat("Variant", waterSpriteAnimVariant);

        if(isShootingIndefinitely == true)
        {
            isShoot = true;
            waterTrigger.enabled = true;
            waterAnimator.SetBool("ShootInfinitely", true);
        }
    }
    private void Update()
    {
        if(isShootingIndefinitely == false)
        {
            currentCooldownTime -= Time.deltaTime;
            if (currentCooldownTime <= 1 && currentCooldownTime > 0)
            {
                if(isPlayingWarningSound == false && waterpipeAudioSource.enabled == true && usedByBoss == false)
                {
                    isPlayingWarningSound = true;
                    waterpipeAudioSource.clip = waterPipeAudioClipArr[1];
                    waterpipeAudioSource.loop = true;
                    waterpipeAudioSource.Play();
                }
                waterAnimator.SetBool("WaterWarning", true);
                waterAnimator.SetBool("WaterIdle", false);
                waterAnimator.SetBool("WaterShoot", false);
            }
            if (currentCooldownTime <= 0 && isShoot == false)
            {
                isPlayingWarningSound = false;
                if(usedByBoss == false)
                {
                    StartCoroutine(WaterShootAnim());
                    currentCooldownTime = waterCooldownTime;
                }
                else
                {
                    StartCoroutine(WaterShoot_Boss());
                }
            }
        }
        if (isMovingPipe == true)
        {
            MovePipe(moveTargetArr[0], moveTargetArr[1]);
        }
    }
    private IEnumerator WaterShootAnim()
    {
        isShoot = true;
        waterTrigger.enabled = true;
        waterAnimator.SetBool("WaterShoot", true);
        waterAnimator.SetBool("WaterIdle", false);
        waterAnimator.SetBool("WaterWarning", false);
        if(waterpipeAudioSource.enabled == true)
        {
            waterpipeAudioSource.clip = waterPipeAudioClipArr[2];
            waterpipeAudioSource.loop = false;
            waterpipeAudioSource.Play();
        }
        yield return new WaitForSeconds(0.83f);
        isShoot = false;
        waterTrigger.enabled = false;
        waterAnimator.SetBool("WaterIdle", true);
        waterAnimator.SetBool("WaterWarning", false);
        waterAnimator.SetBool("WaterShoot", false);
        if(waterpipeAudioSource.enabled == true && waterpipeAudioSource.enabled == true)
        {
            waterpipeAudioSource.clip = waterPipeAudioClipArr[0];
            waterpipeAudioSource.loop = true;
            waterpipeAudioSource.Play();
        }
    }

    // For Elephant Kid Boss
    private IEnumerator WaterShoot_Boss()
    {
        isShoot = true;
        waterTrigger.enabled = true;
        waterAnimator.SetBool("WaterShoot", true);
        waterAnimator.SetBool("WaterIdle", false);
        waterAnimator.SetBool("WaterWarning", false);
        if (waterpipeAudioSource.enabled == true)
        {
            waterpipeAudioSource.clip = waterPipeAudioClipArr[2];
            waterpipeAudioSource.loop = false;
            waterpipeAudioSource.Play();
        }
        yield return new WaitForSeconds(0.83f);
        isShoot = false;
        waterTrigger.enabled = false;
        waterAnimator.SetBool("WaterIdle", true);
        waterAnimator.SetBool("WaterWarning", false);
        waterAnimator.SetBool("WaterShoot", false);
        waterpipeAudioSource.Stop();
        gameObject.SetActive(false);
    }
    private void MovePipe(Transform pointA, Transform pointB)
    { 
        if (Vector2.Distance(transform.position, pointB.position) <= 0.05f)
        {
            currentTarget = pointA;
        }
        else if (Vector2.Distance(transform.position, pointA.position) <= 0.05f)
        {
            currentTarget = pointB;
        }
        Vector2 dir = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
        transform.position = dir;
    }
}
