using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BabyMovement : MonoBehaviour {

    public Transform baby;
    public Transform torso;
    public float SPEED;
	public float Stamina = 100;
    public Rigidbody rb;
    public Vector3 velocity;

    public Material EnemyColor;
    public Material ToyTeamColor;
    public Material TOYHand;
    public Material RegularHand;

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

    public Animator anim;

    public Text Blue;
    private int blueScore;
    public Text Red;
    private int redScore;
	private GameObject toy;
  
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

		checkTag ();

        if(tag == "team1TOY" || tag == "Team2TOY")
        {
            SPEED = SPEED * 0.8f;
        }
		//Debug.Log (Input.GetButton(crossBtn));


		float h = Input.GetAxis (horizontalJoyCtrl);
		float v = Input.GetAxis (verticalJoyCtrl);
		bool moving = false;
		velocity = rb.angularVelocity;

		if (h != 0 || v != 0) {
			baby.eulerAngles = new Vector3(baby.eulerAngles.x,Mathf.Atan2(h,v) * 180/Mathf.PI,baby.eulerAngles.z);	
			rb.velocity = baby.forward * speed() * Time.fixedDeltaTime;
			moving = true;
		} 


		if (Input.GetButtonDown(crossBtn)) {
			//Debug.Log ("pressed push button");
			/*if (anim.GetCurrentAnimatorStateInfo(0).IsName ("Push")) {
				Debug.Log ("already in animation");
			}*/
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Push")) {
				//pushing = true;
				//Debug.Log ("triggered animation");
				anim.SetTrigger ("TestPush");
				//anim.SetBool ("Push", pushing);
			} //else {
				//pushing = false;
				//anim.SetBool ("Push", pushing); //performance hit??
			//}
				//anim.SetBool ("Push", true);
		}

		rb.angularVelocity = Vector3.zero;
		anim.SetBool ("Walking", moving);

	}

  
    void OnCollisionEnter(Collision other)
    {
		if (other.collider.tag == "Team1TOY" && tag == "Team2")
        {
			Rigidbody rbenemy = other.collider.GetComponent<Rigidbody>();
            StartCoroutine(FreezePlayer(rbenemy));
            //DO THE 3 SECOND THING BEFORE CALLING FUNCTION
            GameController.AttackingTeamWon();

        }
        else if (other.collider.tag == "Team2TOY" && tag == "Team1")
        {
            //DO THE 3 SECOND THING BEFORE CALLING FUNCTION
            GameController.AttackingTeamWon();
            Rigidbody rbenemy = other.collider.GetComponent<Rigidbody>();
            StartCoroutine(FreezePlayer(rbenemy));
            
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

}
