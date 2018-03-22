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

    public GameObject particleEffect;

    public Transform lever;
    private Transform leverInit;
    

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(true);
        fInput = FindObjectOfType<FWSInput>();
        pCtrl = GameObject.FindGameObjectWithTag("Player");

        isActive = false;

        pos = transform.position;

        key = fInput.use;

        pressE.SetActive(false);

        leverInit = lever;
    }
    
    //activates the attached traps
    protected void trigger()
    {
        for (int i = 0; i < traps.Capacity; i++)
        {
            if (traps[i] != null && !traps[i].gameObject.activeSelf)
            {
                if(traps[i].transform.tag == "shooter")
                {
                    traps[i].gameObject.GetComponent<Shooter>().shooterType = Shooter.ShooterType.STANDARD;
                }
                else
                {
                    traps[i].gameObject.SetActive(true);
                    traps[i].activate();
                }
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

    private void OnTriggerEnter(Collider other)
    {
        pressE.SetActive(true);
        spawnParticleEffect(pressE.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Use") && other.transform.tag == "Player")
        {
            if (!isActive)
            {
                for (int i = 0; i < traps.Capacity; i++)
                {
                    if (traps[i] != null)
                    {
                        if (traps[i].transform.tag == "shooter")
                        {
                            if (traps[i].gameObject.GetComponent<Shooter>().shooterType == Shooter.ShooterType.TIMED)
                            {
                                traps[i].gameObject.GetComponent<Shooter>().shooterType = Shooter.ShooterType.STANDARD;
                                if (gameObject.name == "Switch")
                                {
                                    lever.rotation = Quaternion.Euler(new Vector3(45, -90, 0));
                                }
                            }
                            else
                            {
                                traps[i].gameObject.GetComponent<Shooter>().shooterType = Shooter.ShooterType.TIMED;
                                if (gameObject.name == "Switch")
                                {
                                    lever.rotation = Quaternion.Euler(new Vector3(-45, -90, 0));
                                }
                            }
                        }
                        else
                        {
                            Debug.Log(traps[i]);
                            if (!traps[i].gameObject.activeSelf)
                            {
                                traps[i].gameObject.SetActive(true);
                                spawnParticleEffect(traps[0].transform);
                                isActive = true;
                            }
                        }
                    }
                }
            }
            else
            {
                traps[0].gameObject.SetActive(false);
                traps[0].deactivate();
                isActive = false;
                spawnParticleEffect(traps[0].transform);
            }
        }
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
        spawnParticleEffect(pressE.transform);
    }

    public void spawnParticleEffect(Transform t)
    {
        Transform clone = Instantiate(particleEffect.transform, t.position, Quaternion.identity);
        particleEffect.SetActive(true);
        Destroy(clone.gameObject, 0.75f);
    }
}