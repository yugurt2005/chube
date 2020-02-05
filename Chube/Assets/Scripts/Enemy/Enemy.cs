using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour //TODO: inherit from pathfinder
{

    /*
     * different enemies do different things.
     * Droid enemy will kill troops first - shoot bullets within 2 blocks, and then try to reach chube.
     * Other enemies will do the opposite, and will move differently (jump tiles) and have different attacking methods.
     */

    public float health = 10f;
    public int attackrange = 2;

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    private void Update()
    {
        //find enemy in attack range and fire. -NOTE: ENEMY CAN'T MOVE WHILE SHOOTING.
    }

    private void FixedUpdate()
    {
        //put movement and pathfinding here
    }
}
