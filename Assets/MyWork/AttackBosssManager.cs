
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

public class AttackBosssManager : MonoBehaviour {

	public ManaggerBossRoom manager;
	public Animator anim;
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

			Debug.Log ("tigeer"+collision.gameObject.name);
			if (collision.gameObject.name == "FireBall(Clone)") {
				Debug.Log ("tigeer"+collision.gameObject.name);
				manager.Damage (this.gameObject.name);
			anim.SetTrigger ("hit");
			}	//manager.Damage(collision.gameObject.name);
		}



	}
