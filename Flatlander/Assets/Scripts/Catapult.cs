using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour
{
    private Rigidbody rb;
    private float z;
	// Use this for initialization
	void Start ()
	{
        rb = gameObject.GetComponent<Rigidbody>();
        z = gameObject.transform.eulerAngles.x;
        
    }
	
	// Update is called once per frame
	void Update ()
	{
        gameObject.transform.Rotate(0, 0, 15 * Time.deltaTime);
        if (gameObject.transform.eulerAngles.x >= 90)
        {
            z = 90f;
        }
    }
}

