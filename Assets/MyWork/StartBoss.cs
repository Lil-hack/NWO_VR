using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour {

	public ManaggerBossRoom manag;


	public void StartGame(){
		manag=GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManaggerBossRoom> ();
		StartCoroutine (StartG ());


	}
	IEnumerator StartG()
	{


		yield return new WaitForSeconds(1.5f);
		manag.StartGame ();

		//ShootBall ();
	}
}
