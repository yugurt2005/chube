using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public TileBase chubeTile;

    // Start is called before the first frame update
    void Start()
    {
        tilemap.SetTile(new Vector3Int(0, -1, tilemapRenderer.sortingOrder), chubeTile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
