using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {
    private float z;
	public bool activated;
	protected Vector3 pos;
	protected Vector3 playerPos;
	GameObject player;
	public float unit;
	// Use this for initialization
	void Start () {
        z = transform.eulerAngles.z;
		activated = false;
		pos = transform.position;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerPos = player.transform.position;
		if (Input.GetButtonDown("Use") && !activated && Vector3.Distance(playerPos, pos) < unit) 
		{
			transform.localEulerAngles = new Vector3 (0, 0, 30);
			transform.position = (new Vector3 (transform.position.x - 0.3f, transform.position.y, transform.position.z));
			activated = true;
		} 
		else if (Input.GetButtonDown("Use") && activated && Vector3.Distance(playerPos, pos) < unit)
		{
			transform.localEulerAngles = new Vector3 (0, 0, 0);
			transform.position = (new Vector3 (transform.position.x + 0.3f, transform.position.y, transform.position.z));
			activated = false;
		}
	}
}
