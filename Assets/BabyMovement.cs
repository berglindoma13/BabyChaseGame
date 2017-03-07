using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMovement : MonoBehaviour {

	public Transform baby;
	public Transform torso;

	public Animator anim;
	// Use this for initialization
	void Start () {
		baby = gameObject.GetComponent<Transform> ();
		//torso = baby.FindChild ("Torso").GetComponent <Transform>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.W)) {
			//torso.position += Vector3.forward / 5;
			//baby.position += Vector3.forward / 5;
			anim.SetBool("Walking", true);
		} else {
			anim.SetBool ("Walking", false);
		}
	}

	void OnCollisionEnter(Collision coll){

	}
}
