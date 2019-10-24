using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterManager : MonoBehaviour {
	
	public Text nameCharacter;
	public Text crytalText;
	public Text moneyText;
	public Text lvlText;



	public int characterType;
	public GameObject heroes;
	public Transform trans;
	public GameObject hero;
	public GameObject mainSkin;
	public int layerType;
	// данные пользователя

	public string uuid;
	public string username;
	public string first_name;
	public string email;
	public int exp;
	public int money;
	public int crystal;
	public int rating;
	public int skin;
	public int skin1;
	public int skin2;
	public int skin3;
	public int skin4;
	public int skin5;
	public int skin6;
	public int skin7;
	public int skin8;
	public int skin9;
	public int skin10;
	public int skin11;
	public int skin12;
	public int skin13;
	public int skin14;
	public int skin15;
	public int skin16;
	public int skin17;
	public int skin18;
	public int skin19;
	public int skin20;





	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("uuid").CompareTo ("")!=0) UpdateStats ();

	}

	void OnDisable()
	{
		Destroy (hero);
	}



	public static void ChangeLayers(GameObject go, int layer)
	{
		go.layer = layer;
		foreach (Transform child in go.transform)
		{
			ChangeLayers(child.gameObject, layer);
		}
	}

	public void UpdateStats()
	{
		Destroy (hero);
		mainSkin.SetActive (true);
		uuid=PlayerPrefs.GetString ("uuid");
		username=PlayerPrefs.GetString ("username");
		first_name=PlayerPrefs.GetString ("first_name");
		email=PlayerPrefs.GetString ("email");

		exp = PlayerPrefs.GetInt ("exp");
		money=PlayerPrefs.GetInt ("money");
		crystal=PlayerPrefs.GetInt ("crystal");
		rating=PlayerPrefs.GetInt ("rating");
		skin=PlayerPrefs.GetInt ("skin");
		skin1=PlayerPrefs.GetInt ("skin1");
		skin2=PlayerPrefs.GetInt ("skin2");
		skin3=PlayerPrefs.GetInt ("skin3");
		skin4=PlayerPrefs.GetInt ("skin4");
		skin5=PlayerPrefs.GetInt ("skin5");
		skin6=PlayerPrefs.GetInt ("skin6");
		skin7=PlayerPrefs.GetInt ("skin7");
		skin8=PlayerPrefs.GetInt ("skin8");
		skin9=PlayerPrefs.GetInt ("skin9");
		skin10=PlayerPrefs.GetInt ("skin10");
		skin11=PlayerPrefs.GetInt ("skin11");
		skin12=PlayerPrefs.GetInt ("skin12");
		skin13=PlayerPrefs.GetInt ("skin13");
		skin14=PlayerPrefs.GetInt ("skin14");
		skin15=PlayerPrefs.GetInt ("skin15");
		skin16=PlayerPrefs.GetInt ("skin16");
		skin17=PlayerPrefs.GetInt ("skin17");
		skin18=PlayerPrefs.GetInt ("skin18");
		skin19=PlayerPrefs.GetInt ("skin19");
		skin20=PlayerPrefs.GetInt ("skin20");

		hero=Instantiate(	heroes.GetComponent<Heroes>().heroes[skin],trans);
		//hero.layer = layerType;
		ChangeLayers (hero,layerType);
		// данные пользователя
		int i=1;
		double lvl = 0.0;
		double expirence = 1000;

		while (i != 10) {


			if (exp > expirence) 
			{
				expirence = expirence + i * 2000 + 1000;
			
				i++;
			}
			else 
			{ 
				lvl = i-1 +(1-(expirence-exp) /((i-1) * 2000 + 1000));
			
				break;
			}
		
		
		}

		nameCharacter.text = username;
		crytalText.text = crystal.ToString();
		moneyText.text = money.ToString();

		lvlText.text=string.Format("{0:0.##}", lvl);






	}


}
