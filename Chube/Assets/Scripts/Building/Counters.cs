using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counters : MonoBehaviour
{
    public Text materialText;
    public Materials materials;
    public Text energyText;
    public Energy energy;

    void Start()
    {
        
    }

    void Update()
    {
        materialText.text = materials.amount.ToString();
        energyText.text = energy.amount.ToString();
    }
}
