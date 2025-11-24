using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource backgroundMusic;

    public void ChangeVolume(float value, VolumeType volumeType)
    {
        string volume = volumeType.Equals(VolumeType.Sound) ? "SoundVolume" : "MusicVolume";

        if (value <= 0.0001f)
        {
            this.audioMixer.SetFloat(volume, -80f);
            return;
        }

        this.audioMixer.SetFloat(volume, Mathf.Log10(value) * 20);
    }
}