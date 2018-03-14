using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : Trap {

    public GameObject particleEffect;
    public float destroyTime;
    Rigidbody doorRb;

	// Use this for initialization
	void Start () {
        active = false;
        doorRb = gameObject.GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player")
        {
            active = true;
        }
    }

    public override void checkActive()
    {
        if (active)
        {
            doorRb.isKinematic = false;
            if (destroyTime <= 0f)
            {
                GameObject deathParticle = Instantiate(particleEffect, transform.position, transform.rotation);
                Destroy(deathParticle);
                if(transform.parent != null)
                {
                    Debug.Log("Detected a parent!");
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                destroyTime = destroyTime - Time.deltaTime;
            }
        }
    }
}
