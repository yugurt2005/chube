using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

// This is the basic code for movement: go to where it says "IMPLEMENT PATHFINDING HERE" to fill in animation for movement
// (right now it just teleports there)

public class TroopMode : MonoBehaviour
{
    // TODO:
    // - check if spot is available (no obstacle or other troops there)
    // - change what spots you can move to based on animal (like chess)

    private Vector3Int previousTile;
    private bool firstTouch;

    public Tilemap tilemap;
    public Tile editTile;
    public Tile neutralTile;
    public TilemapRenderer tilemapRenderer;
    public IsoPathfinder pathfinder;

    public float speed = 0.5f;

    void Start()
    {        
        transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position));
    }

    void Update()
    {
        Vector3Int mouseTile = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseTile.z = tilemapRenderer.sortingOrder;

        if (tilemap.HasTile(mouseTile))
        {          
            if (Input.GetButtonDown("Fire1"))
            {
                // IMPLEMENT PATHFINDING HERE
            }

            if (tilemap.GetTile(mouseTile) == neutralTile)
            {
                
                tilemap.SetTile(mouseTile, editTile);

                if (previousTile != mouseTile)
                {
                    if (firstTouch) tilemap.SetTile(previousTile, neutralTile);
                    previousTile = mouseTile;
                    firstTouch = true;                
                }
            }
        }
    }

    public void resetTile()
    {
        if (tilemap.HasTile(previousTile)) tilemap.SetTile(previousTile, neutralTile);
    }
}