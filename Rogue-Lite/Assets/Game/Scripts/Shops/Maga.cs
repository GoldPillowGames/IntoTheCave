using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maga : NPCBehaviour
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        dialogueIndex = Config.data.magaDialogueIndex;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void ShowInteractBox()
    {
        base.ShowInteractBox();
    }

    

    public override void StartConversation()
    {
        if (!interacting)
        {
            interacting = true;
            player.playerState = PlayerState.DIALOGUE;
            if((Config.data.newGamePlusStarted == false && Config.data.dungeonsCompleted > 0) || (Config.data.dungeonsStarted >= 7 && Config.data.newGamePlusStarted == false))
            {
                Config.data.newGamePlusStarted = true;
                dialogueController.StartDialogue(7);
            }
            else
            {
                dialogueController.StartDialogue(dialogueIndex);
            }
            //dialogueController.StartDialogue(dialogueIndex);
            Debug.Log("Starting Conversation");
        }
    }

    public override void ShowMenu()
    {
        if (!interacted)
            menu.SetActive(true);
        base.ShowMenu();
    }

    public override void CloseMenu()
    {
        base.CloseMenu();
        
        Config.data.magaDialogueIndex++;
        if (Config.data.magaDialogueIndex > 6)
        {
            Config.data.magaDialogueIndex = 2;
        }
        dialogueIndex = Config.data.magaDialogueIndex;
    }

    public override void CloseMenuPermanently()
    {
        base.CloseMenuPermanently();
        Config.data.magaDialogueIndex++;
        if (Config.data.magaDialogueIndex > 6)
        {
            Config.data.magaDialogueIndex = 2;
        }
        dialogueIndex = Config.data.magaDialogueIndex;
    }
}
