using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectVariation : MonoBehaviour {

	public Rigidbody objRb;
	public Vector2 massRange;
	public Vector2 scaleRange;

	// Use this for initialization
	void Start () 
	{
		objRb = gameObject.GetComponent<Rigidbody> ();
		objRb.mass = Random.Range (massRange.x,massRange.y);
		gameObject.transform.localScale=new Vector3(transform.localScale.x,Random.Range (scaleRange.x,scaleRange.y),transform.localScale.z);
	}
}
