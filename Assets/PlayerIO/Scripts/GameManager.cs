//-----------------------------------------------------------------------------------------------------------------
// 
//	Mushroom Example
//	Created by : Luis Filipe (filipe@seines.pt)
//	Dec 2010
//
//	Source code in this example is in the public domain.
//  The naruto character model in this demo is copyrighted by Ben Mathis.
//  See Assets/Models/naruto.txt for more details
//
//-----------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;

public class ChatEntry {
	public string text = "";
	public bool mine = true;
}

public class GameManager : MonoBehaviour {
    public float smooth = 0.25f;
    public float smooth2 = 0.25f;
    public float speed = 0.25f;
    public GameObject target;
	public GameObject PlayerPrefab;
	public GameObject ToadPrefab;
	private Connection pioconnection;
	private List<Message> msgList = new List<Message>(); //  Messsage queue implementation
	private bool joinedroom = false;
    public GameObject CameraVR;
    public USpeaker Speaker;
    // UI stuff
    private Vector2 scrollPosition;
	private ArrayList entries = new ArrayList();
	private string inputField = "";
	private Rect window = new Rect(10, 10, 300, 150);
	private int toadspicked = 0;
	private string infomsg = "";
    private bool _make = true;
    private bool _make2 = true;
    private bool _make3 = true;
    private string userid;
    public float sendtime = 0.15f;
    private float roty;
    public USpeakJitterTestSender uspeakmain;
    void Start() {
		Application.runInBackground = true;
        CameraVR.SetActive(true);
        // Create a random userid 
        System.Random random = new System.Random();
		 userid = "Guest" + random.Next(0, 10000);

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
				infomsg = "Successfully connected to Player.IO";

				target.transform.Find("NameTag").GetComponent<TextMesh>().text = userid;
				target.transform.name = userid;


				Debug.Log("Create ServerEndpoint");
				// Comment out the line below to use the live servers instead of your development server
			//	client.Multiplayer.DevelopmentServer = new ServerEnd88point("localhost", 8184);

				Debug.Log("CreateJoinRoom");
				//Create or join the room 

				client.Multiplayer.CreateJoinRoom(
				//	"123"+userid,
					PlayerPrefs.GetString("RoomName")	,                    //Room id. If set to null a random roomid is used
					"GunTypeOne",                   //The room type started on the server
					true,                               //Should the room be visible in the lobby?
					null,
					null,
					delegate (Connection connection) {
						Debug.Log("Joined Room.");
						infomsg = "Joined Room.";
						// We successfully joined a room so set up the message handler
						pioconnection = connection;
						pioconnection.OnMessage += handlemessage;
						joinedroom = true;
					},
					delegate (PlayerIOError error) {
						Debug.Log("Error Joining Room: " + error.ToString());
						infomsg = error.ToString();
					}
				);
			},
			delegate (PlayerIOError error) {
				Debug.Log("Error connecting: " + error.ToString());
				infomsg = error.ToString();
			}
		);

	}

	void handlemessage(object sender, Message m) {
		msgList.Add(m);
	}

	void FixedUpdate() {
		// process message queue
		foreach (Message m in msgList) {
            switch (m.Type) {
                case "PlayerJoined":
                    GameObject newplayer = GameObject.Instantiate(PlayerPrefab) as GameObject;
				newplayer.transform.position = new Vector3(m.GetFloat(1), m.GetFloat(2), m.GetFloat(3));
                    newplayer.name = m.GetString(0);
                    newplayer.transform.Find("NameTag").GetComponent<TextMesh>().text = m.GetString(0);
                    break;
                case "Move":
                    if (userid != m.GetString(0)) { 
                    GameObject upplayer = GameObject.Find(m.GetString(0));
                  
					upplayer.transform.position = Vector3.Lerp(upplayer.transform.position, new Vector3(m.GetFloat(1), m.GetFloat(2), m.GetFloat(3)), smooth);
            }
                     break;
			case "Fire":

					

				if (GameObject.Find(m.GetString(1)).GetComponent<PlayerInfo>()!=null)
				{
					GameObject.Find(m.GetString(1)).GetComponent<PlayerInfo>().hp -= m.GetInt(2);

				}

				break;

                case "Rotation":

                    if (userid != m.GetString(0))
                    {
                        GameObject upplayer2 = GameObject.Find(m.GetString(0));
                        // remove the object when it's picked up
                        upplayer2.transform.rotation = Quaternion.Lerp(upplayer2.transform.rotation, Quaternion.Euler(new Vector3(0, m.GetFloat(1), 0)), smooth2);
                        //     upplayer2.transform.rotation = Quaternion.Euler(new Vector3(0, m.GetFloat(1), 0));
                    }
                    break;
                case "Speak":

                      GameObject upplayer3 = GameObject.Find(m.GetString(0));
                        // remove the object when it66's picked up56
                        Speaker.ReceiveAudio(m.GetByteArray(1));
                        //  upplayer3.transform.rotation = Quaternion.Lerp(upplayer2.transform.rotation, Quaternion.Euler(new Vector3(0, m.GetFloat(1), 0)), smooth2);
                        //     upplayer2.transform.rotation = Quaternion.Euler(new Vector3(0, m.GetFloat(1), 0));
                    
                    break;
			case "Die":

				GameObject upplayer4 = GameObject.Find(m.GetString(0));

				break;
                 
            
				case "PlayerLeft":
					// remove characters from the scene when they leave
					GameObject playerd = GameObject.Find(m.GetString(0));
					Destroy(playerd);
					break;

			}
		}

		// clear message queue after it's been processed
		msgList.Clear();
	}



    private void Update()
	{
		

        if (Input.GetKey(KeyCode.W))
		{
			
            target.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (_make2)
                StartCoroutine(SendPosition());
	

        }
     
       // pioconnection.Send("Speak", uspeakmain.data2);
       // if (Input.GetKey(KeyCode.V))
      //  {
        //    if (_make3)
       //         StartCoroutine(SendSpeak());
       // }
       
        if (_make && roty!= target.transform.rotation.eulerAngles.y)
        StartCoroutine(SendRotate());
    }



    private IEnumerator SendRotate()
    {
        _make = false;
        yield return new WaitForSeconds(sendtime);
        _make = true;
        roty = target.transform.rotation.eulerAngles.y;

        pioconnection.Send("Rotation", roty);


    }
    private IEnumerator SendPosition()
    {
        _make2 = false;
        yield return new WaitForSeconds(sendtime);
        _make2 = true;
		pioconnection.Send("Move", target.transform.position.x,target.transform.position.y, target.transform.position.z);



    }
	public void SendMove()
	{
		pioconnection.Send("Move", target.transform.position.x,target.transform.position.y, target.transform.position.z);

	}
    private IEnumerator SendSpeak()
    {
        _make3 = false;
        yield return new WaitForSeconds(sendtime);
        _make3 = true;
        pioconnection.Send("Speak", uspeakmain.data2);
   


    }
   

	public void FireAll(string playerWhoDamage,int damage) {
		pioconnection.Send ("Fire", playerWhoDamage, damage);
	}
	public void PlayerDie() {
		pioconnection.Send ("Die");
	}



}
