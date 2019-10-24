using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeleteAcc : MonoBehaviour {
	public string URL_API_USERS="https://api-user-game.herokuapp.com//users/";
	public string URL_API_STATS="https://api-stats-game.herokuapp.com/stats/";
	public GameObject loading;
	private MenuManager menu;

	// Use this for initialization
	void Start () {
		menu=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<MenuManager> ();
	}
	
	public void DelBut()
	{
		loading.SetActive (true);
				StartCoroutine (Upload ());



	}

	private IEnumerator Upload() {

		string uuid=PlayerPrefs.GetString ("uuid");

		UnityWebRequest www = UnityWebRequest.Get(URL_API_USERS+uuid+"/");
		www.method="DELETE";
		www.SetRequestHeader("Content-Type","application/json");
		yield return www.Send();
		if (www.isError) {
			Debug.Log (www.error);
			loading.SetActive (false);
			menu.GoToMenu (menu.ErrorMenu);
			menu.ErrorText.text = "Нет соединения!";
		} else {
			UnityWebRequest www2 = UnityWebRequest.Get (URL_API_STATS + uuid + "/");
			www2.method = "DELETE";
			www2.SetRequestHeader ("Content-Type", "application/json");

			yield return www2.Send ();
			if (www2.isError) {
				Debug.Log (www.error);
				loading.SetActive (false);
				menu.GoToMenu (menu.ErrorMenu);
				menu.ErrorText.text = "Нет соединения!";
			} else {
				menu.LogOut ();
			}
		}
		
	}
}
