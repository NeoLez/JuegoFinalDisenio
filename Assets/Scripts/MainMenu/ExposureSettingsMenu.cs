using UnityEngine;
using UnityEngine.UI;

public class ExposureSettingsMenu : MonoBehaviour
{
    public Slider postExposureSlider;
    private const string ExposureKey = "PostExposureValue";

    void Start()
    {
        float savedValue = PlayerPrefs.GetFloat(ExposureKey, 0.75f);
        postExposureSlider.value = savedValue;
        postExposureSlider.onValueChanged.AddListener(SaveExposureValue);
    }

    void SaveExposureValue(float value)
    {
        PlayerPrefs.SetFloat(ExposureKey, value);
        PlayerPrefs.Save();
    }
}