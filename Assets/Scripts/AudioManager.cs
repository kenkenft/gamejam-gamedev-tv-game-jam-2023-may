using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager audioManager;
    AudioClip tempClip;

    void OnEnable()
    {
        PlayerMain.PlaySFX += PlaySound;
        UIManager.PlaySFX += PlaySound;
        DimensionSwitch.PlaySFX += PlaySound;
        WaterCurrent.PlaySFX += PlaySound;
    }

    void OnDisable()
    {
        PlayerMain.PlaySFX -= PlaySound;
        UIManager.PlaySFX -= PlaySound;
        DimensionSwitch.PlaySFX -= PlaySound;
        WaterCurrent.PlaySFX -= PlaySound;
    }

    void Awake()
    {
        if(audioManager == null)
        {
            DontDestroyOnLoad(gameObject);
            audioManager = this;
        }
        else if (audioManager != this)
            Destroy(gameObject);

        foreach(Sound s in sounds)
        {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;

                s.source.loop = s.loop;
        }
    }

    void Start()
    {
        PlaySound("ThemeA");
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}