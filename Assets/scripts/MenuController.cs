using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Canvas MainMenu;
    public Canvas AboutGameMenu;

	public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);   
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AboutGame()
    {
        MainMenu.gameObject.SetActive(false);
        AboutGameMenu.gameObject.SetActive(true);
    }

    public void Return()
    {
        MainMenu.gameObject.SetActive(true);
        AboutGameMenu.gameObject.SetActive(false);
    }

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene ("EnvironmentScene");
		}
	}
}
