﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    //PUBLIC SCRIPT REFERENCES
    public GM gm;
    public GrappleController gCtrl;

    //Objects in engine
    public GameObject ws;   //win screen
    public GameObject ds;   //death screen
    public GameObject ps;   //pause screen
    public Text phaseText;
    public Text shotsText;
    public float shots;

    //PRIVATE BOOLEANS
    private bool pauseScreen;
    private bool winScreen;
    private bool deathScreen;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GM>();
        gCtrl = FindObjectOfType<GrappleController>();
        ws.SetActive(false);
        ds.SetActive(false);
        ps.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        checkPause();
        checkDead();
        checkWin();
        updatePhaseText();
        updateShotsText();
	}

    //checks if game is paused for pause screen
    public void checkPause()
    {
        if (gm.paused && !gm.gameOver && gm.frozen)
        {
            ps.SetActive(true);
            gm.handlePauseScreen();
        }
        else
        {
            ps.SetActive(false);
        }
    }

    public void checkDead()
    {
        if (gm.gameOver && !gm.paused && !gm.exitArea.win)
        {
            ds.SetActive(true);
            gm.handleDeath();
        }
        else
        {
            ds.SetActive(false);
        }
    }

    public void checkWin()
    {
        if (gm.exitArea.win && !gm.gameOver && !gm.paused)
        {
            ws.SetActive(true);
            gm.handleWin();
        }
        else
        {
            ws.SetActive(false);
        }
    }

    public void updatePhaseText()
    {
        phaseText.text = "Phase: " + gm.getPhase();
    }

    public void updateShotsText()
    {
        shots = gCtrl.shots;
        shotsText.text = shots.ToString();
    }

}
