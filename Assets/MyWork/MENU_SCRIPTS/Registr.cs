using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Registr : MonoBehaviour {
	public MenuManager menu;
	public string URL_API_USERS="https://api-user-game.herokuapp.com//users/";
	public InputField nickName;
	public InputField firstName;
	public InputField email;
	public InputField password;
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

	}


	public void RegBut()
	{
		if (!nickName.text.Equals("") && !password.text.Equals("") && !firstName.text.Equals("") && !email.text.Equals("")) {
			if (CheckEmail (email.text)) {
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
			menu.GoToMenu (menu.ErrorMenu);
			menu.ErrorText.text = "Нет соединения!";
		} else {
			string json_user = www.downloadHandler.text;
			long code = www.responseCode;
				if (code==400) {
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
				menu.GoToMenu (menu.StartMenu);
			}



		}
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
