using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;

public class PrefabBrushManager : MonoBehaviour
{
    public Dictionary<string, int> prefabMap = new Dictionary<string, int>();
    public PrefabBrush prefabBrush;
    private string[] prefabs = new string[]{"Chube", "Chubator", "Trash Collector", "Walkable", "Energy Generator"};

    void Start()
    {
        foreach (string prefab in prefabs) {
            prefabMap.Add(prefab, 0);
        }
        
    }
    public void paint(Tilemap tilemap, GameObject prefab, Vector3Int pos) {
        prefabBrush.Paint(tilemap, prefab, pos);
    }

    public void addCount(GameObject prefab) {
        prefabMap[prefab.name] += 1;
    }

    public void subtractCount(string prefabname)
    {
        prefabMap[prefabname] -= 1;
        //Debug.Log("Walkable count: " + prefabMap["Walkable"]);
    }
}
