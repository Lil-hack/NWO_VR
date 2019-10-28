using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlideHero : MonoBehaviour, IBeginDragHandler,IDragHandler {
	public CharacterManager charMan;
	public HerouesShop shopHero;
	public void OnBeginDrag(PointerEventData eventData)
	{
		
	}
	public void OnDrag(PointerEventData eventData)
	{
		if ((Mathf.Abs (eventData.delta.x)) > (Mathf.Abs (eventData.delta.y))) {
			if (eventData.delta.x > 0) {
				if (charMan.hero.activeInHierarchy) {

					charMan.hero.transform.localEulerAngles = new Vector3 (0, charMan.hero.transform.localEulerAngles.y - eventData.delta.x, 0);
				}
				if(shopHero.myHeroes.Count>0)
				if (shopHero.myHeroes[shopHero.nextBack].activeInHierarchy) {

					shopHero.myHeroes[shopHero.nextBack].transform.localEulerAngles = new Vector3 (0, shopHero.myHeroes[shopHero.nextBack].transform.localEulerAngles.y - eventData.delta.x, 0);
				}


			} else {
				if (charMan.hero.activeInHierarchy) {
				charMan.hero.transform.localEulerAngles = new Vector3 (0, charMan.hero.transform.localEulerAngles.y - eventData.delta.x, 0);
				}
				if(shopHero.myHeroes.Count>0)
				if (shopHero.myHeroes[shopHero.nextBack].activeInHierarchy) {

					shopHero.myHeroes[shopHero.nextBack].transform.localEulerAngles = new Vector3 (0, shopHero.myHeroes[shopHero.nextBack].transform.localEulerAngles.y - eventData.delta.x, 0);
				}

			}
		}

	}
	// Use this for initialization
	void Start () {
		charMan=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<CharacterManager> ();
	}
	

}
