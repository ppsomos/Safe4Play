using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public GameObject Player;
    public GameData GData;
    public bool FromOther;
    public GameObject InfoPanal;
    public GameObject staticInfoPanal;

    public Text RiddleText;
    public Text RiddleAnswerText;
    public Text RiddleGameText;
    public GameObject[] AllRaddel;

    public GameObject linkNotAvailable_Page;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        SetRadle(GameManager.Instance.Path_No);
    }
    public void SetRadle(int no)
    {
        for(int i = 0; i < AllRaddel.Length; i++)
        {
            AllRaddel[i].transform.GetChild(0).GetComponent<Text>().text = GData.R_P[no].RT[i].riddleQuestionEnglish;
            GData.R_P[no].RT[i].isCompleted = false;
            PersistentDataManager.instance.SaveData();
        }
    }

    void Start()
    {
        if (!GData.isMusic)
        {
            Player.GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            Player.GetComponent<AudioSource>().enabled = true;
        }

        Invoke(nameof(ActivePlayer), .25f);
    }

    public void ActivePlayer()
    {
        if (GameManager.Instance.fromMiniGame)
        {
            Player.SetActive(true);
            Player.transform.position = new Vector3(GameManager.Instance.PlayerPos.x, GameManager.Instance.PlayerPos.y,
                GameManager.Instance.PlayerPos.z);
            Player.transform.rotation = Quaternion.Euler(GameManager.Instance.PlayerRot.x,
                GameManager.Instance.PlayerRot.y + 15f, GameManager.Instance.PlayerRot.z);
            GameManager.Instance.fromMiniGame = false;
        }
        else
        {
            Player.SetActive(true);
        }
    }
}