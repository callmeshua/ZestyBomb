using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class; does not get implemented
public abstract class Trigger : MonoBehaviour {

    //the attached trap getting activated
    public Trap trap;

    //activates the attached trap
    protected void trigger()
    {
        trap.activate();
    }

    //deactivates the attached trap
    protected void detrigger()
    {
        trap.activate();
    }
}
