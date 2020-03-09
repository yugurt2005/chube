using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildCursor : MonoBehaviour
{
    public SpriteRenderer render;
    public Tilemap tilemap;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Cursor follows mouse position
    void Update()
    {
        transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
    }
}
