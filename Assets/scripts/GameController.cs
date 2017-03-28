using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private float ROUND_TIME = 10f;

    private float timeLeft;

    private int roundNumber;
	public Canvas UISideSwitcher;

    public GameObject baby1;
    public GameObject baby2;
    public GameObject baby3;
    public GameObject baby4;

    public Text Winner;
    public Text Timer;

    public Text RedPoints;
    private float redpoints;
    public Text BluePoints;
    private float bluepoints;

    //0 = blue is defender
    //1 = red is defender
    private int TieDefender;

    private string[] randomRoles;

    private Vector3[] originPos;

    private bool stoptimer;

    private GameController instance;

    void Start () {
        instance = this;

        bluepoints = 0;
        redpoints = 0;

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
   
        timeLeft = ROUND_TIME;
        Debug.Log(timeLeft.ToString());
        roundNumber = 1;
        originPos = new Vector3[4];
        originPos[0] = baby1.transform.position;
        originPos[1] = baby2.transform.position;
        originPos[2] = baby3.transform.position;
        originPos[3] = baby4.transform.position;

		StartCoroutine (AssignTeams ());

        Winner.enabled = false;
        

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
        BluePoints.text = bluepoints.ToString();
        RedPoints.text = redpoints.ToString();

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

        if (!stoptimer)
        {
           timeLeft -= Time.deltaTime;
        }
        Timer.text = timeLeft.ToString("F0");
        if (timeLeft < 0)
        {
            if(roundNumber == 1)
            {
                StartCoroutine(GameReset(false));
                roundNumber += 1;
                Winner.text = "Congratulations Blue Team";
                Winner.enabled = true;
                bluepoints += 1;
            }
            else if(roundNumber == 2)
            {
                redpoints += 1;
                Winner.text = "Congratulations Red Team";
                Winner.enabled = true;
                EndOfGameRoutine();
            }
            else if(roundNumber == 3)
            {
                if(TieDefender == 0)
                {
                    Winner.text = "Congratulations Blue Team";
                    Winner.enabled = true;
                    bluepoints += 1;
                    EndOfGameRoutine();
                }
                else if(TieDefender == 1)
                {
                    Winner.text = "Congratulations Red Team";
                    Winner.enabled = true;
                    redpoints += 1;
                    EndOfGameRoutine();
                }
            }
        }
	}


    IEnumerator GameReset(bool tie)
    {
		stoptimer = true;
		timeLeft = ROUND_TIME;
		//RESET POINTS 
		yield return new WaitForSeconds(5);
        if (!tie)
        {
            changeTags();
        }
		Winner.enabled = false;
		UISideSwitcher.enabled = true;
		yield return new WaitForSeconds (3);

		baby1.transform.position = originPos[0];
		baby2.transform.position = originPos[1];
		baby3.transform.position = originPos[2];
		baby4.transform.position = originPos[3];

		UISideSwitcher.enabled = false;
		stoptimer = false;
        

    }

	IEnumerator AssignTeams()
	{
		stoptimer = true;
		timeLeft = ROUND_TIME;
		UISideSwitcher.GetComponentInChildren<Text> ().text = "Assigning teams";
		UISideSwitcher.enabled = true;
		yield return new WaitForSeconds (3);
		UISideSwitcher.enabled = false;
		UISideSwitcher.GetComponentInChildren<Text> ().text = "Switching sides";
		stoptimer = false;


	}

    public void AttackingTeamWon()
    {
        if (roundNumber == 1)
        {
            Winner.text = "Congratulations Red Team";
            Winner.enabled = true;
            instance.StartCoroutine(GameReset(false));
            roundNumber += 1;
            redpoints += 1;
        }
        else if (roundNumber == 2)
        {
            bluepoints += 1;
            Winner.text = "Congratulations Blue Team";
            Winner.enabled = true;
            EndOfGameRoutine();
        }
        else if(roundNumber == 3)
        {
            if (TieDefender == 0)
            {
                Winner.text = "Congratulations Red Team";
                Winner.enabled = true;
                redpoints += 1;
                EndOfGameRoutine();
            }
            else if (TieDefender == 1)
            {
                Winner.text = "Congratulations Blue Team";
                Winner.enabled = true;
                bluepoints += 1;
                EndOfGameRoutine();
            }
        }
    }

    void EndOfGameRoutine()
    {
        if(bluepoints > redpoints)
        {
            Winner.text = "Blue Team IS THE BEST";
        }
        if(redpoints > bluepoints)
        {
            Winner.text = "Red Team IS THE BEST";
        }
        if (bluepoints == redpoints)
        {
            Winner.text = "Both teams won one game, so it's a TIE! Get ready for a tiebreaker";
            Winner.enabled = true;
            TieBraker();
            
        }
    }

    void TieBraker()
    {	
        roundNumber += 1;
        int defender = Random.Range(0, 1);
        if(defender == 0)
        {
            TieDefender = 0;
			if (baby1.tag == "Team2TOY")
			{
				baby1.tag = "Team2";
			}
			else if (baby2.tag == "Team2TOY")
			{
				baby2.tag = "Team2";
			}
			else if (baby3.tag == "Team2TOY")
			{
				baby3.tag = "Team2";
			}
			else if (baby4.tag == "Team2TOY")
			{
				baby4.tag = "Team2";
			}

			if(baby1.tag == "Team1")
			{
				baby1.tag = "Team1TOY";
			}
			else if (baby2.tag == "Team1")
			{
				baby2.tag = "Team1TOY";
			}
			else if(baby3.tag == "Team1")
			{
				baby3.tag = "Team1TOY";
			}
        }
        else
        {
            TieDefender = 1;
        }
        instance.StartCoroutine(GameReset(true));
    }

    void QuitGame()
    {
		if (SceneManager.GetActiveScene ().name.Equals ("EnvironmentScene")) {
			SceneManager.LoadScene ("MainMenu");
		} else {
			Application.Quit ();
		}
        
    }

    void changeTags()
    {
        if (baby1.tag == "Team1TOY")
        {
            baby1.tag = "Team1";
        }
		else if (baby2.tag == "Team1TOY")
        {
            baby2.tag = "Team1";
        }
		else if (baby3.tag == "Team1TOY")
        {
            baby3.tag = "Team1";
        }
		else if (baby4.tag == "Team1TOY")
        {
            baby4.tag = "Team1";
        }

        if(baby1.tag == "Team2")
        {
            baby1.tag = "Team2TOY";
        }
        else if (baby2.tag == "Team2")
        {
            baby2.tag = "Team2TOY";
        }
        else if(baby3.tag == "Team2")
        {
            baby3.tag = "Team2TOY";
        }

    }
}
