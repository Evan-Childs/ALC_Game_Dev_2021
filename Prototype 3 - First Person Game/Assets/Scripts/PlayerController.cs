using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]//movement variables
    public float moveSpeed;
    public float sprintSpeed;
    private bool isShiftKeyDown;
    public float jumpForce;
    [Header("Camera")]//camera variables
    public float lookSensitivity; //mouse look sensitivity
    public float maxLookX; //Lowest position we can look(neck hitting body)
    public float minLookX; // Highest poisition we can look
    private float rotX; // Current X rotation of the camera
    [Header("Gameobjects & Components")]//Gameobjects & Component variables
    private Camera cam;
    private Rigidbody rb;
    private Weapons weapons;

    [Header("Stats")]
    public int curHP;
    public int maxHP;

    void Awake()
    {
        //Get the components
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        weapons = GetComponent<Weapons>();
        //Disable cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Start()
    {
        GameUI.instance.UpdateHealthBar(curHP, maxHP);
        GameUI.instance.UpdateScoreText(0);
        GameUI.instance.UpdateAmmoText(weapons.curAmmo, weapons.maxAmmo);
    }

    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if(curHP <= 0)
            Die();
        GameUI.instance.UpdateHealthBar(curHP, maxHP);
    }

    void Die()
    {
        GameManager.instance.LoseGame();
    }
 
    // Update is called once per frame
    void Update()
    {
        //Don't do anything when game is paused
        if(GameManager.instance.gamePaused == true)
            return;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isShiftKeyDown = true;
        }
        else
        {
            isShiftKeyDown = false;
        }
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
        if(isShiftKeyDown == true)
            sprintSpeed = 2f;
        if(isShiftKeyDown == false)
            sprintSpeed = 1f;
        
        float x = Input.GetAxis("Horizontal") * moveSpeed * sprintSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed * sprintSpeed;

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
    public void GiveHealth(int amountToGive)
    {
        curHP = Mathf.Clamp(curHP + amountToGive, 0, maxHP);
        GameUI.instance.UpdateHealthBar(curHP, maxHP);
    }

    public void GiveAmmo(int amountToGive)
    {
        weapons.curAmmo = Mathf.Clamp(weapons.curAmmo + amountToGive, 0, weapons.maxAmmo);
        GameUI.instance.UpdateAmmoText(weapons.curAmmo, weapons.maxAmmo);
    }
}
