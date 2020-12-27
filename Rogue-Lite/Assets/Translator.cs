using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class Translator : MonoBehaviour
{

    [SerializeField] private string _translationPath = "Menus/MainMenu";
    [SerializeField] private int _sentenceIndex = 0;
    [SerializeField] private bool _checkInUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        CheckText();
    }

    // Update is called once per frame
    void Update()
    {
        if (_checkInUpdate)
            CheckText();
    }

    void CheckText()
    {
        string text = TableReader.GetString(_translationPath, 1, _sentenceIndex);

        if (GetComponent<ButtonManager>())
        {
            GetComponent<ButtonManager>().buttonText = text;
            GetComponent<ButtonManager>().UpdateUI();
            return;
        }

        if (GetComponents<TextMeshPro>().Length > 0)
        {
            
            foreach(TextMeshPro textMesh in GetComponents<TextMeshPro>())
            {
                textMesh.text = text;
            }
            return;
        }

        if (GetComponents<TextMeshProUGUI>().Length > 0)
        {
            foreach (TextMeshProUGUI textMesh in GetComponents<TextMeshProUGUI>())
            {
                textMesh.text = text;
            }
            return;
        }

        if (GetComponentsInChildren<TextMeshPro>().Length > 0)
        {
            foreach (TextMeshPro textMesh in GetComponentsInChildren<TextMeshPro>())
            {
                textMesh.text = text;
            }
            return;
        }

        if (GetComponentsInChildren<TextMeshProUGUI>().Length > 0)
        {
            foreach (TextMeshProUGUI textMesh in GetComponentsInChildren<TextMeshProUGUI>())
            {
                textMesh.text = text;
            }
            return;
        }
    }
}
