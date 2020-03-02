using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public Tilemap tilemapTemp;
    private static Tilemap tilemap;
    public TilemapRenderer tilemapRendererTemp;
    private static TilemapRenderer tilemapRenderer;
    public TileBase chubeTileTemp;
    private static TileBase chubeTile;
    // used to avoid GameObject.Find command in TileManager script
    public static float cascadeTime = 0.1f;

    public PrefabBrushManager prefabBrushManagerTemp;
    private static PrefabBrushManager prefabBrushManager;
    
    [Header("Static Tilemap Properties")] //used for cascading effect
    public static int tilemapsizex = 0;
    public static int tilemapsizey = 0; //in number of cells
    public static int tilemapoffsetx = 0; //offset because some cell positions are negative
    public static int tilemapoffsety = 1; //because chube position is (0, -1, sortingorder)

    public static bool inCascade = false;

    private void Awake()
    {
        tilemap = tilemapTemp;
        tilemapRenderer = tilemapRendererTemp;
        chubeTile = chubeTileTemp; // to convert from non-static to static
        prefabBrushManager = prefabBrushManagerTemp;
    }

    // Start is called before the first frame update
    void Start()
    {
        tilemap.SetTile(new Vector3Int(0, -1, tilemapRenderer.sortingOrder), chubeTile);
    }

    //IMPORTANT: BECAUSE IDK WHY EVERY TIME U GET THE SIZE, U ALWAYS HAVE TO ADD IT BY 1
    public static int getSizeX()
    {
        return tilemapsizex + 1;
    }

    public static int getSizeY()
    {
        return tilemapsizey + 1;
    }

    //---------------------------------------cascade------------------------------------------
    private class Point //needed for cascade
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private static List<Point> getAdjacentPos(int x, int y)
    {
        x += tilemapoffsetx;
        y += tilemapoffsety;
        List<Point> ret = new List<Point>();
        if (y < getSizeY() && y >= 0)
        {
            if (x - 1 < getSizeX() && x - 1 >= 0)
                ret.Add(new Point(x - 1 - tilemapoffsetx, y - tilemapoffsety));
            if (x + 1 < getSizeX() && x + 1 >= 0)
                ret.Add(new Point(x + 1 - tilemapoffsetx, y - tilemapoffsety));
        }
        if (x < getSizeX() && x >= 0)
        {
            if (y - 1 < getSizeY() && y - 1 >= 0)
                ret.Add(new Point(x - tilemapoffsetx, y - 1 - tilemapoffsety));
            if (y + 1 < getSizeY() && y + 1 >= 0)
                ret.Add(new Point(x - tilemapoffsetx, y + 1 - tilemapoffsety));
        }
        return ret;
    }

    private static int addQueue(List<List<Point>> queues, List<List<Point>> finals, int queueID, int x, int y, bool[,,] b, int sections)
    { //queues/finals are lists which are reference types - they will be modified.
        if (tilemap.GetTile(new Vector3Int(x, y, tilemapRenderer.sortingOrder)) == chubeTile)
            return 2;
        if (b[queueID, y + tilemapoffsety, x + tilemapoffsetx]) // already checked this spot
            return -1;
        if (tilemap.HasTile(new Vector3Int(x, y, tilemapRenderer.sortingOrder)))
        {
            bool notchecked = true;
            for (int i = 0; i < sections; i++)
            {
                if (b[i, y + tilemapoffsety, x + tilemapoffsetx])
                {
                    notchecked = false;
                    break;
                }
            }
            if (notchecked)
            {
                finals[queueID].Add(new Point(x, y));
            }
            b[queueID, y + tilemapoffsety, x + tilemapoffsetx] = true; //mark that it is checked
            queues[queueID].Add(new Point(x, y));
        }
        return 0;
    }

    private static Point popQueue(List<List<Point>> queues, int queueID) //list is reference type so queues will be changed
    {
        Point ret = queues[queueID][0];
        queues[queueID].RemoveAt(0);
        return ret;
    }

    public static IEnumerator cascade(Vector3Int destroypos) //cascading destroying effect
    {
        inCascade = true;
        Debug.Log("Calculating cascade...");
        //0 = nothing
        //1 = something
        //2 = chube
        //format: array[y][x]
        //INIT
        List<List<Point>> queues = new List<List<Point>>();
        List<List<Point>> finals = new List<List<Point>>();
        //check number of sections (adjacent tiles) around destroy coordinates
        int sections = 0;
        foreach (Point p in getAdjacentPos(destroypos.x, destroypos.y))
        {
            if (tilemap.HasTile(new Vector3Int(p.x, p.y, tilemapRenderer.sortingOrder)))
            {
                queues.Add(new List<Point>());
                finals.Add(new List<Point>());
                sections++;
            }
        }
        bool[,,] b = new bool[sections, getSizeY(), getSizeX()];
        int tmpi = 0;
        foreach (Point p in getAdjacentPos(destroypos.x, destroypos.y))
        {
            if (tilemap.HasTile(new Vector3Int(p.x, p.y, tilemapRenderer.sortingOrder)))
            {
                addQueue(queues, finals, tmpi++, p.x, p.y, b, sections);
            }
        }

        //main expansion/finding loop
        while (true)
        {
            bool finish = true;
            for (int i = 0; i < sections; i++)
            {
                if (queues[i].Count == 0) //if queue is empty
                {
                    continue;
                }
                else
                {
                    finish = false;
                }

                Point cur = popQueue(queues, i);
                //expand from cur outwards in 4 directions
                foreach (Point adjacent in getAdjacentPos(cur.x, cur.y))
                {
                    int result = addQueue(queues, finals, i, adjacent.x, adjacent.y, b, sections);
                    if (result == 2) //if connected to chube
                    {
                        queues[i].Clear();
                        finals[i].Clear();
                    }
                }
            }
            if (finish)
                break;
        }
        Debug.Log("Cascade: Destroying tiles");
        //destroy each final one by one in cascading effect
        while (true)
        {
            bool finish = true;
            foreach (List<Point> l in finals)
            {
                if (l.Count == 0)
                    continue;
                else
                    finish = false;
                Point cur = l[0];
                l.RemoveAt(0);
                yield return new WaitForSeconds(cascadeTime);
                tilemap.SetTile(new Vector3Int(cur.x, cur.y, tilemapRenderer.sortingOrder), null);
                //note that tileManager/buildMode will check if the tile is null and then delete itself
            }
            if (finish)
                break;
        }

        Debug.Log("Finished cascade.");
        inCascade = false;
    }

    public void coroutine(Vector3Int pos)
    {
        StartCoroutine(cascade(pos));
    }
    //---------------------------------------cascade------------------------------------------
    

    public static void changeProperties(Vector3Int pos) //changes the static properties of this class
    {// important note: PROPERTIES DON'T CHANGE WHEN TILE IS BROKEN (VERY INEFFICIENT)
        if (pos.x < 0 && pos.x < -tilemapoffsetx)
        {
            tilemapsizex -= tilemapoffsetx;
            tilemapoffsetx = -pos.x;
            tilemapsizex += tilemapoffsetx;
        }
        if (pos.y < 0 && pos.y < -tilemapoffsety)
        {
            tilemapsizey -= tilemapoffsety;
            tilemapoffsety = -pos.y;
            tilemapsizey += tilemapoffsety;
        }

        if (pos.x + tilemapoffsetx > tilemapsizex)
            tilemapsizex = pos.x + tilemapoffsetx;
        if (pos.y + tilemapoffsety > tilemapsizey)
            tilemapsizey = pos.y + tilemapoffsety;

        //Debug.Log("Changed properties: Offsetx = " + tilemapoffsetx + " | Offsety = " + tilemapoffsety + " | Sizex = " + tilemapsizex + " | Sizey = " + tilemapsizey);
    }
}