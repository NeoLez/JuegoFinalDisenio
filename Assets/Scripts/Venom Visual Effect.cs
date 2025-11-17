using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class VenomVisualEffect : MonoBehaviour
{
    public Material effectMaterial;
    public bool activeEffect = false;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (activeEffect && effectMaterial != null)
            Graphics.Blit(src, dest, effectMaterial);
        else
            Graphics.Blit(src, dest);
    }
}
