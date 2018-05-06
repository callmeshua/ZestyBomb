using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVariation : MonoBehaviour {
    GM gm;
    public List<GameObject> elements;

    public int pathNum;
	// Use this for initialization
	void Start () 
	{
        gm = FindObjectOfType<GM>();
        RandomizePath();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(Input.GetButtonDown("Reset")||gm.resetLevel)
  //      {
  //          RandomizePath();
  //      }
    }

    public void RandomizePath()
    {
        pathNum=Random.Range(0,elements.Count);

        elements[pathNum].SetActive(true);

        for (int i = 0; i < elements.Count; i++)
        {
            if (i != pathNum)
            {
                elements[i].SetActive(false);
            }
        }
    }
}
