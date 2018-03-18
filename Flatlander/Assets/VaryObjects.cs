using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaryObjects : MonoBehaviour {
	private int length;
	private Transform child;
	// Use this for initialization
	void Start () {
		length = transform.childCount;

		for (int i = 0; i < length; i++)
		{
			child = transform.GetChild (i);
			int rand = Random.Range (0, 99);
			Debug.Log (rand);
			if (rand < 50)
				child.gameObject.SetActive (false);
			else
				child.gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
