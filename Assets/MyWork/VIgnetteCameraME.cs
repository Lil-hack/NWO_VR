using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIgnetteCameraME : MonoBehaviour {
	public UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration vig; 
	// Use this for initialization
	void Start () {
		vig= this.gameObject.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		vig.intensity -= 0.5f*Time.deltaTime;
		if (vig.intensity < 0) {
			vig.enabled = false;
			this.enabled = false;
		}
	}
}
