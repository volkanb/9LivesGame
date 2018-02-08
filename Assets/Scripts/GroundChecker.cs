using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {

	public Cat player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Ground" || other.gameObject.tag == "InvisiblePlatform"){
			player.IsGrounded();
		}
	}
}
