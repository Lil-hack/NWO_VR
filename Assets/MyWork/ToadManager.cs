using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadManager : MonoBehaviour {


public void GiveToad()
    { 
		AnimationPixerStart ();

}

	public void AnimationPixerStart()
	{ 
		var pix = GameObject.FindGameObjectWithTag ("Player").GetComponent<PixarMineDistansAndAnimation> ();
		pix.GiveToad(this.name);
		pix.metka=true;
	

	}

	public void AnimationPixerStop()
	{ 
	GameObject.FindGameObjectWithTag("Player").GetComponent<PixarMineDistansAndAnimation>().metka=false;


	}



}
