using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UnderwaterEffectActivator : MonoBehaviour
{
    public Transform waterSurface;
    public UniversalRendererData rendererData;

    public float darknessFadeTime = 2f;  
    public float distortionHoldTime = 2f;   
    public float distortionFadeTime = 1f;

    [Header("Effect Intensity")]
    public float originalDepth = 0.04f;
    public float originalBlend = 0.2f;
    public float originalDistortion = 1f;

    private Underwater underwater;
    private ScriptableRendererFeature feature;

    private bool wasUnderwater;
    private float darknessFadeValue = 1f;
    private float distortionTimer;
    private float refractionFadeValue = 1f;

    void Start()
    {
        foreach (var f in rendererData.rendererFeatures)
        {
            if (f.name.ToLower().Contains("water") || f.name.ToLower().Contains("underwater"))
            {
                feature = f;
                underwater = f as Underwater;
                break;
            }
        }
    }

    void Update()
    {
        if (underwater == null) return;

        bool isUnderwater = Camera.main.transform.position.y < waterSurface.position.y;

        //ENTER WATER
        if (isUnderwater)
        {
            wasUnderwater = true;
            feature.SetActive(true);

            //Reset all fades
            darknessFadeValue = 1f;
            refractionFadeValue = 1f;

            underwater.settings.DepthIntensity = originalDepth;
            underwater.settings.BlendAmount = originalBlend;
            underwater.settings.Distortion = originalDistortion;

            distortionTimer = distortionHoldTime;
            return;
        }

        //EXIT WATER (first frame)
        if (wasUnderwater)
        {
            wasUnderwater = false;
        }

        //FADE DARKNESS OUT (depth + blend)
        if (darknessFadeValue > 0f)
        {
            darknessFadeValue -= Time.deltaTime / darknessFadeTime;
            darknessFadeValue = Mathf.Clamp01(darknessFadeValue);

            underwater.settings.DepthIntensity = originalDepth * darknessFadeValue;
            underwater.settings.BlendAmount = originalBlend * darknessFadeValue;

            underwater.settings.Distortion = originalDistortion; //still 100% for now
            return;
        }

        //DARKNESS ALREADY GONE → HOLD DISTORTION
        if (distortionTimer > 0f)
        {
            distortionTimer -= Time.deltaTime;

            underwater.settings.Distortion = originalDistortion;
            return;
        }

        //FADE REFRACTION OUT (distortion)
        if (refractionFadeValue > 0f)
        {
            refractionFadeValue -= Time.deltaTime / distortionFadeTime;
            refractionFadeValue = Mathf.Clamp01(refractionFadeValue);

            underwater.settings.Distortion = originalDistortion * refractionFadeValue;
        }
        else
        {
            //end
            feature.SetActive(false);
        }
        
        
    }
    
    void OnDestroy()
    {
        if (feature != null)
            feature.SetActive(false);

        if (underwater != null)
        {
            underwater.settings.DepthIntensity = 0f;
            underwater.settings.BlendAmount = 0f;
            underwater.settings.Distortion = 0f;
        }
    }
}