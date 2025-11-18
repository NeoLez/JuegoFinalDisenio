using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
[ExecuteInEditMode]

public class Underwater : ScriptableRendererFeature
{
    [System.Serializable]
    public class WaterSettings
    {
        // Material containing the water distortion shader
        public Material material;

        // Pipeline injection point for this effect
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingSkybox;

        // Visual parameters matching shader properties
        public Color TintColor;
        public float DepthIntensity = 1;
        [Range(0, 1)]
        public float BlendAmount;
        public float Distortion = 0.1f;

        // Distortion map texture
        public Texture DistortionMap;

        // UV animation settings (tiling X/Y, scroll speed X/Y)
        public Vector4 UVSettings = new Vector4(1, 1, 0.2f, 0.1f);
    }

    public WaterSettings settings = new WaterSettings();

    class WaterRenderPass : ScriptableRenderPass
    {
        public WaterSettings settings;
        private RenderTargetIdentifier sourceTarget;
        RenderTargetHandle tempRenderTarget;

        private string passName;

        public void SetupPass(RenderTargetIdentifier source)
        {
            // Store camera render target reference
            this.sourceTarget = source;
        }

        public WaterRenderPass(string name)
        {
            // Profiler label for performance tracking
            this.passName = name;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // Allocate temporary texture matching camera resolution
            cmd.GetTemporaryRT(tempRenderTarget.id, cameraTextureDescriptor);

            // Set temporary texture as render destination
            ConfigureTarget(tempRenderTarget.Identifier());

            // Clear before drawing
            ConfigureClear(ClearFlag.All, Color.black);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            // Get command buffer for recording GPU commands
            CommandBuffer cmd = CommandBufferPool.Get(passName);
            cmd.Clear();
            
            try
            {
                // Update shader properties with current settings
                settings.material.SetFloat("_FogDensity", settings.DepthIntensity);
                settings.material.SetFloat("_alpha", settings.BlendAmount);
                settings.material.SetColor("_color", settings.TintColor);
                settings.material.SetTexture("_NormalMap", settings.DistortionMap);
                settings.material.SetFloat("_refraction", settings.Distortion);
                settings.material.SetVector("_normalUV", settings.UVSettings);

                // Copy camera output to temporary buffer
                cmd.Blit(sourceTarget, tempRenderTarget.Identifier());

                // Apply water effect shader and write back to camera target
                cmd.Blit(tempRenderTarget.Identifier(), sourceTarget, settings.material, 0);

                // Execute all queued commands
                context.ExecuteCommandBuffer(cmd);
            }
            catch
            {
                Debug.LogError("Failed to execute water effect render pass.");
            }

            // Release command buffer resources
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }

    WaterRenderPass renderPass;
    RenderTargetHandle targetHandle;

    public override void Create()
    {
        // Initialize the custom render pass
        renderPass = new WaterRenderPass("Water Effect Pass");

        // Display name in URP Renderer Features list
        name = "Water Effect";

        // Link settings to render pass
        renderPass.settings = settings;

        // Set execution timing in pipeline
        renderPass.renderPassEvent = settings.renderPassEvent;
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        // Get camera's color output buffer
        var cameraColorTarget = renderer.cameraColorTarget;

        // Provide render target to pass
        renderPass.SetupPass(cameraColorTarget);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // Queue render pass for execution
        renderer.EnqueuePass(renderPass);
    }
}