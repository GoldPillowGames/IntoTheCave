using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonToTalk : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            switch (Config.data.language)
            {
                case Language.EN:
                    text.text = "Tap the screen to talk";
                    break;
                case Language.ES:
                    text.text = "Pulsa la pantalla para hablar";
                    break;
                case Language.DE:
                    text.text = "Bildschirm drücken um zu sprechen";
                    break;
                default:
                    text.text = "Tap the screen to talk";
                    break;
            }
        }
        else
        {
            switch (Config.data.language)
            {
                case Language.EN:
                    text.text = "Press E to talk";
                    break;
                case Language.ES:
                    text.text = "Pulsa E para hablar";
                    break;
                case Language.DE:
                    text.text = "E drücken um zu sprechen";
                    break;
                default:
                    text.text = "Tap the screen to talk";
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
