using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameData GData;
    public GameObject SettingPanal;
    public GameObject LevelSelectionPanal;
    public GameObject MainManuPanal;
    public GameObject LoadingPanal;
    public GameObject MusicOn;
    public GameObject MusicOff;
    public GameObject SoundOn;
    public GameObject SoundOff;
    public GameObject VibrationOn;
    public GameObject VibrationOff;
    public GameObject StartSeting;
    public Dropdown StartSettingDropDown;
    public Dropdown SettingDropDown;
    public Image LoadigFillBar;
    public string RateUrl;
    public Button resetProgressButton;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        Invoke(nameof(PlayBackgroundSound), .25f);
        Invoke(nameof(StartSoundSettingBtn), .25f);
        Invoke(nameof(StartVibrationSettingBtn), .25f);
        Invoke(nameof(StartMusicSettingBtn), .25f);
        if (GData.isGamePlayFirstTime)
        {
            GData.isGamePlayFirstTime = false;
            MainManuPanal.SetActive(false);
            StartSeting.SetActive(true);
            GData.languageSel = 1;
            SettingDropDown.value = GData.languageSel;
            StartSettingDropDown.value = GData.languageSel;
            PersistentDataManager.instance.SaveData();
        }
        else
        {
            StartLanguageSetting();
        }
    }

    // private void ResetProgress()
    // {
    //     
    // }
    public void OnOkBtnClick()
    {
        StartSeting.SetActive(false);
        MainManuPanal.SetActive(true);
    }

    public void OnStartSettingDropDownValueChange()
    {
        GData.languageSel = StartSettingDropDown.value;
        PersistentDataManager.instance.SaveData();
        SetLanguage();
    }

    public void OnSettingDropDownValueChange()
    {
        GData.languageSel = SettingDropDown.value;
        PersistentDataManager.instance.SaveData();
        SetLanguage();
    }

    private void StartLanguageSetting()
    {
        SettingDropDown.value = GData.languageSel;
        StartSettingDropDown.value = GData.languageSel;
        SetLanguage();
    }

    private void SetLanguage()
    {
        Dictionary<int, string> languageMap = new Dictionary<int, string>
        {
            { 0, "ar" },
            { 1, "en" },
            { 2, "fr" },
            { 3, "el" }
        };

        if (languageMap.TryGetValue(GData.languageSel, out var value))
        {
            GData.selectLanguage = value;
        }

        PersistentDataManager.instance.SaveData();
        LocaleSelector.instance.ChangeLocale();
        ResetProgress();
    }

    public void ResetProgress()
    {
        for (int i = 0; i < GData.allRadel.Length; i++)
        {
            GData.allRadel[i].isCompleted = false;
        }

        PersistentDataManager.instance.SaveData();
    }

    public void StartSoundSettingBtn()
    {
        if (GData.isSound)
        {
            SoundManager.instance.EffectsVolume = 1f;
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
            PersistentDataManager.instance.SaveData();
        }
        else if (!GData.isSound)
        {
            SoundManager.instance.EffectsVolume = 0f;
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
            PersistentDataManager.instance.SaveData();
        }
    }

    public void StartVibrationSettingBtn()
    {
        if (GData.isVibrate)
        {
            VibrationOn.SetActive(true);
            VibrationOff.SetActive(false);
            PersistentDataManager.instance.SaveData();
        }
        else if (!GData.isVibrate)
        {
            VibrationOn.SetActive(false);
            VibrationOff.SetActive(true);
            PersistentDataManager.instance.SaveData();
        }
    }

    public void StartMusicSettingBtn()
    {
        if (GData.isMusic)
        {
            SoundManager.instance.MusicVolume = 1f;
            MusicOn.SetActive(true);
            MusicOff.SetActive(false);
            PersistentDataManager.instance.SaveData();
        }
        else if (!GData.isMusic)
        {
            SoundManager.instance.MusicVolume = 0f;
            MusicOn.SetActive(false);
            MusicOff.SetActive(true);
            PersistentDataManager.instance.SaveData();
        }
    }

    private void GenaricBtnClcikSound()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.BtnClick);
    }

    public void PlayBackgroundSound()
    {
        SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.BgSound);
    }

    public void SettingBtnClcik()
    {
        MainManuPanal.SetActive(false);
        SettingPanal.SetActive(true);
        LevelSelectionPanal.SetActive(false);
        GenaricBtnClcikSound();
    }

    public void SettingBackBtnClcik()
    {
        MainManuPanal.SetActive(true);
        SettingPanal.SetActive(false);
        LevelSelectionPanal.SetActive(false);
        GenaricBtnClcikSound();
    }

    public void LevelSelctionBackBtnClcik()
    {
        MainManuPanal.SetActive(true);
        SettingPanal.SetActive(false);
        LevelSelectionPanal.SetActive(false);
        GenaricBtnClcikSound();
    }

    public void GameStartBtnClcik()
    {
        MainManuPanal.SetActive(false);
        SettingPanal.SetActive(false);
        LevelSelectionPanal.SetActive(true);
        GenaricBtnClcikSound();
    }

    public void PlayBtnClcik()
    {
        ChangeGmeScene(2);
        GenaricBtnClcikSound();
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }

    public void RateUsBtnClick()
    {
        GenaricBtnClcikSound();
        Application.OpenURL(RateUrl);
    }

    public void SettingClose()
    {
        GenaricBtnClcikSound();
        SettingPanal.SetActive(false);
    }

    public void SoundSettingBtn()
    {
        GenaricBtnClcikSound();
        if (GData.isSound)
        {
            SoundManager.instance.EffectsVolume = 0f;
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
            GData.isSound = false;
            PersistentDataManager.instance.SaveData();
        }
        else if (!GData.isSound)
        {
            SoundManager.instance.EffectsVolume = 1f;
            SoundOff.SetActive(false);
            SoundOn.SetActive(true);
            GData.isSound = true;
            PersistentDataManager.instance.SaveData();
        }
    }

    public void MusicSettingBtn()
    {
        GenaricBtnClcikSound();
        if (GData.isMusic)
        {
            SoundManager.instance.MusicVolume = 0f;
            MusicOff.SetActive(true);
            MusicOn.SetActive(false);
            GData.isMusic = false;
            PersistentDataManager.instance.SaveData();
        }
        else if (!GData.isMusic)
        {
            SoundManager.instance.MusicVolume = 1f;
            MusicOff.SetActive(false);
            MusicOn.SetActive(true);
            GData.isMusic = true;
            PersistentDataManager.instance.SaveData();
        }
    }

    public void VibratSettingBtn()
    {
        GenaricBtnClcikSound();
        if (GData.isVibrate)
        {
            VibrationOff.SetActive(true);
            VibrationOn.SetActive(false);
            GData.isVibrate = false;
            PersistentDataManager.instance.SaveData();
        }
        else if (!GData.isVibrate)
        {
            VibrationOff.SetActive(false);
            VibrationOn.SetActive(true);
            GData.isVibrate = true;
            PersistentDataManager.instance.SaveData();
        }
    }

    public void ChangeGmeScene(int sceneindex)
    {
        MainManuPanal.SetActive(false);
        SettingPanal.SetActive(false);
        LevelSelectionPanal.SetActive(false);
        LoadingPanal.SetActive(true);
        GenaricBtnClcikSound();
        StartCoroutine(Loadasyncouronsly(sceneindex));
    }

    IEnumerator Loadasyncouronsly(int sceneindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            LoadigFillBar.fillAmount += progress;
            yield return null;
        }
    }
}