using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private float timeLeft;

    public GameObject baby1;
    public GameObject baby2;
    public GameObject baby3;
    public GameObject baby4;

    public Text Winner;
    public Text Timer;


    // Use this for initialization
    void Start () {
        timeLeft = 60.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        Timer.text = timeLeft.ToString();
        if(timeLeft < 0)
        {
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
           
        }
	}
}
