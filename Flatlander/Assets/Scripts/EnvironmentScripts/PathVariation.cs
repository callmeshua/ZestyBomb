using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVariation : MonoBehaviour {

	public GameObject elementsToDisable;
	public GameObject elementsToEnable;
	public bool pathEnabled;
	// Use this for initialization
	void Start () 
	{
		if (Random.Range (0f, 1f) > 0.5f) 
		{
			pathEnabled = true;

			elementsToDisable.SetActive (false);
			elementsToEnable.SetActive (true);
		} 
		else 
		{
			elementsToDisable.SetActive (true);
			elementsToEnable.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
