using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisInteraction : MonoBehaviour
{
    // TODO: 
    // - change appearance when mouse is over
    // - show damage

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
        }
    }
}
