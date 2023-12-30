using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatchGameStatus : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.isCatchBug = true;
        GameManager.Instance.ChangeScene("Loading");
    }

    public void ExitGame()
    {
        GameManager.Instance.isHouseGamePlay = true;
        GameManager.Instance.ChangeScene("Loading");
    }
}
