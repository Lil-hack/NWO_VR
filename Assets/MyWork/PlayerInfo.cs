using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    public int damage = 10;
    public int hp = 100;
    public float timeFire = 0.1f;
    public float rangeFire = 200;
    public bool fire = false;
	public GameObject blood;
	public Slider slid; 
	public void BloodUp()
	{
		blood.SetActive (true);

		StartCoroutine (BluudStoop ());

	}
	IEnumerator BluudStoop()
	{


		yield return new WaitForSeconds(0.5f);
		blood.SetActive (false);

		//ShootBall ();
	}

	public void GetDamage(int hpFrom)
	{
		hp = hpFrom;
		slid.value = hp;

		

	}
}
