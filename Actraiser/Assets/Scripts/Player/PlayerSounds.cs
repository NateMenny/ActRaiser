using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSounds : MonoBehaviour
{

    List<AudioSource> currentAudioSources = new List<AudioSource>();
    bool didPlay = false;


    // Start is called before the first frame update
    void Start()
    {
        currentAudioSources.Add(gameObject.AddComponent<AudioSource>());
    }

    public void Play(AudioClip clip, AudioMixerGroup group)
    {
        foreach (AudioSource source in currentAudioSources)
        {
            if (source.isPlaying)
                continue;

            didPlay = true;
            source.PlayOneShot(clip);
            source.outputAudioMixerGroup = group;
            break;
        }

        if (!didPlay)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            currentAudioSources.Add(temp);
            temp.PlayOneShot(clip);
            temp.outputAudioMixerGroup = group;
        }

        didPlay = false;
    }

    public void Play(AudioClip clip)
    {
        foreach (AudioSource source in currentAudioSources)
        {
            if (source.isPlaying)
                continue;

            didPlay = true;
            source.PlayOneShot(clip);
            break;
        }

        if (!didPlay)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            currentAudioSources.Add(temp);
            temp.PlayOneShot(clip);
        }

        didPlay = false;
    }
}
