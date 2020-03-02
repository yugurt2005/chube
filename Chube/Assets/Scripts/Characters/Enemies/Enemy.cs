using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor.Tilemaps;
#endif

public class Enemy : MonoBehaviour //TODO: inherit from pathfinder
{
    /*
     * different enemies do different things.
     * Droid enemy will kill troops first - shoot bullets within 2 blocks, and then try to reach chube.
     * Other enemies will do the opposite, and will move differently (jump tiles) and have different attacking methods.
     */

    public Controller movementController;
    public Difficulty difficulty;

    public int attackrange = 2;
    public float structDamage = 0.01f;
    public float baseShipSpeed = 0.25f;

    private bool shipMode = true;
    private bool destroying = false;
    private Vector3Int chubePos;

    private Tilemap tilemap;
    private TilemapRenderer tilemapRenderer;

    public float shipSpeed;

    private TileManager structure;

    private Vector2[] directions = new Vector2[] {new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1)};

    // Because you need the references because they're instantiated so you can't set the references in the editor
    public void onInstantiate(Tilemap tilemap, TilemapRenderer tilemapRenderer)
    {
        chubePos = new Vector3Int(0, -1, tilemapRenderer.sortingOrder);
        this.tilemap = tilemap;
        this.tilemapRenderer = tilemapRenderer;

        movementController = GameObject.Find("EnemyMovement").GetComponent<Controller>();
        difficulty = GameObject.Find("Difficulty").GetComponent<Difficulty>();
    }
    
    void Update()
    {
        shipSpeed = baseShipSpeed * difficulty.difficultyMultiplier;

        Vector3Int tilemappos = tilemap.WorldToCell(transform.position);
        tilemappos.z = tilemapRenderer.sortingOrder;

        if (!tilemap.HasTile(tilemappos))
        {
            StopAllCoroutines(); //stop pathfinding
            shipMode = true;
        }
        if (shipMode)
        {
            transform.position = Vector3.MoveTowards(transform.position, tilemap.GetCellCenterWorld(chubePos), shipSpeed * Time.deltaTime);

            if (tilemap.HasTile(tilemappos))
            {
                shipMode = false;

                StopAllCoroutines();
                StartCoroutine(movementController.Move(transform, transform.position, tilemap.GetCellCenterWorld(chubePos) /*, false, false*/));
            }            
        }
        else if (!destroying)
        {
            foreach (Vector2 dir in directions)
            {
                // Bit shift the index of the layer (8) to get a bit mask
                int layerMask = 1 << 8;

                // This would cast rays only against colliders in layer 8, so we just inverse the mask.
                layerMask = ~layerMask;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 3, layerMask);

                Debug.DrawRay(transform.position, new Vector3(dir.x, dir.y, 0), Color.yellow);

                if (hit && hit.collider.tag == "Structure")
                {
                    Debug.Log("Raycast hit a structure! Destroying structure...");
                    destroying = true;
                    StopAllCoroutines();
                    structure = hit.collider.GetComponent<TileManager>();
                    break;
                }
            }
        }
        else {
            attack();
        }
    }

    void attack() {
        if (structure != null)
        {
            structure.damage(structDamage);
            if (structure.health <= 0)
            {
                StopAllCoroutines();
                StartCoroutine(movementController.Move(transform, transform.position, tilemap.GetCellCenterWorld(chubePos)));
            }
        }
    }
}
