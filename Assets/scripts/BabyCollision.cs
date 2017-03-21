using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Team2")
        {
            //change color to team 2 and change tags, add to score
            Debug.Log("touched by other team");
        }
        
    }
}
