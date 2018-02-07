using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSelect : MonoBehaviour {
	//PUBLIC SCRIPT REFERENCES
	public GM gm;
	public GameObject menu;
	public Button back;

    //references the Canvas Text
    public Image paramScreen;

    //player sliders attached to SideScrollerController on the player
    public Slider jumpPowerSlider;
    public Text JPNum;
    public Slider maxSpeedSlider;
    public Text MaxSpeedNum;

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

    // Use this for initialization
    void Start()
    {
        gm = FindObjectOfType<GM>();

        Button btn = back.GetComponent<Button>();
        btn.onClick.AddListener(GoBack);
        
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
    void Update()
    {
        updateGM();
    }

    //all values are updated from the slider values
    public void updateGM()
    {
        //player
        gm.jPow = jumpPowerSlider.value;
        gm.mSpeed = maxSpeedSlider.value;

        //hook
        gm.range = hookRangeSlider.value;
        gm.power = hookPowerSlider.value;
        gm.recoil = hookRecoilForceSlider.value;

        //camera
        gm.dist = camDistanceSlider.value;

        updateSliders();
    }

    //all slider values are set from the player initial values
    public void updateSliders()
    {
        //player values
        jumpPowerSlider.value = gm.jPow;
        JPNum.text = gm.jPow.ToString();

        maxSpeedSlider.value = gm.mSpeed;
        MaxSpeedNum.text = gm.mSpeed.ToString();

        //grapple controller
        hookRangeSlider.value = gm.range;
        HRNum.text = gm.range.ToString();

        hookPowerSlider.value = gm.power;
        HPNum.text = gm.power.ToString();

        hookRecoilForceSlider.value = gm.recoil;
        HRFNum.text = gm.recoil.ToString();

        //camera controller
        camDistanceSlider.value = gm.dist;
        CDNum.text = gm.dist.ToString();
    }
	
	void GoBack()
	{
		menu.SetActive (true);
		gameObject.SetActive (false);
	}
}
