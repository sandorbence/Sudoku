using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.SetVolume(SaveManager.Data.GetVolume(VolumeType.Sound), VolumeType.Sound);
        this.SetVolume(SaveManager.Data.GetVolume(VolumeType.Music), VolumeType.Music);
    }

    public void ChangeVolume(float value, VolumeType volumeType)
    {
        this.SetVolume(value, volumeType);

        SaveManager.Data.SetVolume(volumeType, value);
        SaveManager.Save();
    }

    private void SetVolume(float value, VolumeType volumeType)
    {
        bool isSoundVolume = volumeType.Equals(VolumeType.Sound);
        string volume = isSoundVolume ? "SoundVolume" : "MusicVolume";

        if (value <= 0.01f)
        {
            this.audioMixer.SetFloat(volume, -80f);
            return;
        }

        this.audioMixer.SetFloat(volume, Mathf.Log10(value) * 20);
    }
}