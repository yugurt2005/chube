using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    public float health;
    public int maxHealth;
    public bool isChube;
    public int maxAmount;

    public Tile pollutedTile;
    public Tilemap tilemap;
    public TilemapRenderer tRenderer;
    public PrefabBrush prefabBrush;
    public PrefabBrushManager manager;

    void Start()
    {
        health = maxHealth;
        GameObject temp = GameObject.FindGameObjectWithTag("Tilemap");
        tilemap = temp.GetComponent<Tilemap>();
        tRenderer = temp.GetComponent<TilemapRenderer>();
        manager = temp.GetComponent<TilemapController>().prefabManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Debug.Log("DESTROYED A STRUCTURE");
            manager.prefabMap[gameObject.name] -= 1;
            if (isChube) {
                onChubeDeath();
            }
            Vector3Int pos = tilemap.WorldToCell(transform.position);
            pos.z = tRenderer.sortingOrder;
            tilemap.SetTile(pos, null);
            Destroy(gameObject);
            Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && gameObject.tag == "Walkable") {
            tilemap.SetTile(tilemap.WorldToCell(transform.position), pollutedTile);
        }
    }
    void OnTriggerStay2D()
    {
        health-= 0.002f;
    }

    public void damage(float amount) {
        //Debug.Log("STRUCTURE BEING DAMAGED. HEALTH AT: " + health);
        health -= amount;
    }


    private void onChubeDeath() {
        Debug.Log("You died.");
        SceneManager.LoadScene("Menu");
    }
}
