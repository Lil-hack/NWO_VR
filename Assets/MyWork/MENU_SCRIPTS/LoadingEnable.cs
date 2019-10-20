using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingEnable : MonoBehaviour {
	public GameObject menu;
	public GameObject hero;
	// Use this for initialization
	void OnEnable()
	{
		menu.SetActive (false);
		hero.SetActive (false);
	}
	void OnDisable()
	{
		menu.SetActive (true);
		hero.SetActive (true);
	}
}
