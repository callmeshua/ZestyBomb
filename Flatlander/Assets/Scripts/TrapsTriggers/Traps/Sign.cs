using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Trap {
    
    public GameObject visual;

    // Use this for initialization
    void Start () {
        gm = FindObjectOfType<GM>();
	}

    public override void checkActive()
    {
    }
}
