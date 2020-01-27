using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuildChoiceButton : MonoBehaviour
{
    public Tile tile;
    public int cost;
    public Button button;

    public BuildButtonsController controller;

    public void Start()
    {
        button.onClick.AddListener(switchTile);
    }

    public void switchTile() {
        controller.currentTile = tile;
        controller.cost = cost;
    }
}
