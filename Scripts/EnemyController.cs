using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform target;
	public float moveSpeed;
	public float rotationSpeed;
	public float detectionRange;
	public float attackRange;
	public int hp;

	void Start () 
	{
		target = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		healthControl ();

		if ((Vector3.Distance (target.position, transform.position) < attackRange) == false) {
			moveTowards ();
		} else 
			EnemyAttack ();
	}

	void moveTowards()
	{
		if (Vector3.Distance (target.position, transform.position) < detectionRange) 
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position), Time.deltaTime * rotationSpeed);
			transform.position = transform.position + (transform.forward * (moveSpeed * Time.deltaTime));
		}
	}

	void EnemyAttack()
	{
	}


	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "bullet") 
		{
			hp -= 2;
		} 
	}

	void healthControl()
	{
		if (hp <= 0) 
		{
			Destroy(gameObject);
		}
	}
	

}
