using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildMode : MonoBehaviour
{
    public Tile tile;
    public Tilemap tilemap;
    public BuildCursor cursor;
    
    // Sets chube as a tile
    void Start()
    {
        tilemap.SetTile(new Vector3Int(0, -1, -4), tile);
    }

    void Update()
    {
        // Cursor color is red by default
        cursor.render.color = Color.red;
        Vector3Int cellPosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // If current cell is available, set cursor to green
        if (checkAvailability(cellPosition))
        {
            cursor.render.color = Color.green;

            // If it's available and the player clicks the mouse, build a floor tile there
            if (Input.GetButtonDown("Fire1") && !tilemap.HasTile(cellPosition))
            {
                tilemap.SetTile(cellPosition, tile);
            }
        }        
    }

    // Checks if any of the surrounding 4 tiles contains a floor
    private bool checkAvailability(Vector3Int tile)
    {
        ArrayList borderTiles = new ArrayList();
        borderTiles.Add(new Vector3Int(tile.x, tile.y + 1, tile.z));
        borderTiles.Add(new Vector3Int(tile.x, tile.y - 1, tile.z));
        borderTiles.Add(new Vector3Int(tile.x + 1, tile.y, tile.z));
        borderTiles.Add(new Vector3Int(tile.x - 1, tile.y, tile.z));

        foreach (Vector3Int borderTile in borderTiles)
        {
            if (tilemap.HasTile(borderTile)) return true;
        }

        return false;
    }
}
