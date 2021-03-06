﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    public int selectedWeaponIndex;

    public List<WeaponBehavior> weapons = new List<WeaponBehavior>();
    
    // Start is called before the first frame update
    void Start()
    {
        References.thePlayer = gameObject;
        selectedWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // WSAD to move
 
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        Rigidbody ourRigidbody = GetComponent<Rigidbody>();
        ourRigidbody.velocity = inputVector * speed;

        // Get position of cursor in Player plane
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);

        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);      // face new position  

        // Left click to fire

        if (Input.GetButton("Fire1") && weapons.Count > 0)
        {
            // Fire weapon
            weapons[selectedWeaponIndex].Fire(cursorPosition);
        }
        
        if (Input.GetButtonDown("Fire2") && weapons.Count > 0)
        {
            // change weapon
            ChangeWeaponIndex(selectedWeaponIndex + 1);
            
        }
    }

    private void ChangeWeaponIndex(int index)
    {
        selectedWeaponIndex = index % weapons.Count;

        for (int i = 0; i < weapons.Count; i++)
        {
            if (i != selectedWeaponIndex)
            {
                weapons[i].gameObject.SetActive(false);
            }
            else
            {
                weapons[i].gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        WeaponBehavior theirWeapon = other.GetComponentInParent<WeaponBehavior>();
        if (theirWeapon != null)
        {
            weapons.Add(theirWeapon);
            theirWeapon.transform.position = transform.position;
            theirWeapon.transform.rotation = transform.rotation;
            theirWeapon.transform.SetParent(transform);
            ChangeWeaponIndex(weapons.Count - 1);
        }    
    }
}
