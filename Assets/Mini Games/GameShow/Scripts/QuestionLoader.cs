using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Fungus;
using UnityEngine.Networking;
using ArabicSupport;
using System.Collections;

public class QuestionLoader : MonoBehaviour
{
	private int currentQuestion = 0;
	public List<Question> questions;

	public GameObject score;
	public List<GameObject> peaches;
	public Sprite correctPeach;
	public Sprite wrongPeach;

	public Sprite background;

	public GameObject NextObject;

	[SerializeField]
	private TextMeshProUGUI questionText;

	[SerializeField]
	private GameObject questionBox;

	[SerializeField]
	private GameObject correctButton;

	[SerializeField]
	private GameObject wrongButton;

	public int selectedAnswer;
	public int correctAnswer;

	public Button A;
	public Button B;
	public Button C;
	public Button D;

	public Button sound;
	public Button exit;

	public bool correct;

	public Flowchart flowchart;
	public SayDialog sayDialog;

	public Image leftPanel;
	public Image rightPanel;

	string language;

	string topic;

	// Called at the first frame of scene 'Question'
    void Start()
    {
		correct = false;
		DisableButtons();

		topic = "HeatDestroysCondoms";

		language = PlayerPrefs.GetString("language", "en");
		Debug.Log(language);
		FindObjectOfType<Localization>().SetActiveLanguage(language.ToUpper());
	}

	public void BeginGame()
    {
		// Create a list with the quiz questions
		questions = new List<Question>();
		GameStatus gs = GetGameStatus();
		questions = gs.GetQuestions().ToList();

		// Add method listener to the Next button
		Button nextButton = NextObject.GetComponentInChildren<Button>();
		nextButton.onClick.AddListener(NextQuestion);

		// Display the first question
		DisplayQuestion(0);


		// Display score bananas
		int numberOfQuestions = questions.Count;

		score.SetActive(true);
		for (int i = 0; i < numberOfQuestions; i++)
		{
			peaches[i].SetActive(true);
		}
	}

	public void SetQuestionImage()
    {
		leftPanel.sprite = Resources.Load<Sprite>(topic + "/" + currentQuestion.ToString());
    }

	public void SetAnswerImage()
    {
		var sprite = Resources.Load<Sprite>(topic + "/" + currentQuestion.ToString() + "." + selectedAnswer.ToString());
		if (sprite)
        {
			rightPanel.sprite = sprite;
		} else
        {
			rightPanel.sprite = background;
        }
		
    }

	public void SetCorrectAnswerImage()
	{
		var sprite = Resources.Load<Sprite>(topic + "/" + currentQuestion.ToString() + "." + correctAnswer.ToString());
		if (sprite)
		{
			rightPanel.sprite = sprite;
		}
		else
		{
			rightPanel.sprite = background;
		}
	}

	public void ResetAnswerImage()
    {
		rightPanel.sprite = background;
    }

	public void ShowAnswer()
    {
		GameObject opt = GameObject.FindGameObjectWithTag("Correct");
		opt.GetComponent<Image>().color = Color.green;

		if (correct)
        {
			flowchart.ExecuteBlock("Correct");
		} else
        {
			flowchart.ExecuteBlock("Wrong");
        }

		ShowFeedback();
		UpdateScore(correct);
	}

	public void ShowFeedback()
    {
		string initialString = questions[currentQuestion].feedback[selectedAnswer];
		if (initialString.Length > 230)
        {
			string temp = initialString.Substring(0, 230);
			int x = temp.LastIndexOf(".", StringComparison.Ordinal);

			string s1 = initialString.Substring(0, x + 1);
			string s2 = initialString.Substring(x + 1);

			flowchart.SetStringVariable("feedback1", s1);
			flowchart.SetStringVariable("feedback2", s2);
			if (currentQuestion == questions.Count - 1)
			{
				flowchart.ExecuteBlock("EndLong");
			} else
            {
				flowchart.ExecuteBlock("ShowFeedbackLong");
			}
			
		} else
        {
			flowchart.SetStringVariable("feedback1", initialString);
			if (currentQuestion == questions.Count - 1)
			{
				flowchart.ExecuteBlock("EndShort");
			}
			else
			{
				flowchart.ExecuteBlock("ShowFeedbackShort");
			}
		}
	}

