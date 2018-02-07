using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour {

	public Transform target;
	public float speed;

	Vector3 initPosition;
	bool moveTarget = true;


	// Use this for initialization
	void Start () {
		initPosition = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if (moveTarget) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, initPosition, step);
		}

		if (transform.position == target.position) {
			moveTarget = false;
		} else if (transform.position == initPosition) {
			moveTarget = true;
		}


		
	}

	//void OnCollisionStay(Collision other){

	//	if(other.gameObject.tag == "Player"){
	//		other.transform.parent = transform;

			

	//	}
	//}

	//void OnCollisionExit(Collision other){
	//	if(other.gameObject.tag == "Player"){
	//		other.transform.parent = null;

	//	}
	//}  
}
