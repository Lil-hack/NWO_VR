using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoloHero : MonoBehaviour {

	// Use this for initialization
	public GameObject target;
	public WebCam webcam;
	public float speed = 0.25f;
	// Update is called once per frame
	void Update () {
		if (webcam.footmetka == true) {
			target.transform.Translate (Vector3.forward * Time.deltaTime * speed);
		}

		



		if (Input.GetKey (KeyCode.W)) {
			target.transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}
	
}
}
