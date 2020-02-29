using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEditor.Tilemaps;

public class Troop : MonoBehaviour //TODO: inherit from pathfinder
{
    private SpriteRenderer spriteRenderer;
    public Controller movementController;
    public bool chosen;

    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public List<TileBase> walkableTiles;

    private TileManager structure;

    private List<Vector3Int> resetattack;
    private List<Vector3Int> resetmove;
    private Color resetcolor;

    //private Vector2[] directions = new Vector2[] { new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1) };

    private void Start()
    {
        movementController = GameObject.Find("TroopMovement").GetComponent<Controller>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        resetattack = new List<Vector3Int>();
        resetmove = new List<Vector3Int>();
        resetcolor = new Color(1, 1, 1, 1);
    }

    // Because you need the references because they're instantiated so you can't set the references in the editor

    private bool SetTileColor(int x, int y, Color color, bool ismove)
    {
        Vector3Int pos = new Vector3Int(x, y, tilemapRenderer.sortingOrder);
        if (!walkableTiles.Contains(tilemap.GetTile(pos)))
            return false;

        tilemap.SetTileFlags(pos, TileFlags.None);
        tilemap.SetColor(pos, color);
        if (ismove)
            resetmove.Add(pos);
        else
            resetattack.Add(pos);

        return true;
    }

    private void ResetTileColors()
    {
        foreach (Vector3Int pos in resetmove)
        {
            tilemap.SetColor(pos, resetcolor);
        }
        foreach (Vector3Int pos in resetattack)
        {
            tilemap.SetColor(pos, resetcolor);
        }
        resetmove.Clear();
        resetattack.Clear();
    }

    private void HighlightTiles(Vector3Int pos)
    {
        //wolf: attack around (8 tiles) movement 4 tiles in each direction

        //highlight movement tile
        for (int i = 1; i < 5; i ++)
        {
            if (!SetTileColor(i + pos.x, pos.y, Color.blue, true)) break;
        }
        for (int i = 1; i < 5; i++)
        {
            if (!SetTileColor(pos.x, i + pos.y, Color.blue, true)) break;
        }
        for (int i = -1; i > -5; i --)
        {
            if (!SetTileColor(i + pos.x, pos.y, Color.blue, true)) break;
        }
        for(int i = -1; i > -5; i--)
        {
            if (!SetTileColor(pos.x, i + pos.y, Color.blue, true)) break;
        }

        //highlight attack tile
        // NOTE: commented out for testing stuff
        /*
        SetTileColor(pos.x - 1, pos.y - 1, Color.red, false);
        SetTileColor(pos.x, pos.y - 1, Color.magenta, true);
        SetTileColor(pos.x + 1, pos.y - 1, Color.red, false);

        SetTileColor(pos.x - 1, pos.y, Color.magenta, true);
        SetTileColor(pos.x + 1, pos.y, Color.magenta, true);

        SetTileColor(pos.x - 1, pos.y + 1, Color.red, false);
        SetTileColor(pos.x, pos.y + 1, Color.magenta, true);
        SetTileColor(pos.x + 1, pos.y + 1, Color.red, false);
        */
    }

    private bool isValidMoveSpot(Vector3Int movePos)
    {
        //for wolves
        foreach (Vector3Int pos in resetmove)
        {
            if (pos.Equals(movePos))
                return true;
        }
        return false;
    }

    void Update()
    {
        Vector3Int selfpos = tilemap.WorldToCell(transform.position);
        selfpos.z = tilemapRenderer.sortingOrder;
        Vector3Int mouseTile = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseTile.z = tilemapRenderer.sortingOrder;

        if (Input.GetButtonDown("Fire1") && tilemap.HasTile(mouseTile) && chosen && isValidMoveSpot(mouseTile))
        {
            Debug.Log(this.name + " should be moving to " + mouseTile);
            StopAllCoroutines();
            StartCoroutine(movementController.Move(transform, transform.position, tilemap.CellToWorld(mouseTile)));
        }

        // If place is valid thru specific troop's range
        if (mouseTile == tilemap.WorldToCell(transform.position) || chosen)
        {
            spriteRenderer.color = Color.green;
            HighlightTiles(selfpos);
            if (Input.GetButtonDown("Fire1"))
            {
                chosen = true;
            }
        }
        else {
            spriteRenderer.color = Color.blue;
            ResetTileColors();
        }

        if (Input.GetButtonDown("Fire1") && chosen && mouseTile != tilemap.WorldToCell(transform.position)) {
            chosen = false;
        }
    }
}
