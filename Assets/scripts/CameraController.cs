using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private GameObject[] Players;


    // Use this for initialization
    void Start () {
        Players = new GameObject[4];
        Players[0] = player1;
        Players[1] = player2;
        Players[2] = player3;
        Players[3] = player4;

	}
	
	// Update is called once per frame
	void Update () {
        float minX = Players[0].transform.position.x;
        float minZ = Players[0].transform.position.z;
        float maxX = Players[0].transform.position.x;
        float maxZ = Players[0].transform.position.z;
        for(int i = 1; i < 4; i++)
        {
            if(Players[i].transform.position.x < minX)
            {
                minX = Players[i].transform.position.x;
            }
            if(Players[i].transform.position.z < minZ)
            {
                minZ = Players[i].transform.position.z;
            }
            if(Players[i].transform.position.x > maxX)
            {
                maxX = Players[i].transform.position.x;
            }
            if(Players[i].transform.position.z > maxZ)
            {
                maxZ = Players[i].transform.position.z;
            }
        }
		if (minZ > 7) {
			minZ = 7;
		}
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, minZ - 15);

        //BOX ((minX, minZ),(minX,maxZ),(maxX,maxZ),(maxX,minZ))
        //Debug.Log(Camera.main.WorldToScreenPoint(new Vector3(minX, 0, minZ)) + " " + Screen.width + " " + Screen.height);



        /*Debug.Log("player positions");
        Debug.Log(player1.transform.position);
        Debug.Log(player2.transform.position);
        Debug.Log(player3.transform.position);
        Debug.Log(player4.transform.position);

        Debug.Log("Camera position");
        Debug.Log(this.gameObject.transform.position);

        Debug.Log("MINX");
        Debug.Log(minX);

        Debug.Log("MINZ");
        Debug.Log(minZ);*/

        //transform.position += new Vector3 (0,0,0.2f);
        // Camera.main.fieldOfView += 10f;
        
    }
}
