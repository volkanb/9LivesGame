using UnityEngine;
using System.Collections;

public class PussInBoots : Cat {

	public Collider2D rightAttackingPoint;
	public Collider2D leftAttackingPoint;
	public Collider2D rightParryPoint;
	public Collider2D leftParryPoint;
 

	public float parryingTime;

	public string parryGamepadButton;
	public string attackGamepadButton;
	public string returnToHubGamepadButton;

	public KeyCode returnToHubKey;
	public KeyCode attackKey;
	public KeyCode parryKey;

	public bool startedParryStance;
	public bool parryStanceActivated;
	public bool isParrying;

	public Enemy enemyBeingParried;

	void Awake(){
		rightAttackingPoint.enabled = false;
		leftAttackingPoint.enabled = false;
		rightParryPoint.enabled = false;
		leftParryPoint.enabled = false;
	}

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myRigidBody2D = GetComponent<Rigidbody2D>();
		myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x,0);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown(returnToHubGamepadButton) || Input.GetKeyDown(returnToHubKey)){
			ReturnToHub();
		}

		if((Input.GetButtonDown(freakoutGamepadButton) || Input.GetKeyDown(freakoutKey) ) && !isDying && !isWalking && !isJumping && !isAttacking && !isParrying && !startedParryStance && !parryStanceActivated){
			freakoutMode = true;
			animator.SetBool("freakout",true);
		}

		if(!isDying && !freakoutMode){

			if(!isAttacking && !parryStanceActivated  && !startedParryStance  && !isParrying){

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

				if(Input.GetKeyDown (attackKey) || Input.GetButtonDown(attackGamepadButton)){
					StartAttack();
				}

				if((Input.GetKeyDown(parryKey ) || Input.GetButtonDown(parryGamepadButton)) && !isJumping && !isFalling){
					StartParryStance();
				}
			}


			if(myRigidBody2D.velocity.y < -1){
				isFalling = true;
			}
		}

		if (invulnerableTimeStamp < Time.time && !isParrying ) {
			invulnerable = false;
			if(!freakoutMode){
				mySpriteRenderer.enabled = true;
			}
		}

		if (invulnerable && !isParrying && !parryStanceActivated) {
			Flash ();
		}

		if (receivedDamage && life > 0) {

			if(startedParryStance || parryStanceActivated){
				CancelParryStance();
			}

			ToggleInvinsibility ();
		}

		if(life <= 0 ){
			isDying = true;
			Destroy(gameObject);
			//animator.SetBool("dying",true);
		}

	}

	private void StartAttack(){

		isAttacking = true;
		animator.SetBool("attacking", true);
	}

	public void ActivateAttackCollider(){

		if(isLookingRight){
			rightAttackingPoint.enabled = true;
		} else {
			leftAttackingPoint.enabled = true;
		}
	}

	public void FinishAttack(){
		
		isAttacking = false;
		animator.SetBool("attacking", false);
		rightAttackingPoint.enabled = false;
		leftAttackingPoint.enabled = false;
	}

	public void CancelParryStance(){ //Cancels the instance if the player got hit during the preparation to parry or if the damage came from a different direction

		startedParryStance = false;
		parryStanceActivated = false;

		rightParryPoint.enabled = false;
		leftParryPoint.enabled = false;

		invulnerable = false;

		animator.SetBool("parryStance",false);
		animator.SetBool("idle",true);
	}

	void StartParryStance(){ //Starts the preparation to the parry stance

		StopCoroutine(StartParryTimer()); //Kills the last parry stance timer
		startedParryStance = true;

		//Start animation to prepare for stance
		animator.SetBool("idle",false);
		animator.SetBool("walking",false);
		animator.SetBool("parryStance",true);

	}

	public void ParryStance(){ //The player was not interrupted during preparation to the stance and the parry stance is active

		invulnerable = true;
		 invulnerableTimeStamp = Time.time + parryingTime; 
		startedParryStance = false;
		parryStanceActivated = true;
		 
		ActivateParryCollider(); // Activate the collider
		StartCoroutine(StartParryTimer()); //Starts the timer
	}

	public void ActivateParryCollider(){

		if(isLookingRight){
			rightParryPoint.enabled = true;
		} else {
			leftParryPoint.enabled = true;
		}
	}

	public void ParrySuccess(Enemy targetEnemy){ 

		
		rightParryPoint.enabled = false;
		leftParryPoint.enabled = false;
		parryStanceActivated = false;
		isParrying = true;
		enemyBeingParried = targetEnemy;

		Time.timeScale = 0.5f;

		animator.SetBool("parryStance",false);
		animator.SetBool("parrySuccess",true);
	}

	public void RefreshTimeScale(){
		Time.timeScale = 1f;	
	}

	public void FinishedParrySuccess(){ //Function called by the animator to finish the "parry success" animation

		 
		invulnerable = false;
		isParrying = false;
		animator.SetBool("parrySuccess",false);
		animator.SetBool("idle",true);
		enemyBeingParried.ReceiveParry();
		CancelParryStance();
	}

	IEnumerator StartParryTimer(){

		yield return new WaitForSeconds(parryingTime);
		if(parryStanceActivated){
			CancelParryStance();
		}

	}
}
