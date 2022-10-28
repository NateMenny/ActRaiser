using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerSounds))]
public class LevelManager : MonoBehaviour
{
    public int startingLives;
    public Transform spawnPoint;
    public SceneName nextScene;

    public AudioSource levelMusic;
    public AudioMixerGroup levelMusicSG;
    public AudioMixerGroup effectsSG;
    PlayerSounds ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PlayerSounds>();

        GameManager.instance.lives = startingLives;
        GameManager.instance.SpawnPlayer(spawnPoint);
        GameManager.instance.currentLevel = this;

        if (nextScene != SceneName.None) GameManager.instance.nextScene = nextScene;

    }

    public void PlaySoundEffect(AudioClip clip)
    {
        ps.Play(clip);
    }

    public void PauseLevelMusic()
    {
        if (levelMusic)
        {
            levelMusic.Pause();
        }
    }
}
