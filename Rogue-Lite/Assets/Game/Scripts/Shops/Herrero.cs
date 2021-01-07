using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herrero : NPCBehaviour
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        dialogueIndex = Config.data.herreroDialogueIndex;
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
        base.StartConversation();
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
        
        Config.data.herreroDialogueIndex++;
        if (Config.data.herreroDialogueIndex > 4)
        {
            Config.data.herreroDialogueIndex = 2;
        }
        dialogueIndex = Config.data.herreroDialogueIndex;
    }

    public override void CloseMenuPermanently()
    {
        base.CloseMenuPermanently();
        Config.data.herreroDialogueIndex++;
        if (Config.data.herreroDialogueIndex > 4)
        {
            Config.data.herreroDialogueIndex = 2;
        }
        dialogueIndex = Config.data.herreroDialogueIndex;
    }
}
