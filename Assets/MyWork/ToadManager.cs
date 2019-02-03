using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadManager : MonoBehaviour {


public void GiveToad()
    { 
    StartCoroutine(GiveToadEnum());
}



private IEnumerator GiveToadEnum()
{
   
    yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerWalkRoom>().GiveToadServer(this.name);

}
}
