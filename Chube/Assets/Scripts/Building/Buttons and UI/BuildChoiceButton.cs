using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuildChoiceButton : MonoBehaviour
{
    public bool buildable = true;
    public Tile tile;
    public int cost; //set in inspector
    public Button button;
    public float buildTime;

    public BuildButtonsController controller;

    public void Start()
    {
        button.onClick.AddListener(switchTile);
        BuildButtonsController.costs.Add(tile.name, cost);
    }

    public void switchTile() {
        if (buildable)
        {
            controller.currentTile = tile;
            controller.cost = cost;
            controller.buildTime = buildTime;
        }
    }
}
