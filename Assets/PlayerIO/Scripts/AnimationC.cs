using UnityEngine;
using System.Collections;

// Helper functions for animation, called by GameManager

public class AnimationC : MonoBehaviour {
	
	public GameManager gm;
	public GameObject target;
	private int t;
	void stopwalk()
	{
		gameObject.GetComponent<Animation>().CrossFade("idle");
	}
	
	void stopharvest()
	{
		gameObject.GetComponent<Animation>().CrossFade("idle");
	
	}
	
	void startwalk()
	{
		gameObject.GetComponent<Animation>().CrossFade("walking");
	}
	
}
