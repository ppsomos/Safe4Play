using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayButtonBM : MonoBehaviour
{

    public TextMeshProUGUI loadingText;
    public Image missConception;
    bool qReady = false;

    public void Awake()
    {
        StartCoroutine(WaitForQuestions());
    }

    public void PlayGame()
    {
        Debug.Log("Pressed");
        SceneManager.LoadScene("QuestionBM");
    }

    IEnumerator WaitForQuestions()
    {
        yield return new WaitWhile(() => !qReady);
        // After questions are ready, remove loading object and make Play button appear
        loadingText.gameObject.SetActive(false);
        this.gameObject.GetComponent<Button>().interactable = true;
        missConception.enabled = true;
    }

    // Called in every frame
    void Update()
    {
        GameStatusBM gs = GetGameStatus();

        // Check if questions have been retrieved and stored in list
        if (!qReady && gs.questions.Length > 0)
        {
            qReady = !(gs.questions[gs.questions.Length - 1] is null);
        }
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
