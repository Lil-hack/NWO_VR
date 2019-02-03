using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalWorlds : MonoBehaviour {
	private ManagerGunRoom manag;
	// Use this for initialization
	void Start () {
		manag = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerGunRoom> ();
	}

	void OnTriggerEnter (Collider other)
	{

		Debug.Log (other.gameObject.name);

		if (other.gameObject.name == manag.playerInGame[0].name) {


		StartCoroutine (Teleport ());

		}
	}

	void OnTriggerExit (Collider other)
	{

		StopAllCoroutines ();
	}

	private IEnumerator Teleport()
	{

		yield return new WaitForSeconds(1f);
		manag.DisconnectAfterCreateRoom ();
		SceneManager.LoadScene("MainRoom");



	}

}
