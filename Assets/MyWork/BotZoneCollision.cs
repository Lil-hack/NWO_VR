using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotZoneCollision : MonoBehaviour {

	public GameObject ZoneStartSpawn;
	
	void OnTriggerExit (Collider other)
	{


		if (other.gameObject.name == "ZonePlayer") {
		

			var rezone = ZoneStartSpawn.transform.localScale;
			this.gameObject.transform.position = ZoneStartSpawn.transform.position+new Vector3(Random.Range(-rezone.z/2, rezone.z/2), 
				Random.Range(-rezone.y/2, rezone.y/2)
				, Random.Range(-rezone.x/2, rezone.x/2));
			
		}
	}
}
