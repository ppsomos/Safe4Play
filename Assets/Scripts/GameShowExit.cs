using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShowExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnExitBtnClick()
    {
        GameManager.Instance.isHouseGamePlay = true;
        GameManager.Instance.ChangeScene("Loading");
    }
}
