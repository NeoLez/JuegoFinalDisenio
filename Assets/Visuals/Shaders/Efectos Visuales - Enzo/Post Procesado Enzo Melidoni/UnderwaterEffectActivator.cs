using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UnderwaterEffectActivator : MonoBehaviour
{
    public Transform waterSurface;                
    public UniversalRendererData rendererData;    

    private ScriptableRendererFeature underwaterFeature;

    void Start()
    {
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
        
        bool isUnderwater = Camera.main.transform.position.y < waterSurface.position.y;

        underwaterFeature.SetActive(isUnderwater);
    }
}
