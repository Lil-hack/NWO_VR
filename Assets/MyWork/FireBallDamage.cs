using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDamage : MonoBehaviour {

	public ManagerGunRoom manager;
	public float timecolliderstart=0.2f;
	// Use this for initialization
	void Start () {
		manager=GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerGunRoom> ();
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

		//Debug.Log ("tigeer"+collision.gameObject.name);
		if (collision.gameObject.name == "Sphere") {
			Debug.Log ("tigeer"+collision.gameObject.name);
			manager.Damage (this.gameObject.name);
		}	//manager.Damage(collision.gameObject.name);
	}

	private IEnumerator SphereColliderStart()
	{

		yield return new WaitForSeconds(timecolliderstart);
		this.gameObject.GetComponent<SphereCollider> ().enabled = true;


	}

}
