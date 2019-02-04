using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WebCam : MonoBehaviour
{




	public WebCamTexture cam_texture;
	private Color textureData1;
	private Color textureData2;
	private Color textureData3;
	private Color textureData4;
	private Color textureData5;
	//private CharacterLiving living;
	private 	bool metkaMove = true;

	private int time=5;
	public float accdelta = 0.5f;

	public bool footmetka=false;


	void Start()
	{

		if(PlayerPrefs.GetFloat("slider")==0)
		accdelta = 0.5f;
	else accdelta = PlayerPrefs.GetFloat ("slider");

	// living = player.GetComponent<CharacterLiving> ();
	try{
	WebCamDevice[] cam_devices = WebCamTexture.devices;

	cam_texture = new WebCamTexture (cam_devices [0].name, 1280, 720, 1);
	if (cam_texture != null)
		cam_texture.Play ();
	// controller = GetComponent<CharacterController> ();
	}        catch {

	}

}



	void Update()
	{
		if (cam_texture == null)
			return;
		if(	metkaMove == true)
		StartCoroutine (GetPixel ());


	}

	private IEnumerator GetPixel()
	{metkaMove = false;

		yield return new WaitForSeconds(0.1f);

		metkaMove = true;
		textureData1 = cam_texture.GetPixel (640, 360);
		textureData2 = cam_texture.GetPixel (940, 360);
		textureData3 = cam_texture.GetPixel (340, 360);
		textureData4 = cam_texture.GetPixel (640, 640);
		textureData5 = cam_texture.GetPixel (640, 240);



		if ((compareColor (textureData1, Color.black) == true) && (compareColor (textureData2, Color.black) == true)
			&& (compareColor (textureData3, Color.black) == true) &&
			(compareColor (textureData4, Color.black) == true) && (compareColor (textureData5, Color.black) == true)) {
			footmetka = true;

			//Vector3 forward = vrHead.TransformDirection (Vector3.forward);
			//controller.SimpleMove (forward * speed);

		} else {
			footmetka = false;

		}




	}
	public void StopCam()
	{
		cam_texture.Stop ();
	}

	public bool compareColor(Color a, Color b)
	{

		bool result = false;
		if (Mathf.Abs(a.r - b.r) < accdelta*0.2)
		if (Mathf.Abs(a.g - b.g) < accdelta * 0.2)
		if (Mathf.Abs(a.b - b.b) < accdelta * 0.2) result = true;

		return result;
	}



}