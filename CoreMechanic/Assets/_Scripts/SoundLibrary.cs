using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    public static SoundLibrary Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundLibrary>();
            }
            return _instance;
        }
    }

    private static SoundLibrary _instance;

    public AudioSource audioSource;
    public AudioClip[] UIsounds;
    public AudioClip[] pickUpSounds;
    public AudioClip[] SwordSounds;
    public AudioClip[] WoodSounds;
    public AudioClip[] HitSounds;
    public AudioClip[] arrowSounds;
    public AudioClip arrowTok;


    private void OnDestroy()
    {
        if (this == _instance)
        {
            _instance = null;
        }
    }
}
