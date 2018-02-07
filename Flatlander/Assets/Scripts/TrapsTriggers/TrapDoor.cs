using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : Trap {

    Rigidbody doorRb;

	// Use this for initialization
	void Start () {
        active = false;
        doorRb = gameObject.GetComponent<Rigidbody>();
	}
	
    public void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.gameObject.tag == "Player")
            active = true;
    }

    public override void checkActive()
    {
        if(active)
        {
            doorRb.isKinematic = false;
        }
    }
}
