using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

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
    public float shipSpeed = 5f;

    private bool shipMode = true;
    private Vector3Int chubePos;

    private Tilemap tilemap;
    private TilemapRenderer tilemapRenderer;

    // Because you need the references because they're instantiated so you can't set the references in the editor
    public void onInstantiate(Tilemap tilemap, TilemapRenderer tilemapRenderer)
    {
        chubePos = new Vector3Int(0, 0, tilemapRenderer.sortingOrder);
        this.tilemap = tilemap;
        this.tilemapRenderer = tilemapRenderer;

        movementController = GameObject.Find("Movement").GetComponent<Controller>();
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }
    
    void Update()
    {
        //TODO: find enemy in attack range and fire. -NOTE: ENEMY CAN'T MOVE WHILE SHOOTING.

        if (shipMode)
        {
            Vector3Int tilemappos = tilemap.WorldToCell(transform.position);
            tilemappos.z = tilemapRenderer.sortingOrder;

            // For some reason, it only detects when it's on the chube
            if (tilemap.HasTile(tilemappos))
            {
                shipMode = false;
                
                StopAllCoroutines();
                StartCoroutine(movementController.Move(transform, transform.position, chubePos));
            }
            transform.position = Vector3.MoveTowards(transform.position, tilemap.CellToWorld(chubePos), shipSpeed * Time.deltaTime);
        }
    }
}
