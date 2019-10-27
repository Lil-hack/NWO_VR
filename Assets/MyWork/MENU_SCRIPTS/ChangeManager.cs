using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangeManager : MonoBehaviour {
	public MenuManager menu;
	public string URL_API_USERS="https://api-user-game.herokuapp.com/users/";
	public string URL_API_STATS="https://api-stats-game.herokuapp.com/stats/";
	public CharacterManager charManger;
	public GameObject loading;
	// Use this for initialization
	[System.Serializable]
	public class User
	{
		public string uuid;
		public string username;
		public string first_name;
		public string last_name;
		public string email;
		public string password;
	}



	[System.Serializable]
	public class Stats
	{
		public string uuid;
		public int exp;
		public int crystal;
		public int money;
		public int rating;
		public int skin;
		public bool skin1;
		public bool skin2;
		public bool skin3;
		public bool skin4;
		public bool skin5;
		public bool skin6;
		public bool skin7;
		public bool skin8;
		public bool skin9;
		public bool skin10;
		public bool skin11;
		public bool skin12;
		public bool skin13;
		public bool skin14;
		public bool skin15;
		public bool skin16;
		public bool skin17;
		public bool skin18;
		public bool skin19;
		public bool skin20;
	}




	public void ChangeMethod(List <string> headers,List <string> data )
	{
		loading.SetActive (true);
		StartCoroutine (ChangeMain (headers,data));

	}

	private IEnumerator ChangeMain(List <string> headers,List <string> data ) {

		string uuid = PlayerPrefs.GetString ("uuid");

		WWWForm form = new WWWForm();

		int i = 0;
		foreach (var header in headers) {

			form.AddField(header, data[i]);

			i++;
		}
		UnityWebRequest www = UnityWebRequest.Post(URL_API_STATS+uuid+"/",form);
		www.method="PATCH";

		yield return www.Send();

		string json_stat = www.downloadHandler.text;
		long code = www.responseCode;

		if (code == 201 || code == 200) {

			Debug.Log (json_stat);
			Stats me = JsonUtility.FromJson<Stats> (json_stat);


			PlayerPrefs.SetInt ("exp", me.exp);
			PlayerPrefs.SetInt ("crystal", me.crystal);
			PlayerPrefs.SetInt ("money", me.money);
			PlayerPrefs.SetInt ("rating", me.rating);
			PlayerPrefs.SetInt ("skin", me.skin);
			PlayerPrefs.SetInt ("skin1", me.skin1?1:0);
			PlayerPrefs.SetInt ("skin2", me.skin2?1:0);
			PlayerPrefs.SetInt ("skin3", me.skin3?1:0);
			PlayerPrefs.SetInt ("skin4", me.skin4?1:0);
			PlayerPrefs.SetInt ("skin5", me.skin5?1:0);
			PlayerPrefs.SetInt ("skin6", me.skin6?1:0);
			PlayerPrefs.SetInt ("skin7", me.skin7?1:0);
			PlayerPrefs.SetInt ("skin8", me.skin8?1:0);
			PlayerPrefs.SetInt ("skin9", me.skin9?1:0);
			PlayerPrefs.SetInt ("skin10", me.skin10?1:0);
			PlayerPrefs.SetInt ("skin11", me.skin11?1:0);
			PlayerPrefs.SetInt ("skin12", me.skin12?1:0);
			PlayerPrefs.SetInt ("skin13", me.skin13?1:0);
			PlayerPrefs.SetInt ("skin14", me.skin14?1:0);
			PlayerPrefs.SetInt ("skin15", me.skin15?1:0);
			PlayerPrefs.SetInt ("skin16", me.skin16?1:0);
			PlayerPrefs.SetInt ("skin17", me.skin17?1:0);
			PlayerPrefs.SetInt ("skin18", me.skin18?1:0);
			PlayerPrefs.SetInt ("skin19", me.skin19?1:0);
			PlayerPrefs.SetInt ("skin20", me.skin20?1:0);
			loading.SetActive (false);
			charManger.UpdateStats ();

		} else 
		{
			Debug.Log (json_stat);
			loading.SetActive (false);
			menu.GoToMenu (menu.ErrorMenu2);
			menu.Hero.SetActive (false);
			menu.ErrorText.text = "Нет соединения!";
		}




	}

}
