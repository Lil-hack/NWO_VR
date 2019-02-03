using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;


public class ManagerGunRoom : MonoBehaviour {
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
	public Slider hpslider;
	private string inputField = "";
	private string infomsg = "";
	private bool _make = true;
	private bool _make2 = true;
	private bool _make3 = true;
	private bool _makeBot = true;
	private string userid;
	public float sendtime = 0.05f;
	public float sendtimeForRotate = 0.05f;
	public int win=0;
	public int lose=0;
	public Text RedText;
	public Text BlueText;
	public float roty=0;
	public float rotx=0;
	public int PosRoomEditor=109;
	public int startPosRoomEditor=1469;
	public float randomSpawnX=5;
	public float randomSpawnZ=5;
	public int team = 0;
	public GameObject ZoneRedSpawn;
	public GameObject ZoneBlueSpawn;
	public GameObject ZoneStartSpawn;
	public bool statusGame = true;
	public GameObject fireball;
	public GameObject portal;
	public GameObject round;
	public GameObject winGame;
	public GameObject loseGame;
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
	//	target.transform.position = new Vector3(Random.Range(-randomSpawnX, randomSpawnX), 0, Random.Range(-randomSpawnZ, randomSpawnZ));

		var rezone = ZoneStartSpawn.transform.localScale;
		target.transform.position = ZoneStartSpawn.transform.position+new Vector3(Random.Range(-rezone.z/2, rezone.z/2), 
			Random.Range(-rezone.y/2, rezone.y/2)
			, Random.Range(-rezone.x/2, rezone.x/2));
		
		Debug.Log("Starting");

