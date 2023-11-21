using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class was based on the tutorial "Introduction to AUDIO in Unity"
// by Brackeys (https://www.youtube.com/watch?v=6OT43pvUyfY)
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // private float[] elapsedTimes = {30f, 40f, 50f};
    // private float nextSoundTime;
    // private string[] scarySoundNames = { "Scary1", "Scary2", "Scary3", "Scary4", "Scary5", "Scary6" };

    void Awake()
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
        }

        // nextSoundTime = GetNextRandomSoundTime();
    }

    void Start() 
    {
        Play("Theme");
    }

    void Update()
    {
        // if (SceneManager.GetActiveScene().name == "SampleScene")
        // {
        //     if (Time.time >= nextSoundTime)
        //     {
        //         if (UnityEngine.Random.value > 0.6f)
        //         {
        //             string randomScarySound = scarySoundNames[UnityEngine.Random.Range(0, scarySoundNames.Length)];
        //             Play(randomScarySound);

        //             nextSoundTime = GetNextRandomSoundTime();
        //         }
        //     }
        // }
    }

    // private float GetNextRandomSoundTime()
    // {
    //     return Time.time + elapsedTimes[UnityEngine.Random.Range(0, elapsedTimes.Length)];
    // }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound with name " + name + " not found.");
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound with name " + name + " not found.");
        }
    }
}
