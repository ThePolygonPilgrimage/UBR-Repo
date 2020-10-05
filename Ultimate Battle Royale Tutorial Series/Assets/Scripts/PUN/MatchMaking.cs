using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MatchMaking : MonoBehaviourPunCallbacks
{
    //Setup our instance to this script
    public static MatchMaking instance;

    public string roomNameCreated;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMatch()
    {
        PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.LoadLevel(3);
    }

    public void CreateRoomOnClick()
    {
        string roomName = "room_ " + PhotonNetwork.Time + " _players";
        roomNameCreated = roomName;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 13;
        PhotonNetwork.CreateRoom(roomName, roomOptions, null);
    }
}
