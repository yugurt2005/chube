using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chube : MonoBehaviour
{    
    private bool switchedMode = false;

    public GameObject BuildMode;
    public GameObject BuildCursor;
    public Button BuildModeButton;

    void Start()
    {
        BuildModeButton.onClick.AddListener(onButtonPress);
    }

    void Update()
    {
        // If the button was pressed, building-related things are turned on/off
        if (switchedMode)
        {
            BuildCursor.SetActive(!BuildCursor.activeSelf);
            BuildMode.SetActive(!BuildMode.activeSelf);
            switchedMode = false;
        }
    }    

    void onButtonPress()
    {
        switchedMode = true;
    }
}
