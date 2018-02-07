using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Toggle {
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            trigger();
            transform.position = new Vector3(pos.x, pos.y - .2f, pos.z);
        }
    }
}
