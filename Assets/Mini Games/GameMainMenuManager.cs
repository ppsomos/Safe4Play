using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameMainMenuManager : MonoBehaviour
{
    [SerializeField] Button GameShowBtn;
    [SerializeField] Button FindBugsBtn;
    [SerializeField] Button Safe4PlayBtn;

    private void Start()
    {
        GameShowBtn.onClick.RemoveAllListeners();
        GameShowBtn.onClick.AddListener(GameShowFunc);

        FindBugsBtn.onClick.RemoveAllListeners();
        FindBugsBtn.onClick.AddListener(FindTheBugsFunc);

        Safe4PlayBtn.onClick.RemoveAllListeners();
        Safe4PlayBtn.onClick.AddListener(Safe4PlayFunc);
    }

    private void Safe4PlayFunc()
    {
        SceneManager.LoadScene("Safe4playMenu");
    }

    private void FindTheBugsFunc()
    {
        SceneManager.LoadScene("NavigationMovement");
    }

    private void GameShowFunc()
    {
        SceneManager.LoadScene("GameShowMenu");
    }
}
