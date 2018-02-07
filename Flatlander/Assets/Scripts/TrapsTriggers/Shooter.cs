using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Trap {

    public GameObject dart;
    public bool canShoot;
    public float rateOfFire;
    public float fireCount;

	// Use this for initialization
	void Start () {
        active = false;
        canShoot = true;
        rateOfFire = 30;
        fireCount = 0;
	}

    private void FixedUpdate()
    {
        if(canShoot == false && fireCount <= rateOfFire)
        {
            fireCount++;
        }
        if(fireCount > rateOfFire)
        {
            canShoot = true;

        }

    }

    public void shoot()
    {
        var bullet = (GameObject)Instantiate(dart, transform.position + (transform.forward), transform.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 5;

        Destroy(bullet, 2.0f);
        active = false;
    }

    public override void checkActive()
    {
        if (active && canShoot)
        {
            shoot();
            canShoot = false;
            fireCount = 0;
        }
    }
}
