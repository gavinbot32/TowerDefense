using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AudioType
{
    Music,
    Sfx
}
public class SettingsManager : MonoBehaviour
{
    public OptionData _settings;
    public OptionData _settingCache;
    public UnityEvent e_settingSave;

    public bool loadSettings = false;
    private void Start()
    {
        if (loadSettings)
        {
            LoadSettings();
        }
    }
    private void OnDisable()
    {
        e_settingSave.RemoveAllListeners();
    }

    public void SaveSettings()
    {
        _settings.PushCache(_settingCache);
        _settingCache.PushCache(_settings);

        PushOption(_settings.v_music_key, _settings.v_music);
        PushOption(_settings.v_sfx_key, _settings.v_sfx);
        PlayerPrefs.Save();
        e_settingSave.Invoke();
    }

    public void LoadSettings()
    {
        _settings.v_music = PullOption(_settings.v_music_key,0.5f);
        _settings.v_sfx = PullOption(_settings.v_sfx_key, 0.5f);
        PlayerPrefs.Save();
        _settingCache.PushCache(_settings);
        e_settingSave.Invoke();
    }


    //Push Pull Commands
    #region  
    private void PushOption(string key, float amount)
    {
        PlayerPrefs.SetFloat(key, amount);
    }

    private void PushOption(string key, int amount)
    {
        PlayerPrefs.SetInt(key, amount);
    }
    private void PushOption(string key, string line)
    {
        PlayerPrefs.SetString(key, line);
    }

    private float PullOption(string key, float defaultValue = 0)
    {
        float ret = -1;

        if (PlayerPrefs.HasKey(key))
        {
            ret = PlayerPrefs.GetFloat(key, defaultValue);
        }
        return ret;
    }
    private int PullOption(string key, int defaultValue = 0)
    {
        int ret = -1;

        if (PlayerPrefs.HasKey(key))
        {
            ret = PlayerPrefs.GetInt(key, defaultValue);
        }
        return ret;
    }
    private string PullOption(string key, string defaultValue = "")
    {
        string ret = "";

        if (PlayerPrefs.HasKey(key))
        {
            ret = PlayerPrefs.GetString(key, defaultValue);
        }
        return ret;
    }
    #endregion   
}
