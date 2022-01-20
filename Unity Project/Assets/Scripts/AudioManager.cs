using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager:MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    AudioManager audioManager;
    private void Start()
    {

        Play("GameBackGroundSound");

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }   
        else
        {
            Destroy(gameObject);
            return;
        }
         

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.SpatialBlend;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
            return;
        s.source.Stop();
    }

    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
            return;
        s.source.volume = 0;
    }

    public void UnMute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
            return;
        s.source.volume = 0.5f;
    }
}

