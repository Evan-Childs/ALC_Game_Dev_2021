using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.00f;
    public float turnSpeed = 10.00f;
    private float vInput;
    private float hInput;
    

    // Update is called once per frame
    void Update()
    {
        //Gathering Keyboard Input(Movement)
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        //Moves Player Character
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);
        //Rotates Player Character
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime * hInput);
    }
}
