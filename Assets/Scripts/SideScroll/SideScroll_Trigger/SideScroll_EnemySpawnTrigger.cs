using UnityEngine;
public class SideScroll_EnemySpawnTrigger : SideScroll_TriggerEvent
{
    [SerializeField] private NormalEnemySpawnerController normalEnemySpawnController;
    [SerializeField] private NormalEnemyType enemyType;
    [SerializeField] private bool startSpawn = true;
    private bool isTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && isTriggered == false)
        {
            isTriggered = true;
            if (startSpawn == true)
            {
                Trigger_StartSpawnEnemy(enemyType, normalEnemySpawnController);
            }
            else
            {
                Trigger_StopSpawnEnemy(normalEnemySpawnController);
            }
        }
    }
}
