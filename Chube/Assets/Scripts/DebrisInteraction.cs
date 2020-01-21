using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisInteraction : MonoBehaviour
{
    public int health = 5;
    public SpriteRenderer self;
    public Collider2D col;
    public Materials materials;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (health <= 0) {
            Debug.Log("Broke a debris");
            materials.amount += Random.Range(2, 25);
            self.enabled = false;
            health = 5;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {            
            health--;
            Debug.Log("Hit, health at " + health);
        }
    }
}
