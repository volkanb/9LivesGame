using UnityEngine;
using System.Collections;

public class FinishLevelTrigger : MonoBehaviour {

	private Cat player;
	private CameraController mainCamera;
	private ScreenFade screenFade;

	public GameObject finishLevelText;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Cat>();
		mainCamera = FindObjectOfType<CameraController>();
		screenFade = FindObjectOfType<ScreenFade>();
	}
	
	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			player.finishedLevel = true;
			mainCamera.follow = false;
			StartCoroutine(FadeScreenOut());
		}

	}

	IEnumerator FadeScreenOut(){

		yield return new WaitForSeconds(1);
		screenFade.FadeOut();
		StartCoroutine(ShowFinishLevelText());
	}

	IEnumerator ShowFinishLevelText(){

		yield return new WaitForSeconds(1.2f);
		finishLevelText.SetActive(true);
	}

}
