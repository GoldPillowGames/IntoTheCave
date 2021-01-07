using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Realtime;

public class OnlineRoomManager : MonoBehaviourPunCallbacks
{
    public static OnlineRoomManager Instance;
    public bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 4)
        {
            print("Loaded Scene");
            gameStarted = true;
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        if(gameStarted)
            PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        if(gameStarted)
            PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        // PhotonNetwork.NetworkStatisticsReset();
        if (gameStarted)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            PhotonNetwork.OfflineMode = true;
            // SceneManager.LoadScene(1);
            FindObjectOfType<GameManager>().DeleteAllDontDestroyOnLoadAndLoadScene(1);
            OnlineRoomManager[] roomsManager = FindObjectsOfType<OnlineRoomManager>();
            foreach(OnlineRoomManager r in roomsManager)
            {
                if (r != this)
                {
                    Destroy(r.gameObject);
                }
            }
            Destroy(this.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
