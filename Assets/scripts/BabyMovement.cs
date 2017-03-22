using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BabyMovement : MonoBehaviour {

    public Transform baby;
    public Transform torso;
    public float SPEED;
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

    public string horizontalJoyCtrl = "HorizontalJoy_P1";
    public string verticalJoyCtrl = "VerticalJoy_P1";
	public string buttonX = "Fire1_P1";

   /* public string horizontalCtrl = "Horizontal_P1";
    public string verticalCtrl = "Vertical_P1";*/

    public Animator anim;

    public Text Blue;
    private int blueScore;
    public Text Red;
    private int redScore;
  
    // Use this for initialization
    void Start() {
        foreach (string name in Input.GetJoystickNames()) {
            //Debug.Log(name);
        }
        baby = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        
        rightHand = transform.Find("Torso/RightHand").gameObject;
        if (tag == "Team1TOY")
        {
            rightHand.GetComponent<Renderer>().material = TOYHand;
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

        SPEED = 230;
    }

	void FixedUpdate () {

        if(tag == "team1TOY" || tag == "Team2TOY")
        {
            SPEED = SPEED * 0.8f;
        }

		if(Input.GetButtonDown(buttonX)){
			//Debug.Log("x is down");
		}

		/*float h2 = Input.GetAxis (horizontalCtrl);
		float v2 = Input.GetAxis (verticalCtrl);*/
		float h = Input.GetAxis (horizontalJoyCtrl);
		float v = Input.GetAxis (verticalJoyCtrl);
		bool moving = false;
		velocity = rb.angularVelocity;

		Vector3 movement = Vector3.zero;

		//Debug.Log ("hori is: " + h + " and verti is: " + v);
		//if (Input.GetKey (KeyCode.W)) {
		if (v > 0 || Input.GetKey (KeyCode.W)) {
			//torso.position += Vector3.forward / 5;
			//baby.rotation = Quaternion.Euler(new Vector3(0,360,0));
			//baby.position += Vector3.forward * SPEED;
			//rb.velocity = (Vector3.forward * SPEED * Time.deltaTime);
			movement.z = SPEED * Time.deltaTime;


			moving = true;
			//anim.SetBool("Walking", true);
		}
		//else if (Input.GetKey (KeyCode.S)) {
		else if (v < 0 || Input.GetKey (KeyCode.S)) {
			//torso.position += Vector3.forward / 5;
			//baby.rotation = Quaternion.Euler(new Vector3(0,180,0));
			//baby.position += Vector3.back * SPEED;
			//rb.velocity = (Vector3.back * SPEED * Time.deltaTime);
			movement.z = -SPEED * Time.deltaTime;
			moving = true;
			//anim.SetBool("Walking", true);
		}

		//if (Input.GetKey (KeyCode.A)) {
		if (h < 0 || Input.GetKey (KeyCode.A)) {
			//torso.position += Vector3.forward / 5;
			//baby.rotation = Quaternion.Euler(new Vector3(0,270,0));
			//baby.position += Vector3.left * SPEED;
			//rb.velocity = (Vector3.left * SPEED * Time.deltaTime);
			movement.x = -SPEED * Time.deltaTime;
			moving = true;
			//anim.SetBool("Walking", true);
		}
		//else if (Input.GetKey (KeyCode.D)) {
		else if (h > 0 || Input.GetKey (KeyCode.D)) {
			//torso.position += Vector3.forward / 5;
			//baby.rotation = Quaternion.Euler(new Vector3(0,90,0));
			//baby.position += Vector3.right * SPEED;
			//rb.velocity = (Vector3.right * SPEED * Time.deltaTime);
			movement.x = SPEED * Time.deltaTime;
			moving = true;
			//anim.SetBool("Walking", true);
		}

		if (movement.x != 0 && movement.z != 0) {
			movement.x = movement.x / 2;
			movement.z = movement.z / 2;
		}

		rb.velocity = movement;
		baby.rotation = Quaternion.LookRotation (movement);
		//Debug.Log ("rb velocity is: " + rb.velocity + " and move is: " + movement);
		rb.angularVelocity = Vector3.zero;
		anim.SetBool ("Walking", moving);

	}

  
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Team1TOY" && tag == "Team2")
        {
            rightHand.GetComponent<Renderer>().material = TOYHand;
            GameObject enemy = other.transform.Find("Torso/RightHand").gameObject;
            enemy.GetComponent<Renderer>().material = RegularHand;

            Rigidbody rbenemy = other.GetComponent<Rigidbody>();
            StartCoroutine(FreezePlayer(rbenemy));

            gameObject.tag = "Team2TOY";
            other.tag = "Team1";

        }
        else if (other.tag == "Team2TOY" && tag == "Team1")
        {
            rightHand.GetComponent<Renderer>().material = TOYHand;
            GameObject enemy = other.transform.Find("Torso/RightHand").gameObject;
            enemy.GetComponent<Renderer>().material = RegularHand;

            Rigidbody rbenemy = other.GetComponent<Rigidbody>();
            StartCoroutine(FreezePlayer(rbenemy));
            
            gameObject.tag = "Team1TOY";
            other.tag = "Team2";
        }

    }

    IEnumerator FreezePlayer(Rigidbody rbenemy)
    {
        rbenemy.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSecondsRealtime(3);
        rbenemy.constraints = RigidbodyConstraints.None;
    }

}
