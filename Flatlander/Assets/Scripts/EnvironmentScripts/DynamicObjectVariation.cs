using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectVariation : MonoBehaviour {

	public Rigidbody objRb;
	public Vector2 massRange;
	public Vector2 scaleRange;
    public bool setScale=true;
    public bool setMass=true;
    GM gm;

	// Use this for initialization
	void Start () 
	{
        gm = FindObjectOfType<GM>();
		objRb = gameObject.GetComponent<Rigidbody> ();

        Randomize();
	}

    private void Update()
    {
        //if(gm.resetLevel)
        //{
        //    Randomize();
        //}
    }

    public void Randomize()
    {
        if (setMass)
            objRb.mass = Random.Range(massRange.x, massRange.y);

        if (setScale)
            gameObject.transform.localScale = new Vector3(transform.localScale.x, Random.Range(scaleRange.x, scaleRange.y), transform.localScale.z);
    }
}
