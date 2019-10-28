using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

	public Button but;

	public void ButtonStart ()
	{

		StartCoroutine (Open ());
	}

	private IEnumerator Open()
	{

		yield return new WaitForSeconds(1f);
		but.onClick.Invoke();
	




	}
}
