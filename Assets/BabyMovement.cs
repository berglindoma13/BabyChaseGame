using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMovement : MonoBehaviour {

	public Transform baby;
	public Transform torso;
	public float SPEED;

	public string horizontalJoyCtrl = "HorizontalJoy_P1";
	public string verticalJoyCtrl = "VerticalJoy_P1";

	public string horizontalCtrl = "Horizontal_P1";
	public string verticalCtrl = "Vertical_P1";




	public Animator anim;
	// Use this for initialization
	void Start () {
		foreach (string name in Input.GetJoystickNames()) {
			Debug.Log (name);
		}
		baby = gameObject.GetComponent<Transform> ();
		//torso = baby.FindChild ("Torso").GetComponent <Transform>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		

		float h2 = Input.GetAxis (horizontalCtrl);

		float v2 = Input.GetAxis (verticalCtrl);

		float h = Input.GetAxis (horizontalJoyCtrl);

		float v = Input.GetAxis (verticalJoyCtrl);
		bool moving = false;

		Debug.Log ("Horizontal is: " + h + " and Vertical is: " + v);
		Debug.Log ("Horizontal is: " + h2 + " and Vertical is: " + v2);
		//if (Input.GetKey (KeyCode.W)) {
		if (v > 0 || Input.GetKey (KeyCode.W)) {
			//torso.position += Vector3.forward / 5;
			baby.rotation = Quaternion.Euler(new Vector3(0,360,0));
			baby.position += Vector3.forward * SPEED;
			moving = true;
			//anim.SetBool("Walking", true);
		}
		//else if (Input.GetKey (KeyCode.S)) {
		else if (v < 0 || Input.GetKey (KeyCode.S)) {
			//torso.position += Vector3.forward / 5;
			baby.rotation = Quaternion.Euler(new Vector3(0,180,0));
			baby.position += Vector3.back * SPEED;
			moving = true;
			//anim.SetBool("Walking", true);
		}

		//if (Input.GetKey (KeyCode.A)) {
		if (h < 0 || Input.GetKey (KeyCode.A)) {
			//torso.position += Vector3.forward / 5;
			baby.rotation = Quaternion.Euler(new Vector3(0,270,0));
			baby.position += Vector3.left * SPEED;
			moving = true;
			//anim.SetBool("Walking", true);
		}
		//else if (Input.GetKey (KeyCode.D)) {
		else if (h > 0 || Input.GetKey (KeyCode.D)) {
			//torso.position += Vector3.forward / 5;
			baby.rotation = Quaternion.Euler(new Vector3(0,90,0));
			baby.position += Vector3.right * SPEED;
			moving = true;
			//anim.SetBool("Walking", true);
		}

		anim.SetBool ("Walking", moving);
		/*else {
			anim.SetBool ("Walking", false);
		}*/
	}

	void OnCollisionEnter(Collision coll){

	}
}
