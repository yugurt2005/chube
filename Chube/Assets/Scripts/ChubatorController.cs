using UnityEngine;
using UnityEngine.Tilemaps;

// NOTE: THIS SCRIPT IS TEMPORARY & A MESS LOL
// It's gonna be replaced with scriptable tiles later

public class ChubatorController : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int pos;
    public GameObject prefabToSpawn;
    public int selfMaterials = 0;
    private float time;
    private float countdown;
    public int cost;
    public Tile walkable;
    private Vector3Int[] borderTiles;
    private Materials materials;
    public SpriteRenderer spriteRenderer;

    // lol the pain of not scriptable tiles
    public void setInfo(Tilemap tilemap, Vector3Int pos, GameObject prefabToSpawn, float time, int cost, Materials materials, Tile walkable)
    {
        this.tilemap = tilemap;
        this.pos = pos;
        transform.position = tilemap.GetCellCenterWorld(pos);

        this.prefabToSpawn = prefabToSpawn;
        this.time = time;
        this.cost = cost;
        this.materials = materials;
        this.walkable = walkable;
        spriteRenderer = GetComponent<SpriteRenderer>();

        borderTiles = new Vector3Int[4]; //get bordering tiles
        borderTiles[0] = new Vector3Int(pos.x, pos.y + 1, pos.z);
        borderTiles[1] = new Vector3Int(pos.x, pos.y - 1, pos.z);
        borderTiles[2] = new Vector3Int(pos.x + 1, pos.y, pos.z);
        borderTiles[3] = new Vector3Int(pos.x - 1, pos.y, pos.z);
    }

    public void Update()
    {
        Vector3 mousePos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePos.z = pos.z;
        if (mousePos == pos && materials.amount >= cost)
        {
            spriteRenderer.color = Color.green;
            if (Input.GetButtonDown("Fire1"))
            {
                selfMaterials += cost;
                materials.amount -= cost;
                Debug.Log(selfMaterials);
            }
        }
        else spriteRenderer.color = Color.white;

        if (countdown <= 0)
        {
            if (selfMaterials >= cost)
            {
                for (int i = 0; i < 4; i++)
                {
                    Debug.Log(tilemap.GetTile(borderTiles[i]) == walkable);
                    if (tilemap.GetTile(borderTiles[i]) == walkable)
                    {
                        selfMaterials -= cost;                        
                        Instantiate(prefabToSpawn, tilemap.GetCellCenterWorld(borderTiles[i]), transform.rotation); //first one is the one to instantiate wolf
                        countdown = time;
                        return;
                    }
                }
            }

            countdown = time;
            return;
        }
        countdown -= Time.deltaTime;
    }
}
