using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Energy energy;
    private float countdown;
    public float generationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        energy = GameObject.FindObjectOfType<Energy>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = generationSpeed;
            energy.amount += 1;
        }
    }
}
