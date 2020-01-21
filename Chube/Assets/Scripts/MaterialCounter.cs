using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialCounter : MonoBehaviour
{
    public Text text;
    public Materials materials;

    void Start()
    {
        
    }

    void Update()
    {
        text.text = materials.amount.ToString();
    }
}
