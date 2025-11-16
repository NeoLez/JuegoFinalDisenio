using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UnderwaterEffectActivator : MonoBehaviour
{
    public Transform waterSurface;                 // Asigná tu objeto WaterSurface acá
    public UniversalRendererData rendererData;     // Arrastrá tu Renderer (URP Asset) acá

    private ScriptableRendererFeature underwaterFeature;

    void Start()
    {
        // Buscar automáticamente un Renderer Feature relacionado al agua
        foreach (var feature in rendererData.rendererFeatures)
        {
            string n = feature.name.ToLower();

            if (n.Contains("water") || n.Contains("underwater") || n.Contains("ocean"))
            {
                underwaterFeature = feature;
                Debug.Log("Underwater feature encontrado: " + feature.name);
                break;
            }
        }

        if (underwaterFeature == null)
            Debug.LogWarning("No pude encontrar el renderer feature de agua. Poné un nombre que contenga 'Water' o 'Underwater'.");
    }

    void Update()
    {
        if (underwaterFeature == null) return;

        // Si la cámara está debajo de la superficie del agua
        bool isUnderwater = Camera.main.transform.position.y < waterSurface.position.y;

        underwaterFeature.SetActive(isUnderwater);
    }
}
