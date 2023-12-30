using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    static public string uid;
    static public string customToken;
    static public string uri;
    static public string topicPk;
    static public string activeLanguage;
    static public bool is_assessment_quiz = false;

    public int score = 0;
    private DateTime startTimestamp;
    private DateTime endTimestamp;
    private List<int> questionTimes;
    private List<int> answerValues;
    public Question[] questions;

    //[DllImport("__Internal")]
    //private static extern void OnGameStarted();

    //[DllImport("__Internal")]
    //private static extern void LoadLearnPage();

    void Awake()
    {
        // Don't destroy the GameStatus game object as it holds the user data and questions
        // Preserves data between Menu scene and Question scene
        DontDestroyOnLoad(this);

        topicPk = "2";
        PlayerPrefs.SetString(" language", " en ");
        //PlayerPrefs.SetString("topic", "HeatDestroysCondoms");
        //PlayerPrefs.SetString("language", "ar");

        LoadQuestions();

        //OnGameStarted();
    }

    public void ExitGame()
    {
        //Application.Quit();
        GameManager.Instance.isHouseGamePlay = true;
        GameManager.Instance.fromMiniGame = true;
        GameManager.Instance.ChangeScene("Loading");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadQuestions()
    {
        // A list that holds the time in seconds it took to the user to answer each question
        questionTimes = new List<int>();
        // A list that holds boolean values to show which questions the user answered correctly
        answerValues = new List<int>();

        NetworkManager nm = GetNetworkManager();

        nm.SetJsonUrl();
        nm.GetQuizQuestions();
    }

    public void LoadWebPage()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
       //     LoadLearnPage();
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void SetTopicPk(string topic)
    {
        topicPk = "2";
    }

    public void SetActiveLanguage(string language)
    {
        activeLanguage = language;
    }

    public void SetStartTimestamp()
    {
        startTimestamp = System.DateTime.Now;
    }

    public void SetAssessmentQuiz()
    {
        is_assessment_quiz = true;
    }

    public void SetUid(string uidArg)
    {
        uid = uidArg;
    }

    public void SetCustomToken(string token)
    {
        customToken = token;
    }

    public void SetUri(string uriArg)
    {
        uri = uriArg;
    }

    // Create an array of the correct size to store the questions
    public void CreateQuestionList(int num)
    {
        questions = new Question[num];
    }

    public void AddScore()
    {
        score++;
    }

    /*public void PostToDatabase()
    {
        endTimestamp = System.DateTime.Now;

        RestClient.DefaultRequestHeaders["Authorization"] = customToken;
        UserData userData = new UserData();

        RestClient.Request(new RequestHelper
        {
            Uri = uri,
            Method = "POST",
            Timeout = 10,
            Body = userData, //Serialize object using JsonUtility by default
            ContentType = "application/json", //JSON is used by default
        }).Then(response => {
            //EditorUtility.DisplayDialog("Status", response.StatusCode.ToString(), "Ok");
        }).Catch(err => {
            var error = err as RequestException;
            Debug.Log(error.Response);
            //EditorUtility.DisplayDialog("Error Response", error.Response, "Ok");
        });
    }*/

    // Getters and setters
    public string GetUid()
    {
        return uid;
    }

    public bool GetAssessmentQuiz()
    {
        return is_assessment_quiz;
    }

    private NetworkManager GetNetworkManager()
    {
        GameObject go = GameObject.Find("NetworkManager");
        if (go == null)
        {
            Debug.LogError("Failed to find an object named 'NetworkManager'");
            return null;
        }
        NetworkManager nm = go.GetComponent<NetworkManager>();
        return nm;
    }

    public string GetTopicPk()
    {
        return topicPk;
    }

    public string GetActiveLanguage()
    {
        return activeLanguage;
    }

    public float GetScorePercentage()
    {
        return score*100/questions.Length;
    }

    public DateTime GetStartTimestamp()
    {
        return startTimestamp;
    }

    public DateTime GetEndTimestamp()
    {
        return endTimestamp;
    }

    public Question[] GetQuestions()
    {
        return questions;
    }

    public List<int> GetQuestionTimes()
    {
        return questionTimes;
    }

    public List<int> GetAnswerValues()
    {
        return answerValues;
    }
    public void SetQuestionTime(int time)
    {
        questionTimes.Add(time);
    }

    public void SetAnswer(int value)
    {
        answerValues.Add(value);
    }
}
