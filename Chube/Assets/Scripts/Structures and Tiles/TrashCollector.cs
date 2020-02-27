using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    public Materials materials;
    private float countdown;
    public float generationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        materials = GameObject.FindObjectOfType<Materials>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0) {
            countdown = generationSpeed;
            materials.amount += 1;
        }
    }
}
