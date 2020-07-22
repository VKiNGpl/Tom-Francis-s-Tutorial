using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;

    public WeaponBehavior myWeapon;
    
    // Start is called before the first frame update
    void Start()
    {
        References.thePlayer = gameObject;
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

        if (Input.GetButton("Fire1"))
        {
            // Fire weapon
            myWeapon.Fire(cursorPosition);
        }
    }
}
