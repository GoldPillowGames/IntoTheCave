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

    // Load the data from the database
    string sentecesDBcontent;
    string[] sentencesData;


    List<string[]> values;

    // Start is called before the first frame update
    void Start()
    {
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
            
            values.Add(sentencesData[i].Split(','));
        }
        print("Values: " + values[1].Length);
        int random = UnityEngine.Random.Range(0, values.Count);
        //ShowSentence(random);
        ShowSentence(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueText)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            //int random = UnityEngine.Random.Range(0, values.Count);
            //sentence = values[random][1];
            //textAnimatorPlayer.ShowText(sentence);
            if(currentDialogueIndex == int.Parse(values[currentSentenceIndex+1][1]))
            {
                ShowSentence(currentSentenceIndex+1);
            }
            else
            {
                print("currentSentenceIndex: " + currentSentenceIndex);
                print("currentDialogueIndex: " + currentDialogueIndex);
                print("int.Parse(values[currentSentenceIndex + 1][1]): " + int.Parse(values[currentSentenceIndex + 1][1]));
            }
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    int random = UnityEngine.Random.Range(0, values.Count);
        //    sentence = values[random][1];
        //    textAnimatorPlayer.ShowText(sentence);
        //}
    }

    public void ShowSentence(int sentenceIndex)
    {
        sentence = values[sentenceIndex][2];
        currentSentenceIndex = int.Parse(values[sentenceIndex][0]) - 1;
        currentDialogueIndex = int.Parse(values[sentenceIndex][1]);
        textAnimatorPlayer.ShowText(sentence);

    }

    public void HideQuote()
    {
        Invoke("FinallyHideQuote", 2.5f);
    }



    void FinallyHideQuote()
    {
        hideText = true;
    }
}
