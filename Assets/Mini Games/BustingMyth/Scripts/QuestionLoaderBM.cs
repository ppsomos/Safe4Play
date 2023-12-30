using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Fungus;
using System.Data;
using ArabicSupport;
using RTLTMPro;

public class QuestionLoaderBM : MonoBehaviour
{
    private int currentQuestion = 0;
    public List<QuestionBM> questions;
    public List<QuestionBM> allQuestions;

    public Image questionBackground;

    bool statementTrue;

    public GameObject NextObject;

    public Flowchart flowchart;

    public GameObject questionObject;

    [SerializeField]
    private RTLTextMeshPro questionText;

    [SerializeField]
    private GameObject questionBox;

    [SerializeField]
    private GameObject correctButton;

    [SerializeField]
    private GameObject wrongButton;

    [SerializeField]
    private GameObject flags;

    public TextMeshProUGUI explanation;

    public TextMeshProUGUI questionNumber;

    string language;

    // Called at the first frame of scene 'Question'
    void Start()
    {
        language = PlayerPrefs.GetString("language");
        // Create a list with the quiz questions
        allQuestions = new List<QuestionBM>();
        GameStatusBM gs = GetGameStatus();
        allQuestions = gs.GetQuestions().ToList();
        for (int i = 0; i < allQuestions.Count; i++)
        {
            QuestionBM temp = allQuestions[i];
            int randomIndex = UnityEngine.Random.Range(i, allQuestions.Count);
            allQuestions[i] = allQuestions[randomIndex];
            allQuestions[randomIndex] = temp;
        }
        questions = new List<QuestionBM>(allQuestions.Take(20));

        // Add method listener to the Next button
        Button nextButton = NextObject.GetComponentInChildren<Button>();
        nextButton.onClick.AddListener(NextQuestion);

        // Display the first question
        DisplayQuestion(0);
    }

    public void DisplayQuestion(int id)
    {
        // Display the question string
        questionText.text = questions[id].questionString;

        questionNumber.text = (id + 1).ToString() + "/20";

        // Create the option buttons for the current question
        InitialiseButtons(id);

        // Hide the 'Next' button until the question is answered
        NextObject.SetActive(false);


    }

    public void ShowFeedback()
    {
        string initialString = questions[currentQuestion].explanation;
        if (initialString.Length > 440)
        {
            string temp = initialString.Substring(0, 440);
            int x = temp.LastIndexOf(".", StringComparison.Ordinal);

            string s1 = initialString.Substring(0, x + 1);
            string s2 = initialString.Substring(x + 1);

            flowchart.SetStringVariable("feedback1", s1);
            flowchart.SetStringVariable("feedback2", s2);
            if (currentQuestion == questions.Count - 1)
            {
                flowchart.ExecuteBlock("EndLong");
                Debug.Log("111");
                ///NextQuestion();
            }
            else
            {
                Debug.Log("222");
                flowchart.ExecuteBlock("ShowFeedbackLong");
            }

        }
        else
        {
            flowchart.SetStringVariable("feedback1", initialString);
            if (currentQuestion == questions.Count - 1)
            {
                Debug.Log("333");
                flowchart.ExecuteBlock("EndShort");
               // NextQuestion();
            }
            else
            {
                Debug.Log("4444");
                flowchart.ExecuteBlock("ShowFeedbackShort");
            }
        }
    }

    // Creates position vectors that correspons to the number of options of the current question
    private Vector2[] GetPositions(int num)
    {
        Vector2[] positions = default;

        if (num == 2)
        {
            Vector2 option1 = new Vector2(-150, 0);
            Vector2 option2 = new Vector2(150, 0);
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
            Vector2 option1 = new Vector2(-167, 60);
            Vector2 option2 = new Vector2(175, 60);
            Vector2 option3 = new Vector2(-167, -80);
            Vector2 option4 = new Vector2(175, -80);
            positions = new Vector2[] { option1, option2, option3, option4 };
        }
        return positions;
    }

