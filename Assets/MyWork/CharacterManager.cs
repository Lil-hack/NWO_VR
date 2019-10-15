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
	// данные пользователя

	public string uuid;
	public string username;
	public string first_name;
	public string email;
	public float lvl;
	public int money;
	public int crystal;
	public int skin;

	// Use this for initialization
	void Start () {
		

	}

	void OnDisable()
	{
		Destroy (hero);
	}

	void OnEnable()
	{
		// данные пользователя

		uuid=PlayerPrefs.GetString ("uuid");
		username=PlayerPrefs.GetString ("username");
		first_name=PlayerPrefs.GetString ("first_name");
		email=PlayerPrefs.GetString ("email");

		lvl=PlayerPrefs.GetFloat ("lvl");
		money=PlayerPrefs.GetInt ("money");
		crystal=PlayerPrefs.GetInt ("crystal");
		skin=PlayerPrefs.GetInt ("skin");

		// данные пользователя

		nameCharacter.text = PlayerPrefs.GetString ("username");
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
