using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour
{
    public Image FillBar;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.isMainMenu)
        {
            ChangeGmeScene("");
            GameManager.Instance.isMainMenu = false;
        }
        else if (GameManager.Instance.isHouseGamePlay)
        {
            ChangeGmeScene("HouseGamePlay");
            GameManager.Instance.isHouseGamePlay = false;
        }
        else if (GameManager.Instance.isCatchBug)
        {
            ChangeGmeScene("NavigationMovement");
            GameManager.Instance.isCatchBug = false;
        }
        else if (GameManager.Instance.isGameShow)
        {
            ChangeGmeScene("GameShowMenu");
            GameManager.Instance.isGameShow = false;
        }
        else if (GameManager.Instance.isSafeForPlay)
        {
            ChangeGmeScene("Safe4playMenu");
            GameManager.Instance.isSafeForPlay = false;
        }
        else if (GameManager.Instance.isBustingMyth)
        {
            ChangeGmeScene("MenuBM");
            GameManager.Instance.isBustingMyth = false;
        }
        else if (GameManager.Instance.isworstPlay)
        {
            ChangeGmeScene("GameMenu");
            GameManager.Instance.isworstPlay = false;
        }
    }
    public void ChangeGmeScene(string Name)
    {
        StartCoroutine(Loadasyncouronsly(Name));
    }
    IEnumerator Loadasyncouronsly(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            FillBar.fillAmount += progress;
            yield return null;
        }
    }
}
