using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public float fireRate;
    public float accuracy;
    public int ammoBurn;

    float secondsSinceFire;

    public GameObject bulletObject;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceFire = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Left click to fire
        secondsSinceFire += Time.deltaTime;        
    }

    public void Fire(Vector3 targetPosition)
    {
        if (Input.GetButton("Fire1") && secondsSinceFire >= 1.0f / fireRate)
        {
            for (int i = 0; i < ammoBurn; i++)
            {
                GameObject newBullet = Instantiate(bulletObject, transform.position + transform.forward, Quaternion.LookRotation(transform.forward));
                float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy;

                // Offset target position by a random amount per accuracy value
                targetPosition.x += Random.Range(-inaccuracy, inaccuracy);
                targetPosition.z += Random.Range(-inaccuracy, inaccuracy);

                newBullet.transform.LookAt(targetPosition);

                secondsSinceFire = 0.0f;

                newBullet.name = i.ToString();
            }
        }
    }
}
