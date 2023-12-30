using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreBM : MonoBehaviour
{
    public TextMeshProUGUI ScoreNumber;
    public TextMeshProUGUI ContinueText;
    public TextMeshProUGUI ScoreTitle;

    void Start()
    {
        GameStatusBM gs = GetGameStatus();
        string activeLanguage = gs.GetActiveLanguage();
        if (activeLanguage == "el") {
            ContinueText.SetText("ΣΥΝΕΧΙΣΕ");
            ScoreTitle.SetText("ΒΑΘΜΟΛΟΓΙΑ");
        }
        ShowScore(gs);
        // Posts all user stats on the database
        //gs.PostToDatabase();
    }

    // Shows score on the score scene
    public void ShowScore(GameStatusBM gs)
    {
        ScoreNumber.SetText(Mathf.Round(gs.GetScorePercentage()).ToString() + "%");
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
