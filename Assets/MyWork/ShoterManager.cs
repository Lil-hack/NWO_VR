using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoterManager : MonoBehaviour {

    public bool Shoot;
    public PlayerInfo playerInfo;
    public GameObject reticle;
    public LayerMask mask;
	public ManagerGunRoom manager;
	public Animator anim;
	//public Transform cuberespawn;
    private bool _make=true;

	public Vector3 outFire;
	public Quaternion inFire;

    private void Update()
    {
		
	
        if (Shoot == true && _make)
        {
			StartCoroutine(Fire());
            StartCoroutine(TimerFire());
        }
        
    }

    IEnumerator TimerFire()
    {

        _make = false;
        yield return new WaitForSeconds(playerInfo.timeFire);
        _make = true;

       
		//ShootBall ();
    }

	IEnumerator Fire()
	{


		yield return new WaitForSeconds(0.25f);
		ShootBall ();
		anim.SetTrigger ("fireball");


		//ShootBall ();
	}
	private void ShootBall()
	{
		RaycastHit hit;
		if (Physics.Raycast(reticle.transform.position, reticle.transform.forward, out hit, playerInfo.rangeFire, mask))
		{outFire = reticle.transform.position;
			inFire = reticle.transform.rotation;
			manager.FireAll (hit.collider.name,1,outFire,inFire);
			Debug.Log(hit.collider.name +"это куда попал");

		}

	}
}
