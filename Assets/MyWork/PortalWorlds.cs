using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalWorlds : MonoBehaviour {
	private ManagerGunRoom manag;
	private ManagerStartRoom manag2;
	private WebCam webcam;
	public string nameScene;
	// Use this for initialization
	void Start () {
		try{
		manag = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerGunRoom> ();
			webcam=GameObject.FindGameObjectWithTag ("GameManager").GetComponent<WebCam> ();
		}
		catch{
		}
		try{
			manag2 = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerStartRoom> ();
			webcam=GameObject.FindGameObjectWithTag ("GameManager").GetComponent<WebCam> ();
		}
		catch{
		}

	}
	

	void OnTriggerEnter (Collider other)
	{

		Debug.Log (other.gameObject.name);
		if(manag!=null)
		if (other.gameObject.name == manag.playerInGame[0].name) {


		StartCoroutine (Teleport ());

		}
		if(manag2!=null)
		if (other.gameObject.name == manag2.playerInGame[0].name) {


			StartCoroutine (Teleport2 ());

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
		webcam.StopCam ();
		SceneManager.LoadScene(nameScene);



	}
	private IEnumerator Teleport2()
	{

		yield return new WaitForSeconds(1f);
		webcam.StopCam ();
		manag2.DisconnectAfterCreateRoom ();
		SceneManager.LoadScene(nameScene);



	}

}
