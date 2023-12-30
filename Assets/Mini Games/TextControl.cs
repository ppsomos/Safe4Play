using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;
using UnityEngine.UI;

public class TextControl : MonoBehaviour
{
    string language;
    Localization localization;

    // Start is called before the first frame update
    void Start()
    {
        language = PlayerPrefs.GetString("language");
        localization = FindObjectOfType<Localization>();
        localization.SetActiveLanguage(language.ToUpper());
    }

    public void FixArabicLineOrder(GameObject storyText)
    {
        if (language == "ar")
        {
            Text myText = storyText.GetComponent<Text>();
            string[] lineArray = new string[myText.cachedTextGenerator.lines.Count];

            Canvas.ForceUpdateCanvases();
            for (int i = 0; i < myText.cachedTextGenerator.lines.Count; i++)
            {
                int startIndex = myText.cachedTextGenerator.lines[i].startCharIdx;
                int endIndex = (i == myText.cachedTextGenerator.lines.Count - 1) ? myText.text.Length
                    : myText.cachedTextGenerator.lines[i + 1].startCharIdx;
                int length = endIndex - startIndex;
                lineArray[i] = myText.text.Substring(startIndex, length);
            }

            Array.Reverse(lineArray, 0, lineArray.Length);

            myText.text = String.Join("\n", lineArray);
        }
    }
}
