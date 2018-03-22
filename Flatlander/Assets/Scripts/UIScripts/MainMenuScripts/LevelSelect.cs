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
	public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button back;

	public GameObject mode;
	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GM>();
		Button btn = level1.GetComponent<Button> ();
		btn.onClick.AddListener (loadLevel1);

		btn = level2.GetComponent<Button>();
		btn.onClick.AddListener(loadLevel2);

        btn = level3.GetComponent<Button>();
		btn.onClick.AddListener(loadLevel3);

        btn = level4.GetComponent<Button>();
        btn.onClick.AddListener(loadLevel4);

        btn = level5.GetComponent<Button>();
        btn.onClick.AddListener(loadLevel5);

        btn = back.GetComponent<Button> ();
		btn.onClick.AddListener (GoBack);
	}

	void loadLevel1()
	{
		SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex(1), LoadSceneMode.Single);
		gm.level = GM.Levels.LEVEL1;
	}
	void loadLevel2()
	{
		SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex(2), LoadSceneMode.Single);
		gm.level = GM.Levels.LEVEL2;
	}
	void loadLevel3()
	{
		SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex(3), LoadSceneMode.Single);
		gm.level = GM.Levels.LEVEL3;
	}
	void loadLevel4()
	{
		SceneManager.LoadScene (SceneUtility.GetScenePathByBuildIndex(4), LoadSceneMode.Single);
		gm.level = GM.Levels.LEVEL4;
	}
    void loadLevel5()
    {
        SceneManager.LoadScene(SceneUtility.GetScenePathByBuildIndex(5), LoadSceneMode.Single);
        gm.level = GM.Levels.LEVEL5;
    }

    //go back to main menu screen
    void GoBack()
	{
		mode.SetActive(true);
		gameObject.SetActive(false);
	}
}
