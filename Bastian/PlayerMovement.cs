using UnityEngine;
using System.Collections;

public class PlayerMovement : Photon.MonoBehaviour {

	//Tuning
	public float horizontalDrag = 3;

	//Components of Game Object
	PlayerStatus status;
	Rigidbody rbody;
	AudioSource jumpSound;

	//Camera
	private Vector3 cameraTopLimit;
	private Vector3 cameraBottomLimit;
	private float cameraPos = 0;
	private Vector3 offset;

	//Physic Simulation
	RaycastHit hitInfo;

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Vector3 syncStartRotation = Vector3.zero;
	private Vector3 syncEndRotation = Vector3.zero;

	bool isGrounded = false;

	// Use this for initialization
	void Awake () {
		jumpSound = GetComponent<AudioSource> ();
		status = gameObject.GetComponentInChildren<PlayerStatus>();
		rbody = gameObject.GetComponentInChildren<Rigidbody> ();
		offset = status.model.transform.position - status.playerCamera.transform.position;
	}

	// Update is called once per timestep
	void FixedUpdate () {
		if (GameManager.isMultiplayer) {
			if (photonView.isMine)
				ManageInput ();
			else
				SyncedMovement();
		} else
			ManageInput ();

	}

	//Interpolation
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;

		//Position
		transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);

		//Rotation
		transform.eulerAngles = Vector3.Lerp(syncStartRotation, syncEndRotation, syncTime / syncDelay);
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			//Position
			stream.SendNext(transform.position);
			stream.SendNext(rbody.velocity);
			//Rotation
			stream.SendNext(transform.eulerAngles);
			stream.SendNext(rbody.angularVelocity);
		}
		else
		{
			Vector3 syncPosition = (Vector3)stream.ReceiveNext();
			Vector3 syncVelocity = (Vector3)stream.ReceiveNext();
			Vector3 syncRotation = (Vector3)stream.ReceiveNext();
			Vector3 syncAngularVelocity = (Vector3)stream.ReceiveNext();
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;

			//Position
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = transform.position;

			//Rotation
			syncEndRotation = syncRotation + syncAngularVelocity * syncDelay;
			syncStartRotation = transform.eulerAngles;
		}
	}


	//Input
	void ManageInput(){
		//Mouse and keyboard navigation
		rbody.AddForce (gameObject.transform.forward * Input.GetAxis("Horizontal") * -1 * status.speed);
		rbody.AddForce (gameObject.transform.right * Input.GetAxis("Vertical")  * status.speed);
		gameObject.transform.Rotate(gameObject.transform.up * Input.GetAxis("Mouse X") * status.turnspeed);

		//Camera Z axis Rotation
		status.playerCamera.transform.LookAt (status.model.transform.position + new Vector3(0,4,0));
		
		//Movement drag and extra gravity
		rbody.AddForce(new Vector3(-rbody.velocity.x * horizontalDrag, -GameManager.extraGravity, -rbody.velocity.z * horizontalDrag));
		
		//Jump function
		if(!isGrounded &&
		   Physics.Raycast (new Ray (gameObject.transform.position, new Vector3(0,-1,0)), out hitInfo, 0.65f)){
			isGrounded = true;
		}
		if (Input.GetButtonDown("Jump") && isGrounded){
			jumpSound.Play();
			rbody.AddForce (gameObject.transform.up * status.jumpforce);
			isGrounded = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "Water")
			status.hp = 0;
	}
}
