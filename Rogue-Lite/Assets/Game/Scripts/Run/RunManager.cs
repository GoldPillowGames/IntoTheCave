using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class RunManager : MonoBehaviour
{
    #region Variables
    public DoorPosition currentDoor;

    [SerializeField] private CameraController[] _cameraController;
    [SerializeField] private TimeManager[] _timeManager;
    [SerializeField] private AudioClip[] _musicStage0;
    [SerializeField] private AudioClip[] _musicStage1;
    [SerializeField] private AudioClip[] _musicStage2;
    [SerializeField] private AudioClip[] _musicStage3;

    public bool isOnlineManager = false;
    private GameObject[] _doors;
    private GameObject[] _previousDoors;
    private int currentRoom = 0;
    public int currentStage { get; private set; }

    #endregion

    public void SetUp(bool isOnline)
    {
        isOnlineManager = isOnline;
    }

    private void Awake()
    {
        
        DontDestroyOnLoad(this);
    }

    #region Methods
    private void Start()
    {
        //if (Config.data.isOnline && PhotonNetwork.IsMasterClient && !isOnlineManager)
        //{
        //    RunManager[] managers = FindObjectsOfType<RunManager>();
        //    //bool isOnline = false;
        //    //foreach (RunManager manager in managers)
        //    //{
        //    //    if (manager.isOnlineManager)
        //    //    {
        //    //        isOnline = true;
        //    //    }
        //    //}
        //    //if (!isOnline)
        //    //{

        //    //}

        //    GameObject runManager = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "RunManager"), new Vector3(0, 2.4f, 0), Quaternion.identity);
        //    runManager.GetComponent<RunManager>().SetUp(true);
        //}

        //if (Config.data.isOnline && !isOnlineManager)
        //{
        //    Destroy(this.gameObject);
        //}
        //RunManager[] run = FindObjectsOfType<RunManager>();
        //for(int i = 0; i < run.Length; i++)
        //{
        //    if(run[i] != this)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}

        if (isOnlineManager)
        {
            this.transform.parent = FindObjectOfType<GameManager>().transform;
        }



        SceneManager.activeSceneChanged -= OnSceneLoaded;

        // SceneManager.activeSceneChanged += OnSceneLoaded;
        if (!Config.data.isOnline)
            SceneManager.activeSceneChanged += OnSceneLoaded;
        else
        {
            if (GetComponent<Photon.Pun.PhotonView>().IsMine)
            {
                SceneManager.activeSceneChanged += OnSceneLoaded;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        //}

                //if (!_cameraController)
                //{
                //    _cameraController = FindObjectOfType<CameraController>();
                //}

                //if (!_timeManager)
                //{
                //    _timeManager = FindObjectOfType<TimeManager>();
                //}
                Audio.ChangeTracks(_musicStage0);
        }

    public void ResetOnSceneLoaded()
    {
        SceneManager.activeSceneChanged -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene previousScene, Scene currentScene)
    {
        //bool apply = false;

        //foreach (GameObject g in previousScene.GetRootGameObjects())
        //{
        //    if (g.GetComponent<RunManager>())
        //    {
        //        apply = true;
        //    }
        //}

        //if (!apply)
        //    return;

        // Reiniciamos el array de puertas
        _doors = new GameObject[4];
        // _previousDoors = new GameObject[4];
        int currentIndex = 0;

        // Obtenemos las puertas de la escena actual
        foreach (GameObject g in currentScene.GetRootGameObjects())
        {
            if (g.CompareTag("Door"))
            {
                _doors[currentIndex] = g;
                _doors[currentIndex].GetComponent<EventTrigger>().Awake();
                currentIndex++;
            }
        }

        //// Obtenemos las puertas de la escena actual
        //foreach (GameObject g in previousScene.GetRootGameObjects)
        //{
        //    if (g.CompareTag("Door"))
        //    {
        //        _previousDoors[currentIndex] = g;
        //        currentIndex++;
        //    }
        //}

        //for(int i = 0; i < _previousDoors.Length; i++)
        //{
        //    for(int j = 0; j < _doors.Length; j++)
        //    {
        //        print(i);
        //        if (_previousDoors[i].GetComponent<EventTrigger>().doorPosition == _doors[j].GetComponent<EventTrigger>().doorPosition)
        //        {
        //            _previousDoors[i].transform.position = _doors[j].transform.position;
        //            Destroy(_doors[j]);
        //            _doors[j] = _previousDoors[i];
        //        }
        //    }
        //}

        //// Obtenemos las puertas de la escena actual
        //foreach (GameObject g in currentScene.GetRootGameObjects())
        //{
        //    if (g.CompareTag("Door"))
        //    {
        //        _doors[currentIndex] = g;
        //        currentIndex++;
        //    }
        //}
        EventTrigger[] _myDoors = FindObjectsOfType<EventTrigger>();

        print("Entra en el Run Manager");

        // Comprobamos por cada una de las puertas si su ubicación coincide con la deseada
        foreach (EventTrigger g in _myDoors)
        {
            g.GetComponent<BoxCollider>().enabled = false;

            if (g.GetComponent<EventTrigger>().eventType != EventType.ROOM_DOOR)
                continue;

            RunManager runManager = FindObjectOfType<RunManager>();

            foreach(RunManager run in FindObjectsOfType<RunManager>())
            {
                if(run != null)
                {
                    runManager = run;
                }
            }

            // Comprobación
            if (g.GetComponent<EventTrigger>().doorPosition.Equals(currentDoor))
            {
                PlayerController[] p = FindObjectsOfType<PlayerController>();

                float distanceFromDoor = 4.75f;

                foreach (PlayerController player in p)
                {
                    if (player.isMe || !Config.data.isOnline)
                    {
                        if (PhotonNetwork.IsMasterClient || !Config.data.isOnline)
                        {
                            distanceFromDoor = 4.75f;
                        }
                        else
                        {
                            distanceFromDoor = 8.75f;
                        }
                        // Desactivamos el player controller y el character controller para cambiar la posición del jugador sin errores
                        player.enabled = false;
                        player.GetComponent<CharacterController>().enabled = false;


                        // Actualizamos la ubicación del jugador
                        switch (currentDoor)
                        {
                            case DoorPosition.BOTTOM:
                                player.transform.position = g.transform.position - transform.right * distanceFromDoor;
                                break;
                            case DoorPosition.TOP:
                                player.transform.position = g.transform.position + transform.right * distanceFromDoor;
                                break;
                            case DoorPosition.LEFT:
                                player.transform.position = g.transform.position + transform.forward * distanceFromDoor;
                                break;
                            case DoorPosition.RIGHT:
                                player.transform.position = g.transform.position - transform.forward * distanceFromDoor;
                                break;
                        }

                        distanceFromDoor += 7;

                        // Reactivamos el player controller y el character controller
                        player.enabled = true;
                        player.GetComponent<CharacterController>().enabled = true;
                        player.cameraFollower.ResetFollowing();
                    }
                    
                }


            }
        }

        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in players)
        {
            player.doorOpened = false;
            player.weaponTrail.Start();
        }
    }

    private void Update()
    {
        // DEBUG
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    EndCurrentStage();
        //}
    }

    public void EndCurrentStage()
    {
        foreach(CameraController cam in FindObjectsOfType<CameraController>())
        {
            cam.cameraState = CameraState.END_ROOM;
        }

        foreach (TimeManager timer in FindObjectsOfType<TimeManager>())
        {
            timer.timeScale = 0.6f;
        }

        
        Invoke("CallDoorsToOpen", 1f);
    }

    private void CallDoorsToOpen()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("DoorModel");
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<Animator>())
                door.GetComponent<Animator>().SetBool("IsOpen", true);
        }

        foreach (CameraController cam in FindObjectsOfType<CameraController>())
        {
            cam.cameraState = CameraState.IDLE;
        }

        foreach (TimeManager timer in FindObjectsOfType<TimeManager>())
        {
            timer.timeScale = 1f;
        }

        // _timeManager.timeScale = 1f;
        if (FindObjectOfType<RoomManager>())
        {
            FindObjectOfType<RoomManager>().OpenDoors();
        }
            

        ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
        if (itemSpawner)
        {
            itemSpawner.SpawnRandomItem();
        }

    }

    public void LoadNextRoom()
    {
        Invoke("LoadRoom", 1.2f);
    }

    
    public void LoadRoom()
    {
        if (Config.data.isOnline)
        {
            GetComponent<PhotonView>().RPC("ChangeRoom", RpcTarget.All);
            // ChangeRoom();
        }
        else
        {
            ChangeRoom();
        }

        
        //Fade.OnPlay = () => {

        //    ChangeRoom();

        //    //if (!Config.data.isOnline)
        //    //    StartCoroutine(LoadASynchrously(roomToLoad));
        //    //else
        //    //    PhotonNetwork.LoadLevel(roomToLoad);

        //};
        //Fade.PlayFade(FadeType.UPPER_LOWER);
    }

    [PunRPC]
    public void ChangeRoom()
    {
        Fade.OnPlay = () => {


            currentRoom++;
            int roomToLoad = SceneManager.GetActiveScene().buildIndex;

            while(roomToLoad == SceneManager.GetActiveScene().buildIndex && currentRoom != 0)
            {
                if (currentRoom == 10)
                {
                    switch (currentStage)
                    {
                        case 1:
                            print("Boss Fight Reached");
                            roomToLoad = 9;
                            currentRoom = 0;
                            // Audio.ActivateTrack(3);
                            currentStage = 2;
                            break;
                        case 2:
                            print("Boss Fight Reached");
                            roomToLoad = 9;
                            currentRoom = 0;
                            currentStage = 3;
                            break;
                         case 3:
                            print("Boss Fight Reached");
                            roomToLoad = 15;
                            currentRoom = 0;
                            currentStage = 3;
                            break;
                        default:
                            roomToLoad = Random.Range(5, 10);
                            break;
                    }
                }
                else
                {
                    switch (currentStage)
                    {
                        case 0:
                            if (currentRoom == 1)
                            {
                                Audio.ChangeTracks(_musicStage1);

                                

                                currentStage = 1;
                                roomToLoad = 6;
                            }
                            break;
                        case 1:
                            //if (currentRoom == 1)
                            //{
                            //    Audio.ChangeTracks(_musicStage1);
                            //    currentStage = 1;
                            //    roomToLoad = 6;
                            //}
                            //else
                            //{
                            
                            roomToLoad = Random.Range(5, 10);
                            //}
                            break;
                        case 2:
                            if (currentRoom == 1)
                            {
                                Audio.ChangeTracks(_musicStage2);
                                //currentStage = 1;
                                //roomToLoad = 6;
                            }
                            //if (currentRoom == 1)
                            //{
                            //    currentStage = 1;
                            //    roomToLoad = 6;
                            //}
                            //else
                            //{
                            roomToLoad = Random.Range(11, 15);
                            //}
                            break;
                        case 3:
                            if (currentRoom == 1)
                            {
                                Audio.ChangeTracks(_musicStage3);
                                //currentStage = 1;
                                //roomToLoad = 6;
                            }

                            roomToLoad = Random.Range(15, 19);
                            break;
                        default:
                            roomToLoad = Random.Range(5, 10);
                            break;
                    }
                }
            }


            print("Room to Load: " + roomToLoad);
            if (!Config.data.isOnline)
            {
                StartCoroutine(LoadASynchrously(roomToLoad));
                // print(FindObjectsOfType<RunManager>().Length);
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                print("Change Room");
                // StartCoroutine(LoadASynchrously(roomToLoad));
                PhotonNetwork.LoadLevel(roomToLoad);
            }
                

        };
        // if (PhotonNetwork.IsMasterClient) // Quizas eliminar
        Fade.PlayFade(FadeType.UPPER_LOWER);
    }

    IEnumerator LoadASynchrously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        //slider.enabled = true;

        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //if (loadPercentageText)
            //    loadPercentageText.text = (int)(operation.progress * 100) + "%";
            yield return null;

        }

        //GetComponent<Animator>().SetTrigger("FadeOutIn");
    }
    #endregion
}
