using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//USED
//Zachary Lopez
// Attach to parent object, children randdomly get turned off, 50% chance

public class VaryObjects : MonoBehaviour {
	private int length;
	private Transform child;
	// Use this for initialization
	void Start () {
		//get # of children 
		length = transform.childCount;
		// go through each child and randomly turn off
		for (int i = 0; i < length; i++)
		{
			child = transform.GetChild (i);
			int rand = Random.Range (0, 99);
			//Debug.Log (rand);
			if (rand < 50)
				child.gameObject.SetActive (false);
			else
				child.gameObject.SetActive(true);
		}
	}

}
