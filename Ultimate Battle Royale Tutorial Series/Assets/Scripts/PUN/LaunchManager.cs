using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    //Set up a static so that we can call this script from anywhere
    public static LaunchManager instance;

    public string username;

    [SerializeField] GameObject playerPrefab;

    #region Unity Methods
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("PlayerName"))
        {
            PhotonNetwork.NickName = SystemInfo.deviceName + "_" + SystemInfo.deviceModel;
            username = PhotonNetwork.NickName;
            Debug.Log(username);
        }

        PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods
    public void LoadSettings()
    {
        if(PlayerPrefs.HasKey("PlayerName"))
        {
            ConnectToPhotonServers();
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void ConnectToPhotonServers()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void InputName(string playerName)
    {
        if(string.IsNullOrEmpty(playerName))
        {
            return;
        }
        username = playerName;
        PhotonNetwork.NickName = username;
    }

    public void SetPlayerName(string playerName)
    {
        username = playerName;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("YourRank", 1);
        ConnectToPhotonServers();
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel(1);
    }

    public void SpawnPlayer()
    {
        int randPoint = Random.Range(-10, 10);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randPoint, 0f, randPoint), Quaternion.identity);
    }
    #endregion

    #region PUN Callbacks
    public override void OnConnected()
    {
        username = PlayerPrefs.GetString("PlayerName");
        PhotonNetwork.NickName = username;
        Debug.Log(PlayerPrefs.GetString("PlayerName") + " has connected to the Photon Servers");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
        Debug.Log(username + " has joined lobby");
        Debug.Log(PhotonNetwork.NickName);
        Debug.Log(PhotonNetwork.CountOfPlayers);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We have joined " + PhotonNetwork.CurrentRoom.Name + " with " + PhotonNetwork.CurrentRoom.PlayerCount + " total players.");
        PhotonNetwork.AutomaticallySyncScene = true;
        SpawnPlayer();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        MatchMaking.instance.CreateRoomOnClick();
        Debug.Log(message);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("New Room was created!");
        Debug.Log("Created " + PhotonNetwork.CurrentRoom.Name);
        MatchMaking.instance.roomNameCreated = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer + " has entered " + PhotonNetwork.CurrentRoom.Name);
    }
    #endregion
}
