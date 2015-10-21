using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float xSpeed;
	public float ySpeed;

	private bool jumping;
	private bool doubleJumping;

	private Vector3 movement;

	// Use this for initialization
	void Start () {
		jumping = false;
		doubleJumping = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

		if (isGrounded ()) {
			jumping = false;
			doubleJumping = true;
		}

		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.Space)){
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {
				movement = new Vector3(Input.GetAxis ("Horizontal") * xSpeed,rigidbody.velocity.y,rigidbody.velocity.z);

				//Debug.Log(Input.GetAxis("Horizontal"));
				rigidbody.velocity = movement;
			}

			if (Input.GetKeyDown(KeyCode.Space) && (isGrounded () || doubleJumping)) {
				//Debug.Log("IsGrounded?" + isGrounded ());
				if(!isGrounded ()){
					Debug.Log ("Double jumping");
					doubleJumping = false;
				}

				movement = new Vector3(rigidbody.velocity.x,Input.GetAxis("Jump") * ySpeed,rigidbody.velocity.z);

				//Debug.Log (Input.GetAxis ("Jump"));
				rigidbody.velocity = movement;
			}

		}
	}

	bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, 0.5f);

	}
}
