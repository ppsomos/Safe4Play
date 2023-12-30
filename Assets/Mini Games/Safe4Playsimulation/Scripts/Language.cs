using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class Language : MonoBehaviour
{
    public Localization loc;
    public Flowchart flowchart;
    // Start is called before the first frame update
    void Start()
    {
        string lan = PlayerPrefs.GetString("language").ToUpper();
        loc.SetActiveLanguage(lan);
        flowchart.SetStringVariable("Language", lan);
        StartCoroutine(InitialiseLocales(lan));

    }

    IEnumerator InitialiseLocales(string lan)
    {
        yield return LocalizationSettings.InitializationOperation;

        switch (lan)
        {
            case "EN":
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                break;
            case "AR":
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                break;
            case "FR":
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
                break;
            case "EL":
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
                break;
            default:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                break;
        }
    }
}
