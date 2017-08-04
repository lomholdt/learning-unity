using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public int playerSpeed = 10;
	private bool facingRight = true;
	private bool isGrounded;
	private bool isJumping;
	public bool canDoubleJump;
	public int playerJumpPower = 1250;
	private float moveX;

	const string GROUND_TAG = "Ground";
	const float DOUBLE_JUMP_DELAY = 0.05f;

	private Animator myAnimator;

	void Start(){
		myAnimator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		PlayerMove ();
	}

	void PlayerMove() {

		// X axis movement -1 to 0 to +1
		moveX = Input.GetAxis ("Horizontal");

		// Jumping
		if (Input.GetButtonDown ("Jump")) {
			Jump ();
		}

		myAnimator.SetFloat ("speed", Mathf.Abs(moveX));

		// Player direction
		if (moveX > 0.0f && !facingRight) {
			FlipPlayer ();
		} else if (moveX < 0.0f && facingRight) {
			FlipPlayer ();
		}

		gameObject.GetComponent<Rigidbody2D> ().velocity = 
			new Vector2(
				moveX * playerSpeed,
				gameObject.GetComponent<Rigidbody2D>().velocity.y
			);
	}

	/**
	 * Handle collision with other objects 
	 */
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == GROUND_TAG) {
			isGrounded = true;
			isJumping = false;
		}	
	}

	/**
	 * Jump player 
	 */
	void Jump(){
		if (isGrounded) {
			isGrounded = false;
			canDoubleJump = false;
			JumpHero ();
			Invoke ("EnableDoubleJump", DOUBLE_JUMP_DELAY);
		}

		if (canDoubleJump) {
			canDoubleJump = false;
			JumpHero ();
		}
	}

	void EnableDoubleJump()
	{
		canDoubleJump = true;
	}
		
	/**
	 * Makes the hero jump by
	 * adding upward force to the rigidbody
	 */
	private void JumpHero()
	{
		isJumping = true;

		// Play Jump sound
		SoundManager.PlaySfx ("jump");

		// Apply upward force 
		GetComponent<Rigidbody2D> ().AddForce (
			Vector2.up * playerJumpPower
		);	
	}

	/**
	 * Flip the player depending on direction
	 */
	void FlipPlayer() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
