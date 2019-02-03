using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
	public float speed = 15;
	public float lifeTime=3;
	public float colliderTime=0.2f;
	public SphereCollider sphere;
	// Use this for initialization

	void Start () {
		StartCoroutine (DestroyBall ());
		StartCoroutine (ColliderBall ());
		//Debug.Log ("направление файрбола: "+this.gameObject.transform.localRotation.eulerAngles);
		//Debug.Log ("точка файрбола: "+this.gameObject.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Translate (Vector3.forward  * Time.deltaTime * speed);
	}

	private IEnumerator DestroyBall()
	{

		yield return new WaitForSeconds(lifeTime);
		Destroy (this.gameObject);


	}
	private IEnumerator ColliderBall()
	{

		yield return new WaitForSeconds(colliderTime);
		sphere.enabled = true;


	}
}
