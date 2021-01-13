using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject menu;
    public DialogueController dialogueController;
    public AudioSource audioSource;
    public AudioClip[] voiceSounds;

    protected int dialogueIndex;
    protected Quaternion originalRotation;

    protected bool interacting = false;
    protected float timeToStopInteracting;
    protected float maxTimeToStopInteracting = 0.3f;
    protected bool interacted = false;
    protected PlayerController player;

    protected bool playSound = true;
    protected bool playSound2 = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        originalRotation = transform.rotation;
        player = FindObjectOfType<PlayerController>();
    }

    public void Interact()
    {
        if (timeToStopInteracting <= 0 && !interacting)
        {

        }
        else
        {
            if (interacting)
            {
                dialogueController.ContinueDialogue();
            }
            else
            {
                if (menu != null)
                {
                    FindObjectOfType<CameraController>().cameraState = CameraState.DIALOGUE;
                    StartConversation();
                }
            }
            
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        dialogueController.UpdateDialogue();
        if(timeToStopInteracting <= 0 && !interacting)
        {
            
        }
        else
        {
            timeToStopInteracting -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E) && menu != null)
            {
                FindObjectOfType<CameraController>().cameraState = CameraState.DIALOGUE;
                
                StartConversation();
            }
        }

        if (!interacting)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, 6 * Time.deltaTime);
        }
        else
        {
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 6 * Time.deltaTime);
            player.cameraFollower.transform.position = Vector3.Lerp(player.cameraFollower.transform.position, (this.transform.position + player.transform.position) / 2, 3* Time.deltaTime);
        }
    }

    public virtual void PlayVoiceSound()
    {
        if (playSound)
        {
            if (playSound2)
            {
                audioSource.PlayOneShot(voiceSounds[Random.Range(0, voiceSounds.Length)]);
                playSound = false;
            }
            else
            {
                playSound2 = true;
            }
            
        }
        else
        {
            playSound = true;
        }
        
    }

    public virtual void ShowInteractBox()
    {
        if (interacting)
            return;
        timeToStopInteracting = maxTimeToStopInteracting;
    }

    

    public virtual void StartConversation()
    {
        if (!interacting)
        {
            interacting = true;
            player.playerState = PlayerState.DIALOGUE;
            // ShowMenu();
            dialogueController.StartDialogue(dialogueIndex);
            Debug.Log("Starting Conversation");
        }
    }

    public virtual void ShowMenu()
    {
        interacted = true;
        // dialogueController.gameObject.SetActive(false);
    }

    public virtual void CloseMenuPermanently()
    {
        dialogueController.ShowLastSentence();
        // player.playerState = PlayerState.NEUTRAL;
    }

    public virtual void CloseMenu()
    {
        dialogueController.ShowLastSentence();
    }

    public virtual void StopDialogue()
    {
        interacted = false;
        interacting = false;
        player.playerState = PlayerState.NEUTRAL;
        player.cameraFollower.transform.position = player.transform.position;
        FindObjectOfType<CameraController>().cameraState = CameraState.INTERACT;
    }
}
