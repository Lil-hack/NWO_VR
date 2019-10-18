using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerouesShop : MonoBehaviour {


	public int characterType;
	public GameObject heroes;
	public Transform trans;
	public Vector3 rangeHeroes;
	public Vector3 rangeHeroesNextBack;
	public int layerType;
	public int countHeroes=0;
	public int nextBack=0;
	public List<GameObject> myHeroes;
	public CharacterManager charManger;
	// Use this for initialization
	public List <string> headers ;
	public List <string> data ;
	public ChangeManager changeManger;
	void Start () {


		changeManger=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<ChangeManager> ();



	}
	void OnEnable()
	{
		// данные пользователя
		countHeroes=0;
		int caseSwitch = 1;
		foreach (var hero in heroes.GetComponent<Heroes>().heroes) {

			switch (caseSwitch)
			{
			case 3:
				if (charManger.skin3 == 0) AddHero(hero);	
				break;
			case 4:
				if (charManger.skin4 == 0) AddHero(hero);	
				break;
			case 5:
				if (charManger.skin5 == 0) AddHero(hero);	
				break;
			case 6:
				if (charManger.skin6 == 0) AddHero(hero);	
				break;
			case 7:
				if (charManger.skin7 == 0) AddHero(hero);	
				break;
			case 8:
				if (charManger.skin8 == 0) AddHero(hero);	
				break;
			case 9:
				if (charManger.skin9 == 0) AddHero(hero);	
				break;
			case 10:
				if (charManger.skin10 == 0) AddHero(hero);	
				break;
			case 11:
				if (charManger.skin11 == 0) AddHero(hero);	
				break;
			case 12:
				if (charManger.skin12 == 0) AddHero(hero);	
				break;
			case 13:
				if (charManger.skin13 == 0) AddHero(hero);	
				break;
			case 14:
				if (charManger.skin14 == 0) AddHero(hero);	
				break;
			case 15:
				if (charManger.skin15 == 0) AddHero(hero);	
				break;
			case 16:
				if (charManger.skin16 == 0) AddHero(hero);	
				break;
			case 17:
				if (charManger.skin17 == 0) AddHero(hero);	
				break;
			case 18:
				if (charManger.skin18 == 0) AddHero(hero);	
				break;
			case 19:
				if (charManger.skin19 == 0) AddHero(hero);	
				break;
			case 20:
				if (charManger.skin20 == 0) AddHero(hero);	
				break;
			}



			caseSwitch++;
		}

	}
	private void AddHero(GameObject hero)
	{

		myHeroes.Add (Instantiate (hero,trans));
		myHeroes[countHeroes].transform.localPosition = rangeHeroes * countHeroes;
		ChangeLayers (myHeroes[countHeroes],layerType);

		countHeroes++;
	}
	public static void ChangeLayers(GameObject go, int layer)
	{
		go.layer = layer;
		foreach (Transform child in go.transform)
		{
			ChangeLayers(child.gameObject, layer);
		}
	}

	public void Next()
	{if (nextBack < countHeroes-1) {
			nextBack++;
			trans.localPosition += rangeHeroesNextBack;
		}
	}
	public void Back()
	{if (nextBack != 0 ) {
			nextBack--;
			trans.localPosition -= rangeHeroesNextBack;
		}
	}
	public void Ok()
	{
		data.Clear ();
		data.Add (nextBack.ToString ());
		changeManger.ChangeMethod (headers, data);

	}
}
