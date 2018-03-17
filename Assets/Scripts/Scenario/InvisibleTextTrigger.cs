using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvisibleTextTrigger : MonoBehaviour {

	public float timeToStayRevealed;

	public bool isRevealed;

	public Text myText;

	private float revealedTimestamp;

	void Awake(){
		myText.enabled = false;
		isRevealed = false;
		revealedTimestamp = 0;
	}


	// Update is called once per frame
	void Update () {

		if(isRevealed && revealedTimestamp == 0){
			revealedTimestamp = Time.time + timeToStayRevealed;
		}

		if(Time.time > revealedTimestamp ){
			isRevealed = false;
			myText.enabled = false;
			revealedTimestamp = 0;
		}

	}
}
