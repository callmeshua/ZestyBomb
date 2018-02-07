using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// max speed and acceleration force
/// change camera distance to camera zoom
/// </summary>
/*
 * USED 
 * JOSH KARMEL
 * 
 * The parameter screen
 * 
 * script attached to the CameraRig
*/

public class ParameterScreen : MonoBehaviour {

    //Script References
    public SideScrollController player;
    public WeponRecoil gunRecoil;
    public GrappleController gCtrl;
    public CameraController cam;
    public GM gm;

    //references the Canvas Text
    public Image paramScreen;
    public GameObject pauseScreen;

    //player sliders attached to SideScrollerController on the player
	public Slider jumpPowerSlider;
    public Text JPNum;
    public Slider maxSpeedSlider;
    public Text MaxSpeedNum;
    public Slider stickForceSlider;

    //grapple sliders attached the GrappleController on the player
    public Slider hookRangeSlider;
    public Text HRNum;
    public Slider hookPowerSlider;
    public Text HPNum;
    public Slider hookRecoilForceSlider;
    public Text HRFNum;

    //camera
    public Slider camDistanceSlider;
    public Text CDNum;
    
    public bool isPaused;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<SideScrollController>();
        gunRecoil = FindObjectOfType<WeponRecoil>();
        gCtrl = FindObjectOfType<GrappleController>();
        cam = FindObjectOfType<CameraController>();
        gm = FindObjectOfType<GM>();
        isPaused = false;
        paramScreen.gameObject.SetActive(false);
        pauseScreen.SetActive(false);

        //parameters
        jumpPowerSlider.value = gm.jPow;
        maxSpeedSlider.value = gm.mSpeed;
        hookRangeSlider.value = gm.range;
        hookPowerSlider.value = gm.power;
        hookRecoilForceSlider.value = gm.recoil;
        camDistanceSlider.value = gm.dist;

        updateSliders();
    }
	
	// Update is called once per frame
	void Update () {
        updateScripts();
        //updateScore();
        //checkPause();

        if (isPaused && Input.GetButtonDown("Jump"))
        {
            gm.ResetScene();
            pauseScreen.gameObject.SetActive(false);
        }
    }
		
    //Called in fInput.cs when escape is pressed
    public void handleParamScreen()
    {
        //changes staus of being paused
        isPaused = !isPaused;

        //pauses the game and changes the text on the canvas
        if (isPaused)
        {
            paramScreen.gameObject.SetActive(true);
        }
        else
        {
            paramScreen.gameObject.SetActive(false);
        }
    }

    //Calls in the pause screen
    public void handlePauseScreen()
    {
        isPaused = !isPaused;

        if (isPaused && !gm.gameOver && !player.isDead)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.gameObject.SetActive(false);
        }
    }
    
    //all values are updated from the slider values
    public void updateScripts()
    {
        //player
        player.jumpPower = jumpPowerSlider.value;
        player.maxSpeed = maxSpeedSlider.value;
        player.stickForce = stickForceSlider.value;

        //hook
        gCtrl.maxRopeRange = hookRangeSlider.value;
        gCtrl.power = hookPowerSlider.value;
        gCtrl.recoilForce = hookRecoilForceSlider.value;

        //camera
        cam.distance = camDistanceSlider.value;

        updateSliders();
    }
 
    //all slider values are set from the player initial values
    public void updateSliders()
    {
        //player values
        jumpPowerSlider.value = player.jumpPower;
        JPNum.text = player.jumpPower.ToString();

        maxSpeedSlider.value = player.maxSpeed;
        MaxSpeedNum.text = player.maxSpeed.ToString();

        //grapple controller
        hookRangeSlider.value = gCtrl.maxRopeRange;
        HRNum.text = gCtrl.maxRopeRange.ToString();

        hookPowerSlider.value = gCtrl.power;
        HPNum.text = gCtrl.power.ToString();

        hookRecoilForceSlider.value = gCtrl.recoilForce;
        HRFNum.text = gCtrl.recoilForce.ToString();

        //camera controller
        camDistanceSlider.value = cam.distance;
        CDNum.text = cam.distance.ToString();
    }
   
}
