using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * JOSH KARMEL
 * USED
 * 
 * ATTACHED TO THE WIN AREA OBJECT
*/

public class WinArea : MonoBehaviour {

    //PUBLIC REFERENCED SCRIPTS
    public ParameterScreen ps;      //to pause the game
    public GM gm;
    public AudioClip winSound;


    [HideInInspector]
    public bool win;

	// Use this for initialization
	void Start () {
        ps = FindObjectOfType<ParameterScreen>();
        gm = FindObjectOfType<GM>();
        win = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            SoundManager.PlaySFX(winSound, false, .6f);
            gm.triggerEscape();

            /*
            ps.isPaused = true;
            win = true;
            */
        }
    }

    //transitions between phases
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //transitions from exploration to escape at relic area
            if (tag == "relicArea" && gm.phase == GM.Phases.EXPLORE)
            {
                SoundManager.PlaySFX(winSound, false, .6f);
                gm.triggerEscape();
                Destroy(gameObject);
            }

            //win condition for escape area
            if (tag == "exitArea" && gm.phase == GM.Phases.ESCAPE)
            {
                SoundManager.PlaySFX(winSound, false, .6f);
                ps.isPaused = true;
                win = true;
            }
        }
    }
}
