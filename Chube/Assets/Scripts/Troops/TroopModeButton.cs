using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inherits from MainButton class
public class TroopModeButton : MainButton
{
    public MainButtonsController controller;
    public Button troopButton;
    public TroopMode troopController;
    public GameObject troopMode;

    void Start()
    {
        troopButton.onClick.AddListener(onButtonPress);
    }

    // Overrides turnOn and turnOff functions
    public override void turnOn()
    {
        controller.mode = 2;
        troopMode.SetActive(true);
        on = true;
    }

    public override void turnOff()
    {
        controller.mode = 0;
        troopController.resetTile();
        troopMode.SetActive(false);
        on = false;
    }

    public void onButtonPress() {
        controller.buttonPressed = 2;
    }
}
