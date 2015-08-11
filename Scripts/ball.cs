using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour 
{
	public ParticleSystem particle;
	MeshRenderer mesh;

	IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds (0.5f);
	}


	void Start()
	{
		particle = GetComponent<ParticleSystem>();
		mesh = GetComponent<MeshRenderer> ();
	}

	IEnumerator OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag != "Player") 
		{
			particle.Play ();
			mesh.enabled = false;
			yield return StartCoroutine("WaitAndDestroy");
			Destroy (gameObject); 
		}
	}

	void OnTriggerEnter (Collider col)
	{
		Destroy (gameObject);
	}

}
