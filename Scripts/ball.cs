using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour 
{
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag != "Player")
			Destroy (gameObject);
	}
}
