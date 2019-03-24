using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInPlace : MonoBehaviour
{
	public Vector3 placePortal;


	void OnTriggerEnter (Collider other)
	{

		Debug.Log (other.gameObject.name);
		other.gameObject.transform.position = placePortal;

	}
}
