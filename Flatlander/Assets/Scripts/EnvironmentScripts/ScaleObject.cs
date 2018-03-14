using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour {
	private MeshRenderer mesh;
	public Material mat;
	private float x;
	private float y;
	private float z;
	public int xi;
	public int yi;
	public int zi;
	// Use this for initialization
	void Start () {
		mesh = GetComponentInChildren<MeshRenderer>();
		mat = mesh.material;
		x = transform.localScale.x;
		y = transform.localScale.y;
		z = transform.localScale.z;
		xi = Mathf.RoundToInt (x);
		yi = Mathf.RoundToInt (y);
		zi = Mathf.RoundToInt (z);
		//clone.Resize (clone.width * zi, clone.height * yi);
		///TextureScale.Bilinear (clone, tex.width * zi, tex.height * yi);
		mat.mainTextureScale = new Vector2 (xi, yi);

	}

	void Update()
	{
		Start ();
	}
}
