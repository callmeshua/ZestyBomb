using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour {

    //PUBLIC SCRIPT REFERENCES
    public GM gm;
    public FWSInput fInput;
    public SoundManager sm;

    //OPTIONS SCREEN PARTS
    public Dropdown controlDropdown;

    public Slider mouseSens;
    public Text mouseSensNum;

    public Slider musicSlider;
    public Text musicNum;

    public Slider sfxSlider;
    public Text sfxNum;

    public Slider masterSlider;
    public Text masterNum;

	// Use this for initialization
	void Start ()
    {
		gm = FindObjectOfType<GM>();
        fInput = FindObjectOfType<FWSInput>();
        sm = FindObjectOfType<SoundManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateSliders();
	}

    public void updateController()
    {
        if(controlDropdown.value == 0)
        {
            gm.isUsingController = false;
        }
        else if(controlDropdown.value == 1)
        {
            gm.isUsingController = true;
        }
    }

    public void Apply()
    {
        updateController();
        fInput.mouseSens = mouseSens.value * 1000;

        SoundManager.SetBGMVolume(musicSlider.value * 10);
        sm.bgmSource.volume = musicSlider.value * 10;
        SoundManager.SetSFXVolume(sfxSlider.value * 10);
        SoundManager.SetGlobalVolume(masterSlider.value *10);
    }

    public void updateSliders()
    {
        int x = (int)(mouseSens.value * 10);
        mouseSensNum.text = x.ToString();

        x = (int)(musicSlider.value * 10);
        musicNum.text = x.ToString();

        x = (int)(sfxSlider.value * 10);
        sfxNum.text = x.ToString();

        x = (int)(masterSlider.value * 10);
        masterNum.text = x.ToString();

    }
}
