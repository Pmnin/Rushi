using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float xSpeed;
	public float ySpeed;

	//private bool jumping;
	private bool doubleJumping;

	private Vector3 movement;

	// Use this for initialization
	void Start () {
		//jumping = false;
		doubleJumping = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

		if (isGrounded ()) {
			//jumping = false;
			Debug.Log ("Can't Jump!");

			doubleJumping = true;
		}

		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.Space)){
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {
				movement = new Vector3(Input.GetAxis ("Horizontal") * xSpeed,GetComponent<Rigidbody>().velocity.y,GetComponent<Rigidbody>().velocity.z);

				//Debug.Log(Input.GetAxis("Horizontal"));
				GetComponent<Rigidbody>().velocity = movement;
			}

			if (Input.GetKeyDown(KeyCode.Space) && (isGrounded () || doubleJumping)) {
				//Debug.Log("IsGrounded?" + isGrounded ());
				if(!isGrounded ()){
					Debug.Log ("Double jumping");
					doubleJumping = false;
				}

				movement = new Vector3(GetComponent<Rigidbody>().velocity.x,Input.GetAxis("Jump") * ySpeed,GetComponent<Rigidbody>().velocity.z);

				//Debug.Log (Input.GetAxis ("Jump"));
				GetComponent<Rigidbody>().velocity = movement;
			}

		}
	}

	bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, 0.5f);

	}
}
