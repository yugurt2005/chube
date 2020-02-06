using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefabEnemy;
    private float countdown = 5f;
    public float speed = 5f;

    public Tilemap tilemap;

    void Update()
    {
        if (countdown <= 0)
        {
            Vector2 pos = findSpawn();
            GameObject enemyObj = (GameObject)Instantiate(prefabEnemy, pos, transform.rotation); //need to set information for enemy
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
                enemy.setInfo(tilemap);
            countdown = speed;
        }
        countdown -= Time.deltaTime;
    }

    Vector2 findSpawn()
    {
        Vector2 dir = Random.insideUnitCircle;
        Vector2 position = Vector2.zero;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {//make it appear on the side
            position = new Vector2(Mathf.Sign(dir.x) * Camera.main.orthographicSize * Camera.main.aspect,
                                    dir.y * Camera.main.orthographicSize);
        }
        else
        {//make it appear on the top/bottom
            position = new Vector2(dir.x * Camera.main.orthographicSize * Camera.main.aspect,
                                    Mathf.Sign(dir.y) * Camera.main.orthographicSize);
        }

        return position;
    }
}
