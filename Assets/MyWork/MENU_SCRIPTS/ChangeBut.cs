using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBut : MonoBehaviour {

	public List <string> headers ;
	public List <string> data ;
	public ChangeManager changeManger;

	void Start () {
		changeManger=GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<ChangeManager> ();
	}
	public void ChangeButton()
	{
		changeManger.ChangeMethod (headers, data);
	}
}
