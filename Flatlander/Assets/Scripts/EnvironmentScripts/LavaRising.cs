﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRising : MonoBehaviour {

    public GM gm;
    private MeshRenderer mesh;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (gm.phase == GM.Phases.ESCAPE)
        {
            transform.position += transform.up * Time.deltaTime;
        }
	}
}
