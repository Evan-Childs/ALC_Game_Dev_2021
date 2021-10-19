using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement variables
    public float moveSpeed;
    public float jumpForce;
    //camera variables
    public float lookSensitivity; //mouse look sensitivity
    public float maxLookX; //Lowest position we can look(neck hitting body)
    public float minLookX; // Highest poisition we can look
    private float rotX; // Current X rotation of the camera
    //Gameobjects & Component variables
    private Camera cam;
    private Rigidbody rb;
    private Weapons weapons;

    void Awake()
    {
        //Get the components
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        weapons = GetComponent<Weapons>();
        //Disable cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CamLook();
        //Jump when spacebar is pressed
        if(Input.GetButtonDown("Jump"))
            Jump();
        if(Input.GetButton("Fire1"))
        {
            if(weapons.CanShoot())
            {
                weapons.Shoot();
            }
        }
    }
    
    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 dir = transform.right * x + transform.forward * z;
        //Jump direction
        dir.y = rb.velocity.y;
        //Move in the direction of the camera
        rb.velocity = dir;
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, 1.1f))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }    
    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        //Clamps camera up/down rotation
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        //Apply rotation to Camera
        cam.transform.localRotation = Quaternion.Euler(-rotX,0,0);
        transform.eulerAngles += Vector3.up * y;
    }
}
