using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControll : MonoBehaviour {
	public Vector3 newPos;
	public bool start = false;
	public Vector3 newRot=new Vector3(0,0,0);
	public float roty;
	public float smooth=15;
	public float smooth2=15;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (start == true) {
			newRot.y = roty;
			this.gameObject.transform.position =	Vector3.Lerp 
				(this.gameObject.transform.position, newPos, smooth * Time.fixedDeltaTime);
			this.gameObject.transform.rotation = Quaternion.Lerp (this.gameObject.transform.rotation, Quaternion.Euler (newRot), smooth * Time.fixedDeltaTime);

		}
	}
}
