using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameData GData;
    public Vector3 PlayerPos;
    public Quaternion PlayerRot;
    public int Path_No;
    public bool isMainMenu;
    public bool isHouseGamePlay;
    
    public bool isCatchBug;
     public bool isGameShow;
     public bool isSafeForPlay;
     public bool isBustingMyth;
     public bool isworstPlay;
    public bool isStaticContent;
    public bool isExternalContent;
    public bool fromMiniGame;
    bool IsPause;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PauseGame(bool Status)
    {
        if (Status)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        IsPause = Status;
    }
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
