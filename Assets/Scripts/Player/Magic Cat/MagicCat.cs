using UnityEngine;
using System.Collections;

public class MagicCat : Cat {

	//	//Childs objects
	public Transform rightFiringPoint;
	public Transform leftFiringPoint;
	public GameObject projectile;
	public GameObject magicPulsePrefab;

	public string shootMagicGamepadButton;
	public string magicPulseGamepadButton;
	public string returnToHubGamepadButton;


	public KeyCode shootKey;
	public KeyCode magicPulseKey;
	public KeyCode returnToHubKey;


	public float yVelocity;

	public bool isPulsing;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myRigidBody2D = GetComponent<Rigidbody2D>();
		myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x,0);
	}
	
	// Update is called once per frame
	void Update () {

		yVelocity = myRigidBody2D.velocity.y;

		if(!finishedLevel){
			DefaultControl();
		} else {
			FinishLevel();
		}

	}

	void DefaultControl(){

		if(Input.GetButtonDown(returnToHubGamepadButton) || Input.GetKeyDown(returnToHubKey)){
			ReturnToHub();
		}

		if((Input.GetButtonDown(freakoutGamepadButton) || Input.GetKeyDown(freakoutKey) ) && !isDying && !isAttacking && !isJumping && !isPulsing && ready){
			freakoutMode = true;
			animator.SetBool("freakout",true);
		}

		if(!isDying && !freakoutMode){

			if(!isAttacking && !isPulsing){

				if (Input.GetKey (moveRightKey) || (Input.GetAxis (moveHorizontalGamepadAxis) >= 0.5f)) {
					MoveRight ();
				} else if (Input.GetKey (moveLeftKey) || (Input.GetAxis (moveHorizontalGamepadAxis) <= -0.5f)) {
					MoveLeft ();
				} else {
					Idle();
				}

				if(Input.GetKeyDown (jumpKey) || Input.GetButtonDown(jumpGamepadButton)){

					if(!isFalling){

						if(!isJumping){
							Jump();
						} 
					}
				}

				if(Input.GetKeyDown (shootKey) || Input.GetButtonDown(shootMagicGamepadButton)){
					StartProjectile();
				}

				if((Input.GetKeyDown (magicPulseKey) || Input.GetButtonDown(magicPulseGamepadButton)) && !isJumping && !isFalling){ 
					StartMagicPulse();
				}
			}

			if(myRigidBody2D.velocity.y < -1){
				isFalling = true;
			}
		}

		if (invulnerableTimeStamp < Time.time) {
			invulnerable = false;
			if(!freakoutMode){
				mySpriteRenderer.enabled = true;
			}
		}

		if (invulnerable) {
			Flash ();
		}

		if (receivedDamage && life > 0) {
			ToggleInvinsibility ();
		}

		if(life <= 0 ){
			isDying = true;
			Destroy(gameObject);
			//animator.SetBool("dying",true);
		}
	}


//	void LateUpdate(){
//
//		transform.position = new Vector3(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y),0);	
//	}

	void FinishLevel(){
		MoveRight();
	}

	private void StartProjectile(){

		isAttacking = true;
		animator.SetBool("projectile", true);
	}

	public void FireProjectile(){

		if(isLookingRight){
			Instantiate(projectile,rightFiringPoint.position,Quaternion.identity);
		} else {
			Instantiate(projectile,leftFiringPoint.position,Quaternion.identity);
		}
	}

	public void FinishProjectile(){
		
		isAttacking = false;
		animator.SetBool("projectile", false);
	}

	void StartMagicPulse(){

		isPulsing = true;
		animator.SetBool("magicPulse",true);
		animator.SetBool("idle",false);
		animator.SetBool("walking",false);
	}

	public void CreateMagicPulse(){
		Instantiate(magicPulsePrefab,transform.position,Quaternion.identity);
	}

	public void FinishMagicPulse(){

		animator.SetBool("magicPulse",false);
		animator.SetBool("walking",false);
		animator.SetBool("idle",true);

		isPulsing = false;
	}
}
