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
		float convertedAmount;

		convertedAmount = fillAmountConvert (baby.grabTime, 0, 3);
		bar.fillAmount = 0;
		//Debug.Log (bar.fillAmount);
		bar.fillAmount = Mathf.Lerp (bar.fillAmount, convertedAmount, Time.deltaTime * lerpSpeed);
	}


	private float fillAmountConvert(float value,float valueMin,float valueMax, float min=fillMin, float max=fillMax){
		return (value - min) * (max - min) / (valueMax - valueMin) + min;
	}
}
