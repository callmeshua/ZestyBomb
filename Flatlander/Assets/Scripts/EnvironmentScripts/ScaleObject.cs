using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour {
	private MeshRenderer mesh;
	public Material mat;
	private float y;
	private float z;
	public int yi;
	public int zi;
	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshRenderer> ();
		mat = mesh.material;
		y = transform.localScale.y;
		z = transform.localScale.y;
		yi = Mathf.RoundToInt (y);
		zi = Mathf.RoundToInt (z);
		//clone.Resize (clone.width * zi, clone.height * yi);
		///TextureScale.Bilinear (clone, tex.width * zi, tex.height * yi);
		mat.mainTextureScale = new Vector2 (zi, yi);



	}
}
