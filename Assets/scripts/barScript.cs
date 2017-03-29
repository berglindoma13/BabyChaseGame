using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barScript : MonoBehaviour {

	public float lerpSpeed = 2f;

	[SerializeField]
	private UnityEngine.UI.Image bar;

	[SerializeField]
	private BabyMovement baby;
	private const float fillMin = 0f;
	private const float fillMax = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (baby.grabTime);
		bar.fillAmount = baby.grabTime / 3.0f;
	}


	private float fillAmountConvert(float value,float valueMin,float valueMax, float min=fillMin, float max=fillMax){
		return (value - min) * (max - min) / (valueMax - valueMin) + min;
	}
}
