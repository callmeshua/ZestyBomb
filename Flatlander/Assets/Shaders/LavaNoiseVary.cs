using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LavaNoiseVary : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MaterialPropertyBlock props = new MaterialPropertyBlock();
		MeshRenderer renderer;

		Vector2 v=new Vector2(Random.Range(0,1f),Random.Range(0,1f));
		props.SetVector("_NoiseOff",v);
		//Debug.Log(v);

		renderer = GetComponent<MeshRenderer>();
		renderer.SetPropertyBlock(props);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
