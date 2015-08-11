using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public Rigidbody bullet;
	public float power;
	public float moveSpeed;
	public Vector3 adjust;

	// Use this for initialization
	void Start () 
	{
			

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Rigidbody Instance = Instantiate (bullet, transform.position + adjust, Quaternion.identity) as Rigidbody;
			Vector3 fwd = transform.TransformDirection(Vector3.right);
			Instance.AddForce (fwd * power);
		}

	}
}
