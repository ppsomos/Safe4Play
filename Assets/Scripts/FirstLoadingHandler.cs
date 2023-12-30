using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstLoadingHandler : MonoBehaviour
{
    [SerializeField] Image FillBar;
    [SerializeField] GameData GData;
    [SerializeField] GameObject Disclaimer_Page;
    [SerializeField] GameObject AssismentQuiez_Page;
    [SerializeField] GameObject Result_Page;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(CheckForFirstTime), 1f);
    }
    public void CheckForFirstTime()
    {
        if (GData.isFirst)
        {
            GData.isFirst = false;
            GData.isVibrate = true;
            GData.isGamePlayFirstTime = true;
            GData.isSound = true;
            GData.isMusic = true;
            for (int i = 0; i < GData.allRadel.Length; i++)
            {
                GData.allRadel[i].isCompleted = false;
            }
            Disclaimer_Page.SetActive(true);
            PersistentDataManager.instance.SaveData();
        }
        else
        {
            AssismentQuiez_Page.SetActive(true);
        }
    }
    public void OkButton()
    {
        AssismentQuiez_Page.SetActive(true);
        Disclaimer_Page.SetActive(false);
    } 
    public void SubmitBtnClick()
    {
        Result_Page.SetActive(false);
        ChangeGmeScene();
    }
    public void ChangeGmeScene()
    {
        StartCoroutine(StartFillBar());
    }
    IEnumerator StartFillBar()
    {
        yield return new WaitForSeconds(.001f);
        if (FillBar.fillAmount < 1f)
        {
            FillBar.fillAmount += Random.Range(.0025f, .005f);
            StartCoroutine(StartFillBar());
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