	public void FixArabicLineOrder(GameObject storyText)
	{
		if (language == "ar")
		{
            Text myText = storyText.GetComponent<Text>();
            string[] lineArray = new string[myText.cachedTextGenerator.lines.Count];

            Canvas.ForceUpdateCanvases();
            for (int i = 0; i < myText.cachedTextGenerator.lines.Count; i++)
            {
                int startIndex = myText.cachedTextGenerator.lines[i].startCharIdx;
                int endIndex = (i == myText.cachedTextGenerator.lines.Count - 1) ? myText.text.Length
                    : myText.cachedTextGenerator.lines[i + 1].startCharIdx;
                int length = endIndex - startIndex;
                lineArray[i] = myText.text.Substring(startIndex, length);
            }

            Array.Reverse(lineArray, 0, lineArray.Length);

            myText.text = String.Join("\n", lineArray);
        }
    }

    public void ShowNextButton()
    {
		NextObject.SetActive(true);
		A.gameObject.SetActive(false);
		B.gameObject.SetActive(false);
		C.gameObject.SetActive(false);
		D.gameObject.SetActive(false);
	}

	public void SelectAnswer(int answer)
    {
		selectedAnswer = answer;
		DisableButtons();
		Counter counter = GetCounter();
		counter.StopCounter();
		counter.StopTimerAnimation();
		Color orange = new Color(1f, 0.7f, 0.3f);


		if (answer == correctAnswer)
        {
			GameObject opt = GameObject.FindGameObjectWithTag("Correct");
			opt.GetComponent<Image>().color = orange;
			correct = true;
		}
		else
        {
			GameObject opt = GameObject.Find("OptionButtonWrong"+ answer.ToString());
			opt.GetComponent<Image>().color = orange;
			correct = false;
		}

		SetAnswerImage();

		flowchart.ExecuteBlock("SelectAnswer");
	}

	public void TimeUp()
	{
        DisableButtons();
        correct = false;
        flowchart.ExecuteBlock("TimeUp1");
    }

	public void DisableButtons()
    {
		Button[] buttons = GameObject.FindObjectsOfType<Button>();
		foreach (Button b in buttons)
		{
			b.interactable = false;
		}
		sound.interactable = true;
		exit.interactable = true;
	}

	public void EnableButtons()
	{
		Button[] buttons = GameObject.FindObjectsOfType<Button>();
		foreach (Button b in buttons)
		{
			b.interactable = true;
		}
	}

	public void DisplayQuestion(int id)
	{
		// Display the question string
        questionText.text = questions[id].questionString;

		// Hide the 'Next' button until the question is answered
		NextObject.SetActive(false);

		// Create the option buttons for the current question
		InitialiseOptions(id);

		SetQuestionImage();
		ResetAnswerImage();

		EnableButtons();
	}

	// Creates position vectors that correspons to the number of options of the current question
	private Vector2[] GetPositions(int num)
    {
		Vector2[] positions = default;

		if (num == 2)
		{
			Vector2 option1 = new Vector2(-175, 0);
			Vector2 option2 = new Vector2(175, 0);
			positions = new Vector2[] { option1, option2 };
		}
		else if (num == 3)
		{
			Vector2 option1 = new Vector2(-167, 60);
			Vector2 option2 = new Vector2(175, 60);
			Vector2 option3 = new Vector2(0, -80);
			positions = new Vector2[] { option1, option2, option3 };
		}
		else if (num == 4)
        {
			Vector2 option1 = new Vector2(-180, 60);
			Vector2 option2 = new Vector2(180, 60);
			Vector2 option3 = new Vector2(-180, -60);
			Vector2 option4 = new Vector2(180, -60);
			positions = new Vector2[] { option1, option2, option3, option4 };
		}
		return positions;
	}

