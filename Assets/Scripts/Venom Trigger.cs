using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VenomTrigger : MonoBehaviour
{
    [Header("Venom Effect (Full Screen Pass Renderer Feature)")]
    public string featureName = "VenomEffect";

    ScriptableRendererFeature targetFeature;

    void Start()
    {
        targetFeature = FindRendererFeature(featureName);

        if (targetFeature == null)
            Debug.LogError("Renderer Feature no encontrado: " + featureName);
    }

    void ActivateFeature(bool state)
    {
        if (targetFeature == null) return;
        
        targetFeature.SetActive(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            ActivateFeature(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            ActivateFeature(false);
    }
    ScriptableRendererFeature FindRendererFeature(string name)
    {
        var urp = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        
        ScriptableRenderer renderer = urp.GetRenderer(urp.defaultRendererIndex);
        
        var featuresField = typeof(ScriptableRenderer)
            .GetField("m_RendererFeatures", BindingFlags.NonPublic | BindingFlags.Instance);

        var featureList = featuresField.GetValue(renderer) as List<ScriptableRendererFeature>;

        foreach (var feature in featureList)
        {
            if (feature.name == name)
                return feature;
        }
        return null;
    }
}
