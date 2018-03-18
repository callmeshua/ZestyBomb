using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//USED?
//Zachary Lopez
// Executes in edit mode for level designers to see
// Needs to be placed on the parent object that doesn't have a mesh renderer
// Takes texture and scales the tiling in the X and Y axises.
// Objects MUST be have a (0,0,0) rotation!

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
