using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{

    private string VERSION = "v0.0.1";


    public string playerPrefab = "PlayerShip";
	public Text connectionText;

	public GameObject serverWindow;
	public InputField username;
	public InputField roomName;
	public InputField roomList;

    // Use this for initialization
    void Start()
    {
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
