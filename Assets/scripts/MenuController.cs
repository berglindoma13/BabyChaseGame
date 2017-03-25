using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);   
    }

    public void QuitGame()
    {
        Application.Quit();
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
