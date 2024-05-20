using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeHandler : MonoBehaviour
{
    public AudioType type;
    [SerializeField] private float volumeMultipler = 1f;
    private AudioSource audioSource;
    [SerializeField] private SettingsManager _settingsManager;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_settingsManager == null)
        {
            _settingsManager = GameManager._instance._settingsManager;
        }
        _settingsManager.e_settingSave.AddListener(UpdateVolume);
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
