using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TileManager tile;
    public Slider slider;

    private float max;
    private float current;

    // Start is called before the first frame update
    void Start()
    {
        max = tile.maxHealth;
        slider.maxValue = max;
    }

    // Update is called once per frame
    void Update()
    {
        current = tile.health;
        slider.value = current;
        transform.localScale = new Vector3(3 / Camera.main.orthographicSize, 3 / Camera.main.orthographicSize);
        transform.position = Camera.main.WorldToScreenPoint(tile.transform.position) + new Vector3(0, 500/Camera.main.orthographicSize);        
    }
}
