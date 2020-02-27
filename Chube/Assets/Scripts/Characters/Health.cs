using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    public void CheckForDeath()
    {
        if (health <= 0)
        {
            Debug.Log(gameObject.tag);
            Destroy(gameObject);   
        }
    }
}
