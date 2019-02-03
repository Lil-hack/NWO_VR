using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNPC : MonoBehaviour {

	public string animation;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Animator> ().SetBool (animation,true);
	}
	

}
