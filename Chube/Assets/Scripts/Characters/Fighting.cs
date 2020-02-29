﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : MonoBehaviour
{
    public float damage;
    bool fighting;
    int layer;

    public float speed;
    public float perceptionRadius;
    public float attackRadius;

    void Start()
    {
        if (gameObject.tag == "Troop")
            layer = LayerMask.GetMask("Enemies");
        if (gameObject.tag == "Enemy")
            layer = LayerMask.GetMask("Troops");
    }

    void Update()
    {
        GameObject character;
        if (!fighting && Raycast(out character))
        {
            fighting = true;
            Debug.Log(character.tag);
            StopAllCoroutines();
            StartCoroutine(Fight(character));
        }
    }

    IEnumerator Fight(GameObject character)
    {
        Debug.Log("hi");
        Health health = character.GetComponent<Health>();

        while (true)
        {
            try
            {
                float distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance <= attackRadius)
                    health.takeDamage(damage * Time.deltaTime);
                if (distance <= perceptionRadius)
                    transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
                else
                    break;
            }
            catch { break; }

            yield return null;
        }

        fighting = false;
    }

    bool Raycast(out GameObject character)
    {
        Collider2D collider = Physics2D.OverlapCircle(
            (Vector2)transform.position,
            perceptionRadius,
            layer);
        character = collider != null ? collider.gameObject : null;
        return character != null;
    }
}
