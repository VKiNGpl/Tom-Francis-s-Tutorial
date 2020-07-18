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
    //float secondsSinceDamage;
    // Start is called before the first frame update
    void Start()
    {
        secondsSinceSpawn = 0.0f;
        //secondsSinceDamage = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceSpawn += Time.deltaTime;

        if (References.thePlayer != null)
        {
            Rigidbody ourRigidbody = GetComponent<Rigidbody>();
            Vector3 vectorToPlayer = References.thePlayer.transform.position - transform.position;
            ourRigidbody.velocity = vectorToPlayer.normalized * speed;
        }

        if (secondsSinceSpawn >= spawnRate)
        {
            Instantiate(gameObject);
            secondsSinceSpawn = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision thisCollision)
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
