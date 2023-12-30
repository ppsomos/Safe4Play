using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private Sound sound;
    public Button soundButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindObjectOfType<Sound>();
        UpdateIconAndVolume();
    }

    public void PauseSounds()
    {
        sound.ToggleSound();
        UpdateIconAndVolume();
    }

    void UpdateIconAndVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            AudioListener.volume = 0;
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
    }
}
