using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject multiplayer;
	private RoomInfo[] roomsList;


	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		if (roomsList != null) {
			for (int i = 0; i < roomsList.Length; i++) {
				Debug.Log ("Game found");
				if (GUI.Button (new Rect (500, 250 + (110 * i), 250, 100), "Join " + roomsList [i].name))
					PhotonNetwork.JoinRoom (roomsList [i].name);
			}
		}
	}

	void Update(){
		if (!PhotonNetwork.connected)
			multiplayer.SetActive (false);
		else 
			multiplayer.SetActive (true);		
	}
	
	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
		Debug.Log("Received a list: " + roomsList.Length);
	}

	public void CreateRoom(){
		RoomOptions ro = new RoomOptions();
		ro.isVisible = true;
		ro.isOpen = true;
		ro.maxPlayers = 16;
		PhotonNetwork.CreateRoom(System.Guid.NewGuid().ToString("N"), ro, TypedLobby.Default);
	}
	public void JoinRoom(string name){
		PhotonNetwork.JoinRoom(name);
	}

	void OnJoinedRoom(){
		Application.LoadLevel ("Level1");
	}
	
	
}
