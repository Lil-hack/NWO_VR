using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class Record : MonoBehaviour {

	public MenuManager menu;
	public string URL_API_USERS="https://api-user-game.herokuapp.com/users/rating/";
	public string URL_API_STATS="https://api-stats-game.herokuapp.com/stats/rating/";
	public List<GameObject> textList;
	public GameObject text;
	public Text yourRecord;
	public GameObject trans;
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
	public class UserList
	{
		public List<User> list;
	}
	[System.Serializable]
	public class Stats
	{
		public string user_uuid;
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

	[System.Serializable]
	public class StatsList
	{
		public List<Stats> list;
	}
	void Start () {
		menu=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<MenuManager> ();
	}


	public void RecordBut()
	{
		yourRecord.text=PlayerPrefs.GetInt ("rating").ToString();
		foreach (var item in textList) {
			Destroy (item);
		}
		textList.Clear ();
		StartCoroutine (UploadStats ());


	}


	private IEnumerator UploadStats() {
		loading.SetActive (true);

		WWWForm form = new WWWForm ();
		UnityWebRequest www = UnityWebRequest.Get(URL_API_STATS);
		yield return www.Send();
		if (www.isError) {
			Debug.Log (www.error);
			loading.SetActive (false);
			menu.GoToMenu (menu.ErrorMenu2);
			menu.ErrorText2.text = "Нет соединения!";

		} else {
			string json_stat= www.downloadHandler.text;
			long code = www.responseCode;
			if (code==400) {
				loading.SetActive (false);
				menu.GoToMenu (menu.ErrorMenu2);
				menu.ErrorText2.text = "Мы ищем ошибку!";

			}
			else
			{
				Debug.Log ("{\"list\":"+json_stat+"}");
				var me = JsonUtility.FromJson<StatsList> ("{\"list\":"+json_stat+"}");
				for(int i=0;i<5;i++){

					if (i == 0) {
						URL_API_USERS=URL_API_USERS+string.Format("uuid{0}={1}", i+1, me.list[i].user_uuid);
					}
					else{
						URL_API_USERS=URL_API_USERS+string.Format("&uuid{0}={1}", i+1, me.list[i].user_uuid);
					}

				}

				Debug.Log (URL_API_USERS);

				StartCoroutine (UploadUSerData (me));

			


			}



		}
	}
	private IEnumerator UploadUSerData(StatsList statList) {


		WWWForm form = new WWWForm();


		UnityWebRequest www = UnityWebRequest.Get(URL_API_USERS);
		yield return www.Send();
		if (www.isError) {
			Debug.Log (www.error);
			loading.SetActive (false);
			menu.GoToMenu (menu.ErrorMenu2);
			menu.ErrorText2.text = "Нет соединения!";
		} else {
			string json_user = www.downloadHandler.text;
			long code = www.responseCode;
			if (code==400) {
				loading.SetActive (false);
				menu.GoToMenu (menu.ErrorMenu2);
				menu.ErrorText2.text = "Мы ищем ошибку! ";

			}
			else
			{
				URL_API_USERS = "https://api-user-game.herokuapp.com/users/rating/";
				Debug.Log (json_user);
				var me = JsonUtility.FromJson<UserList> ("{\"list\":"+json_user+"}");
				Debug.Log (me.list);
				loading.SetActive (false);
				for(int i=0;i<5;i++){
					text.GetComponent<Text> ().text = string.Format ("{0}:     {1}    -   {2}", i + 1, me.list [4-i].username, statList.list [i].rating);
					var createInfo = Instantiate (text, trans.transform);
					createInfo.transform.localPosition = new Vector3(0,0-i*50,0);
					textList.Add (createInfo);

				}


			}



		}
	}

}
