using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    //Variables

    //Set up a static so that we can call this script from anywhere
    public static LaunchManager instance;

    public string username;
    public bool clearPrefs;
    public GameObject playerPrefab;

    #region Unity Methods
    void Awake()
    {
        if (clearPrefs)
            DeletePlayerPrefs();

        username = "username";

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("PLAYERNAME"))
        {
            PhotonNetwork.NickName = SystemInfo.deviceName + "_" + SystemInfo.deviceModel;
            username = PhotonNetwork.NickName;
        }
        else
        {
            username = PlayerPrefs.GetString("PLAYERNAME");
        }

        LoadSettings();
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("PLAYERNAME"))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PLAYERNAME");
            ConnectToPhotonServers();
            PhotonNetwork.LoadLevel(1);
        }
    }
    public void ConnectToPhotonServers()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void InputName(string playerName)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            return;
        }
        username = playerName;
    }
    public void SetPlayerName(string playerName)
    {
        PlayerPrefs.SetString("PLAYERNAME", username);
        PhotonNetwork.NickName = username;
        PlayerPrefs.SetInt("YourRank", 1);
        ConnectToPhotonServers();
        PhotonNetwork.LoadLevel(1);
    }
    #endregion

    #region PUN Callbacks
    public override void OnConnected()
    {
        Debug.Log(PhotonNetwork.NickName + " has connected to the Photon Servers");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " has connected to the Master Server");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.NickName + " has joined the lobby with " + PhotonNetwork.CountOfPlayers + " players & " + PhotonNetwork.CountOfRooms + " rooms created");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("We have joined " + PhotonNetwork.CurrentRoom.Name + " with " + PhotonNetwork.CurrentRoom.PlayerCount + " players");
        PhotonNetwork.AutomaticallySyncScene = true;

        //Enter a random spot to spawn
        int randPoint = Random.Range(-10, 10);

        //Spawn the player into the game scene
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randPoint, 0f, randPoint), Quaternion.identity);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //If a room fails to be made this will be called from the Matchmaking script
        Matchmaking.instance.CreateRoomOnClick();
        Debug.Log(message);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("New Room was Created");
        Debug.Log("Created " + PhotonNetwork.CurrentRoom.Name);

        //This will be our created room after clicking the button
        Matchmaking.instance.CreateRoomOnClick();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer + " has entered " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnLeftRoom()
    {
        //Player has left the current match and has returned back to the loading screen
        PhotonNetwork.LoadLevel(1);
    }

    #endregion
}
