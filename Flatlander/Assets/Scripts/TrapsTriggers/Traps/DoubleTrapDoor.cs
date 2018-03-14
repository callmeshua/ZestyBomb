using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTrapDoor : Trap {

    private GameObject trapDoor1;
    private GameObject trapDoor2;
    
	// Use this for initialization
	void Start () {
        active = false;
        trapDoor1 = transform.GetChild(0).gameObject;
        trapDoor2 = transform.GetChild(1).gameObject;
	}

    void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            active = true;
        }
    }

    public override void checkActive()
    {
        if (active)
        {
            trapDoor1.GetComponent<TrapDoor>().activate();
            trapDoor2.GetComponent<TrapDoor>().activate();
        }
    }
}
