using System.Collections;
using System;
using UnityEngine;
//using Proyecto26;
using UnityEngine.Networking;

public class NetworkManagerBM : MonoBehaviour
{
    public static NetworkManagerBM Instance;

    // Change this url to get the required quiz questions
    public string jsonUrl;
    public GameData GData;
    public int numberQ;
    GameObject go;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        PlayerPrefs.SetString("language", GData.selectLanguage);
    }

    public void SetJsonUrl()
    {
        GameStatusBM gs = GetGameStatus();
        /*string topicPk = gs.GetTopicPk();
        string activeLanguage = gs.GetActiveLanguage();
        bool assessment_quiz = gs.GetAssessmentQuiz();
        if (assessment_quiz)
        {
            jsonUrl = "https://sguide-a136c-data.europe-west1.firebasedatabase.app/assessment_quiz/" + activeLanguage;
        } else
        {
            jsonUrl = "https://sguide-a136c-data.europe-west1.firebasedatabase.app/topicsLoc/" + activeLanguage + "/topic" + topicPk + "/quizzes/quiz1";
        }*/
        jsonUrl = "https://safe4play-468d0-default-rtdb.europe-west1.firebasedatabase.app/MissConception/" +
                  PlayerPrefs.GetString("language");
    }

    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();

            callback(request);
        }
    }

    // Get the number of questions for this quiz and call to get individual questions
    public void GetQuizQuestions()
    {
        // Debug.Log("Getting questions");
        go = GameObject.Find("GameStatus");
        if (go == null)
        {
            Debug.LogError("Failed to find an object named 'Game Status'");
            this.enabled = false;
        }

        StartCoroutine(GetRequest(jsonUrl + ".json", (UnityWebRequest req) =>
        {
            if (req.result == UnityWebRequest.Result.ConnectionError ||
                req.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                string jsonString = req.downloadHandler.text;


                Debug.Log(jsonString);
                // Parse the retrieved json string to get the number of questions
                int beginIndex = jsonString.IndexOf("numberOfQuestions", StringComparison.Ordinal) + 19;
                int endIndex = jsonString.IndexOf(',', beginIndex);
                int num = int.Parse(jsonString.Substring(beginIndex, endIndex - beginIndex));
                numberQ = num;

                // print("Question num " + numberQ);
                // print("Begin index " + beginIndex);
                // print("end index " + endIndex);
                // Create a list in the GameStatus object to hold the questions of size numberQ
                GameStatusBM gs = go.GetComponent<GameStatusBM>();
                gs.CreateQuestionList(numberQ);

                // Get individual questions
                for (int i = 1; i <= numberQ; i++)
                {
                    GetQuestion(i);
                }
            }
        }));
    }

    //https://safe4play-468d0-default-rtdb.europe-west1.firebasedatabase.app/MissConception/
    // Retrieve an individual question from the database
    public void GetQuestion(int id)
    {
        // print(id);
        // string url = "https://safe4play-468d0-default-rtdb.europe-west1.firebasedatabase.app/MissConception/ar/";
        StartCoroutine(GetRequest(
            jsonUrl + "/questions/question" + id + ".json",
            (UnityWebRequest req) =>
            {
                if (req.result == UnityWebRequest.Result.ConnectionError ||
                    req.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log($"{req.error}: {req.downloadHandler.text}");
                }
                else
                {
                    string jsonString = req.downloadHandler.text;
                    print(jsonString);
                    // Parse the question to store the question string and the options
                    QuestionParserBM parser = new QuestionParserBM();
                    QuestionBM q = parser.ConvertString(jsonString);
                    print(q.questionString);
                    q.questionId = id - 1;

                    // Store the question in the GameStatus object
                    GameStatusBM gs = go.GetComponent<GameStatusBM>();
                    gs.questions[q.questionId] = q;
                }
            }));
    }

    private GameStatusBM GetGameStatus()
    {
        GameObject go = GameObject.Find("GameStatus");
        if (go == null)
        {
            Debug.LogError("Failed to find an object named 'GameStatus'");
            return null;
        }

        GameStatusBM gs = go.GetComponent<GameStatusBM>();
        return gs;
    }
}