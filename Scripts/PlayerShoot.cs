using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public Transform player;
	public float thrust_x;  //Wurfstärke des Spielers
	public float thrust_y;
	public float thrust_z;
	//public GameObject Ball;

	private float mousePosition_x;
	private float mousePosition_y;
	private float mousePosition_z;
	private Vector3 shootDirection;



	private Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{

			rb.useGravity = true;
			rb.AddForce (thrust_x, thrust_y, 0f);
		}

	}
}
