using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;
using System;

public static class TableReader
{

    public static string GetString(string path, int column, int row)
    {
        string sentecesDBcontent = Resources.Load<TextAsset>(path).text;
        string[] sentencesData = sentecesDBcontent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        List<string[]> values = new List<string[]>();

        for (int i = 1; i < sentencesData.Length; i++)
        {
            values.Add(sentencesData[i].Split(','));
        }
        int languageIndex = (int)Config.data.language;

        Debug.Log("Column: " + column);
        Debug.Log("Row: " + row);

        return values[row][column + languageIndex];
    }
}
