using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public float spawnRate;
    public float damageInterval;
    public float damageRate;

    float secondsSinceSpawn;

    protected Rigidbody ourRigidbody;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        secondsSinceSpawn = 0.0f;
        ourRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ChasePlayer();
    }

    protected void ChasePlayer()
    {
        if (References.thePlayer != null)
        {
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;
            ourRigidbody.velocity = vectorToPlayer.normalized * speed;
            Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
            transform.LookAt(playerPositionAtOurHeight);
        }
    }

    protected void SpawnEnemyCopy()
    {
        secondsSinceSpawn += Time.deltaTime;

        if (secondsSinceSpawn >= spawnRate && spawnRate != 0)
        {
            Instantiate(gameObject);
            secondsSinceSpawn = 0.0f;
        }
    }

    protected void OnCollisionEnter(Collision thisCollision)
    {
        if (thisCollision.gameObject != null)
        {
            GameObject theirGameObject = thisCollision.gameObject;

            if (theirGameObject.GetComponent<PlayerBehavior>() != null)
            //secondsSinceDamage += Time.deltaTime;
            {
                HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();
                if (theirHealthSystem != null)      //  && secondsSinceDamage >= 1 / damageRate
                {
                    theirHealthSystem.TakeDamage(damageInterval);
                    //secondsSinceDamage = 0.0f;
                }
            }
        }
    }
}