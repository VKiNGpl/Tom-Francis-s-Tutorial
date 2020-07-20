﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("health")]    // Allows for retainig presets for values in Unity. Need to provide old name
    public float maxHealth;
    
    float currentHealth;

    public GameObject healthBarPrefab;

    HealthBarBehavior myHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        //Create our healt panel on the canvas Raferences.canvas
        currentHealth = maxHealth;
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBarBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Have health bar reflect health
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
        // Have health bar follow the game object
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
    }

    private void OnDestroy()
    {
        if (myHealthBar != null)
        {
            Destroy(myHealthBar.gameObject);
        }
    }

    public void TakeDamage(float damageReceived)
    {
        currentHealth -= damageReceived;

        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}


/* private void LateUpdate() 
 * {
 *      if (myPerson != null)
 *      {
 *          transform.position = Managers.Camera.camera.WorldToScreenPoint(myPerson.transform.position + floatOffset);
 *      }
*/