using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Trap {


    public GameObject visual;


    // Use this for initialization
    void Start () {
        active = true;
	}

    public override void checkActive()
    {
        print(active);
        if (active != false)
        {
            print(active);
            visual.SetActive(true);
        }
        else
        {
            print(active);
            visual.SetActive(false);
        }
    }
}