		PlayerIO.Authenticate(
			//"gun2-gjvpvovlreqgrahvipdya",
			"gunonline-n4yxv5ngekqec1zlcijoga",            //Your game id
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
				client.Multiplayer.ListRooms("GunTypeOne", null, 70, 0,(RoomInfo[] rooms) => {
					if(rooms.Length!=0)
					{
						int countRoom=0;
						//						var myroomList=rooms.ToList();
						//						myroomList.Reverse();
						foreach (var room in rooms)
						{
							Debug.Log("Room: " + room.Id + ". Online: " + room.OnlineUsers+ ". RoomData: " +room.RoomData);
							if(room.OnlineUsers>=2)
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
								"GunTypeOne",                   //The room type started on the server
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
							"GunTypeOne",                   //The room type started on the server
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
	{if (succesConnect) {
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
				portal.SetActive (true);
				Debug.Log ("PlayerLeft : "+m.GetString (0));


				break;
			case "Move":
				if (userid != m.GetString (0)) { 
					if (playerInGame.Find (obj => obj.name == m.GetString (0)) != null) {
						GameObject upplayer = playerInGame.Find (obj => obj.name == m.GetString (0));
						//Debug.Log (m);
						var control = upplayer.GetComponent<PlayerMoveControll> ();
						control.newPos = new Vector3 (m.GetFloat (1), m.GetFloat (2), m.GetFloat (3));
						control.start = true;
						control.roty = m.GetFloat (4);
						upplayer.GetComponent<AnimationSkinManager> ().MoveAnimation ();
					}
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
			case "Fire":

	
				if (playerInGame.Find (obj => obj.name == m.GetString (1))!=null)
				{
					var pl = playerInGame.Find (obj => obj.name == m.GetString (1));
					var pl2=pl.GetComponent<PlayerInfo> ();
						pl2.hp = m.GetInt(2);
					if (pl.name == playerInGame [0].name) {
						hpslider.value = m.GetInt (2);
					}
					pl2.BloodUp ();
					pl.GetComponent<AnimationSkinManager> ().DamageAnimation();

					Debug.Log ("bloood");

				}

				break;
			case "FireBall":
				
				Vector3 posBall = new Vector3 (m.GetFloat (0),m.GetFloat (1),m.GetFloat (2));

				var rotate=Quaternion.Euler (new Vector3 (m.GetFloat (3),m.GetFloat (4),m.GetFloat (5)));
				 Instantiate (fireball, posBall, rotate);



				break;
			case "Die":
				statusGame = false;
				if (playerInGame.Find (obj => obj.name == m.GetString(1))!=null)
				{
					playerInGame.Find (obj => obj.name == m.GetString (1)).GetComponent<AnimationSkinManager>
					().DieAnimation ();
				}
				Debug.Log ("Player die: "+m.GetString(1));
				Debug.Log ("Player kill: "+m.GetString(0));
				//GameObject upplayer4 = GameObject.Find(m.GetString(0));

				break;
			case "Round":
				round.SetActive (true);
				StartCoroutine (CloseObject (round));
				Debug.Log ("My team: " + m.GetInt (0));
				statusGame = true;
				team = m.GetInt (0);

				if (team == 1) {
					var rezone = ZoneRedSpawn.transform.localScale;
					target.transform.position = ZoneRedSpawn.transform.position+new Vector3(Random.Range(-rezone.z/2, rezone.z/2), 
						Random.Range(-rezone.y/2, rezone.y/2)
						, Random.Range(-rezone.x/2, rezone.x/2));
				}
				if (team == 2) {
					var rezone = ZoneBlueSpawn.transform.localScale;
					target.transform.position = ZoneBlueSpawn.transform.position+new Vector3(Random.Range(-rezone.z/2, rezone.z/2), 
						Random.Range(-rezone.y/2, rezone.y/2)
						, Random.Range(-rezone.x/2, rezone.x/2));
				
				}
				pioconnection.Send("Move", target.transform.position.x,target.transform.position.y, target.transform.position.z,target.transform.rotation.eulerAngles.y);


				//GameObject upplayer4 = GameObject.Find(m.GetString(0));

				break;
			case "Restart":

				foreach (var pl in playerInGame) {
				
					pl.GetComponent<PlayerInfo> ().hp = m.GetInt (0);

				}
				hpslider.value = m.GetInt (0);
				if (team == 1) {
					RedText.text = m.GetInt (1).ToString();
					BlueText.text = m.GetInt (2).ToString();
				}
				if (team == 2) {
					RedText.text = m.GetInt (2).ToString();
					BlueText.text = m.GetInt (1).ToString();
				}
				//GameObject upplayer4 = GameObject.Find(m.GetString(0));

				break;
			case "EndGame":
				portal.SetActive (true);
				if (m.GetInt (0) > m.GetInt (1)) {
				
					winGame.SetActive (true);
					StartCoroutine (CloseObject (winGame));
				
				} else {
				
					loseGame.SetActive (true);
					StartCoroutine (CloseObject (loseGame));
				}
				Debug.Log ("Player lose: "+m.GetInt(1));
				Debug.Log ("Player win: "+m.GetInt(0));
				//GameObject upplayer4 = GameObject.Find(m.GetString(0));

				break;

			}

		}

		msgList.Clear();


	}

	public void FireAll(string playerWhoDamage,int type,Vector3 outPos, Quaternion inRot) {
		

		var inrotate=inRot.eulerAngles;
		pioconnection.Send ("FireBall",outPos.x,outPos.y,outPos.z, inrotate.x,inrotate.y,inrotate.z);
	//	GameObject fire=(GameObject) Instantiate (fireball, outPos, inRot);
	//	fire.transform.Translate (Vector3.forward * Time.deltaTime * speed);

	//	gunControl.GunFire ();
	}
	public void Damage(string playerWhoDamage)
	{
		if (playerInGame.Find (obj => obj.name == playerWhoDamage)!=null)
		{
			
		
				pioconnection.Send ("Fire", playerWhoDamage, 1);

		

		}

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
	private IEnumerator CloseObject(GameObject closeit)
	{

		yield return new WaitForSeconds(1f);
		closeit.SetActive (false);


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
