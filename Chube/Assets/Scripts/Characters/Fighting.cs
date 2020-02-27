﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : MonoBehaviour
{
    public float damage;
    IDamage opponent;
    bool fighting;
    int layer;

    float speed;
    Vector3 position;

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
            //Debug.Log(character.tag);
            position = transform.position;
            StopAllCoroutines();
            StartCoroutine(Fight(character));
        }
    }

    IEnumerator Fight(GameObject character)
    {
        fighting = true;

        if (character.tag == "Troop")
            opponent = character.GetComponent<Troop>();
        if (character.tag == "Enemy")
            opponent = character.GetComponent<Enemy>();

        while (true)
        {
            try
            {
                transform.position = position;

                float distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance <= attackRadius)
                    opponent.takeDamage(damage * Time.deltaTime);
                if (distance <= perceptionRadius)
                    transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
                else
                    break;

                position = transform.position;
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
