using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerouesShop : MonoBehaviour {


	public int characterType;
	public Text crystal;
	public Text money;
	public GameObject heroes;
	public Transform trans;
	public Vector3 rangeHeroes;
	public Vector3 rangeHeroesNextBack;
	public GameObject heroListShop;
	public int layerType;
	public int countHeroes=0;
	public int nextBack=0;
	public List<GameObject> myHeroes;
	public CharacterManager charManger;
	// Use this for initialization
	public List <string> headers ;
	public List <string> data ;
	public ChangeManager changeManger;
	public MenuManager menu;
	public int crystalForBuy;
	public int moneyForBuy;
	void Start () {


		changeManger=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<ChangeManager> ();
		menu=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<MenuManager> ();


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
		CheckHeroes();
	}

	private void CheckHeroes()
	{
		countHeroes=0;
		int caseSwitch = 0;
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
		crystalForBuy=myHeroes [nextBack].GetComponent<HeroStartRotate> ().crystal;
		moneyForBuy = myHeroes [nextBack].GetComponent<HeroStartRotate> ().money;
		crystal.text = crystalForBuy.ToString();
		money.text = moneyForBuy.ToString ();
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
			crystalForBuy=myHeroes [nextBack].GetComponent<HeroStartRotate> ().crystal;
			moneyForBuy = myHeroes [nextBack].GetComponent<HeroStartRotate> ().money;
			crystal.text = crystalForBuy.ToString();
			money.text = moneyForBuy.ToString ();
			trans.localPosition += rangeHeroesNextBack;
		}
	}
	public void Back()
	{if (nextBack != 0 ) {
			nextBack--;
			crystalForBuy=myHeroes [nextBack].GetComponent<HeroStartRotate> ().crystal;
			moneyForBuy = myHeroes [nextBack].GetComponent<HeroStartRotate> ().money;
			crystal.text = crystalForBuy.ToString();
			money.text = moneyForBuy.ToString ();
			trans.localPosition -= rangeHeroesNextBack;
		}
	}
	public void Buy()
	{
		data.Clear ();
		headers.Clear ();
		string name = myHeroes [nextBack].name.Replace ("(Clone)", "");

	
		int moneyCount = PlayerPrefs.GetInt ("money");
		int crystalCount = PlayerPrefs.GetInt ("crystal");

		int disMoney = moneyCount - moneyForBuy;
		int disCrystal = crystalCount - crystalForBuy;
		if (disMoney > 0 && disCrystal > 0) {

			headers.Add ("skin" + name);
			headers.Add ("money");
			headers.Add ("crystal");
			headers.Add ("skin");

			data.Add ("true");
			data.Add (disMoney.ToString ());
			data.Add (disCrystal.ToString ());
			data.Add (name);
			menu.GoToMenu (menu.BuyReady);
			heroListShop.SetActive (false);
		
		} else {
		
			menu.GoToMenu (menu.ErrorShop);
			menu.ErrorShopText.text = "Недостаточно ресурсов!";
			heroListShop.SetActive (false);
		}
	}

		public void BuyEnd()
		{
			changeManger.ChangeMethod (headers, data);
			menu.GoToMenu (menu.StartMenu);
			Back ();

		}
			
//		changeManger.ChangeMethod (headers, data);


}
