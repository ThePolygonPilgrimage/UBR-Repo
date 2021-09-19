using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Matchmaking : MonoBehaviourPunCallbacks
{
    #region VARIABLES
    public static Matchmaking instance;
    public byte maxPlayers;
    #endregion

    #region UNITY METHODS
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region PUBLIC METHODS
    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.LoadLevel(3);
    }

    public void CreateRoomOnClick()
    {
        string roomName = "room_" + PhotonNetwork.Time + "_players";
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }
    #endregion
}
