using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterManager : MonoBehaviour {
	
	public Text nameCharacter;
	public int characterType;
	public GameObject heroes;
	public Transform trans;
	public GameObject hero;
	public int layerType;
	// Use this for initialization
	void Start () {
		

	}

	void OnDisable()
	{
		Destroy (hero);
	}

	void OnEnable()
	{
		nameCharacter.text = PlayerPrefs.GetString ("Name");
		characterType = PlayerPrefs.GetInt ("Skin");

		hero=Instantiate(	heroes.GetComponent<Heroes>().heroes[characterType],trans);
		//hero.layer = layerType;
		ChangeLayers (hero,layerType);
	}

	public static void ChangeLayers(GameObject go, int layer)
	{
		go.layer = layer;
		foreach (Transform child in go.transform)
		{
			ChangeLayers(child.gameObject, layer);
		}
	}

}
