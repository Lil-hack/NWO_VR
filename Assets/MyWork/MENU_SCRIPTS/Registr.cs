using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Registr : MonoBehaviour {
	public MenuManager menu;
	public string URL_API_USERS="https://api-user-game.herokuapp.com//users/";
	public string URL_API_STATS="https://api-stats-game.herokuapp.com/stats/";
	public InputField nickName;
	public InputField firstName;
	public InputField email;
	public InputField password;
	public CharacterManager charkManger;
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
	void Start () {
		charkManger=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<CharacterManager> ();
	}


	public void RegBut()
	{
		if (!nickName.text.Equals("") && !password.text.Equals("") && !firstName.text.Equals("") && !email.text.Equals("")) {
			if (CheckEmail (email.text)) {
				loading.SetActive (true);
				StartCoroutine (Upload ());

			} else {
				menu.GoToMenu (menu.ErrorMenu);
				menu.ErrorText.text = "Неправильная почта!";
			}

		}
	}
	private IEnumerator Upload() {

	
		WWWForm form = new WWWForm();
		form.AddField("username", nickName.text);
		form.AddField("first_name", firstName.text);
		form.AddField("last_name", "test");
		form.AddField("email", email.text);
		form.AddField("password", password.text);

		UnityWebRequest www = UnityWebRequest.Post(URL_API_USERS, form);
		yield return www.Send();
		if (www.isError) {
			Debug.Log (www.error);
			loading.SetActive (false);
			menu.GoToMenu (menu.ErrorMenu);
			menu.ErrorText.text = "Нет соединения!";
		} else {
			string json_user = www.downloadHandler.text;
			long code = www.responseCode;
				if (code==400) {
				loading.SetActive (false);
				menu.GoToMenu (menu.ErrorMenu);
				menu.ErrorText.text = "Пользователь существует!";

			}
			else
			{
				
				Debug.Log (json_user);
				User me = JsonUtility.FromJson<User> (json_user);

				PlayerPrefs.SetString ("uuid", me.uuid);
				PlayerPrefs.SetString ("username", me.username);
				PlayerPrefs.SetString ("first_name", me.first_name);
				PlayerPrefs.SetString ("last_name", me.last_name);
				PlayerPrefs.SetString ("email", me.email);
				StartCoroutine (UploadStats (me.uuid));
	
			}



		}
	}
	private IEnumerator UploadStats(string uuid) {


		WWWForm form = new WWWForm();
		form.AddField("user_uuid", uuid);



		UnityWebRequest www = UnityWebRequest.Post(URL_API_STATS, form);
		yield return www.Send();
		if (www.isError) {
			Debug.Log (www.error);
			loading.SetActive (false);
			menu.GoToMenu (menu.ErrorMenu);
			menu.ErrorText.text = "Нет соединения!";
			StartCoroutine (DeleteUser (uuid));
		} else {
			string json_stat= www.downloadHandler.text;
			long code = www.responseCode;
			if (code==400) {
				loading.SetActive (false);
				menu.GoToMenu (menu.ErrorMenu);
				menu.ErrorText.text = "Данные уже существуют!";
				StartCoroutine (DeleteUser (uuid));
			}
			else
			{
				Debug.Log (json_stat);
				Stats me = JsonUtility.FromJson<Stats> (json_stat);


				PlayerPrefs.SetInt ("exp", me.exp);
				PlayerPrefs.SetInt ("crystal", me.crystal);
				PlayerPrefs.SetInt ("money", me.money);
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
				charkManger.UpdateStats ();
				menu.GoToMenu (menu.StartMenu);
				menu.RegClose ();
			}



		}
	}
	private IEnumerator DeleteUser(string uuid) {

		UnityWebRequest www = UnityWebRequest.Get(URL_API_USERS+uuid+"/");
		www.method="DELETE";
		www.SetRequestHeader("Content-Type","application/json");
		yield return www.Send();

	}

	private bool CheckEmail(string email)

	{
		string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
		while (true)
		{
			

			if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
