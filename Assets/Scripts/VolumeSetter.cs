using UnityEngine;
using UnityEngine.UI;

public class VolumeSetter : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private VolumeType volumeType;
    [SerializeField] private Sprite muteImage;
    private Image image;
    private Sprite defaultImage;
    private Button button;
    private float? previousValue = null;

    private void Start()
    {
        this.slider.value = SaveManager.Data.GetVolume(this.volumeType);
        this.slider = GetComponentInChildren<Slider>();
        this.slider.value = SaveManager.Data.GetVolume(this.volumeType);
        this.slider.onValueChanged.AddListener(this.SetVolume);

        this.image = GetComponentInChildren<Image>();
        this.defaultImage = this.image.sprite;
        this.button = GetComponentInChildren<Button>();
        this.button.onClick.AddListener(this.OnButtonClicked);
    }

    public void SetVisibility(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }

    private void SetVolume(float value)
    {
        if (value < 0.001)
        {
            value = 0;
            this.slider.value = 0f;
        }

        this.image.sprite = value == 0 ? this.muteImage : this.defaultImage;
        AudioManager.Instance.ChangeVolume(value, this.volumeType);
    }

    private void OnButtonClicked()
    {
        if (this.slider.value == 0)
        {
            if (this.previousValue == null)
            {
                this.slider.value = 0.5f;
                return;
            }

            this.slider.value = this.previousValue.Value;
        }
        else
        {
            this.previousValue = this.slider.value;
            this.slider.value = 0f;
        }
    }

    public void OnDestroy()
    {
        this.slider.onValueChanged.RemoveAllListeners();
        this.button.onClick.RemoveAllListeners();
    }
}