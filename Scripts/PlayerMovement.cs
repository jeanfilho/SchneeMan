using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//Components of Game Object
	PlayerStatus status;
	Rigidbody rbody;
	Collider col;


	//Physic Simulation
	RaycastHit hitInfo;

	// Use this for initialization
	void Start () {
		status = gameObject.GetComponentInChildren<PlayerStatus>();
		rbody = gameObject.GetComponentInChildren<Rigidbody> ();
		col = gameObject.GetComponentInChildren<Collider> ();
	}

	// Update is called once per timestep
	void FixedUpdate () {
		rbody.AddForce (gameObject.transform.forward * Input.GetAxis("Horizontal") * -1 * status.speed);
		rbody.AddForce (gameObject.transform.right * Input.GetAxis("Vertical")  * status.speed);
		gameObject.transform.Rotate(gameObject.transform.up * Input.GetAxis("Mouse X") * status.turnspeed);

		if (Physics.Raycast (new Ray (gameObject.transform.position, new Vector3(0,-1,0)), out hitInfo, 1f)) {
			rbody.AddForce (gameObject.transform.up * Input.GetAxis("Jump") * status.jumpforce);
			Debug.Log (hitInfo.collider.name);
		}
	}
}
