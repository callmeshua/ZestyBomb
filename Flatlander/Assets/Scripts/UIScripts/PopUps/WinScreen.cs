using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * USED
 * JOSH KARMEL
 * 
 * ATTACHED TO THE WinScreen
*/

public class WinScreen : MonoBehaviour
{

    //PUBLIC SCRIPT REFERENCES
    public GM gm;
    public GrappleController gCtrl;

    //public buttons
    public Button resetButton;
    public Button menuButton;
	public Button nextButton;
    public Text scoreText;
    public GameObject targetPos;
    private Vector3 initPos;

    //Rating Star Images
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    // Use this for initialization
    void Start()
    {
        gCtrl = FindObjectOfType<GrappleController>(); 
        gm = FindObjectOfType<GM>();
        resetButton.onClick.AddListener(buttonReset);
        menuButton.onClick.AddListener(buttonMenu);
		nextButton.onClick.AddListener (buttonNext);
        scoreText.text = "Score: ";
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            gm.LerpUI(gameObject, targetPos.transform.position, 5f, true);
        }

        if (gm.timer <= 0)
        {
            scoreText.text = "Ran out of time!\nTime: " + gm.roundedTimer.ToString() + "   Shots Taken: " + gCtrl.shots + "\nTotal Score: " + gm.calculateScore();
        }
        else if (gm.mode == GM.Modes.LIMSWINGS)
        {
            scoreText.text = "Ran out of shots!\nTime: " + gm.roundedTimer.ToString() + "   Shots Left: " + gCtrl.shots + "\nTotal Score: " + gm.calculateScore();
        }
        else
        {
            scoreText.text = "Time: " + gm.roundedTimer.ToString() + "   Shots Taken: " + gCtrl.shots + "\nTotal Score: " + gm.calculateScore();
        }

        if (gm.scoreRank >= 1)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        if (gm.scoreRank >= 2)
        {
            star2.SetActive(true);
            star3.SetActive(false);
        }
        if (gm.scoreRank >= 3)
        {
            star3.SetActive(true);
        }

    }

    public void buttonReset()
    {
        gm.ResetScene();
        gameObject.transform.position = initPos;
    }

    public void buttonMenu()
    {
		SceneManager.LoadScene(SceneUtility.GetScenePathByBuildIndex(0), LoadSceneMode.Single);
    }
	//
	public void buttonNext()
	{
		//1
		if (gm.level == GM.Levels.TUTORIAL) 
		{
			SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex (2), LoadSceneMode.Single);

		} 
		//2
		else if (gm.level == GM.Levels.LEVEL1) 
		{
			SceneManager.LoadScene(SceneUtility.GetScenePathByBuildIndex(3), LoadSceneMode.Single);
		} 
		//3
		else if (gm.level == GM.Levels.LEVEL2) 
		{
			SceneManager.LoadScene(SceneUtility.GetScenePathByBuildIndex(0), LoadSceneMode.Single);
		} 
		/*
		else if (gm.level == GM.Levels.LEVEL3) 
		{
			SceneManager.LoadScene(SceneUtility.GetScenePathByBuildIndex(0), LoadSceneMode.Single);
		}*/

		//0 Main Menu
		//1 Tutorial
		//2 Level1
		//3 Level2
	}
}
