using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GData", menuName = "GameData")]
public class GameData : ScriptableObject
{
    public bool isFirst;
    public bool isSound;
    public bool isMusic;
    public bool isVibrate;
    public bool isGamePlayFirstTime;
    public string selectLanguage;

    public int languageSel;

    // public Vector3 PlayerLastPos;
    // public Quaternion PlayerLastRot;
    public RidlePath[] R_P;
    public GameRiddle[] allRadel;
    public QuestionData[] AllQuestion;
}
[System.Serializable]
public class RidlePath
{
    public string Path;
    public Ridle[] RT;

}
[System.Serializable]
public class Ridle
{
    public string RidleNo;
    public string gameNameEnglish;
    public string gameNameFrench;
    public string gameNameArabic;
    public string gameNameGreek;
    public string riddleName;
    public string answerName;
    public bool isCompleted;
    public string riddleQuestionEnglish;
    public string riddleQuestionFrench;
    public string riddleQuestionArabic;
    public string riddleQuestionGreek;
    public string descriptionEnglish;
    public string descriptionFrench;
    public string descriptionArabic;
    public string descriptionGreek;
    public string linkEnglish;
    public string linkFrench;
    public string linkArabic;
    public string linkGreek;
}
[System.Serializable]
public class QuestionData
{
    public string Name;
    public int R_Path;
}

[System.Serializable]
public class GameRiddle
{
    public string gameNameEnglish;
    public string gameNameFrench;
    public string gameNameArabic;
    public string gameNameGreek;
    public string riddleName;
    public string answerName;
    public bool isCompleted;
    public string riddleQuestionEnglish;
    public string riddleQuestionFrench;
    public string riddleQuestionArabic;
    public string riddleQuestionGreek;
    public string descriptionEnglish;
    public string descriptionFrench;
    public string descriptionArabic;
    public string descriptionGreek;
    public string linkEnglish;
    public string linkFrench;
    public string linkArabic;
    public string linkGreek;
}