using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;

	void Awake(){
		//Creates a player
		if (PhotonNetwork.inRoom)
			PhotonNetwork.Instantiate (playerPrefab.name, Vector3.up * 63, Quaternion.identity, 0);
		else
			Instantiate (playerPrefab);
	}
}
