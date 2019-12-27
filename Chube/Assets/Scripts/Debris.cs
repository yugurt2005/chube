using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private bool startedWaiting = false;

    public float scale;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!startedWaiting)
        {
            startedWaiting = true;
            rb.AddForce(transform.forward * scale, ForceMode2D.Impulse);
            StartCoroutine(wait(Random.Range(3,7)));
        }
    }

    IEnumerator wait(int time)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds(time);

        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        startedWaiting = false;
    }
}
