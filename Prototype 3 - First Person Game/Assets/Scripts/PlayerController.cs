using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement variables
    public float moveSpeed;
    //camera variables
    public float lookSensitivity; //mouse look sensitivity
    public float maxLookX; //Lowest position we can look(neck hitting body)
    public float minLookX; // Highest poisition we can look
    private float rotX; // Current X rotation of the camera
    //Gameobjects & Component variables
    private Camera cam;
    private Rigidbody rb;

    void Awake()
    {
        //Get the components
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CamLook();
    }
    
    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        rb.velocity = new Vector3(x, rb.velocity.y, z);
    }
    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX =Input.GetAxis("Mouse Y") * lookSensitivity;
        //Clamps camera up/down rotation
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        //Apply rotation to Camera
        cam.transform.localRotation = Quaternion.Euler(-rotX,0,0);
        transform.EulerAngles += Vector3.up * y;
    }
}
