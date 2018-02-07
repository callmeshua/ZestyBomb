using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour {

    protected bool active;

	// Use this for initialization
	void Start () {
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
        checkActive();
	}

    public void activate()
    {
        active = true;
    }

    public void deactivate()
    {
        active = false;
    }
    
    public abstract void checkActive();
}
