using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour 
{
	public Rigidbody ball;
	public float power;
	public Vector3 adjust;
	public float coolDown;

	private float nextShot;


	void start()
	{
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0) && nextShot <= Time.time) 
		{
			Rigidbody instance = Instantiate (ball, gameObject.transform.position + adjust, Quaternion.identity) as Rigidbody;
			Vector3 fwd = (transform.TransformDirection(Vector3.right));
			instance.AddForce (fwd * power);
			nextShot = Time.time + coolDown;
		}
	}
}
