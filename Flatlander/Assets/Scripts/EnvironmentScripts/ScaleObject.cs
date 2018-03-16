using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScaleObject : MonoBehaviour {
	private MeshRenderer mesh;
	public Material mat;
	private float x;
	private float y;
	private Vector3 ogRot;
	public int xi;
	public int yi;
	// Use this for initialization
	void Start () {
		ogRot = transform.rotation.eulerAngles;
		mesh = GetComponentInChildren<MeshRenderer>();
		mat = mesh.sharedMaterial;

		//

	}

	void Update()
	{
		ScaleTiling ();
	}

	void ScaleTiling()
	{
		x = transform.localScale.x;
		y = transform.localScale.y;

		xi = Mathf.RoundToInt (x);
		yi = Mathf.RoundToInt (y);

		mat.mainTextureScale = new Vector2 (xi, yi);
	}
}
