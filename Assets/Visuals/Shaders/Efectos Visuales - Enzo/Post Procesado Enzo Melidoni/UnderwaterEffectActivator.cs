using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UnderwaterEffectActivator : MonoBehaviour
{
    public Transform waterSurface;
    public UniversalRendererData rendererData;

    public float darknessFadeTime = 2f;  
    public float distortionHoldTime = 2f;   
    public float distortionFadeTime = 1f;   

    private Underwater underwater;
    private ScriptableRendererFeature feature;

    private bool wasUnderwater;
    private float darknessFadeValue = 1f;
    private float distortionTimer;
    private float refractionFadeValue = 1f;

    //ORIGINAL VALUES (DO NOT CHANGE)
    private const float ORIGINAL_FOG = 0.04f;
    private const float ORIGINAL_ALPHA = 0.2f;
    private const float ORIGINAL_REFRACTION = 1f;

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

            underwater.settings.FogDensity = ORIGINAL_FOG;
            underwater.settings.alpha = ORIGINAL_ALPHA;
            underwater.settings.refraction = ORIGINAL_REFRACTION;

            distortionTimer = distortionHoldTime;
            return;
        }

        //EXIT WATER (first frame)
        if (wasUnderwater)
        {
            wasUnderwater = false;
        }

        //FADE DARKNESS OUT (fog + alpha)
        if (darknessFadeValue > 0f)
        {
            darknessFadeValue -= Time.deltaTime / darknessFadeTime;
            darknessFadeValue = Mathf.Clamp01(darknessFadeValue);

            underwater.settings.FogDensity = ORIGINAL_FOG * darknessFadeValue;
            underwater.settings.alpha = ORIGINAL_ALPHA * darknessFadeValue;

            underwater.settings.refraction = ORIGINAL_REFRACTION; //still 100% for now
            return;
        }

        // 2) DARKNESS ALREADY GONE → HOLD DISTORTION
        if (distortionTimer > 0f)
        {
            distortionTimer -= Time.deltaTime;

            underwater.settings.refraction = ORIGINAL_REFRACTION;
            return;
        }

        //FADE REFRACTION OUT (distortion)
        if (refractionFadeValue > 0f)
        {
            refractionFadeValue -= Time.deltaTime / distortionFadeTime;
            refractionFadeValue = Mathf.Clamp01(refractionFadeValue);

            underwater.settings.refraction = ORIGINAL_REFRACTION * refractionFadeValue;
        }
        else
        {
            //end
            feature.SetActive(false);
        }
    }
}
