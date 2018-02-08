using UnityEngine;
using System.Collections;

public class InvisiblePlatform : MonoBehaviour {

	public float timeToStayRevealed;

	public bool isRevealed;

	private SpriteRenderer mySpriteRenderer;
	private float revealedTimestamp;

	void Awake(){
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		mySpriteRenderer.enabled = false;
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
			mySpriteRenderer.enabled = false;
			revealedTimestamp = 0;
		}

	}
}
