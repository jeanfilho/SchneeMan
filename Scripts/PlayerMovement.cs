using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//Components of Game Object
	PlayerStatus status;
	Rigidbody rbody;
	Collider col;

	// Use this for initialization
	void Start () {
		status = gameObject.GetComponent<PlayerStatus>();
		rbody = gameObject.GetComponent<Rigidbody> ();
		col = gameObject.GetComponent<Collider> ();
	}

	// Update is called once per timestep
	void FixedUpdate () {
		rbody.AddForce (transform.forward * Input.GetAxis("Horizontal") * status.speed);
		rbody.AddForce (transform.right * Input.GetAxis("Vertical") * status.speed);
	}
}
