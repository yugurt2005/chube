using UnityEngine;
using UnityEngine.Tilemaps;

public class ChubatorController : MonoBehaviour
{
    public Tile normal;
    public Tile highlighted;
    public Tilemap tilemap;
    public TilemapRenderer tRenderer;
    public Tile walkable;
    public Materials materials;

    public Vector3Int pos;
    public GameObject prefabToSpawn;
    public int selfMaterials = 0;
    public float time = 15f;
    public int cost;

    private float countdown;
    private Vector3Int[] borderTiles;
    private bool firstTouch;
    private Vector3Int previousTile;

    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Tilemap");
        tilemap = temp.GetComponent<Tilemap>();
        tRenderer = temp.GetComponent<TilemapRenderer>();

        materials = GameObject.FindGameObjectWithTag("Materials").GetComponent<Materials>();
        
        pos = tilemap.WorldToCell(transform.position);
        pos.z = tRenderer.sortingOrder;

        borderTiles = new Vector3Int[4]; //get bordering tiles
        borderTiles[0] = new Vector3Int(pos.x, pos.y + 1, pos.z);
        borderTiles[1] = new Vector3Int(pos.x, pos.y - 1, pos.z);
        borderTiles[2] = new Vector3Int(pos.x + 1, pos.y, pos.z);
        borderTiles[3] = new Vector3Int(pos.x - 1, pos.y, pos.z);
    }

    public void Update()
    {
        Vector3Int mousePos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePos.z = pos.z;

        if (mousePos == pos && materials.amount >= cost)
        {
            tilemap.SetTile(pos, highlighted);
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fed chubator");
                selfMaterials += cost;
                materials.amount -= cost;
                Debug.Log(selfMaterials);
            }
        }
        else if (mousePos != pos) {
            tilemap.SetTile(pos, normal);
        }

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
