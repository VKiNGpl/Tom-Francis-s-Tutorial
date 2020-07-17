using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public float spawnRate;

    float secondsSinceSpawn;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceSpawn = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceSpawn += Time.deltaTime;

        Rigidbody ourRigidbody = GetComponent<Rigidbody>();
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        ourRigidbody.velocity = vectorToPlayer.normalized * speed;

        if (secondsSinceSpawn >= spawnRate)
        {
            Instantiate(gameObject);
            secondsSinceSpawn = 0.0f;
        }
    }
}
