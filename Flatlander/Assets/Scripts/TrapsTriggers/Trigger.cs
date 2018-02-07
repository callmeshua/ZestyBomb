using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour {

    public Trap trap;

    protected void trigger()
    {
        trap.activate();
    }

    protected void detrigger()
    {
        trap.activate();
    }
}
