using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

public class TileManager : MonoBehaviour
{
    public float health;
    public int maxHealth;
    public bool isChube;

    public Tilemap tilemap;
    public TilemapRenderer tRenderer;
    public PrefabBrush prefabBrush;

    void Start()
    {
        health = maxHealth;
        GameObject temp = GameObject.FindGameObjectWithTag("Tilemap");
        tilemap = temp.GetComponent<Tilemap>();
        tRenderer = temp.GetComponent<TilemapRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Debug.Log("DESTROYED A STRUCTURE");
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

    void OnTriggerExit2D()
    {
        health--;
        //Debug.Log(tilemap.WorldToCell(transform.position) + " health: " + health);
    }

    public void damage(float amount) {
        //Debug.Log("STRUCTURE BEING DAMAGED. HEALTH AT: " + health);
        health -= amount;
    }


    private void onChubeDeath() {
        Debug.Log("You died.");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
