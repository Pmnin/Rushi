using UnityEngine;
using System.Collections;

public class DeathOnContact : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			//Faire des petites particules qui explosent

			//Respawn at StartBlock
			other.transform.position = GameObject.FindWithTag("StartBlock").transform.position;
			other.GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
		}
	}
}
