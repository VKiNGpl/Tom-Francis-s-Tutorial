﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : MonoBehaviour
{
    public GameObject smallEnemyPrefab;
    public GameObject bigEnemyPrefab;

    public float secondsBetweenSpawns;
    float secondsSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastSpawn = 0.0f;
    }

    // Fixed update happens the same number of times for all players, so it's a good place for gameplay critical updates.
    private void FixedUpdate()
    {
        secondsSinceLastSpawn += Time.deltaTime;

        if (secondsSinceLastSpawn >= secondsBetweenSpawns)
        {
            Instantiate(smallEnemyPrefab, transform.position, transform.rotation);
            secondsSinceLastSpawn = 0.0f;
        }
    }
}
