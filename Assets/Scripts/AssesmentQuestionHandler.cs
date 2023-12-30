using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Data;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class AssesmentQuestionHandler : MonoBehaviour
{
    [SerializeField] GameData GData;
    [SerializeField] TMP_Text Question_Text;
    [SerializeField] TMP_Text OptionA_Text;
    [SerializeField] TMP_Text OptionB_Text;
    [SerializeField] TMP_Text OptionC_Text;
    [SerializeField] TMP_Text PathNo_Text;
    [SerializeField] GameObject  okBtn;
    [SerializeField] GameObject  resultText;
    [SerializeField] GameObject AssismentPage;
    [SerializeField] GameObject ResultPage;
    [SerializeField] Button[] OptionBtn;
    [SerializeField] string Paths;
    [SerializeField] Assesment_Question[] Question;
    [SerializeField] string objectToFind;
    [SerializeField] int PathNo;
    int AnswerQiven = 0;
    private void Awake()
    {

    }
    private void Start()
    {
        StartCoroutine(LoadQuestion(AnswerQiven));
        Invoke("PlayBackgroundSound", 1.5f);
    }
    IEnumerator LoadQuestion(int Q_No)
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < OptionBtn.Length; i++)
        {
            OptionBtn[i].interactable = true;
        }
        Question_Text.text = Question[Q_No].Question;
        OptionA_Text.text = Question[Q_No].Option_a;
        OptionB_Text.text = Question[Q_No].Option_b;
        OptionC_Text.text = Question[Q_No].Option_c;
    }
    public void AnswerBtnClcik(string option)
    {
       var Temp  = objectToFind + option;
        objectToFind = Temp;
        AnswerQiven++;
        for(int i = 0; i<OptionBtn.Length; i++)
        {
            OptionBtn[i].interactable = false;
        }
        if(AnswerQiven<11)
        {
            StartCoroutine(LoadQuestion(AnswerQiven));
        }
        else
        {
            if(objectToFind.Length==11)
            {
                okBtn.SetActive(false);
                AssismentPage.SetActive(false);
                ResultPage.SetActive(true);
                resultText.SetActive(true);
                PathNo_Text.gameObject.SetActive(false);
                Invoke("LoadReasult", 1f);
            }
        }
    }
    public void LoadReasult()
    {
       CheckResult(objectToFind);
    }
    public void CheckResult(string findobj)
    {
        for(int i = 0; i < GData.AllQuestion.Length;i++)
        {
            if (GData.AllQuestion[i].Name==findobj)
            {
                PathNo = GData.AllQuestion[i].R_Path;
                GameManager.Instance.Path_No = PathNo;
                Debug.Log("Object Find");
                //Debug.Log("Object Find ==" + values[13]);
                PathNo_Text.text = "Riddle Path number : ' " + PathNo.ToString() + "'";
                resultText.SetActive(false);
                PathNo_Text.gameObject.SetActive(true);
                okBtn.SetActive(true);
                return;
            }
        }
    }
    //private void CheckResult(string paths)
    //{
    //    string[] lines;
    //    string filePath = Path.Combine(Application.streamingAssetsPath, paths + ".csv");
    //    // Android requires special handling for accessing files in the StreamingAssets folder
    //    if (filePath.Contains("://"))
    //    {
    //        // Use WWW to load the file from the StreamingAssets folder on Android
    //        WWW www = new WWW(filePath);
    //        while (!www.isDone) { }
    //        if (!string.IsNullOrEmpty(www.error))
    //        {
    //            Debug.LogError("Failed to load file: " + www.error);
    //            return;
    //        }
    //        // Read the CSV data from the WWW object
    //        lines = www.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
    //        Debug.Log(filePath);
    //    }
    //    else
    //    {
    //        // On other platforms, directly load the file using the StreamingAssets path
    //        string streamingPath = Path.Combine(Application.streamingAssetsPath, paths + ".csv");
    //        if (Application.platform == RuntimePlatform.Android)
    //        {
    //            // Android specific handling to load files from StreamingAssets
    //            UnityWebRequest www = UnityWebRequest.Get(streamingPath);
    //            www.SendWebRequest();
    //            while (!www.isDone) { }
    //            if (www.result != UnityWebRequest.Result.Success)
    //            {
    //                Debug.LogError("Failed to load file: " + www.error);
    //                return;
    //            }
    //            // Read the CSV data from the UnityWebRequest
    //            lines = www.downloadHandler.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
    //            Debug.Log(filePath);
    //        }
    //        else
    //        {
    //            // For other platforms, load the file using regular file I/O
    //            if (File.Exists(streamingPath))
    //            {
    //                string csvData = File.ReadAllText(streamingPath);
    //                lines = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
    //                Debug.Log(filePath);
    //            }
    //            else
    //            {
    //                Debug.LogError("File does not exist: " + streamingPath);
    //                return;
    //            }
    //        }
    //        //Starting the Data Parse process!
    //        //Initialize arrays to store the data
    //        int rowCount = lines.Length - 1; // Subtracting 1 to exclude the header row
    //                                         // Debug.Log("rowCount==" + rowCount + paths);
    //        InitializeArrayToStoreData(objectToFind, rowCount, lines);
    //    }
    //}
    //private void InitializeArrayToStoreData(string data, int rowCount, string[] Lines)
    //{
    //    for(int i = 0; i <= rowCount; i++)
    //    {
    //        string[] values = Lines[i].Split(',');
    //      //  Debug.Log(i);
    //        if (values[13]== data)
    //        {
    //            PathNo = int.Parse(values[12]);
    //            GameManager.Instance.Path_No = PathNo;
    //          //  Debug.Log("Object Find");
    //            //Debug.Log("Object Find ==" + values[13]);
    //            PathNo_Text.text = "Riddle Path number : ' " + PathNo.ToString() + "'";
    //            resultText.SetActive(false);
    //            PathNo_Text.gameObject.SetActive(true);
    //            okBtn.SetActive(true);
    //            return;
    //        }
    //        else
    //        {
    //           // Debug.Log("!");
    //        }
    //    }
    //}
    public void PlayBackgroundSound()
    {
        SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.BgSound);
    }
    [System.Serializable]
    public class Assesment_Question
    {
        public string Q_NO;
        public string Question;
        public string Option_a;
        public string Option_b;
        public string Option_c;
    }
}