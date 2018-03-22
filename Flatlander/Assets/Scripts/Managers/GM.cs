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
    
    //Enumerator
    public enum Modes { CLASSIC, ENDLESS, LIMSWINGS, SOULLESS };
    public enum Levels { LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5 };
    public enum Phases { EXPLORE, ESCAPE };

    [Header("Script References")]
    //PUBLIC REFERENCES
    private SideScrollController pCtrl;
    private FWSInput inputCtrl;
    public HealthDepletion hd;
    public GrappleController gCtrl;
    public GameObject relic;
    public WinArea exitArea;
    public SoundManager sm;
    public GunAim gunAim;
    
    [Header("HUD Texts")]
    //the score nuber in the HUD
    public Text scoreNum;
    public Text goldNum;
    public Text shotText;
    public Text clock;

    [Header("HUD Values")]
    //PUBLIC ATTRIBUTES
    public float colCount;
    public float goldColCount;
    public float totalScore;
    public int scoreRank;
    public int healthVal;
    public float startTime;
    public float shots;
    public float timer, roundedTimer;

    [Header("Conditionals")]
    public bool isUsingController;
    public bool resetLevel;
    public bool touchHazard;
    public bool gameOver;
    public bool frozen;
    public bool paused;

    [Header("Enums")]
    public Modes mode;
    public Levels level;
    public Phases phase;

    [Header("Ranking Thresholds")]
    //Rank Threshold Variables
    public float oneStarScore;
    public float twoStarScore;
    public float threeStarScore;


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
	private List<float> i_scales = new List<float> ();
    private List<float> h_positions = new List<float>();
    private List<float> h_rotations = new List<float>();
	private List<float> h_scales = new List<float> ();
	private List<float> nc_positions = new List<float> ();
	private List<float> nc_rotations = new List<float> ();
	private List<float> nc_scales = new List<float> ();
	private List<float> hc_positions = new List<float> ();
	private List<float> hc_rotations = new List<float> ();
	private List<float> hc_scales = new List<float> ();
	private List<float> gc_positions = new List<float> ();
	private List<float> gc_rotations = new List<float> ();
	private List<float> gc_scales = new List<float> ();

    private BurnInOut[] burnShader;

    void Awake()
    {
        SoundManager.PlaySFX(sound, true, 0f);
    }

    // Use this for initialization
    void Start()
    {
        burnShader = FindObjectsOfType<BurnInOut>();
        phase = Phases.EXPLORE;

        frozen = false;

        //init GO's
        currLevel = SceneManager.GetActiveScene();

        //for restart array
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        hazards = GameObject.FindGameObjectsWithTag("Hazard");
		normCollectibles = GameObject.FindGameObjectsWithTag ("normieCollectible");
		healCollectibles = GameObject.FindGameObjectsWithTag ("healCollectible");
		golCollectables = GameObject.FindGameObjectsWithTag ("scoreCollectible");
        gunAim = FindObjectOfType<GunAim>();
        relic = GameObject.FindGameObjectWithTag("relicArea");
        exitArea = GameObject.FindGameObjectWithTag("exitArea").GetComponent<WinArea>();

		GenerateObjectArrays(interactables, i_positions, i_rotations, i_scales);
		GenerateObjectArrays(hazards, h_positions, h_rotations, h_scales);
		GenerateObjectArrays (normCollectibles, nc_positions, nc_rotations, nc_scales);
		GenerateObjectArrays (healCollectibles, hc_positions, hc_rotations, hc_scales);
		GenerateObjectArrays (golCollectables, gc_positions, gc_rotations, gc_scales);

        resetLevel = false;
        gameOver = false;
        touchHazard = false;
        
        relic.SetActive(true);

        gCtrl = FindObjectOfType<GrappleController>();
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
		if (levelName.Equals("Level1_Final"))
        {
			level = Levels.LEVEL1;
		}
        else if (levelName.Equals("Level2_Final"))
        {
			level = Levels.LEVEL2;
		}
        else if (levelName.Equals("Level3_Final"))
        {
			level = Levels.LEVEL3;
		}
        else if (levelName.Equals("Level4_Final"))
        {
            level = Levels.LEVEL4;
        }
        else if (levelName.Equals("Level4_Final"))
        {
			level = Levels.LEVEL5;
		}

        BurnInOutShaderFX(true);
    }

    // Update is called once per frame
    void Update()
    {
        shots = gCtrl.shots;
        healthVal = hd.healthVal;
       // mode = SoundManager.mode;
        updateScore();
        checkFreeze();
        checkDead();
        updateClock();
        resetLevel = false;

        if(gameOver)
        {
            gunAim.enabled = false;
        }
        else
        {
            gunAim.enabled=true;
        }
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

    public void handlePauseScreen()
    {
        pCtrl.DisableRagdoll();
        gCtrl.Retract();
        if (Input.GetButtonDown("Jump"))
        {
            ResetScene();
        }
    }

    public void handleDeath()
    {
        pCtrl.isDead = true;
        gCtrl.Retract();
        if (Input.GetButtonDown("Jump"))
        {
            ResetScene();
        }
    }

    public void handleWin()
    {
        frozen = true;
        gCtrl.Retract();
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
        phase = Phases.EXPLORE;
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

        if (frozen)
        {
            frozen = false;
        }

        paused = false;
        
        exitArea.win = false;

        relic.SetActive(true);

		ResetObjects (interactables, i_positions, i_rotations, i_scales);
		//// (interactables.Length);
		ResetObjects (hazards, h_positions, h_rotations, i_scales);
		ResetObjects (normCollectibles, nc_positions, nc_rotations, nc_scales);
		ResetObjects (healCollectibles, hc_positions, hc_rotations, hc_scales);
		ResetObjects (golCollectables, gc_positions, gc_rotations, gc_scales);
        BurnInOutShaderFX(true);

    }
    //updates the clock depending on the mode
    public void updateClock()
    {
        if ((mode == Modes.CLASSIC || mode == Modes.LIMSWINGS) && timer > -5 && !gameOver)
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

    //freezes the game
    public void handleFrozen()
    {
        frozen = !frozen;
    }

    public void BurnInOutShaderFX(bool burnIn)
    {
        if(burnIn)
        {
            for (int i = 0; i < burnShader.Length; i++)
            {
                burnShader[i].fadeIn();
            }
        }
        else
        {
            for (int i = 0; i < burnShader.Length; i++)
            {
                burnShader[i].fadeOut();
            }
        }
    }


    //checks paused to stop the Time
    //freezes for pause screen and win screen
    //NOT death screen for ragdoll physics
    public void checkFreeze()
    {
        //checks for condition to freeze and unfreeze the game
        if (frozen && !pCtrl.isDead && !gameOver)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    //Time left - (shots taken * 100)
    //Time is not acconted for if died
    public float calculateScore()
    {
        if (gameOver || mode == Modes.ENDLESS)
        {
            roundedTimer = 0;
        }

        totalScore = roundedTimer - (shots * 100);

        if(totalScore < 0)
        {
            totalScore = 0;
        }

        // Rank System - Adjust Rank Thresholds in inspector
        if (totalScore >= threeStarScore)
        {
            scoreRank = 3;
        }
        else if (totalScore >= twoStarScore)
        {
            scoreRank = 2;
        }
        else if (totalScore >= oneStarScore)
        {
            scoreRank = 1;
        }
        else
        {
            scoreRank = 0;
        }

        return totalScore;
    }

    //sets game over
    public void kill()
    {
        gameOver = true;
        
    }

    //resets the timer to startTime
    public void resetTimer()
    {
        timer = startTime;
    }

	public void GenerateObjectArrays(GameObject[] objects, List<float> positions, List<float> rotations, List<float> scales)
	{
		for (int i = 0; i < objects.Length; i++)
		{
			positions.Add(objects[i].transform.position.x);
			positions.Add(objects[i].transform.position.y);
			positions.Add(objects[i].transform.position.z);

			rotations.Add(objects[i].transform.eulerAngles.x);
			rotations.Add(objects[i].transform.eulerAngles.y);
			rotations.Add(objects[i].transform.eulerAngles.z);

			scales.Add (objects [i].transform.lossyScale.x);
			scales.Add (objects[i].transform.lossyScale.y);
			scales.Add (objects [i].transform.lossyScale.z);
		}
	}

	public void ResetObjects(GameObject[] objects, List<float> positions, List<float> rotations, List<float> scales)
    {
        /*
        if (objects[0].gameObject.CompareTag("normieCollectible") || objects[0].gameObject.CompareTag("healCollectible") || objects[0].gameObject.CompareTag("scoreCollectible"))
        {
            colCount = 0;
            goldColCount = 0;
        }*/
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                if (objects[i].transform.parent != null)
                    objects[i].transform.parent = null;

                GameObject clone = Instantiate(objects[i], new Vector3(positions[i * 3], positions[i * 3 + 1], positions[i * 3 + 2]), Quaternion.Euler(new Vector3(rotations[i * 3], rotations[i * 3 + 1], rotations[i * 3 + 2])));
                //clone.transform.localScale = new Vector3 (scales [i * 3], scales [i * 3 + 1], scales [i * 3 + 2]);
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
    }

    //sends data to the text in the UI
    public void updateScore()
    {
        scoreNum.text = colCount.ToString();
        goldNum.text = goldColCount.ToString();
    }

    //triggers escape phase
    public void triggerEscape()
    {
        phase = Phases.ESCAPE;
    }

    //freezes and puts game in paused state
    public void handlePause()
    {
        handleFrozen();
        paused = !paused;
    }
    
    //returns the phase as a string
    public string getPhase()
    {
        return phase.ToString();
    }

    public void camShake(float dur)
    {
        GetComponent<CameraShake>().setDuration(dur);
    }
    
    public string getLevelName()
    {
        return levelName;
    }
}
