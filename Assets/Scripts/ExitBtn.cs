using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ExitGame()
    {
        GameManager.Instance.isHouseGamePlay = true;
        GameManager.Instance.fromMiniGame = true;
        GameManager.Instance.ChangeScene("Loading");
    }
}
