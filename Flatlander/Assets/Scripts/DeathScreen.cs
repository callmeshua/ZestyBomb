using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * USED
 * JOSH KARMEL
 * 
 * ATTACHED TO THE DEATHSCREEN
*/

public class DeathScreen : MonoBehaviour {

    //PUBLIC SCRIPT REFERENCES
    public GM gm;
    public GrappleController gCtrl;

    //public buttons
    public Button reset;
    public Button menu;
    public Text scoreText;
    public GameObject targetPos;
    private Vector3 initPos;

	// Use this for initialization
	void Start () {
        gCtrl = FindObjectOfType<GrappleController>();
        gm = FindObjectOfType<GM>();
        reset.onClick.AddListener(buttonReset);
        menu.onClick.AddListener(buttonMenu);
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (gm.timer <= 0)
        {
            scoreText.text = "Ran out of time!\nLost Souls: " + gm.colCount.ToString() + "/20   Golden Souls: " + gm.goldColCount.ToString() + "/3\nTime Score: " + gm.roundedTimer.ToString() + "   Shots Taken: " + gCtrl.shots + "\nTotal Score: " + gm.calculateScore();
        }
        else if (gm.mode == GM.Modes.LIMSWINGS)
        {
            scoreText.text = "Ran out of shots!\nLost Souls: " + gm.colCount.ToString() + "/20   Golden Souls: " + gm.goldColCount.ToString() + "/3\nTime Score: " + gm.roundedTimer.ToString() +  "   Shots Left: " + gCtrl.shots + "\nTotal Score: " + gm.calculateScore();
        }
        else
        {
            scoreText.text = "Lost Souls: " + gm.colCount.ToString() + "/20   Golden Souls: " + gm.goldColCount.ToString() + "/3\nTime Score: " + gm.roundedTimer.ToString() + "   Shots Taken: " + gCtrl.shots + "\nTotal Score: " + gm.calculateScore();
        }
        if (gameObject.activeSelf)
        {
            gm.LerpUI(gameObject, targetPos.transform.position, 5f, true);
        }
	}

    public void buttonReset()
    {
        gm.ResetScene();
        transform.position = initPos;
    }

    public void buttonMenu()
    {
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
    }
}
