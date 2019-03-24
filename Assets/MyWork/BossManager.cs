using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {
	public Vector3 pos;

	public bool metkaMove;
	public float speed=5;
	public Animator anim;
	public GameObject damageCloud;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (metkaMove == true) {


			this.gameObject.transform.position =  Vector3.Lerp (this.gameObject.transform.position,pos,speed*Time.deltaTime);

			if (Mathf.Abs(this.gameObject.transform.position.x-pos.x) <1 && Mathf.Abs(this.gameObject.transform.position.z-pos.z) <1 ) {
				metkaMove = false;
				anim.SetBool ("walk", false);
				anim.SetTrigger ("agr");
				damageCloud.SetActive (false);
			}
		}
	}

	public void Move(float x,float z)
	{ metkaMove = true;
		damageCloud.SetActive (true);
		anim.SetBool ("walk", true);
		pos = new Vector3 (x,
			this.gameObject.transform.position.y,
			z);
		this.transform.LookAt (pos);

	}
}
