using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripWire : Toggle {

    public GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            trigger();
            Destroy(parent);
        }
    }
}
