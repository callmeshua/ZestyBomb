using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* USED
 * ===============
 * Zac Lopez
 * 
 * Script attached to LevelSelect canvas and is used for 
 * choosing a level, player can click back button
 * to go back to Main menu
 */

public class LevelSelect : MonoBehaviour {

	//PUBLIC REFERENCES
	public GM gm;

    //PUBLIC ATTRIBUTES
	public Button tutorial;
	public Button level1;
    public Button level2;
    public Button level3;
	public Button back;

	public GameObject mode;
	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GM>();
		Button btn = tutorial.GetComponent<Button> ();
		btn.onClick.AddListener(LevelTutorial);

		btn = level1.GetComponent<Button> ();
		btn.onClick.AddListener (Level1);

		btn = level2.GetComponent<Button>();
		btn.onClick.AddListener(Level2);

        btn = level3.GetComponent<Button>();
		btn.onClick.AddListener(Level3);

		btn = back.GetComponent<Button> ();
		btn.onClick.AddListener (GoBack);
	}

	void LevelTutorial()
	{
		SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex(1), LoadSceneMode.Single);
		gm.level = GM.Levels.LEVEL1;
	}
	void Level1()
	{
		SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex(2), LoadSceneMode.Single);
		gm.level = GM.Levels.LEVEL1;
	}
	void Level2()
	{
		SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex(3), LoadSceneMode.Single);
		gm.level = GM.Levels.LEVEL2;
	}
	void Level3()
	{
		SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex(4), LoadSceneMode.Single);
		gm.level = GM.Levels.LEVEL3;
	}
	//go back to main menu screen
	void GoBack()
	{
		mode.SetActive(true);
		gameObject.SetActive(false);
	}
}
