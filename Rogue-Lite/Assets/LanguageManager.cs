using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private void ChangeLanguage(Language language)
    {
        Config.data.language = language;
    }

    public void ChangeLanguage(int index)
    {
        ChangeLanguage((Language)index);
    }
}
