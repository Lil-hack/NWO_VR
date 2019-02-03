using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTriger : MonoBehaviour {
    public float TimeShoot = 0.3f;

    public void Shoot()
	{GameObject.FindGameObjectWithTag("Player").GetComponent<ShoterManager>().Shoot=true;
       // StartCoroutine(TimerShoot());
	//	GameObject.FindGameObjectWithTag("Player").GetComponent<ShoterManager>().Shoot=true;
    }

    IEnumerator TimerShoot()
    {

        yield return new WaitForSeconds(TimeShoot);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ShoterManager>().Shoot=true;

    }
    public void StopShoot()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ShoterManager>().Shoot = false;
    }
}
