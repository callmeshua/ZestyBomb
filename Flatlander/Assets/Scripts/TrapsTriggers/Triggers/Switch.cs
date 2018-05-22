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
                if(traps[i].transform.tag == "shooter")
                {
                    traps[i].gameObject.GetComponent<Shooter>().shooterType = Shooter.ShooterType.STANDARD;
                }
                else
                {
                    trap.activate();
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (pressE.activeSelf && Input.GetButtonDown ("Use")) 
		{
			isActive = !isActive;
		}
			

		if (isActive) 
		{
			for (int i = 0; i < traps.Count; i++) {
				traps [i].gameObject.SetActive (false);
			}
		} 
		else 
		{
			for (int i = 0; i < traps.Count; i++) {
				traps [i].gameObject.SetActive (true);
			}
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        pressE.SetActive(true);
        spawnParticleEffect(pressE.transform);
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Use") && other.transform.tag == "Player")
        {
			isActive = !isActive;



            if (!isActive)
            {
				traps[0].gameObject.SetActive(isActive);
                traps[0].activate();
                isActive = true;
                spawnParticleEffect(traps[0].transform);
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
	*/

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" && isActive)
        {
            //traps[0].gameObject.SetActive(false);
            //traps[0].deactivate();
            //isActive = false;
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