using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class; does not get implemented
public abstract class Trap : MonoBehaviour {

    //status of activation
    protected bool active;
    public GM gm;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GM>();
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
        checkActive();

	}

    //sets active to true
    public void activate()
    {
        active = true;
    }

    //sets active to false
    public void deactivate()
    {
        active = false;
    }
    
    //implemented in children
    public abstract void checkActive();
}
