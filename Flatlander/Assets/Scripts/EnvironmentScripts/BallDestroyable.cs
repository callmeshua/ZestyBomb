using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyable : MonoBehaviour {

    public Vector3 velocity;
    public Rigidbody ballRB;
    public bool hascollided;
	int frames;

    private void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        hascollided = false;
    }

    // Update is called once per frame
    void FixedUpdate () 
	{
		
		if(frames>3){
			velocity = ballRB.velocity;
			frames=0;
		}else{
			frames++;
		}
            
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (checkParentTag(collision.gameObject, "Hazard") || checkParentTag(collision.gameObject, "Interactable") || checkParentTag(collision.gameObject, "Dart"))
        {
            Debug.Log("ball collide");
            hascollided = true;
            //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
			collision.gameObject.SetActive(false);
            ballRB.velocity = velocity;
            hascollided = false;
        }
    }

    //checks if a parent of the gameobject has tag
    public bool checkParentTag(GameObject g, string t)
    {
        Debug.Log(transform.name);
        Transform trans = g.transform;
        if(trans.tag == t)
        {
            return true;
        }

        while (trans.parent != null)
        {
            if (trans.tag == t)
            {
                return true;
            }
            trans = trans.parent.transform;

        }
        return false;
    }
}
