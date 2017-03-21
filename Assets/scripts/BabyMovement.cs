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
        /*float h2 = Input.GetAxis(horizontalCtrl);
        float v2 = Input.GetAxis(verticalCtrl);*/
        float h = Input.GetAxis(horizontalJoyCtrl);
        float v = Input.GetAxis(verticalJoyCtrl);
        bool moving = false;
        velocity = rb.angularVelocity;
		Vector3 movement = Vector3.zero;


        if (!Frozen)
        {
            //if (Input.GetKey (KeyCode.W)) {
            if (v > 0 || Input.GetKey(KeyCode.W))
            {
				movement.z = SPEED * Time.deltaTime;
                moving = true;
                //anim.SetBool("Walking", true);
            }
            //else if (Input.GetKey (KeyCode.S)) {
            else if (v < 0 || Input.GetKey(KeyCode.S))
            {
				movement.z = -SPEED * Time.deltaTime;
                moving = true;
                //anim.SetBool("Walking", true);
            }

            //if (Input.GetKey (KeyCode.A)) {
            if (h < 0 || Input.GetKey(KeyCode.A))
            {
				movement.x = -SPEED * Time.deltaTime;
                moving = true;
                //anim.SetBool("Walking", true);
            }
            //else if (Input.GetKey (KeyCode.D)) {
            else if (h > 0 || Input.GetKey(KeyCode.D))
            {
				movement.x = SPEED * Time.deltaTime;
                moving = true;
                //anim.SetBool("Walking", true);
            }

			//skítamix
			if(movement.x != 0 && movement.z != 0){
				movement.x = movement.x/2;
				movement.y = movement.y/2;
			}
			rb.velocity = movement;
			baby.rotation = Quaternion.LookRotation (movement);
            rb.angularVelocity = Vector3.zero;
            anim.SetBool("Walking", moving);
        }

    }

  
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Team2" && tag == "Team1TOY")
        {
            //change color to team 2 and change tags, add to score
            
            rightHand.GetComponent<Renderer>().material = RegularHand;
            gameObject.tag = "Team1";
            other.tag = "Team2TOY";
            StartCoroutine(FreezePlayer());
            Debug.Log(this + "with tag" + tag);
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
                Debug.Log(this + "with tag" + tag);
            }

        }
        else if (other.tag == "Team1" && tag == "Team2TOY")
        { 
            rightHand.GetComponent<Renderer>().material = RegularHand;
            gameObject.tag = "Team2";
            other.tag = "Team1TOY";
            StartCoroutine(FreezePlayer());
            Debug.Log(this + "with tag" + tag);
        }
        else if (other.tag == "Team1TOY" && tag == "Team2")
        {
            if (!Frozen)
            {
                rightHand.GetComponent<Renderer>().material = TOYHand;
                gameObject.tag = "Team2TOY";
                other.tag = "Team1";
                Debug.Log(this + "with tag" + tag);
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
}