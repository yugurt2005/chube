using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public Materials materials;

    private void Start()
    {
        materials = GameObject.FindGameObjectWithTag("Materials").GetComponent<Materials>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (tag == "Enemy") materials.amount += 50;
            Destroy(gameObject);   
        }
    }
}
