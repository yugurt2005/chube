using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    //public GameObject portal;

    [Header("Tiles")]
    public Tile chubeTile;
    public Tile trashCollectorTile;
    public Tile chubatorTile;
    public Tile buildProcessTile;
    public Tile pollutedTile;
    public Tile builtTile;
    public Tile generatorTile;
    public Tile WolfChubatorHighlighted;
    //public Tile portalTile;

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
    private float cellBreakingCooldown = 1f;
    private List<Vector3Int> keys = new List<Vector3Int>();
    private List<float> cooldowns = new List<float>();
    private void Start()
    {
        tileToObject.Add(generatorTile, generator);
        tileToObject.Add(builtTile, normal);
        tileToObject.Add(buildProcessTile, normal);
        tileToObject.Add(chubatorTile, chubator);
        tileToObject.Add(trashCollectorTile, collector);
        //tileToObject.Add(portalTile, portal);
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
            if (checkAvailability(cellPosition) && prefabManager.prefabMap[tileToObject[tile].gameObject.name] < tileToObject[tile].GetComponent<TileManager>().maxAmount && !keys.Contains(cellPosition))
            {
                cursor.render.color = Color.green;

                // If it's available and the player clicks the mouse, build a floor tile there
                if (Input.GetButton("Fire1"))
                {
                    // create coroutine of building
                    SFX.playSound(build);

                    materials.amount -= controller.cost;
                    //DELETE LATER7
                    //Debug.Log("position of new tile: " + cellPosition + " | dimensions of tilemap: " + tilemap.cellBounds.size);
                    TilemapController.changeProperties(cellPosition);
                    tilemap.SetTile(cellPosition, buildProcessTile);
                    StartCoroutine(buildNewTile(tile, cellPosition, controller.buildTime));
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && tilemap.HasTile(cellPosition)) //destroy tile
                {
                    string tilename = tilemap.GetTile(cellPosition).name;
                    if (tilename.Equals(chubeTile.name))
                        Debug.Log("Don't vape, don't suicide.");
                    else
                    {
                        if (tilename.Equals(buildProcessTile.name) || tilename.Equals(pollutedTile.name))
                            tilename = builtTile.name;
                        if (tilename.Equals(WolfChubatorHighlighted.name))
                            tilename = chubatorTile.name;
                        materials.amount += (int)(BuildButtonsController.costs[tilename] / 2); //only get half value back
                        tilemap.SetTile(cellPosition, null);
                        StartCoroutine(TilemapController.cascade(cellPosition));
                        /*
                        if (tilename.Equals(portalTile.name))
                        {
                            materials.amount += (int)(BuildButtonsController.costs[tilename] / 2);
                            PortalController.destroyPairedPortals(cellPosition);
                        }
                        */
                        keys.Add(cellPosition);
                        cooldowns.Add(cellBreakingCooldown);
                    }
                }

                SFX.playSound(invalidClick);
            }
        }

        for (int i = 0; i < keys.Count; i ++)
        {
            if (cooldowns[i] <= 0)
            {
                cooldowns.RemoveAt(i);
                keys.RemoveAt(i);
                break;
            }
            cooldowns[i] -= Time.deltaTime;
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
        {
            prefabManager.subtractCount(tileToObject[tile].gameObject.name);
        }
    }
}