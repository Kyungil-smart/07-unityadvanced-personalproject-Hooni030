using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // SFX(효과음) 재생
    public void PlaySound(AudioSource source, float volume)
    {
        if (source == null) return;
        source.volume = volume;
        source.Play();
    }
}