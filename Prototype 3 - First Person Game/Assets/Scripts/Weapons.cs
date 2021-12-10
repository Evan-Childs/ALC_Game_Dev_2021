using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public ObjectPool bulletPool;
    public Transform muzzle;

    public int curAmmo;
    public int maxAmmo;
    public float bulletSpeed;    
    public bool infiniteAmmo;

    public float shootRate;
    private float lastShootTime;
    private bool isPlayer;

    private void Awake()
    {
        // are we attached to the player
        if(GetComponent<PlayerController>())
        {
            isPlayer = true;
        }
    }
    
    public bool CanShoot()
    {
        if(Time.time - lastShootTime >= shootRate)
        {
            if(curAmmo > 0 || infiniteAmmo == true)
                return true;
        }
        return false;
    }

    public void Shoot()
    {
        lastShootTime = Time.time;
        curAmmo--;

        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        //set the velocity
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;

        if(isPlayer)
        {
            GameUI.instance.UpdateAmmoText(curAmmo, maxAmmo);
        } 
    }
}
