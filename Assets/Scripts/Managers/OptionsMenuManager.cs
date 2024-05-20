using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private Slider slider_music;
    [SerializeField] private Slider slider_sound;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private SettingsManager _settingsManager;

    private void Start()
    {
        if(_settingsManager == null)
        {
            _settingsManager = GameManager._instance._settingsManager;
        }
    }

    public void ToggleOptionMenu(bool active)
    {
        slider_music.value = _settingsManager._settings.v_music;
        slider_sound.value = _settingsManager._settings.v_sfx;
        optionMenu.SetActive(active);
    }

    public void OnAudioChanged(int type)
    {
        float amount = 0;
        switch (type)
        {
            case 0:
                amount = slider_music.value;
                _settingsManager._settingCache.v_music = amount;
                break;
            case 1:
                amount = slider_sound.value;
                _settingsManager._settingCache.v_sfx = amount;
                break;
        }
    }

    public void SaveSettings()
    {
        _settingsManager.SaveSettings();
    }


}
