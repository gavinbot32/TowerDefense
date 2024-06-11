using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeHandler : MonoBehaviour
{
    public AudioType type;
    [SerializeField] private float volumeMultipler = 1f;
    private AudioSource audioSource;

    [Header("Fill this field if there's no GameManager in scene")]
    [Header("If no GameManager and no SettingsManager don't add this component")]
    [SerializeField] private SettingsManager _settingsManager;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Reset()
    {
        if(FindObjectOfType<SettingsManager>() == null)
        {
            Debug.LogWarning("FATAL:VolumeHandler needs a SettingManager in _settingsManager field or attached to a GameManager instance");
        }
    }
    private void OnDisable()
    {
        if(_settingsManager != null) _settingsManager.e_settingSave.RemoveListener(this.UpdateVolume);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_settingsManager == null)
        {
            _settingsManager = GameManager._instance._settingsManager;
        }
        _settingsManager.e_settingSave.AddListener(this.UpdateVolume);
        UpdateVolume();
    }
    public void UpdateVolume()
    {
        switch (type)
        {
            case AudioType.Music:
                audioSource.volume = _settingsManager._settings.v_music * volumeMultipler;
                break;
            case AudioType.Sfx:
                audioSource.volume = _settingsManager._settings.v_sfx * volumeMultipler;
                break;
        }
    }
}
