using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//Components of Game Object
	PlayerStatus status;
	CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<CharacterController>();
		status = gameObject.GetComponent<PlayerStatus>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey("w"))
			controller.SimpleMove(transform.forward * status.speed);
	}
}
