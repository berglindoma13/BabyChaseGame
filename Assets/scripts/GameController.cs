using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private float timeLeft;

    public Text roundText;
    private int roundNumber;
	public Canvas UISideSwitcher;

    public GameObject baby1;
    public GameObject baby2;
    public GameObject baby3;
    public GameObject baby4;

    public Text Winner;
    public Text Timer;

    private string[] randomRoles;

    void Start () {
        //RANDOMIZE TEAMS
        randomRoles = new string[4];
        randomRoles[0] = "Team1TOY";
        randomRoles[1] = "Team1";
        randomRoles[2] = "Team2";
        randomRoles[3] = "Team2";

        randomizeArray(randomRoles);
        baby1.tag = randomRoles[0];
        baby2.tag = randomRoles[1];
        baby3.tag = randomRoles[2];
        baby4.tag = randomRoles[3];

        //INITIALIZING
        timeLeft = 20.0f;
        roundNumber = 1;
        roundText.text = "Round: " + roundNumber.ToString();

		UISideSwitcher.enabled = false;

	}
	
    private void randomizeArray(string[] roles)
    {
        for(int i = roles.Length - 1; i > 0 ; i--)
        {
            var r = Random.Range(0, i);
            var tmp = roles[i];
            roles[i] = roles[r];
            roles[r] = tmp;
        }
    }

	void FixedUpdate () {


        //RESET GAME
        if (Input.GetKey(KeyCode.R))
        {
			SceneManager.LoadScene ("EnvironmentScene");
        }

        //QUIT GAME
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame(); 
        }
        
        //WILL BE CHANGED
        timeLeft -= Time.deltaTime;
        Timer.text = timeLeft.ToString();
        if(timeLeft < 0)
        {
			Debug.Log ("TIME < 0");
            //SWITCH AND USE TAGS INSTEAD OF COLOR
            GameObject b1 = baby1.transform.Find("Torso/RightHand").gameObject;
            GameObject b2 = baby2.transform.Find("Torso/RightHand").gameObject;
            GameObject b3 = baby3.transform.Find("Torso/RightHand").gameObject;
            GameObject b4 = baby4.transform.Find("Torso/RightHand").gameObject;

            if (b1.GetComponent<Renderer>().material.name == "TOY (Instance)")
            {
                Winner.text = "CONGRATULATIONS BLUE TEAM!!";
            }
            else if (b2.GetComponent<Renderer>().material.name == "TOY (Instance)")
            {
                Winner.text = "CONGRATULATIONS BLUE TEAM!!";
            }
            else if (b3.GetComponent<Renderer>().material.name == "TOY (Instance)")
            {
                Winner.text = "CONGRATULATIONS RED TEAM!!";
            }
            else if (b4.GetComponent<Renderer>().material.name == "TOY (Instance)")
            {
                Winner.text = "CONGRATULATIONS RED TEAM!!";
            }

            Timer.text = "";
			StartCoroutine(GameReset());
        }
	}


	IEnumerator GameReset()
    {
		Debug.Log ("STARTING 5 SEC WAIT");
        //RESET POINTS
		yield return new WaitForSeconds(5);
		Debug.Log ("Switching sides and wait for 3 sec");
		UISideSwitcher.enabled = true;
		yield return new WaitForSeconds (3);
		Debug.Log ("reset scene");
		SceneManager.LoadScene("EnvironmentScene");
        //timeLeft = 60.0f;
    }

    void QuitGame()
    {
		if (SceneManager.GetActiveScene ().name.Equals ("EnvironmentScene")) {
			SceneManager.LoadScene ("MainMenu");
		} else {
			Application.Quit ();
		}
        
    }
}
