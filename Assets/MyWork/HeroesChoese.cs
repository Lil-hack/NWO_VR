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
	// Use this for initialization
	void Start () {

		//characterType = PlayerPrefs.GetInt ("Character");

		foreach (var hero in heroes.GetComponent<Heroes>().heroes) {
		
			myHeroes.Add (Instantiate (hero,trans));
			myHeroes[countHeroes].transform.localPosition = rangeHeroes * countHeroes;
			ChangeLayers (myHeroes[countHeroes],layerType);
		
			countHeroes++;
		}
		//hero=Instantiate(	heroes.GetComponent<Heroes>().heroes[characterType],trans);
		//hero.layer = layerType;


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
			trans.position += rangeHeroesNextBack;
		}
	}
	public void Back()
	{if (nextBack != 0 ) {
			nextBack--;
			trans.position -= rangeHeroesNextBack;
		}
	}
	public void Ok()
	{PlayerPrefs.SetInt ("Skin", nextBack);

	}
}
