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
        float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy;
        Vector3 targetOffset = targetPosition;

        if (Input.GetButton("Fire1") && secondsSinceFire >= 1.0f / fireRate)
        {
            for (int i = 0; i < ammoBurn; i++)
            {
                GameObject newBullet = Instantiate(bulletObject, transform.position + transform.forward, Quaternion.LookRotation(transform.forward));
                
                // Offset target position by a random amount per accuracy value
                targetOffset.x += Random.Range(-inaccuracy, inaccuracy);
                targetOffset.z += Random.Range(-inaccuracy, inaccuracy);

                newBullet.transform.LookAt(targetOffset);

                secondsSinceFire = 0.0f;

                newBullet.name = i.ToString();
            }
        }
    }
}
