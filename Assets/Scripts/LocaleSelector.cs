using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Serialization;

public class LocaleSelector : MonoBehaviour
{
    public static LocaleSelector instance;
    public GameData gData;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(SetLocale());
    }


    public void ChangeLocale()
    {

        StartCoroutine(SetLocale());
    }

    private IEnumerator SetLocale()
    {

        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[gData.languageSel];

    }
}
