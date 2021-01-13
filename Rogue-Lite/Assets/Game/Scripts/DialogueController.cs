using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;
using System;

public class DialogueController : MonoBehaviour
{
    public GameObject container;
    public string NPC_Name;
    public Transform dialogueContainer;
    public char separator = ';';

    private TextMeshPro dialogueText;
    private TextAnimatorPlayer textAnimatorPlayer;
    private GameObject playerCamera;
    private float timeToHideQuote = 1f;
    private float textFadeSmooth = 8f;
    private bool hideText = false;
    private Color hideColor;
    private Color originalColor;
    private string sentence = "Example Text";

    private int currentSentenceIndex = 1;
    private int currentDialogueIndex = 1;
    private Animator anim;

    // Load the data from the database
    string sentecesDBcontent;
    string[] sentencesData;


    List<string[]> values;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        dialogueText = GetComponentInChildren<TextMeshPro>();
        textAnimatorPlayer = GetComponentInChildren<TextAnimatorPlayer>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (!dialogueText)
            return;

        hideColor = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, 0);
        originalColor = dialogueText.color;

        sentecesDBcontent = Resources.Load<TextAsset>("NPCs/" + NPC_Name).text;
        sentencesData = sentecesDBcontent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        print("Sentences: " + sentencesData.Length);
        values = new List<string[]>();
        //enemy = GetComponent<Enemy>();

        for (int i = 1; i < sentencesData.Length; i++)
        {
            values.Add(sentencesData[i].Split(separator));
        }
        print("Values: " + values[1].Length);
        int random = UnityEngine.Random.Range(0, values.Count);
        // ShowSentence(random);
        // ShowSentence(0);

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    transform.GetChild(i).gameObject.SetActive(false);
        //}
    }

    private bool isInDialogue = false;

    public void StartDialogue(int dialogueIndex)
    {
        anim.SetBool("IsInDialogue", true);

        //for (int i = 0; i < dialogueContainer.transform.childCount; i++)
        //{
        //    dialogueContainer.transform.GetChild(i).gameObject.SetActive(true);
        //}
        
        for(int i = 0; i < sentencesData.Length; i++)
        {
            if (int.Parse(values[i][1]) == dialogueIndex)
            {
                ShowSentence(i);
                currentDialogueIndex = dialogueIndex;
                isInDialogue = true;
                return;
            }
        }
        EndDialogue();
    }

    public void EndDialogue()
    {
        anim.SetBool("IsInDialogue", false);
        //for (int i = 0; i < dialogueContainer.transform.childCount; i++)
        //{
        //    dialogueContainer.transform.GetChild(i).gameObject.SetActive(false);
        //}
        GetComponentInParent<NPCBehaviour>().StopDialogue();

        isInDialogue = false;
    }

    public void ShowLastSentence()
    {
        ShowSentence(currentSentenceIndex + 1);
        isInDialogue = true;
    }

    public void ContinueDialogue()
    {
        if (isInDialogue)
        {
            //int random = UnityEngine.Random.Range(0, values.Count);
            //sentence = values[random][1];
            //textAnimatorPlayer.ShowText(sentence);
            if (values[currentSentenceIndex + 2][1] == "")
            {
                // EndDialogue();
                if (values[currentSentenceIndex + 1][1] == "")
                {
                    EndDialogue();
                }
                else
                {
                    GetComponentInParent<NPCBehaviour>().ShowMenu();
                    isInDialogue = false;
                }

            }
            else if (currentDialogueIndex == int.Parse(values[currentSentenceIndex + 2][1]))
            {
                ShowSentence(currentSentenceIndex + 1);
            }
            else if (currentDialogueIndex == int.Parse(values[currentSentenceIndex + 1][1]))
            {
                GetComponentInParent<NPCBehaviour>().ShowMenu();
                isInDialogue = false;
            }
            else
            {
                //print("currentSentenceIndex: " + currentSentenceIndex);
                //print("currentDialogueIndex: " + currentDialogueIndex);
                //print("int.Parse(values[currentSentenceIndex + 1][1]): " + int.Parse(values[currentSentenceIndex + 1][1]));
                EndDialogue();
            }
        }
    }

    public void UpdateDialogue()
    {
        if (!dialogueText)
            return;

        if (Input.GetKeyDown(KeyCode.E) && isInDialogue)
        {
            //int random = UnityEngine.Random.Range(0, values.Count);
            //sentence = values[random][1];
            //textAnimatorPlayer.ShowText(sentence);
            if (values[currentSentenceIndex + 2][1] == "")
            {
                // EndDialogue();
                if (values[currentSentenceIndex + 1][1] == "")
                {
                    EndDialogue();
                }
                else
                {
                    GetComponentInParent<NPCBehaviour>().ShowMenu();
                    isInDialogue = false;
                }

            }
            else if (currentDialogueIndex == int.Parse(values[currentSentenceIndex + 2][1]))
            {
                ShowSentence(currentSentenceIndex + 1);
            }
            else if (currentDialogueIndex == int.Parse(values[currentSentenceIndex + 1][1]))
            {
                GetComponentInParent<NPCBehaviour>().ShowMenu();
                isInDialogue = false;
            }
            else
            {
                //print("currentSentenceIndex: " + currentSentenceIndex);
                //print("currentDialogueIndex: " + currentDialogueIndex);
                //print("int.Parse(values[currentSentenceIndex + 1][1]): " + int.Parse(values[currentSentenceIndex + 1][1]));
                EndDialogue();
            }
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    int random = UnityEngine.Random.Range(0, values.Count);
        //    sentence = values[random][1];
        //    textAnimatorPlayer.ShowText(sentence);
        //}
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void ShowSentence(int sentenceIndex)
    {
        sentence = values[sentenceIndex][(int)Config.data.language + 4];
        currentSentenceIndex = int.Parse(values[sentenceIndex][0]) - 1;
        currentDialogueIndex = int.Parse(values[sentenceIndex][1]);
        textAnimatorPlayer.ShowText(sentence);

    }

    public virtual void HideQuote()
    {
        Invoke("FinallyHideQuote", 2.5f);
    }



    public virtual void FinallyHideQuote()
    {
        hideText = true;
    }
}
