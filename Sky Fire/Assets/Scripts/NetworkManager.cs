using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour
{

    private string VERSION = "v0.0.1";


    public string playerPrefab = "PlayerShip";
	public Text connectionText;

	public GameObject serverWindow;
	public InputField username;
	public InputField roomName;
	public InputField roomList;
	[SerializeField] InputField messageWindow;

	GameObject player;
	Queue<string> messages;
	const int messageCount = 6;
	PhotonView photonView;

    // Use this for initialization
    void Start()
    {
		photonView = GetComponent<PhotonView> ();
		messages = new Queue<string> (messageCount);


        PhotonNetwork.ConnectUsingSettings(VERSION);
		StartCoroutine ("UpdateConnectionString");
    }

	IEnumerator UpdateConnectionString () {

		while (true) {
		
			connectionText.text = PhotonNetwork.connectionStateDetailed.ToString ();
			yield return null;
		}
	
	
	}

    void OnJoinedLobby()
    {

		serverWindow.SetActive (true);

    }

	void OnReceivedRoomListUpdate()
	{
		roomList.text = "";
		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();
		foreach(RoomInfo room in rooms)
			roomList.text += room.name + "\n";

	}

	public void JoinRoom () {
	
		PhotonNetwork.player.name = username.text;
		RoomOptions roomOptions = new RoomOptions() { IsVisible = true, MaxPlayers = 4 };
		PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
	
	}

    void OnJoinedRoom()

    {
		serverWindow.SetActive (false);
		StopCoroutine ("UpdateConnectionString");
		connectionText.text = "";
		PhotonNetwork.Instantiate(playerPrefab, new Vector3(0, 0, -50.0f), Quaternion.identity, 0);
		//playerPrefab.GetComponent<PlayerNetworkMover> ().SendNetworkMessage += AddMessage;
		AddMessage ("Spawned player: " + PhotonNetwork.player.name);

    }

	void AddMessage(string message)
	{
		photonView.RPC ("AddMessage_RPC", PhotonTargets.All, message);
	}

	[PunRPC]
	void AddMessage_RPC(string message)
	{
		messages.Enqueue (message);
		if(messages.Count > messageCount)
			messages.Dequeue();

		messageWindow.text = "";
		foreach(string m in messages)
			messageWindow.text += m + "\n";
	}

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        Destroy(GameObject.Find("Me"));
    //        PhotonNetwork.Instantiate(playerPrefab, new Vector3(Random.Range(-10, 10), 0, 0), Quaternion.identity, 0);

    //    }
    //}


}
