
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;


public class ManagerStartRoom : MonoBehaviour {
	public bool botMode=false;
	public CardboardHead headBot;
	public bool succesConnect=false;
	public float smooth = 0.25f;
	public float smooth2 = 0.25f;
	public float speed = 0.25f;
	public string skin="0";
	public GameObject target;
	public bool joinRoom=false;
	public GameObject PlayerPrefab;
	private Connection pioconnection;
	private List<Message> msgList = new List<Message>(); //  Messsage queue implementation
	private bool joinedroom = false;
	public List<RoomEditor> roomEditorList;
	public GameObject heroesAll;
	public List<GameObject> skinList;
	private Vector2 scrollPosition;
	public List<GameObject> playerInGame;
	private string inputField = "";
	private string infomsg = "";
	private bool _make = true;
	private bool _make2 = true;
	private bool _make3 = true;
	private bool _makeBot = true;
	private string userid;
	public float sendtime = 0.05f;
	public float sendtimeForRotate = 0.05f;

	public float roty=0;
		public float rotx=0;
	public int PosRoomEditor=109;
	public int startPosRoomEditor=1469;
	public float randomSpawnX=5;
	public float randomSpawnZ=5;

	void Start() {
		if (botMode == false) {
			//userid = PlayerPrefs.GetString ("Name");
			userid=System.Guid.NewGuid().ToString();
			headBot.trackRotation = true;
		} else {

			userid=System.Guid.NewGuid().ToString();
			headBot.trackRotation = false;
		}
		skinList = heroesAll.GetComponent<Heroes> ().heroes;

		Application.runInBackground = true;

		// Create a random userid 
		target.transform.position = new Vector3(Random.Range(-randomSpawnX, randomSpawnX), 0, Random.Range(-randomSpawnZ, randomSpawnZ));


		Debug.Log("Starting");

		PlayerIO.Authenticate(
			//"gun2-gjvpvovlreqgrahvipdya",
			"mainroom-b3lmufjr30aw6ds5r6yjq",            //Your game id
			"public",                               //Your connection id
			new Dictionary<string, string> {        //Authentication arguments
				{ "userId", userid },
			},
			null,                                   //PlayerInsight segments
			delegate (Client client) {
				Debug.Log("Successfully connected to Player.IO");
				succesConnect=true;
				infomsg = "Successfully connected to Player.IO";

				target.transform.Find("NameTag").GetComponent<TextMesh>().text = userid;
				target.transform.name = userid;

				Debug.Log("Create ServerEndpoint");
				// Comment out the line below to use the live servers instead of your development server
				//	client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost", 8184);
				client.Multiplayer.ListRooms("MainRoom", null, 70, 0,(RoomInfo[] rooms) => {
					if(rooms.Length!=0)
					{
						int countRoom=0;
//						var myroomList=rooms.ToList();
//						myroomList.Reverse();
						foreach (var room in rooms)
					{
						Debug.Log("Room: " + room.Id + ". Online: " + room.OnlineUsers+ ". RoomData: " +room.RoomData);
							if(room.OnlineUsers>=8)
							{
								countRoom++;




							}
							else{
								
								client.Multiplayer.JoinRoom(
									room.Id,                    //Room id. If set to null a random roomid is used
								             //The room type started on the server
									new Dictionary<string, string> {        //Authentication arguments
										{ "skin", skin },
										{ "posx", target.transform.position.x.ToString() },
										{ "posy", target.transform.position.y.ToString() },
										{ "posz", target.transform.position.z.ToString() },
										{ "roty", target.transform.rotation.eulerAngles.y.ToString() },
									},                              //Should the room be visible in the lobby?

									delegate (Connection connection) {
										Debug.Log("Joined Room.");
										joinRoom=true;
										// We successfully joined a room so set up the message handler
										pioconnection = connection;
										pioconnection.OnMessage += handlemessage;

									//	pioconnection.Send("Skin", roty);
										joinedroom = true;
									},
									delegate (PlayerIOError error) {
										Debug.Log("Error Joining Room: " + error.ToString());

									}
								);
								break;
							}
						
						}
						Debug.Log(countRoom+"  asdas   "+rooms.Length);
						if(countRoom==rooms.Length)
						{
							client.Multiplayer.CreateJoinRoom(
								null,                    //Room id. If set to null a random roomid is used
								"MainRoom",                   //The room type started on the server
								true,                               //Should the room be visible in the lobby?
								null,
								new Dictionary<string, string> {        //Authentication arguments
									{ "skin", skin },
									{ "posx", target.transform.position.x.ToString() },
									{ "posy", target.transform.position.y.ToString() },
									{ "posz", target.transform.position.z.ToString() },
									{ "roty", target.transform.rotation.eulerAngles.y.ToString() },
								},
								delegate (Connection connection) {
									Debug.Log("Joined Room.");
									joinRoom=true;
									// We successfully joined a room so set up the message handler
									pioconnection = connection;
									pioconnection.OnMessage += handlemessage;

								//	pioconnection.Send("Skin", roty);
									joinedroom = true;
								},
								delegate (PlayerIOError error) {
									Debug.Log("Error Joining Room: " + error.ToString());

								}
							);
						}


					}
					else{

						client.Multiplayer.CreateJoinRoom(
												null,                    //Room id. If set to null a random roomid is used
												"MainRoom",                   //The room type started on the server
												true,                               //Should the room be visible in the lobby?
							null,
							new Dictionary<string, string> {        //Authentication arguments
								{ "skin", skin },
								{ "posx", target.transform.position.x.ToString() },
								{ "posy", target.transform.position.y.ToString() },
								{ "posz", target.transform.position.z.ToString() },
								{ "roty", target.transform.rotation.eulerAngles.y.ToString() },
							},
												delegate (Connection connection) {
													Debug.Log("Joined Room.");
								joinRoom=true;
													// We successfully joined a room so set up the message handler
													pioconnection = connection;
													pioconnection.OnMessage += handlemessage;
							
												
													joinedroom = true;
												},
												delegate (PlayerIOError error) {
													Debug.Log("Error Joining Room: " + error.ToString());
												
												}
											);

				
				
				}
				},
					null
				);


//				Debug.Log("CreateJoinRoom");
//				//Create or join the room 
//				client.Multiplayer.CreateJoinRoom(
//					"MainRoom",                    //Room id. If set to null a random roomid is used
//					"MainRoom",                   //The room type started on the server
//					true,                               //Should the room be visible in the lobby?
//					null,
//					null,
//					delegate (Connection connection) {
//						Debug.Log("Joined Room.");
//						infomsg = "Joined Room.";
//						// We successfully joined a room so set up the message handler
//						pioconnection = connection;
//						pioconnection.OnMessage += handlemessage;
//
//						pioconnection.Send("Skin", roty);
//						joinedroom = true;
//					},
//					delegate (PlayerIOError error) {
//						Debug.Log("Error Joining Room: " + error.ToString());
//						infomsg = error.ToString();
//					}
//				);


			},
			delegate (PlayerIOError error) {
				Debug.Log("Error connecting: " + error.ToString());
				infomsg = error.ToString();
			}
		);
	//	pioconnection.Send("Skin", 2f);
		//target.GetComponent<AnimationSkinManager> ().CreateSkin (skinList[PlayerPrefs.GetInt("Skin")]);
	}
	void OnApplicationQuit()
	{if (joinedroom) {
			pioconnection.Disconnect ();
			Debug.Log ("Application ending after " + Time.time + " seconds");
		}
	}
	void handlemessage(object sender, Message m) {
		msgList.Add(m);
	}

