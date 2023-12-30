using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject hItObj;
    public GameObject riddleGameObject;
    public float rayCastDistance = 10f;
    public Image dotImg;

    private void OnEnable()
    {
        print("OnEnable");
        if (CheckGameComplete() == true)
        {
            print("GameComplete");
            SceneManager.LoadSceneAsync(1);
        }
    }

    public void NextBtnClick()
    {
        GameManager.Instance.fromMiniGame = true;
        if (riddleGameObject != null)
        {
            riddleGameObject.transform.gameObject.GetComponent<RaddelObjectInterface>().OnObjectIntrect();
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out var hit,
                rayCastDistance))
        {
            //Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.CompareTag("Ridle"))
            {
                hItObj = hit.transform.gameObject;
                if (hItObj != null)
                {
                    riddleGameObject = hItObj;
                    int objNo = hItObj.transform.gameObject.GetComponent<RaddelHandler>().riddleNo;
                    if (!GameManager.Instance.GData.allRadel[objNo].isCompleted)
                    {
                        if (GameManager.Instance.GData.languageSel == 0)
                        {
                            //arabic
                            if (objNo == 2 || objNo == 4 || objNo == 10 || objNo == 11) // Static Content
                            {
                                InfoPageData data =
                                    GamePlayManager.Instance.staticInfoPanal.GetComponent<InfoPageData>();
                                GamePlayManager.Instance.staticInfoPanal.SetActive(true);
                                data.riddleTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].riddleQuestionArabic;
                                data.answerTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].gameNameArabic;
                                data.descriptionTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].descriptionArabic;
                                data.linkButton.onClick.RemoveAllListeners();
                                data.linkButton.onClick.AddListener(() =>
                                    OpenLink(GameManager.Instance.GData.allRadel[objNo].linkArabic));
                            }
                            else
                            {
                                GamePlayManager.Instance.RiddleText.text =
                                    GameManager.Instance.GData.allRadel[objNo].riddleQuestionArabic;
                                GamePlayManager.Instance.RiddleAnswerText.text =
                                    GameManager.Instance.GData.allRadel[objNo].gameNameArabic;
                                GamePlayManager.Instance.InfoPanal.SetActive(true);
                                if (objNo == 0 || objNo == 1) //Gameshow
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Game Show";
                                }
                                else if (objNo == 3) // Worst play ever
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Worst play ever";
                                }
                                else if (objNo == 5) // external content
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "External Information";
                                }
                                else if (objNo == 6 || objNo == 8) // Busting myth
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Busting myth";
                                }
                                else if (objNo == 7) //Catch that Bug
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Catch that Bug";
                                }
                                else if (objNo == 9) //HPV simulation
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "HPV simulation";
                                }
                            }
                        }
                        else if (GameManager.Instance.GData.languageSel == 1)
                        {
                            //english
                            if (objNo == 2 || objNo == 4 || objNo == 10 || objNo == 11) // Static Content
                            {
                                InfoPageData data =
                                    GamePlayManager.Instance.staticInfoPanal.GetComponent<InfoPageData>();
                                GamePlayManager.Instance.staticInfoPanal.SetActive(true);
                                data.riddleTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].riddleQuestionEnglish;
                                data.answerTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].gameNameEnglish;
                                data.descriptionTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].descriptionEnglish;
                                data.linkButton.onClick.RemoveAllListeners();
                                data.linkButton.onClick.AddListener(() =>
                                    OpenLink(GameManager.Instance.GData.allRadel[objNo].linkEnglish));
                            }
                            else
                            {
                                GamePlayManager.Instance.RiddleText.text =
                                    GameManager.Instance.GData.allRadel[objNo].riddleQuestionEnglish;
                                GamePlayManager.Instance.RiddleAnswerText.text =
                                    GameManager.Instance.GData.allRadel[objNo].gameNameEnglish;
                                GamePlayManager.Instance.InfoPanal.SetActive(true);
                                if (objNo == 0 || objNo == 1) //Gameshow
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Game Show";
                                }
                                else if (objNo == 3) // Worst play ever
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Worst play ever";
                                }
                                else if (objNo == 5) // external content
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "External Information";
                                }
                                else if (objNo == 6 || objNo == 8) // Busting myth
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Busting myth";
                                }
                                else if (objNo == 7) //Catch that Bug
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Catch that Bug";
                                }
                                else if (objNo == 9) //HPV simulation
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "HPV simulation";
                                }
                            }
                        }
                        else if (GameManager.Instance.GData.languageSel == 2)
                        {
                            //french
                            if (objNo == 2 || objNo == 4 || objNo == 10 || objNo == 11) // Static Content
                            {
                                InfoPageData data =
                                    GamePlayManager.Instance.staticInfoPanal.GetComponent<InfoPageData>();
                                GamePlayManager.Instance.staticInfoPanal.SetActive(true);
                                data.riddleTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].riddleQuestionFrench;
                                data.answerTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].gameNameFrench;
                                data.descriptionTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].descriptionFrench;
                                data.linkButton.onClick.RemoveAllListeners();
                                data.linkButton.onClick.AddListener(() =>
                                    OpenLink(GameManager.Instance.GData.allRadel[objNo].linkFrench));
                            }
                            else
                            {
                                GamePlayManager.Instance.RiddleText.text =
                                    GameManager.Instance.GData.allRadel[objNo].riddleQuestionEnglish;
                                GamePlayManager.Instance.RiddleAnswerText.text =
                                    GameManager.Instance.GData.allRadel[objNo].gameNameFrench;
                                GamePlayManager.Instance.InfoPanal.SetActive(true);
                                if (objNo == 0 || objNo == 1) //Gameshow
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Game Show";
                                }
                                else if (objNo == 3) // Worst play ever
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Worst play ever";
                                }
                                else if (objNo == 5) // external content
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "External Information";
                                }
                                else if (objNo == 6 || objNo == 8) // Busting myth
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Busting myth";
                                }
                                else if (objNo == 7) //Catch that Bug
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Catch that Bug";
                                }
                                else if (objNo == 9) //HPV simulation
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "HPV simulation";
                                }
                            }
                        }
                        else if (GameManager.Instance.GData.languageSel == 3)
                        {
                            //greek
                            if (objNo == 2 || objNo == 4 || objNo == 10 || objNo == 11) // Static Content
                            {
                                InfoPageData data =
                                    GamePlayManager.Instance.staticInfoPanal.GetComponent<InfoPageData>();
                                GamePlayManager.Instance.staticInfoPanal.SetActive(true);
                                data.riddleTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].riddleQuestionGreek;
                                data.answerTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].gameNameGreek;
                                data.descriptionTextComponent.text =
                                    GameManager.Instance.GData.allRadel[objNo].descriptionGreek;
                                data.linkButton.onClick.RemoveAllListeners();
                                data.linkButton.onClick.AddListener(() =>
                                    OpenLink(GameManager.Instance.GData.allRadel[objNo].linkGreek));
                            }
                            else
                            {
                                GamePlayManager.Instance.RiddleText.text =
                                    GameManager.Instance.GData.allRadel[objNo].riddleQuestionGreek;
                                GamePlayManager.Instance.RiddleAnswerText.text =
                                    GameManager.Instance.GData.allRadel[objNo].gameNameGreek;
                                GamePlayManager.Instance.InfoPanal.SetActive(true);
                                if (objNo == 0 || objNo == 1) //Gameshow
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Game Show";
                                }
                                else if (objNo == 3) // Worst play ever
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Worst play ever";
                                }
                                else if (objNo == 5) // external content
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "External Information";
                                }
                                else if (objNo == 6 || objNo == 8) // Busting myth
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Busting myth";
                                }
                                else if (objNo == 7) //Catch that Bug
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "Catch that Bug";
                                }
                                else if (objNo == 9) //HPV simulation
                                {
                                    GamePlayManager.Instance.RiddleGameText.text = "HPV simulation";
                                }
                            }
                        }

                        dotImg.color = Color.green;
                    }
                    else
                    {
                        hItObj = null;
                        dotImg.color = Color.white;
                    }
                }
            }
            else
            {
                hItObj = null;
                dotImg.color = Color.white;
            }
        }
    }

    private void OpenLink(string url)
    {
        if (url == "")
        {
            print("Link not available");
            GamePlayManager.Instance.linkNotAvailable_Page.SetActive(true);
        }
        else
        {
            Application.OpenURL(url);
        }
    }

    private bool CheckGameComplete()
    {
        bool isGameComplete = true;

        foreach (var t in GameManager.Instance.GData.allRadel)
        {
            if (!t.isCompleted)
            {
                isGameComplete = false;
                break;
            }
        }

        return isGameComplete;
    }
}