using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PostExposureController : MonoBehaviour
{
    public Volume volume;
    public Slider postExposureSlider;

    private ColorAdjustments colorAdjustments;

    private const string ExposureKey = "PostExposureValue";

    void Start()
    {
        if (volume.profile.TryGet(out colorAdjustments))
        {
            float savedValue = PlayerPrefs.GetFloat(ExposureKey, colorAdjustments.postExposure.value);
            colorAdjustments.postExposure.value = savedValue;
            postExposureSlider.value = savedValue;

            postExposureSlider.onValueChanged.AddListener(UpdateExposure);
        }
    }

    void UpdateExposure(float value)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = value;
            PlayerPrefs.SetFloat(ExposureKey, value);
            PlayerPrefs.Save();
        }
    }
}