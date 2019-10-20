using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour {
	public MenuManager menu;
	public string URL_API_USERS="https://api-user-game.herokuapp.com//users/";
	public InputField name;
	public InputField password;
	public GameObject loading;
	public GetStats getStat;
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


	void Start () {
		getStat=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<GetStats> ();
	}

	public void LoginBut()
	{
		if (!name.text.Equals("") && !password.text.Equals("")) {
			loading.SetActive (true);
			StartCoroutine (Upload ());

		}
	}
	private IEnumerator Upload() {
		string url = URL_API_USERS + string.Format ("login/username={0}&password={1}/", name.text, password.text);
		Debug.Log(url);
		UnityWebRequest www = UnityWebRequest.Get(url);
			yield return www.Send();

		if (www.isError) {
			Debug.Log (www.error);
			loading.SetActive (false);
			menu.GoToMenu (menu.ErrorMenu);
			menu.ErrorText.text = "Нет соединения!";

		} else {
			string json_user = www.downloadHandler.text;
			long code = www.responseCode;
			if (code==401 || json_user.Equals("401")) {
				Debug.Log (www.error);
				loading.SetActive (false);
				menu.GoToMenu (menu.ErrorMenu);
				menu.ErrorText.text = "Неправильный логин или пароль!";

			}
			if (code==404 || json_user.Equals("404")) {
				loading.SetActive (false);
				menu.GoToMenu (menu.ErrorMenu);
				menu.ErrorText.text = "С таким логином нет пользователя!";

			} 
			if(code==201 || code==200 && !json_user.Equals("404") && !json_user.Equals("401"))
			{Debug.Log (code);
				Debug.Log (json_user);
				User me = JsonUtility.FromJson<User> (json_user);

				PlayerPrefs.SetString ("uuid", me.uuid);
				PlayerPrefs.SetString ("username", me.username);
				PlayerPrefs.SetString ("first_name", me.first_name);
				PlayerPrefs.SetString ("last_name", me.last_name);
				PlayerPrefs.SetString ("email", me.email);
				getStat.GetData ();
				loading.SetActive (false);
				menu.GoToMenu (menu.StartMenu);
				menu.LoginClose ();
			}
				
		

		}
	}

}
