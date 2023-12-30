using UnityEngine;
using TMPro;

public class CounterBM : MonoBehaviour
{
	public GameObject timer;

	private float startTime = 1f;
	private float currentTime;
	private float endTime = 30f;

	public TextMeshProUGUI counterText;
    private bool counterOn;

    // Start is called before the first frame update
    public void Start()
    {
        timer.transform.localScale = new Vector3(0, 1, 1);
        // Timer bar starts growing
        AnimateTimer();
        currentTime = startTime;
        counterOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Counter integer value grows up to 60 seconds
        if (counterOn && currentTime < 60f)
        {
            currentTime += 1 * Time.deltaTime;
            int intTime = (int) currentTime;
            counterText.SetText(intTime.ToString());
        }
       
    }

    // Makes timer bar object grow to its full size in duration endTime
    public void AnimateTimer()
    {
        LeanTween.scaleX(timer, 1, endTime);
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
        GameStatusBM gs = GetGameStatus();
        gs.SetQuestionTime((int) currentTime);
    }

    private GameStatusBM GetGameStatus()
    {
        GameObject go = GameObject.Find("GameStatus");
        if (go == null)
        {
            Debug.LogError("Failed to find an object named 'Game Status'");
            this.enabled = false;
        }

        GameStatusBM gs = go.GetComponent<GameStatusBM>();
        return gs;
    }
}
