using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Health,
    Ammo
}
public class Pickup : MonoBehaviour
{
    public PickupType Type;
    public int value;

    [Header ("Bobbing Anim")]
    public float rotationSpeed;
    public float bobSpeed;
    public float bobHeight;
    private Vector3 startPos;
    private bool bobbingUp;

    void Start()
    {
        //set the start position
        startPos = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController Player = other.GetComponent<PlayerController>();

            switch(Type)
            {
                case PickupType.Health: 
                Player.GiveHealth(value);
                break;

                case PickupType.Ammo:
                Player.GiveAmmo(value);
                break;
            }
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //Rotating
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        //
        Vector3 offset = (bobbingUp == true ? new Vector3(0, bobHeight/2, 0) : (new Vector3(0, -bobHeight/2, 0)));
        transform.position = Vector3.MoveTowards(transform.position, startPos + offset, bobSpeed * Time.deltaTime);

        if(transform.position == startPos + offset)
            bobbingUp = !bobbingUp;
    }
}