    public void InitialiseButtons(int id)
    {
        // Create a list with the options and get positions for buttons
        IList<OptionDataBM> options = questions[id].options;
        int numberOfOptions = options.Count;
        Vector2[] positions = GetPositions(numberOfOptions);

        // Counter used to create different names for option buttons
        int optionCount = 0;

        GameObject optionObject = GameObject.Find("Options");

        foreach (OptionDataBM opt in options)
        {
            // Correct button initialisation
            if (opt.value)
            {
                // Instatiate correctButton prefab under panel
                GameObject optionCorrect = Instantiate(correctButton, optionObject.transform);
                optionCorrect.transform.localPosition = positions[optionCount];
                // Name identifiers for button
                optionCorrect.name = "OptionButtonCorrect" + optionCount.ToString();
                optionCorrect.tag = "Option";

                // Get text object of option
                GameObject optionText = GameObject.Find("/Canvas/Panel/Options/" + optionCorrect.name + "/OptionTextCorrect");
                // Create unique identifier name
                optionText.name = "OptionTextCorrect" + optionCount.ToString();
                // Get the text item and set the string value
                TextMeshProUGUI text = optionText.GetComponentInChildren<TextMeshProUGUI>();
                if (language == "ar")
                {
                    text.SetText(ArabicFixer.Fix(opt.optionText));
                }
                else
                {
                    text.SetText(opt.optionText);
                }

                // Get the value object (Correct/Wrong) that pops up when the user answers
                //GameObject optionValue = GameObject.Find("/Canvas/Panel/Options/" + optionCorrect.name + "/OptionValueCorrect");
                //optionValue.name = "OptionValueCorrect" + optionCount.ToString();
                // The value text object is disabled until the user answers
                //TextMeshProUGUI value = optionValue.GetComponentInChildren<TextMeshProUGUI>();
                //value.enabled = false;

                // Add listener to button
                Button buttonCorrect = optionCorrect.GetComponentInChildren<Button>();
                buttonCorrect.onClick.AddListener(delegate { OnOptionClick(buttonCorrect, optionCorrect.name, opt.value); });

                if (optionCount == 0)
                {
                    statementTrue = true;
                }
            }
            // Wrong buttons initialisation
            else
            {
                GameObject optionWrong = Instantiate(wrongButton, optionObject.transform);
                optionWrong.transform.localPosition = positions[optionCount];
                optionWrong.name = "OptionButtonWrong" + optionCount.ToString();
                optionWrong.tag = "Option";

                var optionText = GameObject.Find("/Canvas/Panel/Options/" + optionWrong.name + "/OptionTextWrong");
                optionText.name = "OptionWrong" + optionCount.ToString();
                TextMeshProUGUI text = optionText.GetComponentInChildren<TextMeshProUGUI>();
                if (language == "ar")
                {
                    text.SetText(ArabicFixer.Fix(opt.optionText));
                }
                else
                {
                    text.SetText(opt.optionText);
                }

                //var optionValue = GameObject.Find("/Canvas/Panel/Options/" + optionWrong.name + "/OptionValueWrong");
                //optionValue.name = "OptionValueWrong" + optionCount.ToString();
                //TextMeshProUGUI value = optionValue.GetComponentInChildren<TextMeshProUGUI>();
                //value.enabled = false;

                Button buttonWrong = optionWrong.GetComponentInChildren<Button>();
                buttonWrong.onClick.AddListener(delegate { OnOptionClick(buttonWrong, optionWrong.name, opt.value); });
            }
            optionCount++;
        }

    }

    // The option buttons have this method as a listener
    private void OnOptionClick(Button button, string textName, bool val)
    {
        //NextObject.SetActive(true);

        GameStatusBM gs = GetGameStatus();
        if (val)
        {
            gs.SetAnswer(1);
            gs.AddScore();
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            gs.SetAnswer(0);
            button.GetComponent<Image>().color = Color.red;
        }

        if (statementTrue)
        {
            questionBackground.color = Color.green;
        }
        else
        {
            questionBackground.color = Color.red;
        }
        statementTrue = false;

        // Disable all buttons except the 'Next' button
        Button[] buttons = GameObject.FindObjectsOfType<Button>();
        foreach (Button b in buttons)
        {
            b.interactable = false;
        }

        //button.GetComponent<Image>().color = new Color32(100,100,100,200);

        // Stop the counter
        //Counter counter = GetCounter();
        //counter.StopCounter();
        //counter.StopTimerAnimation();

        //explanation.gameObject.SetActive(true);
        ShowFeedback();
        //nextButton.interactable = true;
        if (questions[currentQuestion].externalContent.Length > 2)
        {
            flags.SetActive(true);
        }
    }

    public void ExternalContent()
    {
        string url = questions[currentQuestion].externalContent;
        Application.OpenURL(url);
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

    private void NextQuestion()
    {
        questionBackground.color = new Color32(248, 186, 48, 255);
        flags.SetActive(false);
        // Load the score scene if this was the last question
        if (currentQuestion == 19)
        {
            Debug.Log("questionsUp");
            SceneManager.LoadScene("Score");
        }
        else
        {
            // Destroy the option buttons and display the next question
            GameObject[] opts = GameObject.FindGameObjectsWithTag("Option");
            for (int i = 0; i < opts.Length; i++)
            {
                Destroy(opts[i]);
            }
            currentQuestion++;
            DisplayQuestion(currentQuestion);

          //  Start the counter for the next question

           Counter counter = GetCounter();
            counter.Start();
        }
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

[Serializable]
public class QuestionBM
{
    public int questionId;
    public string questionString;
    public string explanation;
    public string externalContent;
    public IList<OptionDataBM> options;

    public QuestionBM()
    {
        options = new List<OptionDataBM>();
    }
}
