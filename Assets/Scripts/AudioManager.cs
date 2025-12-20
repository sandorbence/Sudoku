using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeVolume(float value, VolumeType volumeType)
    {
        bool isSoundVolume = volumeType.Equals(VolumeType.Sound);
        string volume = isSoundVolume ? "SoundVolume" : "MusicVolume";

        if (value <= 0.0001f)
        {
            this.audioMixer.SetFloat(volume, -80f);
            return;
        }

        this.audioMixer.SetFloat(volume, Mathf.Log10(value) * 20);

        SaveManager.Data.SetVolume(volumeType, value);
        SaveManager.Save();
    }
}