using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataManager : MonoBehaviour
{
    public GameData gameData;

    #region Singleton
    public static PersistentDataManager instance;
    void Awake()
    {
        GetInstance();
    }

    void GetInstance()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    void Start()
    {
        LoadData();
        // PlayerPrefs.DeleteAll();
    }

    void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        string gameDataString = JsonConvert.SerializeObject(gameData);
        PlayerPrefs.SetString("GameData", gameDataString);
       // print("GameData Saved In PlayerPrefs: " + PlayerPrefs.GetString("GameData"));
    }

    public void LoadData()
    {
        string gameDataString = PlayerPrefs.GetString("GameData");
        GameData gameDataFromPlayerPrefs = JsonConvert.DeserializeObject<GameData>(gameDataString);
        if (gameDataFromPlayerPrefs == null)
        {
            //print("Game is played first time. No GameData found.");
            return;
        }
      //  print("GameData Loaded From PlayerPrefs");

        // Set Local GameData Variables Here - Start
        gameData.isFirst = gameDataFromPlayerPrefs.isFirst;
        gameData.isSound = gameDataFromPlayerPrefs.isSound;
        gameData.isMusic = gameDataFromPlayerPrefs.isMusic;
        gameData.isVibrate = gameDataFromPlayerPrefs.isVibrate;
        gameData.isGamePlayFirstTime = gameDataFromPlayerPrefs.isGamePlayFirstTime;
        gameData.selectLanguage = gameDataFromPlayerPrefs.selectLanguage;
        gameData.languageSel = gameDataFromPlayerPrefs.languageSel;
        for (int i=0; i < gameData.allRadel.Length; i++)
        {
            gameData.allRadel[i].isCompleted = gameDataFromPlayerPrefs.allRadel[i].isCompleted;
        }
    }
}
