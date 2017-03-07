using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMovement : MonoBehaviour {

	public Transform baby;
	public Transform torso;
	public float SPEED;

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
			baby.rotation = Quaternion.Euler(new Vector3(0,0,0));
			baby.position += Vector3.forward * SPEED;
			anim.SetBool("Walking", true);
		}
		else if (Input.GetKey (KeyCode.A)) {
			//torso.position += Vector3.forward / 5;
			baby.rotation = Quaternion.Euler(new Vector3(0,270,0));
			baby.position += Vector3.left * SPEED;
			anim.SetBool("Walking", true);
		}
		else if (Input.GetKey (KeyCode.S)) {
			//torso.position += Vector3.forward / 5;
			baby.rotation = Quaternion.Euler(new Vector3(0,180,0));
			baby.position += Vector3.back * SPEED;
			anim.SetBool("Walking", true);
		}
		else if (Input.GetKey (KeyCode.D)) {
			//torso.position += Vector3.forward / 5;
			baby.rotation = Quaternion.Euler(new Vector3(0,90,0));
			baby.position += Vector3.right * SPEED;
			anim.SetBool("Walking", true);
		}else {
			anim.SetBool ("Walking", false);
		}
	}

	void OnCollisionEnter(Collision coll){

	}
}
