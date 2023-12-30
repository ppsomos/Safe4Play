using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayButton : MonoBehaviour
{

    public TextMeshProUGUI loadingText;
    bool qReady = false;

    public void PlayGame()
    {
        loadingText.enabled = true;
        StartCoroutine(WaitForQuestions());
    }

    IEnumerator WaitForQuestions()
    {
        yield return new WaitWhile(() => !qReady);
        // After questions are ready, remove loading object and make Play button appear
        loadingText.enabled = false;
        SceneManager.LoadScene("GameShow");
    }

    // Called in every frame
    void Update()
    {
        GameStatus gs = GetGameStatus();

        // Check if questions have been retrieved and stored in list
        if (!qReady && gs.questions.Length > 0)
        {
            qReady = !(gs.questions[gs.questions.Length - 1] is null);
        }
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
