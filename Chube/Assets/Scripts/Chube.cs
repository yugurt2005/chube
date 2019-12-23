using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chube : MonoBehaviour
{
    public Button buildModeButton;
    private bool buildMode = false;

    // start is called before the first frame update
    void Start()
    {
        buildModeButton.onClick.AddListener(checkButton);
    }

    // update is called once per frame
    void Update()
    {
        if (buildMode) build();
    }

    void build()
    {
        // disable all non-building related gameObjects
        // have an isometric diamond follow your cursor and if it's aligned/snapped with another built thing/chube,
        // then if you click it instantiates a structure
    }

    void checkButton()
    {
        buildMode = !buildMode;
        Debug.Log("Button pressed and buildmode is " + buildMode);
    }
}
