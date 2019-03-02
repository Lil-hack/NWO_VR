using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixarMineDistansAndAnimation : MonoBehaviour {

	public Animator hands;
	public GameObject reticle;
	public float rangeFire=20;
	public LayerMask mask;
	public bool metka=false;
	public string nameToad;
	public ManagerWalkRoom walk;
	void Start()
	{
	
		walk = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerWalkRoom> ();
	
	}

	public void PixarFire()
	{
		RaycastHit hit;
		if (Physics.Raycast(reticle.transform.position, reticle.transform.forward, out hit,rangeFire, mask))
		{

			StartCoroutine(GiveToadEnum());
		//	Debug.Log(hit.collider.name +"это куда попал");
			hands.SetTrigger ("pixerfire");
		
		}
	}

	void Update()
	{
		if (metka == true) {
			if (name != null) {
				PixarFire ();
			}
		
		} else {
			StopAllCoroutines ();
			hands.SetBool("relax",false);
		}

	}
	public void GiveToad(string name)
	{
		nameToad = name;

	}
	private IEnumerator GiveToadEnum()
	{

		yield return new WaitForSeconds(5f);
		walk.GiveToadServer(nameToad);
		metka = false;

	}
}
