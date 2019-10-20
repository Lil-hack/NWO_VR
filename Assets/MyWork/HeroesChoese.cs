using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesChoese : MonoBehaviour {
	

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
	void OnDisable()
	{
		foreach (var hero in myHeroes) {
			Destroy (hero);
		}
		myHeroes.Clear();
	}
	void OnEnable()
	{
		// данные пользователя

		countHeroes=0;
		int caseSwitch = 0;

		foreach (var hero in heroes.GetComponent<Heroes>().heroes) {
			
			switch (caseSwitch)
			{
			case 1:
				Debug.Log (hero.name);

					AddHero (hero);	


				break;
			case 2:
				AddHero(hero);	
				break;
			case 3:
				if (charManger.skin3 == 1) AddHero(hero);	
				break;
			case 4:
				if (charManger.skin4 == 1) AddHero(hero);	
				break;
			case 5:
				if (charManger.skin5 == 1) AddHero(hero);	
				break;
			case 6:
				if (charManger.skin6 == 1) AddHero(hero);	
				break;
			case 7:
				if (charManger.skin7 == 1) AddHero(hero);	
				break;
			case 8:
				if (charManger.skin8 == 1) AddHero(hero);	
				break;
			case 9:
				if (charManger.skin9 == 1) AddHero(hero);	
				break;
			case 10:
				if (charManger.skin10 == 1) AddHero(hero);	
				break;
			case 11:
				if (charManger.skin11 == 1) AddHero(hero);	
				break;
			case 12:
				if (charManger.skin12 == 1) AddHero(hero);	
				break;
			case 13:
				if (charManger.skin13 == 1) AddHero(hero);	
				break;
			case 14:
				if (charManger.skin14 == 1) AddHero(hero);	
				break;
			case 15:
				if (charManger.skin15 == 1) AddHero(hero);	
				break;
			case 16:
				if (charManger.skin16 == 1) AddHero(hero);	
				break;
			case 17:
				if (charManger.skin17 == 1) AddHero(hero);	
				break;
			case 18:
				if (charManger.skin18 == 1) AddHero(hero);	
				break;
			case 19:
				if (charManger.skin19 == 1) AddHero(hero);	
				break;
			case 20:
				if (charManger.skin20 == 1) AddHero(hero);	
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
		string name=myHeroes [nextBack].name.Replace("(Clone)","");

		data.Add (name);
		changeManger.ChangeMethod (headers, data);

	}
}
