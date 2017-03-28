using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BabyMovement : MonoBehaviour {
    public GameController gameController;

    public Transform baby;
    public Transform torso;
    private float SPEED;
	private float Stamina = 100;
    public Rigidbody rb;
    public Vector3 velocity;
	public AudioSource cry;

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

    private Animator anim;
	private GameObject toy;

    private float cryingTime = 5f;

	public PlayerState currentState = PlayerState.normal;

	//player states
	public enum PlayerState{
		normal,//never used
		crying,
		grabbing,
		jumping//maybe?

	}


    // Use this for initialization
    void Start() {
        foreach (string name in Input.GetJoystickNames()) {
            //Debug.Log(name);
        }
        baby = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();

        rightHand = transform.Find("Torso/RightHand").gameObject;
        Physics.IgnoreCollision(rightHand.GetComponent<Collider>(), GetComponent<Collider>());
        leftHand = transform.Find("Torso/LeftHand").gameObject;
        Physics.IgnoreCollision(leftHand.GetComponent<Collider>(), GetComponent<Collider>());

        SPEED = 230;
    }


	void FixedUpdate () {

		checkTag ();

        if(tag == "team1TOY" || tag == "Team2TOY")
        {
            SPEED = SPEED * 0.8f;
        }
		//Debug.Log (Input.GetButton(crossBtn));
		//Debug.Log(currentState);
		switch (currentState) {
		case PlayerState.normal:
			Debug.Log ("normal state!");
			float h = Input.GetAxis (horizontalJoyCtrl);
			float v = Input.GetAxis (verticalJoyCtrl);
			bool moving = false;
			velocity = rb.angularVelocity;
			Debug.Log (h + " and " + v);
			if (h != 0 || v != 0) {
				baby.eulerAngles = new Vector3 (baby.eulerAngles.x, Mathf.Atan2 (h, v) * 180 / Mathf.PI, baby.eulerAngles.z);	
				rb.velocity = baby.forward * speed () * Time.fixedDeltaTime;
				moving = true;
				Debug.Log ("running movement code");
			} 
			Debug.Log ("in between log");
			if (Input.GetButtonDown (crossBtn)) {
		//		Debug.Log ("trying to punch by pressing x");
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Push")) {
					anim.SetTrigger ("TestPush");
				}
			}
			rb.angularVelocity = Vector3.zero;
			anim.SetBool ("Walking", moving);

			if (Input.GetButtonDown (circleBtn)) {
		//		Debug.Log ("logging keypresses");
				startCrying ();
			}


			break;
		case PlayerState.crying:
			//cant do anything?
			Debug.Log("crying state!!");
			break;
		case PlayerState.grabbing:
			//cant move? idno
			break;
		}
	}

  
    void OnCollisionEnter(Collision other)
    {
		Debug.Log (other.gameObject.GetComponent<Animator> ().GetBool ("TestPush"));
		if (other.collider.tag == "Hand")
        {
			if (other.gameObject.GetComponent<Animator> ().GetBool ("TestPush")) {
				startCrying();
			}
            
            //DO THE 3 SECOND THING BEFORE CALLING FUNCTION
            //gameController.AttackingTeamWon();
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

    IEnumerator FreezePlayer(Rigidbody rbenemy)
    {
        rbenemy.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSecondsRealtime(3);
        rbenemy.constraints = RigidbodyConstraints.None;
    }

	float speed(){
		if (Input.GetButton (rBumper) && hasStamina()) {
			depleteStamina ();	
			Debug.Log ("sprinting");
			return SPEED * 1.5f;
		} 
		else {
			return SPEED;
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

	void startCrying(){
		currentState = PlayerState.crying;
		Debug.Log ("set walking to false and crying to true");
		anim.SetBool ("Walking", false);
		anim.SetBool ("Crying", true);
		cry.timeSamples = 2;
		cry.Play ();
		StartCoroutine ("cryingState");
	}

	IEnumerator cryingState() {
        while(cryingTime > 0)
        {
            cryingTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
		//yield return new WaitForSeconds(3f);
		Debug.Log ("yield changed currentstate");
		currentState = PlayerState.normal;
		anim.SetBool ("Crying", false);
		cry.Stop ();
	}

}
