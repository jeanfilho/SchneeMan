using UnityEngine;
using System.Collections;

public class PlayerStatus : Photon.MonoBehaviour {

	//Health, status and abilities
	public float hp = 100;
	public float speed = 30;
	public float turnspeed = 10;
	public float jumpforce = 300;

	//Components
	public GameObject playerCamera;
	public float coins;

	bool damaged;

	private bool boughtIce = false;
	private bool showPopUp = false;

	// Use this for initialization
	void Start () {
		coins = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCamera.activeSelf && !photonView.isMine && GameManager.isMultiplayer)
			playerCamera.SetActive (false);
	}

	//Power-ups
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Eis") {
			Destroy (other.gameObject);
			if (hp < 100) {
				hp++;
				Debug.Log (hp);
			} else if (hp == 100) {
				hp = 100;
				Debug.Log (hp);
			}
		} else if (other.gameObject.tag == "Coins") {
			Destroy (other.gameObject);
		} else if (other.gameObject.tag == "BuyIce") {
			OnGUI();
			showPopUp = true;
		}
	}

	void OnGUI()
	{
		if (showPopUp && coins >= 10)
		{
			GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-125
			                       , 300, 250), ShowGUI, "Eiswagen");
		}
	}

	void ShowGUI(int windowID)
	{
		GUI.Label(new Rect(65, 40, 200, 30), "Willst Du Eis kaufen?");
		GUI.Label (new Rect (65, 60, 200, 30), "Preis: 10 Münzen");
		if (GUI.Button(new Rect(50, 150, 75, 30), "Ja"))
		{
			showPopUp = false;
			coins = coins-10;
		} else if (GUI.Button(new Rect(170, 150, 75, 30), "Nein")) {
			showPopUp = false;
		}	
	}

}
