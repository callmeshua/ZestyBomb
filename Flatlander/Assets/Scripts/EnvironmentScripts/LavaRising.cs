using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRising : MonoBehaviour {

    public GM gm;
	public float speed = 1f;
	public float yStopValue;

	private float yStartingPosition;
	public bool move;

	private void Start()
    {
		//get starting position
		yStartingPosition = transform.position.y; 	
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update ()
    {
		//get the position between the starting
		float yPosition = transform.position.y - yStartingPosition;
		yPosition = Mathf.Abs (yPosition);

        // if the position of lava is greater than the stop value then set move bool to false.
        if (yPosition >= yStopValue)
        {
            move = false;
        }

        // else if phases.escape, and game is not over then set move bool to true;
        //else if (gm.phase == GM.Phases.ESCAPE && !gm.gameOver)
        //{
        //    move = true;
        //}

		//Call Move() every frame
		Move ();

		/*
		if (transform.position >= stopPoint.transform.position) {
			transform.position == new Vector3
		}*/
		
        if (gm.phase == GM.Phases.ESCAPE && !gm.gameOver)
        {
            move = true;
        }

        if(gm.resetLevel)
        {
            print("reset");
            move = false;
            transform.position = new Vector3(transform.position.x, yStartingPosition, transform.position.z);
        }

	}

	//Checks if bool move is to true, then moves;
	void Move()
	{
        if (move)
            //transform.position += Time.deltaTime * speed;
            transform.position += new Vector3(0f,Time.deltaTime*speed,0f);
	}

    
}
