﻿using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

    private string VERSION = "v0.0.1";

    public string roomName = "default";
    public string playerPrefab = "PlayerShip";

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(VERSION);
    }

    void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab, new Vector3(Random.Range (-10, 10), 0, 0), Quaternion.identity, 0);
    }

}
