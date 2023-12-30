using System;
using UnityEngine;
//using Newtonsoft.Json.Linq;

[Serializable]
public class UserData
{
    public string user;
    public string topic;
    public string start_timestamp;
    public string end_timestamp;
    public string answer_values;
    public string answer_times;
    public float score_percentage;
    public string is_assessment_quiz;

    public UserData()
    {
        /*GameStatus gs = GetGameStatus();
        user = gs.GetUid();
        topic = gs.GetTopicPk();
        start_timestamp = Newtonsoft.Json.JsonConvert.SerializeObject(gs.GetStartTimestamp()).Replace("\"", "");
        end_timestamp = Newtonsoft.Json.JsonConvert.SerializeObject(gs.GetEndTimestamp()).Replace("\"", "");
        answer_values = Newtonsoft.Json.JsonConvert.SerializeObject(gs.GetAnswerValues());
        answer_times = Newtonsoft.Json.JsonConvert.SerializeObject(gs.GetQuestionTimes());
        score_percentage = gs.GetScorePercentage();
        is_assessment_quiz = gs.GetAssessmentQuiz().ToString();*/
    }

    private GameStatus GetGameStatus()
    {
        GameObject go = GameObject.Find("GameStatus");
        if (go == null)
        {
            Debug.LogError("Failed to find an object named 'GameStatus'");
            return null;
        }
        GameStatus gs = go.GetComponent<GameStatus>();
        return gs;
    }
}
