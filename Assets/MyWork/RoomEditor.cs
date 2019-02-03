using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEditor : MonoBehaviour {
	
	public GameObject pr;
	public List<GameObject> roomsList;
	public ManagerStartRoom manager;
	private RectTransform contentRect;
	public Vector3 backAndNextRect=new Vector3 (0,321,0);
	public GameObject noRooms;
	public string gameId;
	public string roomType;
	public string nameRoomToCreate;
	public string nameLoadScene;
	private bool metka=false;
	// Use this for initialization
	void Start () {
		Vector3 startRoomVector = new Vector3 (-326,0,0);
		contentRect =	this.gameObject.GetComponent<RectTransform> ();
			contentRect.anchoredPosition3D = startRoomVector;


	}

	public void CreateRoom(string roomName,int onlinePlayer,float posy){
		Vector3 roomVector = new Vector3 (0,posy,0);
		Vector3 scaleVector = new Vector3 (0.5f,0.5f,1);
		GameObject go = Instantiate(pr) as GameObject;
		RoomInfoMy myroom= go.GetComponent<RoomInfoMy> ();
		go.transform.SetParent (transform);
		go.GetComponent<RectTransform>().anchoredPosition3D = roomVector;
		myroom.playerCount = onlinePlayer;
		myroom.roomName = roomName;
		myroom.nameLoadScene = nameLoadScene;
		go.transform.localScale = scaleVector;
		go.transform.localRotation =Quaternion.Euler(Vector3.zero);
		roomsList.Add (go);
	}
	public void DeleteAllRooms(){
		foreach (var room in roomsList) {
			Destroy (room);
		}
		roomsList.Clear ();
	}
	public void Resfresh()
	{
		StartCoroutine (ResfreshRooms());
	}
	private IEnumerator ResfreshRooms()
	{

		yield return new WaitForSeconds(1.5f);


	//	manager.GetRoomsInfo (this);



	}

	public void Next()
	{
		StartCoroutine (NextRooms());
	}
	private IEnumerator NextRooms()
	{

		yield return new WaitForSeconds(1.5f);


		contentRect.anchoredPosition3D = contentRect.anchoredPosition3D + backAndNextRect;




	}
	public void Back()
	{
		StartCoroutine (BackRooms());
	}
	private IEnumerator BackRooms()
	{

		yield return new WaitForSeconds(1.5f);


		contentRect.anchoredPosition3D = contentRect.anchoredPosition3D - backAndNextRect;





	}
	public void Create()
	{
		StartCoroutine (CreateRooms());
	}
	private IEnumerator CreateRooms()
	{

		yield return new WaitForSeconds(1.5f);

		PlayerPrefs.SetString("RoomName", nameRoomToCreate+PlayerPrefs.GetString ("Name"));	
		SceneManager.LoadScene(nameLoadScene);
		GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerStartRoom> ().DisconnectAfterCreateRoom ();




	}
	public void FindRoom()
	{
		StartCoroutine (FindRooms());
	}
	private IEnumerator FindRooms()
	{

		yield return new WaitForSeconds(1.5f);

	//	PlayerPrefs.SetString("RoomName", nameRoomToCreate+PlayerPrefs.GetString ("Name"));	
	//	SceneManager.LoadScene(nameLoadScene);
		ManagerStartRoom manager =	GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ManagerStartRoom> ();
	//	manager.GetRoomsInfo (this);


		//manager.DisconnectAfterCreateRoom ();





	}


	void Update(){

		if (roomsList.Count!=0) {
			noRooms.SetActive (false);

		} else {
			noRooms.SetActive (true);

		}

	}


}
