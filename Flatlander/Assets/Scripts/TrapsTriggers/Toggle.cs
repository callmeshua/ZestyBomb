using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : Trigger {

    protected Vector3 pos;
    public GameObject buttonModel;

    // Use this for initialization
    void Start()
    {
        pos = buttonModel.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            trigger();
            buttonModel.transform.position = new Vector3(pos.x, pos.y - .2f, pos.z);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            buttonModel.transform.position = pos;
        }
    }
}
