using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyable : MonoBehaviour {

    public Vector3 velocity;
    public Rigidbody ballRB;
    public bool hascollided;

    private void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        hascollided = false;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(hascollided == false)
        {
            velocity = ballRB.velocity;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "hazard" || collision.gameObject.tag == "Interactable")
        {
            hascollided = true;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
            collision.gameObject.SetActive(false);
            ballRB.velocity = velocity;
            hascollided = false;
        }
    }
}
