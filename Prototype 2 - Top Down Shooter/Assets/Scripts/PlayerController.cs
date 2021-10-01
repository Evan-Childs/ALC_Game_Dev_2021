using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.00f;
    public float turnSpeed = 10.00f;
    private float vInput;
    private float hInput;
    public GameObject Projectile;
    public Vector3 offset = new Vector3(0,1,0);
    public float xRange = 9.98f;
    public float yRange = 4.5f;

    

    // Update is called once per frame
    void Update()
    {
        //Gathering Keyboard Input(Movement)
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        //Moves Player Character
        transform.Translate(Vector3.up * speed * Time.deltaTime * vInput);
        //Rotates Player Character
        transform.Rotate(Vector3.forward, -turnSpeed * Time.deltaTime * hInput);
        //creates left and right walls
        if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange,transform.position.y,transform.position.z);
        }
        if(transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange,transform.position.y,transform.position.z);
        }

        //creates top and bottom walls
        if(transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
        if(transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }

        //Shoots bullet
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Projectile, transform.position + offset, Projectile.transform.rotation);
        }
    }
}
