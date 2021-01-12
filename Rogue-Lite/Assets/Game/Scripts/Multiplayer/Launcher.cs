using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] TMP_Text errorText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;

    [SerializeField] Button startGameButton;
    [SerializeField] TMP_Text nicknameText;
    [SerializeField] TMP_Text roomNamePlaceholder;
    [SerializeField] TMP_Text roomName;
    [SerializeField] GameObject hostButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        startGameButton.interactable = PhotonNetwork.IsMasterClient && (PhotonNetwork.PlayerList.Length == 2);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.OfflineMode = false;
        if (Application.isMobilePlatform)
            hostButton.SetActive(false);
        PhotonNetwork.NickName = "Player " + Random.Range(0, 9999).ToString("0000");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        nicknameText.text = "Nickname: " + PhotonNetwork.NickName;
    }

    public override void OnJoinedLobby()
    {
        LobbyMenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            PhotonNetwork.CreateRoom(roomNamePlaceholder.text, new Photon.Realtime.RoomOptions() { MaxPlayers = 2 });
            LobbyMenuManager.Instance.OpenMenu("loading");
            return;
        }
        // PhotonNetwork.CreateRoom(roomNameInputField.text);
        PhotonNetwork.CreateRoom(roomNameInputField.text, new Photon.Realtime.RoomOptions() { MaxPlayers = 2 });
        
        LobbyMenuManager.Instance.OpenMenu("loading");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(4);
    }

    public override void OnJoinedRoom()
    {
        LobbyMenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }


        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            // print(players[i].NickName + " joined. Number of Players: " + players.Length);
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        // startGameButton.interactable = PhotonNetwork.IsMasterClient;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // startGameButton.interactable = PhotonNetwork.IsMasterClient;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        LobbyMenuManager.Instance.OpenMenu("title");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room creation failed: " + message;
        LobbyMenuManager.Instance.OpenMenu("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        LobbyMenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        LobbyMenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        LobbyMenuManager.Instance.OpenMenu("title");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public void SetUpRoom()
    {
        roomNamePlaceholder.text = PhotonNetwork.NickName + "'s Room";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
