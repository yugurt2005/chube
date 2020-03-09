using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildButtonsController : MonoBehaviour
{
    public Tile currentTile;
    public int cost = 5;
    public float buildTime = 5f;

    public static Dictionary<string, int> costs;

    private void Awake()
    {
        costs = new Dictionary<string, int>();
    }
}
