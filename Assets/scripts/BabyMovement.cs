﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BabyMovement : MonoBehaviour {
    public GameController gameController;

    public Transform baby;
    public Transform torso;
    public float SPEED;
	private float Stamina = 100;
    public Rigidbody rb;
    public Vector3 velocity;
	public AudioSource cry;
    public AudioSource push;

    public Material EnemyColor;
    public Material ToyTeamColor;

    private GameObject leftHand;
    private GameObject rightHand;
    private Material rightHandMat;
    private GameObject diaper;
    private Material diaperMat;


	/* INPUT VARIABLES */
    public string horizontalJoyCtrl;
    public string verticalJoyCtrl;
	public string triangleBtn;
	public string circleBtn;
	public string crossBtn;
	public string squareBtn;
	public string rBumper;
	public string lbumper;
	public string startBtn;

	//dirty dirty flags
	public bool pushing = false;


	//public Animator canvasAnim;
    public Animator anim;
	private GameObject toy;

	bool canCry = true;
    private float cryingTime = 6f;
	public float grabTime = 0f;
    public bool grabbing = false;
    private bool comforted = false;

	private Collider[] collidersInRadius;
	public float findRadiusSize = 1f;
	public PlayerState currentState = PlayerState.normal;

	//player states
	public enum PlayerState{
		normal,//never used
		crying,
		grabbing,
		jumping,//maybe?
        comforting
	}

    // Use this for initialization
    void Start() {
        foreach (string name in Input.GetJoystickNames()) {
            //Debug.Log(name);
        }
        baby = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();


        SPEED = 230;
    }


	void FixedUpdate () {
        /*if (Input.GetButtonDown(circleBtn))
        {
            Debug.Log("circle button");
        }
        if (Input.GetButtonDown(triangleBtn))
        {
            Debug.Log("triangle button");
        }
        if (Input.GetButtonDown(squareBtn))
        {
            Debug.Log("square button");
        }
        if (Input.GetButtonDown(startBtn))
        {
            Debug.Log("start button");
        }*/
        checkTag ();

		//Debug.Log (Input.GetButton(crossBtn));
		//Debug.Log(currentState);

		switch (currentState) {
		case PlayerState.normal:
            grabbing = false;
			//Debug.Log ("normal state!");
			float h = Input.GetAxis (horizontalJoyCtrl);
			float v = Input.GetAxis (verticalJoyCtrl);
			bool moving = false;
			velocity = rb.angularVelocity;
			//Debug.Log (h + " and " + v);
			if (h != 0 || v != 0) {
				baby.eulerAngles = new Vector3 (baby.eulerAngles.x, Mathf.Atan2 (h, v) * 180 / Mathf.PI, baby.eulerAngles.z);	
				rb.velocity = baby.forward * speed () * Time.fixedDeltaTime;
				moving = true;
				//	Debug.Log ("running movement code");
			} 
			//Debug.Log ("in between log");
			if (Input.GetButtonDown (crossBtn)) {
				//Debug.Log ("trying to punch by pressing x");
				push.Play ();
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Push")) {
					anim.SetTrigger ("TestPush");
				}
			}
			rb.angularVelocity = Vector3.zero;
			anim.SetBool ("Walking", moving);

			if (Input.GetButtonDown (squareBtn)) {
				//		Debug.Log ("logging keypresses");
				//startCrying ();
			}

			checkForDisplayIcon ();

            if (Input.GetButtonDown(triangleBtn))
            {
                    startComforting();
            }

			break;
		case PlayerState.crying:




            //FreezePlayer();
            if (comforted)
            {
                //Debug.Log("being comforted");
                cryingTime -= (Time.deltaTime * 2f);
            }
            else
            {
                cryingTime -= Time.deltaTime;
            }

            break;
		case PlayerState.grabbing:
			//cant move? idno
			//Debug.Log("am grabbing");
			if (Input.GetButton (circleBtn) && cryingToyCarrierFound()) {
				if (grabTime > 3f && !grabbing) {
					gameController.AttackingTeamWon();
                    grabbing = true;
				}
				grabTime += Time.fixedDeltaTime;
				//Debug.Log (grabTime);
			} 
			else {
				//Debug.Log ("grabbing is false");
				currentState = PlayerState.normal;
				anim.SetBool ("Grabbing", false);

				grabTime = 0f;
			}
			break;
		}
	}

    IEnumerator FreezePlayer()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(3f);
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }
  
    public void handCollisionDetection(BabyMovement otherBaby)
    {
        
		if (this == otherBaby) {
			return;
		}
        else if (currentState != PlayerState.crying && canCry)
        {
            startCrying();
        }

    }

    public void comfortingCollisionDetection()
    {
        if (currentState == PlayerState.crying)
        {
            comforted = true;
        }
    }


	void checkTag()
	{
		rightHand = transform.Find("Torso/RightHand").gameObject;
		toy = rightHand.transform.Find ("Toy").gameObject;
		if (tag == "Team1TOY" || tag == "Team2TOY") {
			toy.SetActive (true);
		} else {
			toy.SetActive (false);
		}

		diaper = transform.Find("Torso/Diaper").gameObject;
		if (tag == "Team1TOY" || tag == "Team1")
		{
			diaper.GetComponent<Renderer>().material = ToyTeamColor;
		}
		else if (tag == "Team2")
		{
			diaper.GetComponent<Renderer>().material = EnemyColor;
		}
	}

	float speed(){
		float tempSpeed = SPEED;
		if(tag == "Team1TOY" || tag == "Team2TOY")
		{
			tempSpeed = tempSpeed * 0.8f;
		}

		if (Input.GetButton (rBumper) && hasStamina()) {
			depleteStamina ();	
			//Debug.Log ("sprinting");
			return tempSpeed * 1.5f;
		} 
		else {
			return tempSpeed;
		}
	}
	void depleteStamina(){
		if (hasStamina ()) {
			Stamina -= 0.5f;
		}
	}
	bool hasStamina(){
		return Stamina != 0;
	}

    void startComforting()
    {
        currentState = PlayerState.comforting;
        anim.SetBool("Walking", false);
        anim.SetBool("Comfort", true);
        StartCoroutine(comfortingState());
    }

    IEnumerator comfortingState()
    {   
        yield return new WaitForSeconds(2f);
        currentState = PlayerState.normal;
        anim.SetBool("Comfort", false);
    }

	void startCrying(){
		canCry = false;
		currentState = PlayerState.crying;

		//in case he was interrupted in grabbing
		anim.SetBool ("Grabbing", false);
		grabTime = 0f;

		anim.SetBool ("Walking", false);
		anim.SetBool ("Crying", true);
		cry.time = 2;
		cry.Play ();
		StartCoroutine ("cryingState");
	}

	IEnumerator cryingState() {
        yield return new WaitWhile(() => cryingTime > 0);
        Debug.Log("done waiting");
		currentState = PlayerState.normal;
        cryingTime = 6f;    
        anim.SetBool ("Crying", false);
        comforted = false;
		cry.Stop ();
		StartCoroutine ("cryCooldown");
	}
	IEnumerator cryCooldown(){
		yield return new WaitForSeconds(1f);
		canCry = true;
	}

	bool cryingToyCarrierFound(){
		Vector3 forwardPoint = this.transform.position + this.transform.forward * 2;
		collidersInRadius = Physics.OverlapSphere (forwardPoint, findRadiusSize);
		foreach(Collider col in collidersInRadius){
			if (col.gameObject.tag == "Team1TOY" && this.gameObject.tag == "Team2") {
				if (col.gameObject.GetComponent<BabyMovement> ().currentState == PlayerState.crying) {
					return true;
					//gameController.AttackingTeamWon ();
				}
			} else if (col.gameObject.tag == "Team2TOY" && this.gameObject.tag == "Team1") {
				if (col.gameObject.GetComponent<BabyMovement> ().currentState == PlayerState.crying) {
					return true;
				}
			}
		}
		return false;
	}

	void checkForDisplayIcon(){

		bool toyIsGrabbable = cryingToyCarrierFound ();
		if (toyIsGrabbable && Input.GetButton(circleBtn)) {
			anim.SetBool ("Walking", false);
			anim.SetBool ("GrabToy", false);
			anim.SetBool ("Grabbing", true);

			//Debug.Log ("grabbing is true");
			currentState = PlayerState.grabbing;
			//start grabbing
		} 
		else if (toyIsGrabbable && !Input.GetButton(circleBtn)) {
			anim.SetBool ("GrabToy", true);
			//Debug.Log ("grabbing is true");
		} 
		else if (!toyIsGrabbable) {
			anim.SetBool ("GrabToy", false);
			//Debug.Log ("grabbing is false");
		}
		//if is holding a button down and channeling
	}

	public void resetBaby(){
		currentState = PlayerState.normal;
		anim.SetBool ("GrabToy", false);
		anim.SetBool ("Walking", false);
		anim.SetBool ("Grabbing", false);
		anim.SetBool ("Crying", false);
		comforted = false;
		grabbing = false;
		canCry = true;
		cryingTime = 6f;
		grabTime = 0f;
		Stamina = 100;
		cry.Stop ();
	}

}
