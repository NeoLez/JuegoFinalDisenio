using System;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private List<GameObject> currentVfxInstances = new();
    public GameObject vfxPrefab;
    
    public List<DrawingPuzzleArea> planes = new();

    private void Update() {
        foreach (var instance in currentVfxInstances) {
            instance.transform.position = transform.position;
        }
    }

    public void OnScanned()
    {
        if (vfxPrefab != null)
        {
            foreach (var plane in planes) {
                Vector3 spawnPosition = transform.position + Vector3.down * 0.5f; 
                GameObject currentVfxInstance = Instantiate(vfxPrefab, spawnPosition, Quaternion.identity);
                currentVfxInstances.Add(currentVfxInstance);
                GameObject lightgameObject = currentVfxInstance.transform.GetChild(0).gameObject;
                Light light = lightgameObject.GetComponent<Light>();
                GameObject beam = currentVfxInstance.transform.GetChild(1).gameObject;
                GameObject beamVisual = beam.transform.GetChild(0).gameObject;
                Renderer beamRenderer = beamVisual.GetComponent<Renderer>();
                Material materialClone = new Material(beamRenderer.material);
                materialClone.SetColor("_Color", plane.GetComponent<DrawingPuzzleArea>().crateBeamColor);
                beamRenderer.material = materialClone;
                light.color = plane.crateBeamColor;
                float intensity = light.intensity;
                LeanTween.value(beam, 0f, 1f, 19f).setOnUpdate((float val) => {
                    beam.transform.rotation = plane.transform.rotation;
                });

                LeanTween.delayedCall(currentVfxInstance, 20f, o => {
                    currentVfxInstances.Remove(currentVfxInstance);
                    Destroy(currentVfxInstance);
                });
            
                LeanTween.value(lightgameObject, 0f, 19f, 19f).setOnUpdate((float val) => {
                    if(val < 4f)
                        light.intensity = val/4f;
                    else if (val > 15)
                        light.intensity = 1 - (val - 15)/4f;
                    else {
                        light.intensity = 1 - (0.5f - (float)Math.Cos((val - 4) * 2 * Math.PI * 2 / 11f) / 2f) / 2f;
                    }

                    light.intensity *= intensity;
                });
            
                LeanTween.value(beam, 0f, 19, 19f).setOnUpdate((float vale) => {
                    if(vale < 4f)
                        materialClone.SetFloat("_Opacity", vale/4f);
                    else if (vale > 15)
                        materialClone.SetFloat("_Opacity", 1 - (vale - 15)/4f);
                    else {
                        materialClone.SetFloat("_Opacity", 1 - (0.5f - (float)Math.Cos((vale - 4) * 2 * Math.PI * 2 / 11f) / 2f)/1.5f);
                    }
                });
            }
        }
        else
        {
            Debug.LogWarning("No VFX prefab assigned.");
        }
        
    }
}

