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
    public bool isOnlineManager = false;
    private GameObject[] _doors;
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

        if (isOnlineManager)
        {
            this.transform.parent = FindObjectOfType<GameManager>().transform;
        }


        SceneManager.activeSceneChanged += OnSceneLoaded;

        //if (!_cameraController)
        //{
        //    _cameraController = FindObjectOfType<CameraController>();
        //}

        //if (!_timeManager)
        //{
        //    _timeManager = FindObjectOfType<TimeManager>();
        //}
    }

    private void OnSceneLoaded(Scene previousScene, Scene currentScene)
    {
        // Reiniciamos el array de puertas
        _doors = new GameObject[4];
        int currentIndex = 0;

        // Obtenemos las puertas de la escena actual
        foreach (GameObject g in currentScene.GetRootGameObjects())
        {
            if (g.CompareTag("Door"))
            {
                _doors[currentIndex] = g;
                currentIndex++;
            }
        }
        
        // Comprobamos por cada una de las puertas si su ubicación coincide con la deseada
        foreach (GameObject g in _doors)
        {
            g.GetComponent<BoxCollider>().enabled = false;

            // Comprobación
            if (g.GetComponent<EventTrigger>().doorPosition.Equals(FindObjectOfType<RunManager>().currentDoor))
            {
                PlayerController[] p = FindObjectsOfType<PlayerController>();
                
                foreach(PlayerController player in p)
                {
                    // Desactivamos el player controller y el character controller para cambiar la posición del jugador sin errores
                    player.enabled = false;
                    player.GetComponent<CharacterController>().enabled = false;

                    // Actualizamos la ubicación del jugador
                    player.transform.position = g.transform.position;

                    // Reactivamos el player controller y el character controller
                    player.enabled = true;
                    player.GetComponent<CharacterController>().enabled = true;
                    player.cameraFollower.ResetFollowing();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EndCurrentStage();
        }
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
            FindObjectOfType<RoomManager>().OpenDoors();
    }

    public void LoadNextRoom()
    {
        Invoke("LoadRoom", 1.2f);
    }

    private void LoadRoom()
    {
        Fade.OnPlay = () => {
            if (!Config.data.isOnline)
                StartCoroutine(LoadASynchrously(3));
            else
                PhotonNetwork.LoadLevel(3);
        };
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
