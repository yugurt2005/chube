using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Difficulty difficulty;

    public GameObject prefabEnemy;
    //public GameObject prefabArcherEnemy;
    private float countdown = 5f;
    public float spawnSpeed = 5f;

    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public DebrisMovement edgeSpawner;


    public int enemyTypeProbability;

    void Update()
    {
        spawnSpeed = 50 / difficulty.difficultyMultiplier;

        if (countdown <= 0)
        {
            // uses debris's random perimeter position generator function
            Vector3 spawnPos = edgeSpawner.getPositionOnPerimeter();

            GameObject enemyObj = (GameObject)Instantiate(prefabEnemy, spawnPos, transform.rotation);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.onInstantiate(tilemap, tilemapRenderer); //j

            /*enemyTypeProbability = Random.Range(0, 100);
            if (enemyTypeProbability >= 0 & enemyTypeProbability < 50)
            {
                GameObject enemyObj = (GameObject)Instantiate(prefabEnemy, spawnPos, transform.rotation);
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                enemy.onInstantiate(tilemap, tilemapRenderer); //j
            }

            if (enemyTypeProbability >= 50 & enemyTypeProbability <= 100)
            {
                GameObject archerEnemyObj = (GameObject)Instantiate(prefabArcherEnemy, spawnPos, transform.rotation);
                Enemy archerEnemy = archerEnemyObj.GetComponent<Enemy>();
                archerEnemy.onInstantiate(tilemap, tilemapRenderer);
            }
            */

            countdown = spawnSpeed;
        }
        countdown -= Time.deltaTime;
    }
}
