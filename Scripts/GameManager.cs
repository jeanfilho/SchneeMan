using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject topView;
	bool ready = true;

	void Awake(){
		//Creates a player
		if (PhotonNetwork.inRoom) {
			PhotonNetwork.Instantiate (playerPrefab.name, Vector3.up * 63, Quaternion.identity, 0);
			PhotonNetwork.automaticallySyncScene = true;
		}
		else
			Instantiate (playerPrefab);

		topView.SetActive (false);
	}

	void OnRoomJoined(){
		//Creates other players
		PhotonNetwork.Instantiate (playerPrefab.name, Vector3.up * 63, Quaternion.identity, 0);
	}
}
