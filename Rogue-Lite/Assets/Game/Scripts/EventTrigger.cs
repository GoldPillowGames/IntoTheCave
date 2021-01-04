using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    BATTLE_MUSIC,
    BACKGROUND_MUSIC,
    STOP_MUSIC,
    START_BATTLE,
    DEATH,
    ROOM_DOOR
}

public enum DoorPosition
{
    LEFT, TOP, RIGHT, BOTTOM
}

public class EventTrigger : MonoBehaviour
{
    public EventType eventType;
    public int themeIndex;
    public float musicTransitionSpeed = 0.75f;
    public int cameraIndex = 0;
    public DoorPosition doorPosition;
    public int doorIndex = 0;
    DoorPosition nextDoorPosition;

    private float objectiveVolume = 0;
    //private MusicManager musicManager;
    private AudioSource audioSource;
    private bool activated = false;
    private Transform player;
    private LayerMask playerLayer;
    private Vector3 respawnPoint;

    public void Awake()
    {
        if(eventType == EventType.ROOM_DOOR)
        {
            if (Config.data.isOnline)
            {
                if (Photon.Pun.PhotonNetwork.IsMasterClient)
                {
                    GetComponent<BoxCollider>().isTrigger = true;
                }
                else
                {
                    GetComponent<BoxCollider>().isTrigger = false;
                }
            }

            EventTrigger[] eventTriggers = FindObjectsOfType<EventTrigger>();

            foreach(EventTrigger trigger in eventTriggers)
            {
                if(eventType == EventType.ROOM_DOOR && trigger != this)
                {
                    if(doorPosition == trigger.doorPosition)
                    {
                        trigger.activated = false;
                        trigger.transform.localScale = this.transform.localScale;
                        trigger.transform.position = this.transform.position;
                        Destroy(this.gameObject);
                    }
                }
            }

            DontDestroyOnLoad(this);
        }

        playerLayer = 10;
        switch (doorPosition)
        {
            case DoorPosition.TOP:
                nextDoorPosition = DoorPosition.BOTTOM;
                break;
            case DoorPosition.LEFT:
                nextDoorPosition = DoorPosition.RIGHT;
                break;
            case DoorPosition.BOTTOM:
                nextDoorPosition = DoorPosition.TOP;
                break;
            case DoorPosition.RIGHT:
                nextDoorPosition = DoorPosition.LEFT;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        // musicManager = FindObjectOfType<MusicManager>();
        // audioSource = musicManager.GetComponent<AudioSource>();
        
        // audioSource.volume = 0;
        
    }

    private void Update()
    {
        if (!player && GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            respawnPoint = player.position;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Entered");
        switch (eventType)
        {
            case EventType.BATTLE_MUSIC:
                if (!activated)
                {
                    if (other.CompareTag("Player"))
                    {
                        //  FindObjectOfType<SoundManager>().battleMusic.Play(audioSource, themeIndex - 1, out objectiveVolume);
                        // musicManager.volume = objectiveVolume;
                        // musicManager.musicTransitionSpeed = musicTransitionSpeed;
                        activated = true;
                    }
                    
                }
                break;
            case EventType.STOP_MUSIC:
                if (!activated)
                {
                    if (other.CompareTag("Player"))
                    {
                        // FindObjectOfType<SoundManager>().battleMusic.Stop(out objectiveVolume);
                        // musicManager.volume = objectiveVolume;
                        // musicManager.musicTransitionSpeed = musicTransitionSpeed;
                        activated = true;
                    }
                    
                }
                break;
            case EventType.DEATH:
                if (other.CompareTag("Player"))
                {
                    //player.position = respawnPoint;
                    other.GetComponent<CharacterController>().enabled = false;
                    other.transform.position = respawnPoint;
                    print(other.name);
                    other.GetComponent<CharacterController>().enabled = true;
                }
                break;
            case EventType.ROOM_DOOR:
                if (other.CompareTag("Player") && !activated)
                {
                    if (Config.data.isOnline)
                    {
                        if (Photon.Pun.PhotonNetwork.IsMasterClient)
                        {
                            if (other.GetComponent<PlayerController>())
                            {
                                if (!other.GetComponent<PlayerController>().isMe)
                                    return;
                            }
                            else
                            {
                                if (other.GetComponentInParent<PlayerController>())
                                {
                                    if (!other.GetComponentInParent<PlayerController>().isMe)
                                        return;
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    

                    if (!activated && eventType == EventType.ROOM_DOOR)
                    {
                        activated = true;
                        if (!Config.data.isOnline)
                        {
                            // Localizamos el run manager
                            RunManager runManager = FindObjectOfType<RunManager>();
                            if (runManager)
                            {
                                PlayerController[] players = FindObjectsOfType<PlayerController>();
                                foreach (PlayerController player in players)
                                {
                                    player.doorOpened = true;
                                    switch (doorPosition)
                                    {
                                        case DoorPosition.TOP:
                                            player.doorDirection = -player.transform.right;
                                            break;
                                        case DoorPosition.BOTTOM:
                                            player.doorDirection = player.transform.right;
                                            break;
                                        case DoorPosition.LEFT:
                                            player.doorDirection = -player.transform.forward;
                                            break;
                                        case DoorPosition.RIGHT:
                                            player.doorDirection = player.transform.forward;
                                            break;
                                    }
                                    player.currentRotation = Quaternion.LookRotation(player.doorDirection);
                                    player.cameraFollower.StopFollowing();
                                }
                                runManager.currentDoor = nextDoorPosition; // Actualizamos la puerta por la que debe aparecer el jugador
                                runManager.LoadNextRoom();                 // Cargamos la siguiente escena
                            }
                            else
                            {
                                Debug.LogError("No Run Manager Detected, check if it was loaded.");
                            }
                        }
                        else
                        {
                            GetComponent<Photon.Pun.PhotonView>().RPC("UbicatePlayers", Photon.Pun.RpcTarget.All);
                        }
                        
                    }
                }
                break;
            default:
                break;
        }
    }

    [Photon.Pun.PunRPC]
    private void UbicatePlayers()
    {
        activated = true; // Provisional

        // Localizamos el run manager
        RunManager runManager = FindObjectOfType<RunManager>();

        if (runManager)
        {
            PlayerController[] players = FindObjectsOfType<PlayerController>();
            foreach (PlayerController player in players)
            {
                if (player.PV.IsMine)
                {
                    player.doorOpened = true;
                }
                
                print("Door Position: " + doorPosition);
                switch (doorPosition)
                {
                    case DoorPosition.TOP:
                        player.doorDirection = -player.transform.right;
                        break;
                    case DoorPosition.BOTTOM:
                        player.doorDirection = player.transform.right;
                        break;
                    case DoorPosition.LEFT:
                        player.doorDirection = -player.transform.forward;
                        break;
                    case DoorPosition.RIGHT:
                        player.doorDirection = player.transform.forward;
                        break;
                }
                if (player.PV.IsMine)
                {
                    player.currentRotation = Quaternion.LookRotation(player.doorDirection);
                    player.cameraFollower.StopFollowing();
                }
            }
            runManager.currentDoor = nextDoorPosition; // Actualizamos la puerta por la que debe aparecer el jugador
            runManager.LoadNextRoom();                 // Cargamos la siguiente escena
        }
        else
        {
            Debug.LogError("No Run Manager Detected, check if it was loaded.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (eventType)
        {
            case EventType.ROOM_DOOR:
                //if (other.CompareTag("Player") && activated)
                //{
                //    activated = false;
                //}
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if(eventType == EventType.BATTLE_MUSIC || eventType == EventType.BACKGROUND_MUSIC || eventType == EventType.ROOM_DOOR)
            Gizmos.color = new Color(0, 1, 0, 0.5f);
        else if (eventType == EventType.STOP_MUSIC)
            Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);
        else if(eventType == EventType.DEATH)
            Gizmos.color = new Color(1, 0, 0, 0.5f);

        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
