using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallCollision : MonoBehaviour
{

    public float xRange = 9.73f;
    public float yRange = 4.23f;

    // Update is called once per frame
    void Update()
    {
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
    }
}
