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

    public float health = 10f;
    public int attackrange = 2;
    public float shipSpeed = 5f;
    private bool moveasship = true;

    private Tilemap tilemap;

    public void setInfo(Tilemap tilemap) //called by instantiator
    {
        this.tilemap = tilemap;
    }

    private void Start()
    {
        transform.LookAt(new Vector2(0f, 0f));
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    private void Update()
    {
        //find enemy in attack range and fire. -NOTE: ENEMY CAN'T MOVE WHILE SHOOTING.
        if (moveasship)
        {
            Vector3Int tilemappos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(transform.position)); //doesn't work
            if (tilemap.HasTile(tilemappos))
            {
                moveasship = true;
                return;
            }
            transform.LookAt(new Vector3(0f, 0f, 0f));
            transform.Translate(transform.forward * shipSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
            //attack/move tile and do pathfinding
        }
    }
}
