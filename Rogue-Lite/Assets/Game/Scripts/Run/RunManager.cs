using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    #region Variables
    public DoorPosition currentDoor;

    [SerializeField] private CameraController _cameraController;
    [SerializeField] private TimeManager _timeManager;
    private GameObject[] _doors;
    #endregion

    #region Methods
    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;

        if (!_cameraController)
        {
            _cameraController = FindObjectOfType<CameraController>();
        }

        if (!_timeManager)
        {
            _timeManager = FindObjectOfType<TimeManager>();
        }
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
                PlayerController player = FindObjectOfType<PlayerController>();
                
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
        _cameraController.cameraState = CameraState.END_ROOM;
        _timeManager.timeScale = 0.6f;
        Invoke("CallDoorsToOpen", 1f);
    }

    private void CallDoorsToOpen()
    {
        _cameraController.cameraState = CameraState.IDLE;
        _timeManager.timeScale = 1f;
        if (FindObjectOfType<RoomManager>())
            FindObjectOfType<RoomManager>().OpenDoors();
    }

    public void LoadNextRoom()
    {
        Invoke("LoadRoom", 1.2f);
    }

    private void LoadRoom()
    {
        StartCoroutine(LoadASynchrously(3));
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
