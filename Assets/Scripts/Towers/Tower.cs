using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    public Transform rotatingPart;

    public AudioClip[] placementSounds;
    public AudioClip[] triggerSounds;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.PlayOneShot(RandomSound(placementSounds));
    }

    public AudioClip RandomSound(AudioClip[] list)
    {
        AudioClip ret = list[0];
        ret = list[Random.Range(0, list.Length)];
        return ret;
    }

}
