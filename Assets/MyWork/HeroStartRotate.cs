using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStartRotate : MonoBehaviour {

	public int crystal;
	public int money;
	// Use this for initialization
	void Start () {
		this.gameObject.transform.localRotation = Quaternion.identity;
	}
	

}
