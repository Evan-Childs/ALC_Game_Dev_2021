using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Explosion_Position;
    void OnCollisionEnter (Collision coll) 
    {
        if (coll.collider.CompareTag ("Player")) 
        {
            Explode ();
        }
    }
    void Explode () 
    {
        GameObject Oil_Barrel
         = Instantiate(Explosion_Position,  GameObject.Find("Oil_Barrel (1)").transform.position, Quaternion.identity);
        Oil_Barrel
        .GetComponent<ParticleSystem>().Play();
    }
}
