using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DataController : MonoBehaviour {

    

    public int highscore;
    public int currentscore;
    private Text secondHigh;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(currentscore > highscore)
        {
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        if(currentscore < PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("HighScore0", currentscore);
            secondHigh.text = "Second Highscore" + PlayerPrefs.GetInt("Highscore0");
        }
	}
}
