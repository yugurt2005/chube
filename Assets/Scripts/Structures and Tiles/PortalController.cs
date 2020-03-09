using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PortalController : MonoBehaviour
{
    private static Tilemap tilemap;
    private static TilemapRenderer tilemapRenderer;

    public static List<Vector3Int> portals1;
    public static List<Vector3Int> portals2;
    public static Color[] colors;
    public static int portalCnt = 0; //every 2 increase of this = one more portal

    void Start()
    {
        if (portalCnt == 0)
        {
            colors = new Color[] { Color.blue, Color.red, Color.magenta, Color.gray };
            portals1 = new List<Vector3Int>();
            portals2 = new List<Vector3Int>();

            GameObject tmp = GameObject.FindGameObjectWithTag("Tilemap");
            tilemap = tmp.GetComponent<Tilemap>();
            tilemapRenderer = tmp.GetComponent<TilemapRenderer>();
        }

        SetTileColor(colors[(int)(portalCnt / 2)]);

        //start called by instance of prefab.
        addPortal();
        portalCnt++;
    }

    private void SetTileColor(Color color)
    {
        Vector3Int pos = tilemap.WorldToCell(transform.position);
        pos.z = tilemapRenderer.sortingOrder;

        tilemap.SetTileFlags(pos, TileFlags.None);
        tilemap.SetColor(pos, color);
    }

    public void addPortal()
    {
        Vector3Int cellPos = tilemap.WorldToCell(transform.position);
        cellPos.z = tilemapRenderer.sortingOrder;

        if (portalCnt % 2 == 0)
        {
            portals1.Add(cellPos);
        }
        else //add to portals2
        {
            portals2.Add(cellPos);
        }
    }

    public static void destroyPairedPortals(Vector3Int pos)
    {
        if (portals1.Contains(pos))
        {
            int idx = portals1.IndexOf(pos);
            tilemap.SetTile(portals1[idx], null);
            tilemap.SetTile(portals2[idx], null);
        }
        else if (portals2.Contains(pos))
        {
            int idx = portals2.IndexOf(pos);
            tilemap.SetTile(portals1[idx], null);
            tilemap.SetTile(portals2[idx], null);
        }
    }

    public static void subtractProperties(Vector3Int pos)
    {
        //destroying one portal will destroy the linked one.
        if (portals1.Contains(pos))
        {
            int idx = portals1.IndexOf(pos);
            portals1.RemoveAt(idx);
            portals2.RemoveAt(idx);
            portalCnt -= 2;
        }
        else if (portals2.Contains(pos))
        {
            int idx = portals2.IndexOf(pos);
            portals2.RemoveAt(idx);
            portals1.RemoveAt(idx);
            portalCnt -= 2;
        }
    }

    public static Vector3Int getCorrespondingPortal(Vector3Int pos)
    {
        Vector3Int ret = pos;
        if (portals1.Contains(pos))
        {
            int idx = portals1.IndexOf(pos);
            if (idx >= portals2.Count)
                return ret;
            ret = portals2[idx];
        }
        else if (portals2.Contains(pos))
        {
            int idx = portals2.IndexOf(pos);
            if (idx >= portals1.Count)
                return ret;
            ret = portals1[idx];
        }
        return ret;
    }
}
