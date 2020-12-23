using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    public DoorPosition currentDoor;
    private GameObject[] doors;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene previousScene, Scene currentScene)
    {
        // Reiniciamos el array de puertas
        doors = new GameObject[4];
        int currentIndex = 0;

        // Obtenemos las puertas de la escena actual
        foreach (GameObject g in currentScene.GetRootGameObjects())
        {
            if (g.CompareTag("Door"))
            {
                doors[currentIndex] = g;
                currentIndex++;
            }
        }
        
        // Comprobamos por cada una de las puertas si su ubicación coincide con la deseada
        foreach (GameObject g in doors)
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
            }
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
        if(FindObjectOfType<RoomManager>())
            FindObjectOfType<RoomManager>().OpenDoors();
    }

    public void LoadNextRoom()
    {
        StartCoroutine(LoadASynchrously(2));
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
}
