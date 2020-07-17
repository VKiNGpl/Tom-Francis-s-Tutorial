using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehevior : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody ourRigidbody = GetComponent<Rigidbody>();
        ourRigidbody.velocity = transform.forward * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        
        if (lifeTime < 1.0f)
        {
            transform.localScale *= lifeTime;
        }

        if (lifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;

        if (theirGameObject.GetComponent<EnemyBehavior>() != null)
        {
            Destroy(theirGameObject);
            Destroy(gameObject);
        }
    }
}
