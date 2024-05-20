using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/OptionData", order = 1)]
public class OptionData : ScriptableObject
{
    [Header("Audio Settings")]
    public string v_music_key;
    public float v_music;
    public string v_sfx_key;
    public float v_sfx;

    public void PushCache(OptionData cache)
    {
        this.v_music = cache.v_music;
        this.v_sfx = cache.v_sfx;

    }
    
}
