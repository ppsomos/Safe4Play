using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sound : MonoBehaviour
{
    static Sound instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
            AudioListener.volume = 1;
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            AudioListener.volume = 0;
        }
    }
}



