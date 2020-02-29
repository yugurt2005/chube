using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    public void takeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0) { 
            Debug.Log(gameObject.tag);
            Destroy(gameObject);
        }
    }
}
