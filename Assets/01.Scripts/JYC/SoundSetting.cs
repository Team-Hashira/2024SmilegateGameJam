using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioSource[] _masterSource;
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource[] _sfxSource;

    public void SetMasterVolume(float volume)
    {
        for (int i = 0; i < _masterSource.Length; i++)
        {
            _masterSource[i].volume = volume;
        }
    }

    public void SetBGMVolume(float volume)
    {
        _bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        for (int i = 0; i < _sfxSource.Length; i++)
        {
            _sfxSource[i].volume = volume;
        }
    }
}
