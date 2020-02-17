using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEditor.Tilemaps;

public class Troop : MonoBehaviour //TODO: inherit from pathfinder
{
    private SpriteRenderer spriteRenderer;
    public Controller movementController;
    public bool chosen;

    public float health = 10f;

    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;

    private TileManager structure;

    //private Vector2[] directions = new Vector2[] { new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1) };

    private void Start()
    {
        movementController = GameObject.Find("TroopMovement").GetComponent<Controller>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Because you need the references because they're instantiated so you can't set the references in the editor
    public void takeDamage(float damage)
    {
        health -= damage;
    }

    void Update()
    {
        Vector3Int mouseTile = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseTile.z = tilemapRenderer.sortingOrder;

        if (Input.GetButtonDown("Fire1") && tilemap.HasTile(mouseTile) && chosen)
        {
            Debug.Log(this.name + " should be moving to " + mouseTile);
            StopAllCoroutines();
            StartCoroutine(movementController.Move(transform, transform.position, tilemap.CellToWorld(mouseTile)));
        }

        // If place is valid thru specific troop's range
        if (mouseTile == tilemap.WorldToCell(transform.position) || chosen)
        {
            spriteRenderer.color = Color.green;
            if (Input.GetButtonDown("Fire1"))
            {
                chosen = true;
            }
        }
        else {
            spriteRenderer.color = Color.blue;
        }

        if (Input.GetButtonDown("Fire1") && chosen && mouseTile != tilemap.WorldToCell(transform.position)) {
            chosen = false;
        }
    }
}