	void FixedUpdate() {
		// process message queue
		foreach (Message m in msgList) {
			switch (m.Type) {
			case "PlayerJoined":
				if (GameObject.Find (m.GetString (0))) {
					Destroy (GameObject.Find (m.GetString (0)));
				}
			
				GameObject newplayer = GameObject.Instantiate (PlayerPrefab) as GameObject;
				newplayer.transform.position = new Vector3 (m.GetFloat (1), m.GetFloat (2), m.GetFloat (3));
			
				Debug.Log (m.ToString());
				newplayer.name = m.GetString (0);

				newplayer.transform.Find ("NameTag").GetComponent<TextMesh> ().text = m.GetString (0);
				newplayer.GetComponent<AnimationSkinManager> ().CreateSkin (skinList [Mathf.RoundToInt(m.GetFloat(5))]);
				//pioconnection.Send("Skin",PlayerPrefs.GetInt("Skin"));
				newplayer.transform.rotation =  Quaternion.Euler(new Vector3(0, m.GetFloat(4), 0));
				playerInGame.Add (newplayer);
				break;
			case "PlayerLeft":
				Destroy (playerInGame.Find (obj => obj.name == m.GetString (0)));
				playerInGame.RemoveAt(playerInGame.FindIndex (obj => obj.name == m.GetString (0)));

				Debug.Log ("PlayerLeft : "+m.GetString (0));


				break;
			case "Move":
				if (userid != m.GetString (0)) { 
					GameObject upplayer = playerInGame.Find (obj => obj.name == m.GetString (0));
					//Debug.Log (m);
					var control=upplayer.GetComponent<PlayerMoveControll>();
					control.newPos=new Vector3 (m.GetFloat (1), m.GetFloat (2), m.GetFloat (3));
					control.start = true;
					control.roty = m.GetFloat (4);
					upplayer.GetComponent<AnimationSkinManager> ().MoveAnimation ();
				}
				break;
			
			case "Rotation":

				if (userid != m.GetString(0))
				{
					GameObject upplayer2 = GameObject.Find(m.GetString(0));
					// remove the object when it's picked up
					var control=upplayer2.GetComponent<PlayerMoveControll>();
					control.roty = m.GetFloat (1);
				}
				break;
			
			}
		
		}
			
		msgList.Clear();


	}

