using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Trap {

    public GameObject dart;
    public bool canShoot;
    public AudioClip shootSound;
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
        if(canShoot == false && fireCount <= DelayBetweenShots)
        {
            fireCount++;
        }
        if(fireCount > DelayBetweenShots)
        {
            canShoot = true;

        }

    }

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
