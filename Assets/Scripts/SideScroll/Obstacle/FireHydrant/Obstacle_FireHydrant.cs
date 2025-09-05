using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform[] moveTargetArr;
    private Transform currentTarget;
    private void OnEnable()
    {
        waterAnimator.enabled = true;
        currentCooldownTime = waterInitialCooldownTime;
    }
    private void OnDisable()
    {
        waterAnimator.enabled = false;
    }
    private void Start()
    {
        //originalWaterYScale = water.transform.localScale.y;
        waterTrigger.enabled = false;
        currentCooldownTime = waterInitialCooldownTime;
        //currentHoldTime = waterHoldTime;
        waterAnimator.SetBool("WaterIdle", true);
    }
    private void Update()
    {
        if(isShootingIndefinitely == false)
        {
            currentCooldownTime -= Time.deltaTime;
            if (currentCooldownTime <= 1 && currentCooldownTime > 0)
            {
                waterAnimator.SetBool("WaterWarning", true);
                waterAnimator.SetBool("WaterIdle", false);
                waterAnimator.SetBool("WaterShoot", false);
            }
            if (currentCooldownTime <= 0 && isShoot == false)
            {
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
        else
        {
            isShoot = true;
            waterTrigger.enabled = true;
            waterAnimator.SetBool("ShootInfinitely", true);
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
        yield return new WaitForSeconds(0.83f);
        isShoot = false;
        waterTrigger.enabled = false;
        waterAnimator.SetBool("WaterIdle", true);
        waterAnimator.SetBool("WaterWarning", false);
        waterAnimator.SetBool("WaterShoot", false);
    }
    private IEnumerator WaterShoot_Boss()
    {
        isShoot = true;
        waterTrigger.enabled = true;
        waterAnimator.SetBool("WaterShoot", true);
        waterAnimator.SetBool("WaterIdle", false);
        waterAnimator.SetBool("WaterWarning", false);
        yield return new WaitForSeconds(0.83f);
        isShoot = false;
        waterTrigger.enabled = false;
        waterAnimator.SetBool("WaterIdle", true);
        waterAnimator.SetBool("WaterWarning", false);
        waterAnimator.SetBool("WaterShoot", false);
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
