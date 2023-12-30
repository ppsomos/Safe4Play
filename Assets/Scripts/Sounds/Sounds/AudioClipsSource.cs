using UnityEngine;


public class AudioClipsSource : MonoBehaviour
{
    public static AudioClipsSource Instance;

    [Header("Music Clips")]
    public AudioClip BgSound;
    public AudioClip BtnClick;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
