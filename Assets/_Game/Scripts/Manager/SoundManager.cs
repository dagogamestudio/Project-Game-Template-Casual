using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource BGMSource, SFXSource;
    public BGM[] BGMSounds;
    public SFX[] SFXSounds;

    private BGM currentBGM;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if(BGMSounds.Length > 0) PlayBGM(BGMSounds[0].name);
    }

    private void Update()
    {
        if (currentBGM != null && !BGMSource.isPlaying && !BGMSource.loop) PlayNextBGMClip();
    }

    #region BGM Method
    public void PlayBGM(string name)
    {
        currentBGM = Array.Find(BGMSounds, s => s.name == name);

        if (currentBGM != null)
        {
            BGMSource.loop = false;
            PlayNextBGMClip();
        }
        else Debug.LogWarning($"BGM: {name} Not Found");
    }

    void PlayNextBGMClip()
    {
        if (currentBGM == null || currentBGM.clip.Length == 0) return;

        AudioClip nextClip = currentBGM.clip.Length == 1
            ? currentBGM.clip[0]
            : currentBGM.clip[Random.Range(0, currentBGM.clip.Length)];

        BGMSource.clip = nextClip;
        BGMSource.loop = currentBGM.clip.Length == 1;
        BGMSource.Play();
    }

    public void ToggleMuteBGM()
    {
        BGMSource.mute = !BGMSource.mute;
    }

    public void SetBGMVolume(float volume)
    {
        BGMSource.volume = volume;
    }
    #endregion

    #region SFX Method
    public void PlaySFX(string name, bool randomPitch = false, float volume = 1)
    {
        SFX sound = Array.Find(SFXSounds, s => s.name == name);

        if (sound != null)
        {
            AudioClip clip = sound.clip.Length > 1 ? sound.clip[Random.Range(0, sound.clip.Length)] : sound.clip[0];
            SFXSource.pitch = randomPitch ? Random.Range(0.875f, 1.125f) : 1;
            SFXSource.PlayOneShot(clip, volume);
        }
        else Debug.LogWarning($"SFX: {name} Not Found");
    }

    public void ToggleMuteSFX()
    {
        SFXSource.mute = !SFXSource.mute;
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }
    #endregion

}

[Serializable]
public class BGM
{
    public string name;
    public AudioClip[] clip;
}

[Serializable]
public class SFX
{
    public string name;
    public AudioClip[] clip;
}