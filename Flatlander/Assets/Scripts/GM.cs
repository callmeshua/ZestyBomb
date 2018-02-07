using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * JOSH KARMEL
 * BEN SPURR
 * USED
*/

public class GM : MonoBehaviour
{
    public AudioClip sound;

    //Mode enumerator
    public enum Modes { CLASSIC, ENDLESS, LIMSWINGS, SOULLESS };
    public enum Levels { TUTORIAL, LEVEL1, LEVEL2, LEVEL3 };

    //PUBLIC REFERENCES
    private SideScrollController pCtrl;
    private FWSInput inputCtrl;
    public ParameterScreen ps;
    public HealthDepletion hd;
    public GrappleController gCtrl;
    public WinArea winArea;
    public SoundManager sm;

    //the score nuber in the HUD
    public Text scoreNum;
    public Text goldNum;
    public Text shotText;
    public Text clock;

    //PUBLIC ATTRIBUTES
    public bool resetLevel;
    public float colCount;
    public float goldColCount;
    public float totalScore;
    public int healthVal;
    public bool touchHazard;
    public bool gameOver;
    public Modes mode;
    public Levels level;
    public GameObject ks;
    public GameObject ws;
    public GameObject pauseScreen;
    [HideInInspector]
    public float timer, roundedTimer;

    public float jPow;
    public float mSpeed;
    public float dist;
    public float range;
    public float recoil;
    public float power;

    //PRIVATE ATTRIBUTES
	private string levelName;
	private Scene currLevel;
    private GameObject[] interactables;
    private GameObject[] hazards;
	private GameObject[] normCollectibles;
	private GameObject[] healCollectibles;
	private GameObject[] golCollectables;
   // private GameObject clone;
    private List<float> i_positions = new List<float>();
    private List<float> i_rotations = new List<float>();
    private List<float> h_positions = new List<float>();
    private List<float> h_rotations = new List<float>();
	private List<float> nc_positions = new List<float> ();
	private List<float> nc_rotations = new List<float> ();
	private List<float> hc_positions = new List<float> ();
	private List<float> hc_rotations = new List<float> ();
	private List<float> gc_positions = new List<float> ();
	private List<float> gc_rotations = new List<float> ();
   
    private float startTime;
    private float shots;

    void Awake()
    {
        SoundManager.PlaySFX(sound, true, 0f);
    }

    // Use this for initialization
    void Start()
    {

        jPow = 12;
        mSpeed = 7;
        dist = 2.8f;
        range = 15;
        recoil = 0.3f;
        power = 8;

        //init GO's
        currLevel = SceneManager.GetActiveScene();
        ks.SetActive(false);
        ws.SetActive(false);
        pauseScreen.SetActive(false);

        //for restart array
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        hazards = GameObject.FindGameObjectsWithTag("Hazard");
		normCollectibles = GameObject.FindGameObjectsWithTag ("normieCollectible");
		healCollectibles = GameObject.FindGameObjectsWithTag ("healCollectible");
		golCollectables = GameObject.FindGameObjectsWithTag ("scoreCollectible");

        GenerateObjectArrays(interactables, i_positions, i_rotations);
        GenerateObjectArrays(hazards, h_positions, h_rotations);
		GenerateObjectArrays (normCollectibles, nc_positions, nc_rotations);
		GenerateObjectArrays (healCollectibles, hc_positions, hc_rotations);
		GenerateObjectArrays (golCollectables, gc_positions, gc_rotations);

        resetLevel = false;
        gameOver = false;
        touchHazard = false;

        winArea = FindObjectOfType<WinArea>();
        hd = FindObjectOfType<HealthDepletion>();
        inputCtrl = FindObjectOfType<FWSInput>();
        pCtrl = FindObjectOfType<SideScrollController>();
        healthVal = hd.healthVal;

        startTime = 6000;

        mode = SoundManager.mode;

        //mode = Modes.ENDLESS;

        if (mode == Modes.CLASSIC || mode == Modes.ENDLESS || mode == Modes.SOULLESS)
        {
            gCtrl.shots = 0;
        }
        else if (mode == GM.Modes.LIMSWINGS)
        {
            gCtrl.shots = 15;
        }
        if(mode == Modes.ENDLESS)
        {
            clock.rectTransform.sizeDelta *= 3;
            clock.gameObject.transform.position = new Vector3(clock.transform.position.x, clock.transform.position.y + 3, clock.transform.position.z);
        }

        if (mode == Modes.SOULLESS)
        {
            timer = 0;
        }

        levelName = currLevel.name;
		if (levelName.Equals("Tutorial-pass2"))
        {
			level = Levels.TUTORIAL;
		}
        else if (levelName.Equals("Level_1-pass3"))
        {
			level = Levels.LEVEL1;
		}
        else if (levelName.Equals("Level_2-new"))
        {
			level = Levels.LEVEL2;
		}
        else if (levelName.Equals("Level_3_pass2"))
        {
			level = Levels.LEVEL3;
		}
			
		Debug.Log (level);
    }