	public void DisconnectAfterCreateRoom()
	{
		pioconnection.Disconnect();
	}

	private void Update()
	{

		if (botMode == true) {
			target.transform.Translate (Vector3.forward * Time.deltaTime * speed);
			if (_make2)
				StartCoroutine (SendPosition ());

			if (_makeBot)
				StartCoroutine (RandomRotateBot ());
		} else {
			if (joinRoom == true) {

				if (succesConnect) {


					if (Input.GetKey (KeyCode.W)) {
						target.transform.Translate (Vector3.forward * Time.deltaTime * speed);
						if (_make2)
							StartCoroutine (SendPosition ());

					} else {
						if (_make && roty != target.transform.rotation.eulerAngles.y)
							StartCoroutine (SendRotate ());
					}
					//				if (_make3) {
					//					StartCoroutine (UpdateRooms ());
					//		
					//				}


				} else {





				}



			} else {



			}
		}

	}




	private IEnumerator RandomRotateBot()
	{
		_makeBot = false;
		yield return new WaitForSeconds(3f);
		_makeBot = true;
		target.transform.Rotate (new Vector3(0, Random.Range (-1800.0f, 1800.0f), 0));


	}
	private IEnumerator SendPosition()
	{
		_make2 = false;
		yield return new WaitForSeconds(sendtime);
		_make2 = true;
		pioconnection.Send("Move", target.transform.position.x,target.transform.position.y, target.transform.position.z,target.transform.rotation.eulerAngles.y);



	}
	private IEnumerator SendRotate()
	{
		_make = false;
		yield return new WaitForSeconds(sendtime);
		_make = true;
		roty = target.transform.rotation.eulerAngles.y;

		pioconnection.Send("Rotation", roty);


	}

//	private IEnumerator UpdateRooms()
//	{
//		_make3 = false;
//		yield return new WaitForSeconds(15);
//		_make3 = true;
//		foreach(var roomEd in roomEditorList)
//		{
//			GetRoomsInfo (roomEd);
//
//		}
//
//
//
//	}
//

//	public void GetRoomsInfo(RoomEditor roomEditor){
//
//		PlayerIO.Authenticate(
//			//"gun2-gjvpvovlreqgrahvipdya",
//			roomEditor.gameId,            //Your game id
//			"public",                               //Your connection id
//			new Dictionary<string, string> {        //Authentication arguments
//				{ "userId", userid },
//			},
//			null,                                   //PlayerInsight segments
//			delegate (Client client) {
//
//
//				client.Multiplayer.ListRooms(roomEditor.roomType, null, 45, 0, delegate (RoomInfo[] infos) 
//					{
//						roomEditor.DeleteAllRooms();
//						int i=0;
//						foreach (var room in infos) {
//							roomEditor.CreateRoom(room.Id,room.OnlineUsers,startPosRoomEditor-i*PosRoomEditor);
//							i++;
//						}
//
//					},
//					delegate (PlayerIOError e) 
//					{
//						Debug.Log("Unable to list rooms" + e);
//					});
//
//			},
//			delegate (PlayerIOError error) {
//				Debug.Log("Error connecting: " + error.ToString());
//				infomsg = error.ToString();
//			}
//		);
//
//	}


}
