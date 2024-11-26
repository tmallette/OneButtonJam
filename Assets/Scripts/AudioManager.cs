using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private AudioSource musicOne;
    private AudioSource musicTwo;

    private AudioSource[] soundEffects;
    private int currentSFXSource = 0;

    private bool muted = false;
    public float volume = 0.3f;
    private float baseVolume = 0.3f;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            musicOne = gameObject.AddComponent<AudioSource>();
            musicTwo = gameObject.AddComponent<AudioSource>();
            soundEffects = new AudioSource[10];

            var volumeGroup = audioMixer.FindMatchingGroups("Master")[0];

            for (int i = 0; i < soundEffects.Length; i++)
            {
                soundEffects[i] = gameObject.AddComponent<AudioSource>();
                soundEffects[i].outputAudioMixerGroup = volumeGroup;
            }

            musicOne.outputAudioMixerGroup = volumeGroup;
            musicTwo.outputAudioMixerGroup = volumeGroup;

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFXClip(Sound _sound)
    {
        soundEffects[currentSFXSource].clip = _sound.clip;
        soundEffects[currentSFXSource].PlayOneShot(_sound.clip);

        // Cycle through the sfx sources
        currentSFXSource = (currentSFXSource + 1) % soundEffects.Length;
    }

    public void PlayMusic(Sound _sound)
    {
        AudioSource currentTrack = musicOne.isPlaying ? musicOne : musicTwo;
        AudioSource nextTrack = currentTrack == musicOne ? musicTwo : musicOne;

        if (_sound.clip != currentTrack.clip)
        {
            FadeAudioOut(currentTrack, 2f);
            FadeAudioIn(nextTrack, 2f, _sound);
        }
        else
        {
            if (!currentTrack.isPlaying)
            {
                FadeAudioIn(currentTrack, 2f, _sound);
            }
        }
    }

    private void FadeAudioIn(AudioSource _audioSource, float _duration, Sound _s)
    {
        StartCoroutine(FadeIn(_audioSource, _duration, _s));
    }

    private void FadeAudioOut(AudioSource _audioSource, float _duration)
    {
        StartCoroutine(FadeOut(_audioSource, _duration));
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration, Sound s)
    {
        float currentTime = 0;

        audioSource.volume = 0;
        audioSource.clip = s.clip;
        audioSource.loop = s.loop;
        audioSource.Play();

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(0f, 1f, currentTime / duration);
            yield return null;
        }

        yield break;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(1f, 0f, currentTime / duration);
            yield return null;
        }

        audioSource.Stop();

        yield break;
    }

    public void SetMixer(float _volume)
    {
        volume = _volume;

        if (!muted)
        {
            audioMixer.SetFloat("Volume", MusicLogValue(_volume));
        }
    }

    private float MusicLogValue(float _volume)
    {
        return _volume == 0f ? -80f : Mathf.Log(_volume) * 10;
    }

    public void SetBaseVolume()
    {
        SetMixer(baseVolume);
    }

    private void OnDestroy()
    {
        Debug.Log("AudioManager is being destoryed.");
    }

    public void Webgl_Mute()
    {
        audioMixer.SetFloat("Volume", MusicLogValue(0));

        muted = true;
    }

    public void Webgl_Unmute()
    {
        muted = false;

        audioMixer.SetFloat("Volume", MusicLogValue(volume));
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public bool loop = false;
    public bool playOnAwake = false;
}