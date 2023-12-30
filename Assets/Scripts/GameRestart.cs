using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestartGame()
    {
        GameManager.Instance.ChangeScene("Safe4playMenu");
    }
}
