using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehavior : EnemyBehavior
{
    public float turnSpeed;
    public float visionRange;
    public float visionConeAngle;
    public bool playerSpotted;
    public Light myLight;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        myLight.color = Color.white;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (References.thePlayer != null)
        {
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;

            if (playerSpotted)
            {
                //Follow the player
                ChasePlayer();
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
                        References.spawner.isActive = true;
                    }
                }
            }
        }

        SpawnEnemyCopy();
    }
}