    // Update is called once per frame
    void Update()
    {
        shots = gCtrl.shots;
        healthVal = hd.healthVal;
       // mode = SoundManager.mode;
        updateScore();
        checkPause();
        checkDead();
        handlePauses();
        resetLevel = false;
    }

    //increments amount of normal collectibles
    public void handleColCount()
    {
        colCount++;
    }

    //increments amount of gold collectibles
    public void handleGoldColCount()
    {
        goldColCount++;
    }

    //checks if the player is dead
    public void checkDead()
    {
        if (healthVal <= 0 || pCtrl.isDead || (mode == Modes.CLASSIC && timer < 0) || (mode == Modes.LIMSWINGS && gCtrl.shots < 0))
        {
            pCtrl.isDead = true;
            pCtrl.EnableRagdoll();
            gameOver = true;
        }
        else
        {
            pCtrl.DisableRagdoll();
            gameOver = false;
        }
    }

    //handles each case for pausing
    public void handlePauses()
    {
        if (gameOver)
        {
            ks.SetActive(true);
            pCtrl.isDead = true;
            gCtrl.Retract();
            if (Input.GetButtonDown("Jump"))
            {
                ResetScene();
            }
        }
        else if (winArea.win)
        {
            ws.SetActive(true);
            gCtrl.Retract();
            if (Input.GetButtonDown("Jump"))
            {
                ResetScene();
            }
        }
        else if (ps.isPaused && !gameOver && !pCtrl.isDead)
        {
            pauseScreen.SetActive(true);
            pCtrl.DisableRagdoll();
            gCtrl.Retract();
            if (Input.GetButtonDown("Jump"))
            {
                ResetScene();
            }
        }
        else
        {
			ks.SetActive (false);
			ws.SetActive (false);
            pauseScreen.SetActive(false);
            pCtrl.isDead = false;
            updateClock();
        }
    }
    
    public void LerpUI(GameObject uiObject,Vector2 target, float speed, bool lerp)
    {
        if(lerp)
        {
            uiObject.transform.position = Vector2.Lerp(uiObject.transform.position, target, Time.unscaledDeltaTime * speed);
        }
        else
        {
            uiObject.transform.position = Vector2.MoveTowards(uiObject.transform.position, target, Time.unscaledDeltaTime * speed);
        }
    }
    
