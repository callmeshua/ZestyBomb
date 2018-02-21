using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Trigger {

    protected Vector3 pos;
    protected Vector3 playerPos;

    protected KeyCode key;

    public GameObject model;
    public bool isActive;
    public float unit;
    public GameObject player;

	// Use this for initialization
	void Start () {

        pos = model.transform.position;

        key = KeyCode.E;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(key))
        {
            playerPos = player.transform.position;
            if (Vector3.Distance(playerPos, pos) < unit)
            {
                
                if (!isActive)
                { 
                    isActive = !isActive;
                    trigger();
                }
                else
                {
                    isActive = !isActive;
                    detrigger();
                }
            }
        }      
	}
}