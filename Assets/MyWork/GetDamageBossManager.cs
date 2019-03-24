using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamageBossManager : MonoBehaviour {

	public ManaggerBossRoom manager;

	// Use this for initialization
	void Start () {
		manager=GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManaggerBossRoom> ();
		//	StartCoroutine (SphereColliderStart ());
	}

	//
	//	void OnCollisionEnter(Collision collision)
	//	{
	//		//if(collision.gameObject.name=="Sphere")
	//		//Debug.Log ("entercol"+collision.gameObject.name);
	//		if(collision.gameObject.name=="Sphere")
	//		manager.Damage(this.gameObject.name);
	//	}
	void OnTriggerEnter(Collider collision)
	{
		//if(collision.gameObject.name=="Sphere")

		Debug.Log ("bossdamage"+collision.gameObject.name);
		if (collision.gameObject.name == "DamageBoss") {
			Debug.Log ("tigeer"+collision.gameObject.name);
			manager.GetDamage ();
		}	//manager.Damage(collision.gameObject.name);
	}

}