	public void InitialiseOptions(int id)
    {
		// Create a list with the options and get positions for buttons
		IList<OptionData> options = questions[id].options;
		int numberOfOptions = options.Count;
		Vector2[] positions = GetPositions(numberOfOptions);

		// Counter used to create different names for option buttons
		int optionCount = 0;

		GameObject optionObject = GameObject.Find("Options");

		foreach (OptionData opt in options)
        {
			opt.id = optionCount;
			// Correct button initialisation
			if (opt.value)
            {
				correctAnswer = optionCount;
				// Instatiate correctButton prefab under panel
				GameObject optionCorrect = Instantiate(correctButton, optionObject.transform);
				optionCorrect.transform.localPosition = positions[optionCount];
				// Name identifiers for button
				optionCorrect.name = "OptionButtonCorrect" + optionCount.ToString();
				optionCorrect.tag = "Correct";

				// Get text object of option
				GameObject optionText = GameObject.Find("/Canvas/Panel/Options/" + optionCorrect.name + "/OptionTextCorrect");
				// Create unique identifier name
				optionText.name = "OptionTextCorrect" + optionCount.ToString();
				// Get the text item and set the string value
				TextMeshProUGUI text = optionText.GetComponentInChildren<TextMeshProUGUI>();
				if (language == "ar")
				{
                    //text.SetText(ArabicFixer.Fix(opt.optionText));
                    text.SetText(opt.optionText);
                }
				else
				{
                    text.SetText(opt.optionText);
                }

				// Get the value object (Correct/Wrong) that pops up when the user answers
				GameObject optionValue = GameObject.Find("/Canvas/Panel/Options/" + optionCorrect.name + "/OptionValueCorrect");
				optionValue.name = "OptionValueCorrect" + optionCount.ToString();
				// The value text object is disabled until the user answers
				TextMeshProUGUI value = optionValue.GetComponentInChildren<TextMeshProUGUI>();
				value.enabled = false;

				// Add listener to button
				Button buttonCorrect = optionCorrect.GetComponentInChildren<Button>();
				buttonCorrect.onClick.AddListener(delegate { OnOptionClick(buttonCorrect, optionCorrect.name, opt.value, optionValue.name, opt.id); });
			}
			// Wrong buttons initialisation
			else
            {
				GameObject optionWrong = Instantiate(wrongButton, optionObject.transform);
				optionWrong.transform.localPosition = positions[optionCount];
				optionWrong.name = "OptionButtonWrong" + optionCount.ToString();
				optionWrong.tag = "Wrong";
				
				var optionText = GameObject.Find("/Canvas/Panel/Options/"+ optionWrong.name +"/OptionTextWrong");
				optionText.name = "OptionWrong" + optionCount.ToString();
				TextMeshProUGUI text = optionText.GetComponentInChildren<TextMeshProUGUI>();
                if (language == "ar")
                {
                    //text.SetText(ArabicFixer.Fix(opt.optionText));
                    text.SetText(opt.optionText);
                }
                else
                {
                    text.SetText(opt.optionText);
                }

				var optionValue = GameObject.Find("/Canvas/Panel/Options/" + optionWrong.name + "/OptionValueWrong");
				optionValue.name = "OptionValueWrong" + optionCount.ToString();
				TextMeshProUGUI value = optionValue.GetComponentInChildren<TextMeshProUGUI>();
				value.enabled = false;

				Button buttonWrong = optionWrong.GetComponentInChildren<Button>();
				buttonWrong.onClick.AddListener(delegate { OnOptionClick(buttonWrong, optionWrong.name, opt.value, optionValue.name, opt.id); });
			}
			optionCount++;
		}

	}

	// The option buttons have this method as a listener
	private void OnOptionClick(Button buttonClicked, string textName, bool val, string valName, int id)
	{
		DisableButtons();
		Counter counter = GetCounter();
		counter.StopCounter();
		counter.StopTimerAnimation();

		SelectAnswer(id);
	}

	private void NextQuestion()
    {
		// Load the score scene if this was the last question
		if (currentQuestion == questions.Count-1)
        {
			Debug.Log("questionsUp");
			flowchart.ExecuteBlock("End");
		} else
        {
			// Destroy the option buttons and display the next question
			GameObject opt = GameObject.FindGameObjectWithTag("Correct");
			Destroy(opt);
			
			GameObject[] opts = GameObject.FindGameObjectsWithTag("Wrong");
			for (int i = 0; i < opts.Length; i++)
			{
				Destroy(opts[i]);
			}
			currentQuestion++;
			DisplayQuestion(currentQuestion);

			// Start the counter for the next question
			Counter counter = GetCounter();
			counter.Begin();
		}
		A.gameObject.SetActive(true);
		B.gameObject.SetActive(true);
		C.gameObject.SetActive(true);
		D.gameObject.SetActive(true);
		EnableButtons();
	}

	private Counter GetCounter()
	{
		GameObject go = GameObject.Find("Counter");
		if (go == null)
		{
			Debug.LogError("Failed to find an object named 'Counter'");
			this.enabled = false;
		}
		Counter counter = go.GetComponent<Counter>();
		return counter;
	}

	public void UpdateScore(bool val)
    {
		GameStatus gs = GetGameStatus();
	
		if (val)
        {
			peaches[currentQuestion].GetComponent<Image>().sprite = correctPeach;
			gs.AddScore();
		}
		else
        {
			peaches[currentQuestion].GetComponent<Image>().sprite = wrongPeach;
        }
		SetCorrectAnswerImage();
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

[Serializable]
public class Question
{
	public int questionId;
	public string questionString;
	public IList<OptionData> options;
	public IList<string> feedback;

	public Question()
	{
		options = new List<OptionData>();
		feedback = new List<string>();
	}
}
