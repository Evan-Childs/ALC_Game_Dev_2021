using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    private float vInput;
    private float hInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");


        //moves tank
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);

        //rotates tank
        transform.Rotate(Vector3.up, turnSpeed * hInput * Time.deltaTime);
    }
}
