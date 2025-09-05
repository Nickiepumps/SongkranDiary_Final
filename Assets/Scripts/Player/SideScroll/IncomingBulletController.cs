using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingBulletController : MonoBehaviour
{
    [Header("Enemy bullet pooler script reference")]
    [SerializeField] private EnemyBulletPooler enemyBulletPooler;
    [Header("Bullet warning display script reference")]
    public BulletWarningDisplay bulletwarningDisplay;
    [Header("Camera parent and prefabs")]
    [SerializeField] private Transform camParent;
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private GameObject bulletSpawnPrefab;
    [Header("Properties")]
    public float fireCooldown = 10f;
    [SerializeField] private Transform[] incomingBulletSpawnerArr;
    [SerializeField] private IncomingBulletPatternList incomingBulletPatternList = new IncomingBulletPatternList();
    [HideInInspector] public int startPatternRange;
    [HideInInspector] public int endPatternRange;
    public bool startRainingBullet = false;
    private float currentCooldown;
    private int pattern;
    private bool startWarning = false;
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        currentCooldown = fireCooldown;
        startRainingBullet = false;
    }
    private void Start()
    {
        currentCooldown = fireCooldown;
    }
    private void Update()
    {
        if(startRainingBullet == true)
        {
            currentCooldown -= Time.deltaTime;
        }
        if(currentCooldown <= 2f && currentCooldown > 0)
        {
            if(startWarning == false)
            {
                startWarning = true;
                pattern = Random.Range(startPatternRange, endPatternRange);
                for(int i = 0; i < incomingBulletPatternList.PatternList[pattern].incomingBullet.Count; i++)
                {
                    bulletwarningDisplay.ShowBulletWarningSign(incomingBulletPatternList.PatternList[pattern].incomingBullet[i].transform, i);
                }
            }
        }
        else if(currentCooldown <= 0)
        {
            // Hide warning sign and shoot the bullet
            ShootIncomingBullet();
            currentCooldown = fireCooldown;
        }
    }
    private void ShootIncomingBullet()
    {
        for(int i = 0; i < incomingBulletPatternList.PatternList[pattern].incomingBullet.Count; i++)
        {
            GameObject bullet = enemyBulletPooler.EnableIncomingBullet();
            bullet.GetComponent<EnemyBullet>().bulletDirection = incomingBulletPatternList.PatternList[pattern].incomingBullet[i].transform.localRotation * Vector2.up;
            bullet.transform.position = incomingBulletPatternList.PatternList[pattern].incomingBullet[i].transform.position;
            bullet.transform.localRotation = incomingBulletPatternList.PatternList[pattern].incomingBullet[i].transform.localRotation;
            bullet.SetActive(true);
        }
        bulletwarningDisplay.DisableWarningSign();
        startWarning = false;
    }
}

#region Bullet Pattern
[System.Serializable]
public class IncomingBulletPattern
{
    public List<GameObject> incomingBullet;
}
[System.Serializable]
public class IncomingBulletPatternList
{
    public List<IncomingBulletPattern> PatternList;
}
#endregion
