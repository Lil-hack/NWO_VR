using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMangerScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadScene());
	}
	
	// Update is called once per frame

//	IEnumerator LoadYourAsyncScene()
//	{
//		// The Application loads the Scene in the background as the current Scene runs.
//		// This is particularly good for creating loading screens.
//		// You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
//		// a sceneBuildIndex of 1 as shown in Build Settings.
//
//		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("scene"));
//
//		// Wait until the asynchronous scene fully loads
//		while (!asyncLoad.isDone)
//		{
//			yield return null;
//		}
//	}
	IEnumerator LoadScene()
	{
		yield return null;

		//Begin to load the Scene you specify
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync (PlayerPrefs.GetString("scene"));
		//Don't let the Scene activate until you allow it to
		asyncOperation.allowSceneActivation = false;
		Debug.Log ("Pro :" + asyncOperation.progress);
		//When the load is still in progress, output the Text and progress bar
		while (!asyncOperation.isDone) {
			//Output the current progress
			Debug.Log( "Loading progress: " + (asyncOperation.progress * 100) + "%");

//			// Check if the load has finished
//			if (asyncOperation.progress >= 0.9f) {
//				//Change the Text to show the Scene is ready
//
//				//Wait to you press the space key to activate the Scene
//				if (Input.GetKeyDown (KeyCode.Space))
					//Activate the Scene
					asyncOperation.allowSceneActivation = true;
//			}

			yield return null;
		}
	}
}
