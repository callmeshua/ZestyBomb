using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRising : MonoBehaviour {

    public GM gm;
    private MeshRenderer mesh;
	public float speed = 1f;
	
	// Update is called once per frame
	void Update () {
		if (gm.phase == GM.Phases.ESCAPE && !gm.gameOver)
        {
			transform.position += transform.up * Time.deltaTime * speed;
        }
	}
}
