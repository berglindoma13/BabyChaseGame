using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private float timeLeft;
    // Use this for initialization
    void Start () {
        timeLeft = 60.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {

        }
	}
}
