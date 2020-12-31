using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class OnlineLobby : MonoBehaviourPunCallbacks
{

    public Button ConnectBtn;
    public Button JoinRandomBtn;
    public TextMeshProUGUI Log;

    public byte maxPlayersInRoom = 4;
    public byte minPlayersInRoom = 2;

    public int playerCounter;
    public TextMeshProUGUI PlayerCounter;

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.ConnectUsingSettings())
            {
                Log.text += "\nConnected to server";
            }
            else
            {
                Log.text += "\nError. Not connected to server";
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        // Debug.Log("Conectados a la región: " + PhotonNetwork.CloudRegion);
        ConnectBtn.interactable = false;
        JoinRandomBtn.interactable = true;
    }

    public void JoinRandom()
    {
        if (!PhotonNetwork.JoinRandomRoom())
        {
            Log.text += "\nError trying to connect random room";
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Log.text += "\nThere are no rooms. Creating...";

        if (PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = maxPlayersInRoom }))
        {
            Log.text += "\nRoom created.";
        }
        else
        {
            Log.text += "\nError. Room not created.";
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Log.text += "\nRoom joined.";
        JoinRandomBtn.interactable = false;
    }

    public void FixedUpdate()
    {
        if(PhotonNetwork.CurrentRoom != null)
        {
            playerCounter = PhotonNetwork.CurrentRoom.PlayerCount;
        }

        PlayerCounter.text = playerCounter + "/" + maxPlayersInRoom;
    }
}
