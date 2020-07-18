using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageReceived)
    {
        health -= damageReceived;
        if (health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
