using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSkinManager : MonoBehaviour {
	public GameObject skin;
	public Animator animator;
	public bool metkaMove=true;
	// Use this for initialization
	void Start () {
		
	}
	
	public void CreateSkin(GameObject skinObject)
	{
		skin = Instantiate(skinObject) as GameObject;
		skin.transform.SetParent (this.transform);
		skin.transform.localPosition = Vector3.zero;


	}
	public void MoveAnimation()
	{ 
		if(skin != null)
		{
			if ( animator!=null) {
				animator.SetBool ("Run",true);



				if (metkaMove == false) {
					StopAllCoroutines ();}
				
			StartCoroutine (StartMove ());

			
		
		
		}
			else{
				animator=skin.GetComponent<Animator>();
			}
	}
	


	}

	private IEnumerator StartMove()
	{metkaMove = false;

		yield return new WaitForSeconds(0.2f);

		metkaMove = true;
		animator.SetBool ("Run",false);
	



	}
	public void DieAnimation()
	{ 
		if(skin != null)
		{
			if ( animator!=null) {
				animator.SetTrigger ("Die");




			}
			else{
				animator=skin.GetComponent<Animator>();
			}
		}



	}
	public void DamageAnimation()
	{ 
		if(skin != null)
		{
			if ( animator!=null) {
				animator.SetTrigger ("Take Damage");




			}
			else{
				animator=skin.GetComponent<Animator>();
			}
		}



	}


}
