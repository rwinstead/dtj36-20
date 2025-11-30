using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Source (optional)")]
    public AudioSource audioSource;

    [Header("Volume")]
    [Range(0f, 1f)]
    public float volume = 0.7f;

    [Header("Per-Scene Music")]
    public List<SceneMusic> sceneTracks = new List<SceneMusic>();

    [Serializable]
    public class SceneMusic
    {
        public string sceneName;   // Must match scene name exactly
        public AudioClip clip;     // Music to play in that scene
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // A second MusicManager was created → kill it
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        EnsureAudioSource();

        SceneManager.activeSceneChanged += OnSceneChanged;
        OnSceneChanged(default, SceneManager.GetActiveScene());
    }

    private void EnsureAudioSource()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.loop = true;
            audioSource.volume = volume;
        }
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        EnsureAudioSource();

        foreach (var entry in sceneTracks)
        {
            if (entry.sceneName == newScene.name)
            {
                // No clip defined for this scene → just stop
                if (entry.clip == null)
                {
                    audioSource.Stop();
                    return;
                }

                // Always restart the clip when entering this scene
                audioSource.Stop();
                audioSource.clip = entry.clip;
                audioSource.time = 0f;   // start from the beginning
                audioSource.Play();
                return;
            }
        }

        // If we reach here, no track is configured for this scene.
        // You can either stop or keep previous music:
        // audioSource.Stop();
    }

    public void StopMusic()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}
