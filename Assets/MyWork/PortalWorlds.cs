using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalWorlds : MonoBehaviour {
	private ManagerGunRoom manag;
	private ManagerStartRoom manag2;
	private ManagerWalkRoom manag3;
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
		try{
			manag3 = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerWalkRoom> ();
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
		if(manag3!=null)
		if (other.gameObject.name == manag3.playerInGame[0].name) {


			StartCoroutine (Teleport3 ());

		}
	}

	void OnTriggerExit (Collider other)
	{

		StopAllCoroutines ();
	}

	private IEnumerator Teleport()
	{

		yield return new WaitForSeconds(1f);
		if (webcam.cam_texture != null) {
			webcam.StopCam ();
		}
		manag.DisconnectAfterCreateRoom ();

		SceneManager.LoadScene(nameScene);



	}
	private IEnumerator Teleport2()
	{

		yield return new WaitForSeconds(1f);
		if (webcam.cam_texture != null) {
			webcam.StopCam ();
		}

		manag2.DisconnectAfterCreateRoom ();
		SceneManager.LoadScene(nameScene);



	}
	private IEnumerator Teleport3()
	{

		yield return new WaitForSeconds(1f);
		if (webcam.cam_texture != null) {
			webcam.StopCam ();
		}

		manag3.DisconnectAfterCreateRoom ();
		SceneManager.LoadScene(nameScene);



	}

}
