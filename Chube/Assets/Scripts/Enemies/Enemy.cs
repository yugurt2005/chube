using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEditor.Tilemaps;

public class Enemy : MonoBehaviour //TODO: inherit from pathfinder
{
    /*
     * different enemies do different things.
     * Droid enemy will kill troops first - shoot bullets within 2 blocks, and then try to reach chube.
     * Other enemies will do the opposite, and will move differently (jump tiles) and have different attacking methods.
     */

    public Controller movementController;

    public float health = 10f;
    public int attackrange = 2;
    public float structDamage = 0.01f;
    public float shipSpeed = 2f;
    private bool shipMode = true;
    private bool destroying = false;
    private Vector3Int chubePos;

    private Tilemap tilemap;
    private TilemapRenderer tilemapRenderer;

    private TileManager structure;

    private Vector2[] directions = new Vector2[] {new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1)};

    // Because you need the references because they're instantiated so you can't set the references in the editor
    public void onInstantiate(Tilemap tilemap, TilemapRenderer tilemapRenderer)
    {
        chubePos = new Vector3Int(0, -1, tilemapRenderer.sortingOrder);
        this.tilemap = tilemap;
        this.tilemapRenderer = tilemapRenderer;

        movementController = GameObject.Find("EnemyMovement").GetComponent<Controller>();
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }
    
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = tilemapRenderer.sortingOrder;

        //TODO: find enemy in attack range and fire. -NOTE: ENEMY CAN'T MOVE WHILE SHOOTING.

        if (shipMode)
        {
            Vector3Int tilemappos = tilemap.WorldToCell(transform.position);
            tilemappos.z = tilemapRenderer.sortingOrder;
            transform.position = Vector3.MoveTowards(transform.position, tilemap.GetCellCenterWorld(chubePos), shipSpeed * Time.deltaTime);

            if (tilemap.HasTile(tilemappos))
            {
                shipMode = false;

                StopAllCoroutines();
                StartCoroutine(movementController.Move(transform, transform.position, tilemap.GetCellCenterWorld(chubePos)));
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

                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 5, layerMask);

                Debug.DrawRay(transform.position, new Vector3(dir.x, dir.y, 0), Color.yellow);

                if (hit)// && hit.collider.tag == "Structure") //TODO:CHANGE BACK!!@#!@#!@#!@#
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
        structure.damage(structDamage);
        if (structure.health <= 0) {
            this.gameObject.SetActive(false);
            //StartCoroutine(movementController.Move(transform, transform.position, chubePos));
        }
    }
}
