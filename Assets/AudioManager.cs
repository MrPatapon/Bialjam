using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop = false;
    }

    public Sound[] sounds;    

    public string musicTrackName; // Name of the track to play as music

    private Dictionary<string, AudioSource> soundSources = new Dictionary<string, AudioSource>();

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSounds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Play("music");
        Play("ambient");
    }

    private void InitializeSounds()
    {
        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            soundSources[sound.name] = source;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusic();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void PlayMusic()
    {
        if (!string.IsNullOrEmpty(musicTrackName))
        {
            if (soundSources.TryGetValue(musicTrackName, out AudioSource source))
            {
                Debug.Log("Playing music: " + musicTrackName);
                source.Stop();
                source.Play();
            }
            else
            {
                Debug.LogWarning($"AudioManager: Music track '{musicTrackName}' not found.");
            }
        }
    }

    public void Play(string name)
    {
        if (soundSources.TryGetValue(name, out AudioSource source))
        {
            source.Play();
        }
        else
        {
            Debug.LogWarning($"AudioManager: Sound '{name}' not found!");
        }
    }

    public void Stop(string name)
    {
        if (soundSources.TryGetValue(name, out AudioSource source))
        {
            source.Stop();
        }
    }

    public void SetVolume(string name, float volume)
    {
        if (soundSources.TryGetValue(name, out AudioSource source))
        {
            source.volume = volume;
        }
    }

    public void SetPitch(string name, float pitch)
    {
        if (soundSources.TryGetValue(name, out AudioSource source))
        {
            source.pitch = pitch;
        }
    }
}
