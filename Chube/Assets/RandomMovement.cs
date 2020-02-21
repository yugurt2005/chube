using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public float timer;
    public int newTarget;
    public float speed;
    public NavMeshAgent nav;
    public Vector3 target;



    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= newTarget)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            updateSpeed();
            newTargetMethod();
            timer = 0;
        }
    }

    void updateSpeed()
    {
        speed = Random.Range(-5, 6);
    }

    void newTargetMethod()
    {
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;

        float xPos = myX + Random.Range(myX - 100, myX + 100);
        float zPos = myZ + Random.Range(myZ - 100, myZ + 100);

        target = new Vector3(xPos, gameObject.transform.position.y, zPos);

        nav.SetDestination(target);
    }
}
