using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float mapSizeX;
    private float mapSizeZ;

    private float distanceRectangleX;
    private float distanceRectangleZ;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        }

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
