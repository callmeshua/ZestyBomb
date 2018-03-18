using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour {
	private float x;
	private float y;
	public float x_blocks;
	public float y_blocks;
	private float width;
	private float height;
	private Vector3 size;
	private int x_count;
	private int y_count;
	public GameObject duplicant;
	private bool alive = false;
	// Use this for initialization
	void Start () {
		size = transform.localScale;
		width = size.x;
		height = size.y;

		if (duplicant != null) {
			x = duplicant.transform.localScale.x;
			y = duplicant.transform.localScale.y;
		}

		x_count = Mathf.RoundToInt (width / x);
		y_count = Mathf.RoundToInt (height / y);

		x_count = Mathf.Abs (x_count);
		y_count = Mathf.Abs (y_count);
		Debug.Log (x_count);
		Debug.Log (y_count);

		size = new Vector3 (x, y, size.z);
		transform.localScale = size;

		if (!alive) 
		{
			alive = true;

			for (int i = 1; i < x_blocks; i++)
				Instantiate (duplicant, new Vector3 (transform.position.x + (x * i), transform.position.y, transform.position.z), transform.rotation);

			for (int i = 1; i < y_blocks; i++)
				Instantiate (duplicant, new Vector3 (transform.position.x, transform.position.y + (y * i), transform.position.z), transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Start ();
	}

	void Create()
	{

	}
}
