using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour {

    //darts avoid coll with other dars
    //if collides with player, destroy
    //ignore physics
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Dart")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject, 2f);
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
            Destroy(gameObject, 2f);
        }
        
    }
}
