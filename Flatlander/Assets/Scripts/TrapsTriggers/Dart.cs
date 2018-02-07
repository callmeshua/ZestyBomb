using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Dart")
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision.gameObject.GetComponent<BoxCollider>(), true);
        }
        
    }
}
