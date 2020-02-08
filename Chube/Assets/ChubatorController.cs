using UnityEngine;
using UnityEngine.Tilemaps;

public class ChubatorController : MonoBehaviour
{
    private Tilemap tilemap;
    private Vector3Int pos;
    private GameObject prefabToSpawn;
    private int materials = 0;
    private float time;
    private float countdown;
    private int cost;

    public void setInfo(Tilemap tilemap, Vector3Int pos, GameObject prefabToSpawn, float time, int cost)
    {
        this.tilemap = tilemap;
        this.pos = pos;
        this.prefabToSpawn = prefabToSpawn;
        this.time = time;
        this.cost = cost;
    }

    public void Update()
    {
        if (countdown <= 0)
        {
            if (materials >= cost)
            {
                // Maybe assign borderTiles on instantiation, because it won't change
                // so you don't have to do calculations every time

                Vector3Int[] borderTiles = new Vector3Int[4]; //get bordering tiles
                borderTiles[0] = new Vector3Int(pos.x, pos.y + 1, pos.z);
                borderTiles[1] = new Vector3Int(pos.x, pos.y - 1, pos.z);
                borderTiles[2] = new Vector3Int(pos.x + 1, pos.y, pos.z);
                borderTiles[3] = new Vector3Int(pos.x - 1, pos.y, pos.z);

                for (int i = 0; i < 4; i++)
                {
                    if (tilemap.HasTile(borderTiles[i]))
                    {
                        materials -= cost;
                        Instantiate(prefabToSpawn, tilemap.CellToWorld(borderTiles[i]), transform.rotation); //first one is the one to instantiate wolf
                        return;
                    }
                }
            }

            countdown = time;
            return;
        }
        countdown -= Time.deltaTime;
    }

    public void addMaterials(int materials)
    {
        this.materials += materials;
    }
}
