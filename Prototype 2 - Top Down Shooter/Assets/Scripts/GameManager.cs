using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isDoorLocked;
    public bool hasKey;


    // Start is called before the first frame update

    void Start()
    {
        hasKey = false;
        isDoorLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasKey && !isDoorLocked){
            print("Exit out the door");
            Destroy(gameObject);
        }
    }
}
