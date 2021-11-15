using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private Rigidbody playerRb;
    private float zBound = 24.2f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ConstrainMovement();
        Jump();
    }
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * horizontalInput);
        playerRb.AddForce(Vector3.right * speed * verticalInput);
    }

    void ConstrainMovement()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }

        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
        
            playerRb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
