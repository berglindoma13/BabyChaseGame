using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public string horizontalCtrl = "Horizontal_P1";
    public string verticalCtrl = "Vertical_P1";

    public Animator anim;

    private bool Frozen;
    private bool newToy;


    // Use this for initialization
    void Start() {
        foreach (string name in Input.GetJoystickNames()) {
            Debug.Log(name);
        }
        baby = gameObject.GetComponent<Transform>();
        //torso = baby.FindChild ("Torso").GetComponent <Transform>();
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

        SPEED = 200;
    }

    void FixedUpdate() {
        float h2 = Input.GetAxis(horizontalCtrl);
        float v2 = Input.GetAxis(verticalCtrl);
        float h = Input.GetAxis(horizontalJoyCtrl);
        float v = Input.GetAxis(verticalJoyCtrl);
        bool moving = false;
        velocity = rb.angularVelocity;

        if (!Frozen)
        {
            //if (Input.GetKey (KeyCode.W)) {
            if (v > 0 || Input.GetKey(KeyCode.W))
            {
                //torso.position += Vector3.forward / 5;
                baby.rotation = Quaternion.Euler(new Vector3(0, 360, 0));
                //baby.position += Vector3.forward * SPEED;
                rb.velocity = (Vector3.forward * SPEED * Time.deltaTime);
                moving = true;
                //anim.SetBool("Walking", true);
            }
            //else if (Input.GetKey (KeyCode.S)) {
            else if (v < 0 || Input.GetKey(KeyCode.S))
            {
                //torso.position += Vector3.forward / 5;
                baby.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                //baby.position += Vector3.back * SPEED;
                rb.velocity = (Vector3.back * SPEED * Time.deltaTime);
                moving = true;
                //anim.SetBool("Walking", true);
            }

            //if (Input.GetKey (KeyCode.A)) {
            if (h < 0 || Input.GetKey(KeyCode.A))
            {
                //torso.position += Vector3.forward / 5;
                baby.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                //baby.position += Vector3.left * SPEED;
                rb.velocity = (Vector3.left * SPEED * Time.deltaTime);
                moving = true;
                //anim.SetBool("Walking", true);
            }
            //else if (Input.GetKey (KeyCode.D)) {
            else if (h > 0 || Input.GetKey(KeyCode.D))
            {
                //torso.position += Vector3.forward / 5;
                baby.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                //baby.position += Vector3.right * SPEED;
                rb.velocity = (Vector3.right * SPEED * Time.deltaTime);
                moving = true;
                //anim.SetBool("Walking", true);
            }
            rb.angularVelocity = Vector3.zero;
            anim.SetBool("Walking", moving);
        }

    }

    /*
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
		//else {
		//	anim.SetBool ("Walking", false);
		//}
	}
	*/
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Team2" && tag == "Team1TOY")
        {
            if (!newToy)
            {
                rightHand.GetComponent<Renderer>().material = RegularHand;
                gameObject.tag = "Team1";
                other.tag = "Team2TOY";
                StartCoroutine(FreezePlayer());
            }
       
        }
        else if (other.tag == "Team2" && tag == "Team1")
        {

        }
        else if (other.tag == "Team2TOY" && tag == "Team1")
        {
            if (!Frozen)
            {
                rightHand.GetComponent<Renderer>().material = TOYHand;
                gameObject.tag = "Team1TOY";
                other.tag = "Team2";
                StartCoroutine(NewToy());

            }

        }
        else if (other.tag == "Team1" && tag == "Team2TOY")
        {
            if (!newToy)
            {
                rightHand.GetComponent<Renderer>().material = RegularHand;
                gameObject.tag = "Team2";
                other.tag = "Team1TOY";
                StartCoroutine(FreezePlayer());
            }

        }
        else if (other.tag == "Team1TOY" && tag == "Team2")
        {
            if (!Frozen)
            {
                rightHand.GetComponent<Renderer>().material = TOYHand;
                gameObject.tag = "Team2TOY";
                other.tag = "Team1";
                StartCoroutine(NewToy());
            }

        }
        else if (other.tag == "Team1" && tag == "Team2")
        {

        }

    }

    IEnumerator FreezePlayer()
    {
        Frozen = true;
        yield return new WaitForSecondsRealtime(3);
        Frozen = false;
    }

    IEnumerator NewToy()
    {
        newToy = true;
        yield return new WaitForSecondsRealtime(3);
        newToy = false;
    }
}
