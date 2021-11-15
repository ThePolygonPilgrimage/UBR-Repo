using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks, IPunObservable
{
    #region VARIABLES
    CharacterController cControl;
    public Camera playerCamera;

    public Behaviour[] playerComponents;
    #endregion

    #region UNITY METHODS
    void Start()
    {
        //PhotonNetwork.SerializationRate = 8;
        //PhotonNetwork.SendRate = 10;

        var isLocalPlayer = photonView.IsMine;

        for (int i = 0; i < playerComponents.Length; i++)
        {
            playerComponents[i].enabled = isLocalPlayer;
        }

    }
    #endregion

    #region PUBLIC METHODS
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log("HelloWritting!!?");
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(cControl.velocity);

            stream.SendNext(playerCamera.transform.position);
            stream.SendNext(playerCamera.transform.rotation);
        }
        else
        {
            Debug.Log("HelloReading!!?");
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            playerCamera.transform.position = (Vector3)stream.ReceiveNext();
            playerCamera.transform.rotation = (Quaternion)stream.ReceiveNext();

        }
    }

    public void UpdateCameraTransform()
    {

    }
    #endregion
}