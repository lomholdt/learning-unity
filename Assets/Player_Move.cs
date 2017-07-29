using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

	public int playerSpeed = 10;
	private bool facingRight = true;
	public int playerJumpPower = 1250;
	private float moveX;

	private Animator myAnimator;

	void Start(){
		myAnimator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		PlayerMove ();
	}

	void PlayerMove() {
		
		moveX = Input.GetAxis ("Horizontal");

		Debug.Log (moveX);

		if (Input.GetButtonDown ("Jump")) {
			Jump ();
		}

		myAnimator.SetFloat ("speed", Mathf.Abs(moveX));

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

	void Jump(){
		GetComponent<Rigidbody2D> ().AddForce (
			Vector2.up * playerJumpPower
		);
	}

	void FlipPlayer() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
