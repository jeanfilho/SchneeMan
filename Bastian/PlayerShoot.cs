using UnityEngine;
using System.Collections;

public class PlayerShoot : Photon.MonoBehaviour 
{
	public GameObject ball;
	public float power;
	public Vector3 adjust;
	public float coolDown;

	private AudioSource throwSound;
	private float timer;
	private PlayerStatus status;


	void Awake()
	{
		throwSound = GetComponents<AudioSource> ()[1];
		status = gameObject.GetComponent<PlayerStatus> ();
	}

	void Update()
	{
		if (GameManager.isMultiplayer) {
			if (photonView.isMine)
				ManageInput ();
		} else
			ManageInput ();
	}

	void ManageInput(){
		if (Input.GetMouseButton (0) && timer <= Time.time) 
		{
			throwSound.Play();
			GameObject shot;
			if(GameManager.isMultiplayer)
				shot = PhotonNetwork.Instantiate (ball.name, gameObject.transform.position + adjust, Quaternion.identity,0);
			else 
				shot = Instantiate (ball, gameObject.transform.position + adjust, Quaternion.identity) as GameObject;

			shot.GetComponent<Ball>().damage = status.ballDamage;
			Vector3 shootDirection = (transform.TransformDirection(Vector3.right));
			shot.GetComponent<Rigidbody>().AddForce (shootDirection * power * 100);
			timer = Time.time + coolDown;
		}
	}
}
