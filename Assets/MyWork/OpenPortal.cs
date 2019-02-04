using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPortal : MonoBehaviour {
	public GameObject protal;

	public void OpenPort ()
	{

		StartCoroutine (Open ());
	}

	private IEnumerator Open()
	{

		yield return new WaitForSeconds(1f);

		protal.SetActive (true);




	}
}
