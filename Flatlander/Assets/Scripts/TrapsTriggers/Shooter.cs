using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from Trap class
public class Shooter : Trap {

    //PUBLIC REFERENCES
    public GameObject dart;
    public AudioClip shootSound;

    //PUBLIC ATTRIBUTES
    public bool canShoot;
    public float power;
    public bool LimitLifetime;
    public float dartLifetime;
    public float DelayBetweenShots;
    public float fireCount;

	// Use this for initialization
	void Start () {
        active = false;
        canShoot = true;
        fireCount = 0;
	}

    private void FixedUpdate()
    {
        //delays the rapidfire shots
        if(canShoot == false && fireCount <= DelayBetweenShots)
        {
            fireCount++;
        }
        if(fireCount > DelayBetweenShots)
        {
            canShoot = true;

        }

    }

    //instantiates dart prefab to shoot
    public void shoot()
    {
        var bullet = (GameObject)Instantiate(dart, transform.position + (transform.forward), transform.rotation);
        SoundManager.PlaySFX(shootSound, true, .3f);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * power;

        if (LimitLifetime)
        {
            Destroy(bullet, dartLifetime);
        }
        active = false;
    }

    //if active and can shoot, shoot, then reset rof
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
