using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    //PUBLIC SCRIPT REFERENCES
    public GM gm;

    //Objects in engine
    public GameObject ws;   //win screen
    public GameObject ds;   //death screen
    public GameObject ps;   //pause screen

    //PRIVATE BOOLEANS
    private bool pauseScreen;
    private bool winScreen;
    private bool deathScreen;

	// Use this for initialization
	void Start () {
        ws.SetActive(false);
        ds.SetActive(false);
        ps.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

    //checks if game is paused for pause screen
    public void checkPause()
    {
        if (gm.paused)
        {
            ps.SetActive(true);
        }
        else
        {
            ps.SetActive(false);
        }
    }
}
