using UnityEngine;
using TMPro;
using Fungus;

public class Counter: MonoBehaviour
{
	public GameObject timer;

    public Flowchart flowchart;

	private float startTime = 30f;
	private float currentTime;
	private float endTime = 1f;

	public TextMeshProUGUI counterText;
    private bool counterOn;

    // Start is called before the first frame update
    public void Start()
    {
        timer.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Begin()
    {
        // Timer bar starts growing
        timer.transform.localScale = new Vector3(1, 1, 1);
        AnimateTimer();
        currentTime = startTime;
        counterOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Counter integer value grows up to 60 seconds
        if (counterOn && currentTime > 1f)
        {
            currentTime -= 1 * Time.deltaTime;
            int intTime = (int) currentTime;
            counterText.SetText(intTime.ToString());
            if (intTime == 0)
            {
                TimeUp();
            }
        }
    }

    public void TimeUp()
    {
        flowchart.ExecuteBlock("TimeUp");
    }

    // Makes timer bar object grow to its full size in duration endTime
    public void AnimateTimer()
    {
        LeanTween.scaleX(timer, 0, startTime);
    }

    // Stops the timer bar from growing
    public void StopTimerAnimation()
    {
        LeanTween.pause(timer);
    }

    public void StopCounter()
    {
        // Stops increasing the counter integer value
        counterOn = false;

        // Set the time it took for user to answer the question
        GameStatus gs = GetGameStatus();
        gs.SetQuestionTime((int) currentTime);
    }

    private GameStatus GetGameStatus()
    {
        GameObject go = GameObject.Find("GameStatus");
        if (go == null)
        {
            Debug.LogError("Failed to find an object named 'Game Status'");
            this.enabled = false;
        }

        GameStatus gs = go.GetComponent<GameStatus>();
        return gs;
    }
}
