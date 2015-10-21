using UnityEngine;
using System.Collections;

public class ReachEnd : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			//Passer au prochain niveau

			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().nextLevel();

		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
