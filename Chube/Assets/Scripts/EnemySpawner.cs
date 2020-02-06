using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefabEnemy;
    private float countdown = 5f;
    public float spawnSpeed = 5f;

    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public DebrisMovement edgeSpawner;

    void Update()
    {
        if (countdown <= 0)
        {
            // uses debris's random perimeter position generator function
            Vector3 spawnPos = edgeSpawner.getPositionOnPerimeter();

            GameObject enemyObj = (GameObject)Instantiate(prefabEnemy, spawnPos, transform.rotation);    
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.onInstantiate(tilemap, tilemapRenderer);
            countdown = spawnSpeed;
        }
        countdown -= Time.deltaTime;
    }
}
