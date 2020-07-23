using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float spawnRate;
    public float damageInterval;
    public float damageRate;
    public float visionRange;
    public float visionConeAngle;
    public bool playerSpotted;
    public Light myLight;

    float secondsSinceSpawn;

    Rigidbody ourRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        secondsSinceSpawn = 0.0f;
        playerSpotted = false;
        myLight.color = Color.white;
        ourRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceSpawn += Time.deltaTime;

        if (References.thePlayer != null)
        {
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;

            if (playerSpotted)
            {
                //Follow the player
                
                ourRigidbody.velocity = vectorToPlayer.normalized * speed;
                Vector3 playerPositionatOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                transform.LookAt(playerPositionatOurHeight);
                myLight.color = Color.red;
            }
            else
            {
                //Rotate
                Vector3 lateralOffset = transform.right * Time.deltaTime * turnSpeed;
                transform.LookAt(transform.position + transform.forward + lateralOffset);
                ourRigidbody.velocity = transform.forward * speed;
                //Check if player is in vision cone
                if (Vector3.Distance(transform.position, playerPosition) <= visionRange)
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
                    {
                        playerSpotted = true;
                    }
                }
            }
        }

        if (secondsSinceSpawn >= spawnRate && spawnRate != 0)
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
