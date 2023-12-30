using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIManager : MonoBehaviour
{
    public GameObject PausePanal;
    public GameObject RaddelBtn;
    public GameObject RaddelOkBtn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PauseGame(false);
    }
    public void OnPauseBtnClick()
    {
        PausePanal.SetActive(true);
        GameManager.Instance.PauseGame(true);
    }
    public void ResumeBtnClcik()
    {
        PausePanal.SetActive(false);
        GameManager.Instance.PauseGame(false);
    }
    public void HomeBtnClcik()
    {
        GameManager.Instance.PauseGame(false);
        GameManager.Instance.ChangeScene("MainMenu");
    }
    public void RestartBtnClcik()
    {
        GameManager.Instance.ChangeScene("HouseGamePlay");
    }
    public void RaddelBtnClcik()
    {
        RaddelBtn.SetActive(false);
        RaddelOkBtn.SetActive(true);
    }
    public void RaddelOkBtnClcik()
    {
        RaddelBtn.SetActive(true);
        RaddelOkBtn.SetActive(false);
    }
}
