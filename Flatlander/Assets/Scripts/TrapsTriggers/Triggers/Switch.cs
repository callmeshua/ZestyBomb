using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Trigger {

    public FWSInput fInput;

    protected Vector3 pos;
    protected Vector3 playerPos;

    protected KeyCode key;

    public bool isActive;
    public bool isSign;
    public float unit;

    public GameObject pCtrl;

    public GameObject pressE;
    

	// Use this for initialization
	void Start ()
    {
        fInput = FindObjectOfType<FWSInput>();
        pCtrl = GameObject.FindGameObjectWithTag("Player");

        isActive = false;

        pos = transform.position;

        key = fInput.use;

        pressE.SetActive(false);
    }
    
    //activates the attached traps
    protected void trigger()
    {
        for (int i = 0; i < traps.Capacity; i++)
        {
            if (traps[i] != null && !traps[i].gameObject.activeSelf)
            {
                trap = traps[i];
                trap.gameObject.SetActive(true);
                trap.activate();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        /*
        if (Input.GetButtonDown("Jump") && isActive)
        {
            traps[0].gameObject.SetActive(false);
            isActive = false;
        }
        */
	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Use") && other.transform.tag == "Player")
        {
            if (!isActive)
            {
                Debug.Log("yes");
                Debug.Log(traps[0]);
                traps[0].gameObject.SetActive(true);
                traps[0].activate();
                isActive = true;
            }
            else
            {
                traps[0].gameObject.SetActive(false);
                traps[0].deactivate();
                isActive = false;
            }
        }

        pressE.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" && isActive)
        {
            traps[0].gameObject.SetActive(false);
            traps[0].deactivate();
            isActive = false;
        }
        pressE.SetActive(false);
    }
}