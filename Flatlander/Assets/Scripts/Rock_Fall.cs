using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * USED
 * ================================
 * Zac Lopez
 * 
 * Rock_fall can be attached to any game object.
 * After the object detects a collision with the players hook, it waits two seconds
 * and then falls by setting kinematic on objects rb(rigidbody) to be true.
 * Seconds is a public variable that can be changed in the game scene
 */ 
public class Rock_Fall : MonoBehaviour {

	public string collideWithTag = "Interactable";
    public bool playerColCanActivate=true;
    private Rigidbody rb;
    private float delaySeconds;

    //JK~
    //SOUNDS
    public AudioClip impactClip;
    public AudioClip boulderSoundClip;

    // Use this for initialization
    void Start () {
		rb = gameObject.GetComponent<Rigidbody>();

	}
		
    // checks for collision with hook/grapple
	void OnCollisionEnter(Collision col)
	{
        Vector3 velocity = col.relativeVelocity;


        if (col.gameObject.CompareTag("Hook") || col.gameObject.CompareTag(collideWithTag))
        {
            if(delaySeconds == 0f)
            {
                rb.isKinematic = false;
            }
            else
            {
                StartCoroutine(Fall());
            }  
        }
        else if (col.gameObject.name == "Player" && playerColCanActivate)
        {
            if (delaySeconds == 0f)
            {
                rb.isKinematic = false;
            }
            else
            {
                StartCoroutine(Fall());
            }
        }
        else
        {
            SoundManager.PlaySFX(impactClip, true, Mathf.Clamp01(col.impulse.magnitude/80f));
            //SoundManager.PlaySFX(boulderSoundClip, true, 1f);
        }

    } 

    // sets kinematic to be true after alloted seconds
	IEnumerator Fall()
	{
		yield return new WaitForSeconds (delaySeconds);
		rb.isKinematic = false;
		//rb.AddTorque (Vector3.forward * 2, ForceMode.Impulse);
	}


}
