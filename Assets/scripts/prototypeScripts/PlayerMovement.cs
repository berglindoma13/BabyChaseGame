using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public string jumpButton = "Jump_P1";
	public string horizontalCtrl = "Horizontal_P1";
	public string verticalCtrl = "Vertical_P1";

	public float movementSpeed = 0;

	private Rigidbody rBody;



	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(jumpButton)){
			Debug.Log("jumpssss");
		}


		//Debug.Log ("Horizontal is: " + h + " and Vertical is: " + v);
		//Debug.Log ("frame");

	}

	void FixedUpdate(){
		Vector3 movement = new Vector3();
		movement.z = rBody.velocity.z;
		if (Input.GetButtonDown (jumpButton)) {
			movement.y = Vector3.up.y * 5; //shorthand for readability
		}

		float h = Input.GetAxis (horizontalCtrl);

		float v = Input.GetAxis (verticalCtrl);

		/*if (h < 0) {
			rBody.AddForce (Vector3.left * movementSpeed);
			Debug.Log ("moving left");
		}
		else if (h > 0) {
			rBody.AddForce (Vector3.right * movementSpeed);
			Debug.Log ("moving right");
		}

		if (v < 0) {
			rBody.AddForce (Vector3.back * movementSpeed);
			Debug.Log ("moving down");
		}
		else if (v > 0) {
			rBody.AddForce (Vector3.forward * movementSpeed);
			Debug.Log ("moving up");
		}*/

		if (h < 0) {
			movement.x = Vector3.left.x * movementSpeed;
			Debug.Log ("moving left");
		}
		else if (h > 0) {
			movement.x = Vector3.right.x * movementSpeed;
			Debug.Log ("moving right");
		}

		if (v < 0) {
			movement.z = Vector3.back.z * movementSpeed;
			Debug.Log ("moving down");
		}
		else if (v > 0) {
			movement.z = Vector3.forward.z * movementSpeed;
			Debug.Log ("moving up");
		}

		rBody.velocity = movement;


	}
}
