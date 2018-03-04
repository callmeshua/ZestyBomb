using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {
    private Transform trans;
    private float z;
	// Use this for initialization
	void Start () {
        trans = transform;
        z = trans.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
        trans.Rotate(new Vector3(0, 0, z * Time.deltaTime));
	}
}
