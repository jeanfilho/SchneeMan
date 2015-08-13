using UnityEngine;
using System.Collections;

public class EnemyCreator : Photon.MonoBehaviour {

	public Transform[] spawnPoints;	//Array von Emptys (SpawnPoints)
	public int totalEnemies;	//Anzahl der Krabbengegner die gespawnt werden
	public GameObject[] enemy;	//Array von Gegnerarten

	// Use this for initialization
	void Awake () 
	{
		if (GameManager.isMultiplayer) {

			if (totalEnemies <= spawnPoints.Length) {
				for (int i = 0; i < totalEnemies; i++) {
					PhotonNetwork.Instantiate (enemy [Random.Range (0, enemy.Length)].name, spawnPoints [i].position, Quaternion.identity,0);
				}
			}
		} 
		else 
		{
			if (totalEnemies <= spawnPoints.Length)
			{
				for (int i = 0; i < totalEnemies; i++) {
					Instantiate (enemy [Random.Range (0, enemy.Length)], spawnPoints [i].position, Quaternion.identity);
				}
			}
		}
	}
}
