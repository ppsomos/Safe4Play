using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RaddelHandler : MonoBehaviour, RaddelObjectInterface
{
    public int[] R_N;
   [HideInInspector]public int riddleNo;
    public void OnObjectIntrect()
    {
        if (!GamePlayManager.Instance.GData.allRadel[riddleNo].isCompleted)
        {
            GamePlayManager.Instance.AllRaddel[riddleNo].transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, .25f);
            GamePlayManager.Instance.AllRaddel[riddleNo].transform.GetChild(1).transform.gameObject.SetActive(true);
            GamePlayManager.Instance.GData.allRadel[riddleNo].isCompleted = true;
            GameManager.Instance.PlayerPos = GamePlayManager.Instance.Player.transform.position;
            GameManager.Instance.PlayerRot = GamePlayManager.Instance.Player.transform.rotation;
            GamePlayManager.Instance.Player.SetActive(false);
            PersistentDataManager.instance.SaveData();
            if (riddleNo == 0 || riddleNo == 1) //Gameshow
            {
                GameManager.Instance.isGameShow = true;
                GameManager.Instance.ChangeScene("Loading");
            }
            else if (riddleNo == 2 || riddleNo == 4 || riddleNo == 10 || riddleNo == 11) // Static Content
            {
                GameManager.Instance.isStaticContent = true;
                GamePlayManager.Instance.staticInfoPanal.SetActive(false);
                GamePlayManager.Instance.Player.SetActive(true);
                // GameManager.Instance.ChangeScene("Loading");
            }
            else if (riddleNo == 3) // isworstPlay
            {
                GameManager.Instance.isworstPlay = true;
                GameManager.Instance.ChangeScene("Loading");
            }
            else if (riddleNo == 5) // external content
            {
                GameManager.Instance.isExternalContent = true;
                GameManager.Instance.ChangeScene("Loading");
            }
            else if (riddleNo == 6 || riddleNo == 8) // Busting myth
            {
                GameManager.Instance.isBustingMyth = true;
                GameManager.Instance.ChangeScene("Loading");
            }
            else if (riddleNo == 7) //Catch bug
            {
                GameManager.Instance.isCatchBug = true;
                GameManager.Instance.ChangeScene("Loading");
            }
            else if(riddleNo == 9) //HPV simulation
            {
                GameManager.Instance.isSafeForPlay = true;
                GameManager.Instance.ChangeScene("Loading");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetRadle(GameManager.Instance.Path_No);
        if (GamePlayManager.Instance.GData.allRadel[riddleNo].isCompleted)
        {
            GamePlayManager.Instance.AllRaddel[riddleNo].transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, .25f);
            GamePlayManager.Instance.AllRaddel[riddleNo].transform.GetChild(1).transform.gameObject.SetActive(true);
            GamePlayManager.Instance.GData.allRadel[riddleNo].isCompleted = true;
            PersistentDataManager.instance.SaveData();
        }
    }
    public void SetRadle(int no)
    {
        riddleNo = R_N[no];
    }
}
