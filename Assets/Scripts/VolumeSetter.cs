using UnityEngine;
using UnityEngine.UI;

public class VolumeSetter : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private VolumeType volumeType;

    private void Start()
    {
        this.slider = GetComponentInChildren<Slider>();
        AudioManager.Instance.ChangeVolume(this.slider.value, this.volumeType);
        this.slider.onValueChanged.AddListener(value
            => AudioManager.Instance.ChangeVolume(value, this.volumeType));
    }

    public void SetVisibility(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }
}

public enum VolumeType
{
    Sound,
    Music
}