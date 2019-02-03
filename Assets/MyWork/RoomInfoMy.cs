using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RoomInfoMy : MonoBehaviour {
	public string roomName;
	public int playerCount;
	public int maxPlayer=45;
	public Text nameRoom;
	public Text countOnline;
	public GameObject connectButton;
	public GameObject connectButtonClose;
	public string nameLoadScene;


	// Use this for initialization
	void Start () {
		nameRoom.text = roomName;
		countOnline.text = playerCount + "/" + maxPlayer;
		if (playerCount >= maxPlayer) {
			connectButtonClose.SetActive (true);
			connectButton.SetActive (false);
		}


	}
	public void ConnectRoomMain()
	{
		StartCoroutine (ConnectRoom ());
	}
	
	private IEnumerator ConnectRoom()
	{


		yield return new WaitForSeconds (2);
		PlayerPrefs.SetString("RoomName", roomName);	
		SceneManager.LoadScene(nameLoadScene);


	}
}
