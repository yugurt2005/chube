using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public float difficultyMultiplier = 1;

    private float timeSinceStart;

    [SerializeField]
    private int addDifficultyEvery = 15;
    private float difficultyDelta = 0.25f;

    // Update is called once per frame
    void Update()
    {
        timeSinceStart = Time.timeSinceLevelLoad;
        difficultyMultiplier = 1 + ((int)timeSinceStart / addDifficultyEvery) * difficultyDelta;
    }
}
