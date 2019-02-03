using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindRoomButton : MonoBehaviour {

	public string nameLoadScene;
	public void Find()
	{
		StartCoroutine (FindRooms());
	}
	private IEnumerator FindRooms()
	{

		yield return new WaitForSeconds(1.5f);


		SceneManager.LoadScene(nameLoadScene);
		GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerStartRoom> ().DisconnectAfterCreateRoom ();




	}
}
