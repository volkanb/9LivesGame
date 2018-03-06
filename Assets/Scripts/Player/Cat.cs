using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour {

	[SerializeField]
	float speed;
	[SerializeField]
	float jumpForce;

	public int life = 3;
	//Invulnerability variables
	public float invulnerableSeconds = 1;

	private int flashDelay = 2;
	protected int flashingCounter;
	protected bool toggleFlashing = false;
	protected float invulnerableTimeStamp;

	//Components
	protected Animator animator;
	protected Rigidbody2D myRigidBody2D;
	protected SpriteRenderer mySpriteRenderer;

	//States
	public bool isLookingRight;
	public bool isDying;
	public bool receivedDamage;
	public bool invulnerable;
	public bool freakoutMode;


	public KeyCode moveRightKey;
	public KeyCode moveLeftKey;
	public KeyCode jumpKey;
	public KeyCode transformInHubKey;
	public KeyCode freakoutKey;

	public string moveHorizontalGamepadAxis;
	public string moveVerticalGamepadAxis;
	public string jumpGamepadButton;
	public string transformInHubGamepadButton;
	public string freakoutGamepadButton;


	public bool isJumping;
	protected bool isWalking;
	public bool isFalling;
	protected bool isAttacking;

	public bool finishedLevel;

	public bool isNearStatue;
	public CatStatue nearestStatue;

	public FreakoutManager freakoutManager;

    public bool ready;                                          // Checks if the freak out bar is ready.

    // Use this for initialization
    void Start () {
	
	}

    protected void MoveRight(){

		animator.SetBool("walking",true);
		animator.SetBool("idle",false);
		isWalking = true;
		isLookingRight = true;

		myRigidBody2D.transform.position += Vector3.right * speed * Time.deltaTime;
		ChangeLookingDirection();

	
	}

	protected void MoveLeft(){

		animator.SetBool("walking",true);
		animator.SetBool("idle",false);
		isWalking = true;
		isLookingRight = false; 

		myRigidBody2D.transform.position += Vector3.left * speed * Time.deltaTime;
		mySpriteRenderer.flipX = true;

	
	}

	protected void Idle(){

		animator.SetBool("walking",false);
		animator.SetBool("idle",true);
		isWalking = false;
	}

	protected void Jump(){

//		animator.SetBool("jumping",true);

		myRigidBody2D.velocity = new Vector3(myRigidBody2D.velocity.x,0,0);
		myRigidBody2D.AddForce(new Vector3(0, jumpForce,0), ForceMode2D.Impulse);
		isJumping = true;
		
	}



	public void Flash(){

		if(flashingCounter >= flashDelay){ 

			flashingCounter = 0;

			toggleFlashing = !toggleFlashing;

			if(toggleFlashing) {
				mySpriteRenderer.enabled = true;
			}
			else {
				mySpriteRenderer.enabled = false;
			}

		}
		else {
			flashingCounter++;
		}

	}

	public void ToggleInvinsibility(){
		receivedDamage = false;
		invulnerable = true;
		invulnerableTimeStamp = Time.time + invulnerableSeconds;

	}

	public void IsGrounded(){

			isJumping = false;
			isFalling = false;
			myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x,0);
//			animator.SetBool("flying", false);
//			animator.SetBool("jumping", false);
//			animator.SetBool("falling",false);
	}

	public void ChangeLookingDirection(){

		if(isLookingRight){
			GetComponent<SpriteRenderer>().flipX = false;
		} else {
			GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "DeathPit"){
			isDying = true;
			Destroy(gameObject);
		}

        // Increases the fill for the freakout bar
        if(other.gameObject.tag == "FOBPickUp")
        {
            // TODO:: if (other.name == "BigFill"){ freakoutManager.percentage *= 5;} // Add this when we want to make the pickup worth more percentage or less percentage.
            Destroy(other.gameObject);
            freakoutManager.IncreaseFBBar();
        }
	}

	protected void ReturnToHub(){
		SceneManager.LoadScene(0);
	}

	protected void Freakout(){
		freakoutManager.StartFreakout(this);
	}

	public void StopFreakout(){
		animator.SetBool("freakout",false);
	}

}



