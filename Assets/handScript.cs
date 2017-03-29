using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handScript : MonoBehaviour {


	public BabyMovement baby;
	// Use this for initialization
	void Start () {
		baby = GetComponentInParent<BabyMovement> ();
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider coll)
	{
		
		Debug.Log (coll.tag);
		var otherBaby = coll.GetComponentInParent<BabyMovement> ();
		if (otherBaby != null && baby.GetComponent<Animator>().GetCurrentAnimatorStateInfo(1).IsName("Push")) {
			otherBaby.handCollisionDetection (baby);
		}

	}
}
