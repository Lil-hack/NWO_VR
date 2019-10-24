using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionValueCam : MonoBehaviour {
	public Slider slid;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetFloat("slider")==0)
			slid.value = 0.5f;
		else slid.value = PlayerPrefs.GetFloat ("slider");
	}

	// Update is called once per frame
	public void ChangeCamValue()
	{
		PlayerPrefs.SetFloat ("slider", slid.value);
	}
	
}
