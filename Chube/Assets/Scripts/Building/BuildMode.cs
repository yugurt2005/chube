using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;
using System.Linq;

// NOTE: Before building the game (when we're finished), change the 'individual' rendering mode to 'chunk' and make a sprite atlas instead.
// This will fix/make more efficient isometric rendering in the final build; this 'individual' rendering mode is just a temporary way to 
// get the tilemap to sort correctly while still being able to make/edit/add new tiles into the tile palette.

public class BuildMode : MonoBehaviour
{
    
    public PrefabBrushManager prefabManager;
    [Header("Tile Types")]
    public GameObject normal;
    public GameObject chubator;
    public GameObject collector;
    public GameObject generator;

    [Header("Tiles")]
    public Tile chubeTile;
    public Tile trashCollectorTile;
    public Tile chubatorTile;
    public Tile buildProcessTile;
    public Tile builtTile;
    public Tile generatorTile;

    [Header("Tilemap")]
    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public BuildCursor cursor;

    [Header("General")]
    public Materials materials;
    public BuildButtonsController controller;
    public BuildModeButton buildMode;

    public AudioSource invalidClick;
    public AudioSource build;
    public SFXController SFX;

    public Dictionary<Tile, GameObject> tileToObject = new Dictionary<Tile, GameObject>();

    private void Start()
    {
        tileToObject.Add(generatorTile, generator);
        tileToObject.Add(builtTile, normal);
        tileToObject.Add(buildProcessTile, normal);
        tileToObject.Add(chubatorTile, chubator);
        tileToObject.Add(trashCollectorTile, collector);
    }
    void Update()
    {
        if (buildMode.on)
        {
            // Cursor color is red by default
            cursor.render.color = Color.red;
            Vector3Int cellPosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            cellPosition.z = tilemapRenderer.sortingOrder;
            Tile tile = controller.currentTile;

            // If current cell is available, set cursor to green
            if (checkAvailability(cellPosition) && prefabManager.prefabMap[tileToObject[tile].gameObject.name] < tileToObject[tile].GetComponent<TileManager>().maxAmount)
            {
                    cursor.render.color = Color.green;

                    // If it's available and the player clicks the mouse, build a floor tile there
                    if (Input.GetButton("Fire1"))
                    {                   
                        // create coroutine of building
                        SFX.playSound(build);
                    
                        materials.amount -= controller.cost;
                        //DELETE LATER
                        //Debug.Log("position of new tile: " + cellPosition + " | dimensions of tilemap: " + tilemap.cellBounds.size);
                        TilemapController.changeProperties(cellPosition);
                        tilemap.SetTile(cellPosition, buildProcessTile);
                        StartCoroutine(buildNewTile(tile, cellPosition, controller.buildTime));                    
                    }
            }
            else if (Input.GetButtonDown("Fire1")) {
                SFX.playSound(invalidClick);
                Debug.Log("Invalid click at x: " + cellPosition.x + " | y: " + cellPosition.y); //DELETE LATER!
            }
        }
    }

    // Checks if any of the surrounding 4 tiles contains a floor
    private bool checkAvailability(Vector3Int pos)
    {
        if (tilemap.HasTile(pos) || materials.amount < controller.cost) return false;

        Vector3Int[] borderTiles = new Vector3Int[4];
        borderTiles[0] = new Vector3Int(pos.x, pos.y + 1, pos.z);
        borderTiles[1] = new Vector3Int(pos.x, pos.y - 1, pos.z);
        borderTiles[2] = new Vector3Int(pos.x + 1, pos.y, pos.z);
        borderTiles[3] = new Vector3Int(pos.x - 1, pos.y, pos.z);

        for (int i = 0; i < 4; i ++)
        {
            if (tilemap.HasTile(borderTiles[i])) return true;
        }

        return false;
    }

    public IEnumerator buildNewTile(Tile tile, Vector3Int pos, float time)
    {
        prefabManager.addCount(tileToObject[tile].gameObject);

        yield return new WaitForSecondsRealtime(time);

        if (tilemap.HasTile(pos))
        {
            prefabManager.paint(tilemap, tileToObject[tile].gameObject, pos);

            tilemap.SetTile(pos, tile);
        }
        else
            prefabManager.subtractCount(tileToObject[tile].gameObject.name);
    }
}