    //function to reset the scene
    public void ResetScene()
    {
        resetLevel = true;
        pCtrl.isDead = false;
        pCtrl.DisableRagdoll();
        gameOver = false;
        pCtrl.transform.position = pCtrl.initPlayerPos;
        pCtrl.playerRb.velocity = Vector3.zero;
        if (mode == Modes.CLASSIC || mode == Modes.ENDLESS || mode == Modes.SOULLESS)
        {
            gCtrl.shots = 0;
        }
        else if (mode == Modes.LIMSWINGS)
        {
            gCtrl.shots = 15;
        }
        hd.healthVal = 100;
        goldColCount = 0;
        colCount = 0;
        totalScore = 0;
        timer = startTime;
        touchHazard = false;
        if (ps.isPaused)
        {
            ps.isPaused = false;
        }

        winArea.win = false;
		ResetObjects (interactables, i_positions, i_rotations);
		//Debug.Log (interactables.Length);
		ResetObjects (hazards, h_positions, h_rotations);
		ResetObjects (normCollectibles, nc_positions, nc_rotations);
		ResetObjects (healCollectibles, hc_positions, hc_rotations);
		ResetObjects (golCollectables, gc_positions, gc_rotations);
        
    }

    public void updateClock()
    {
        if ((mode == Modes.CLASSIC || mode == Modes.LIMSWINGS) && timer > -5)
        {
            timer -= Time.deltaTime * 12f;
            roundedTimer = Mathf.RoundToInt(timer);
            clock.text = roundedTimer.ToString();
        }
        else if (mode == Modes.SOULLESS)
        {
            timer += Time.deltaTime * 12f;
            roundedTimer = Mathf.RoundToInt(timer);
            clock.text = roundedTimer.ToString();
        }
        else if(mode == Modes.ENDLESS)
        {
            clock.text = "∞";
        }

                
    }

    //checks isPaused to stop the Time
    public void checkPause()
    {
        //pauses the game
        if (ps.isPaused && !pCtrl.isDead && !gameOver)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    //Time left + (Standard ColCount*1500) + (Gold ColCount*3000) + (Shots * 1000)
    //Time is not acconted for if died
    public float calculateScore()
    {
        if (gameOver || mode == Modes.ENDLESS)
        {
            roundedTimer = 0;
        }

        totalScore = roundedTimer + (colCount * 1500) + (goldColCount * 3000) + (shots * 1000);
        return totalScore;
    }

    public void kill()
    {
        gameOver = true;
    }

    public void resetTimer()
    {
        timer = startTime;
    }

	public void GenerateObjectArrays(GameObject[] objects, List<float> positions, List<float> rotations)
	{
		for (int i = 0; i < objects.Length; i++)
		{
			positions.Add(objects[i].transform.position.x);
			positions.Add(objects[i].transform.position.y);
			positions.Add(objects[i].transform.position.z);

			rotations.Add(objects[i].transform.eulerAngles.x);
			rotations.Add(objects[i].transform.eulerAngles.y);
			rotations.Add(objects[i].transform.eulerAngles.z);
		}

	
	}

    public void ResetObjects(GameObject[] objects, List<float> positions, List<float> rotations)
    {
        if (objects[0].gameObject.CompareTag("normieCollectible") || objects[0].gameObject.CompareTag("healCollectible") || objects[0].gameObject.CompareTag("scoreCollectible"))
        {
            colCount = 0;
            goldColCount = 0;
        }
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject clone = Instantiate(objects[i], new Vector3(positions[i * 3], positions[i * 3 + 1], positions[i * 3 + 2]), Quaternion.Euler(new Vector3(rotations[i * 3], rotations[i * 3 + 1], rotations[i * 3 + 2])));
            if (clone.GetComponent<Rigidbody>() != null)
            {
                clone.GetComponent<Rigidbody>().isKinematic = true;
            }
            if (clone.transform.childCount > 0 && clone.name.Contains("Pendulum"))
            {
                clone.GetComponent<Rigidbody>().isKinematic = false;
                Transform child = clone.transform.GetChild(0);
                child.GetComponent<Rigidbody>().isKinematic = true;
            }
            Destroy(objects[i]);
            objects[i] = clone;
            objects[i].SetActive(true);

        }
    }

    //sends data to the text in the UI
    public void updateScore()
    {
        scoreNum.text = colCount.ToString();
        goldNum.text = goldColCount.ToString();
        shotText.text = shots.ToString();
    }

}
