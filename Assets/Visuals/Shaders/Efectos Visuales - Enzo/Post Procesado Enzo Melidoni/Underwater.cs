using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
[ExecuteInEditMode]

public class Underwater : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        // Material that contains the shader for the underwater effect
        public Material material;

        // Defines when in the render pipeline this effect is executed
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingSkybox;

        // Shader parameters that control color tint, fog, alpha blending and refraction
        public Color color;
        public float FogDensity = 1;
        [Range(0, 1)]
        public float alpha;
        public float refraction = 0.1f;

        // Normal map used to generate UV distortion (refraction)
        public Texture normalmap;

        // UV parameters for scrolling/animating the normal map
        public Vector4 UV = new Vector4(1,1,0.2f,0.1f);
    }

    public Settings settings = new Settings();

    class Pass : ScriptableRenderPass
    {
        public Settings settings;
        private RenderTargetIdentifier source;
        RenderTargetHandle tempTexture;

        private string profilerTag;

        public void Setup(RenderTargetIdentifier source)
        {
            // Stores the camera's render target so the effect can be applied to it
            this.source = source;
        }

        public Pass(string profilerTag)
        {
            // Name used by the GPU profiler to identify this render pass
            this.profilerTag = profilerTag;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // Creates a temporary render texture with the same descriptor as the camera target
            cmd.GetTemporaryRT(tempTexture.id, cameraTextureDescriptor);

            // Sets the temporary texture as the target for this pass
            ConfigureTarget(tempTexture.Identifier());

            // Clears the temporary texture before rendering
            ConfigureClear(ClearFlag.All, Color.black);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            // Command buffer where draw and blit operations are recorded
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            cmd.Clear();
            
            try
            {
                // Sends all user-defined parameters to the shader
                settings.material.SetFloat("_FogDensity", settings.FogDensity);
                settings.material.SetFloat("_alpha", settings.alpha);
                settings.material.SetColor("_color", settings.color);
                settings.material.SetTexture("_NormalMap", settings.normalmap);
                settings.material.SetFloat("_refraction", settings.refraction);
                settings.material.SetVector("_normalUV", settings.UV);

                // Copies the current camera color buffer into the temporary texture
                cmd.Blit(source, tempTexture.Identifier());

                // Applies the underwater shader to the temporary texture
                // and writes the result back into the camera target
                cmd.Blit(tempTexture.Identifier(), source, settings.material, 0);

                // Submits the recorded commands for execution
                context.ExecuteCommandBuffer(cmd);
            }
            catch
            {
                Debug.LogError("Error executing underwater pass.");
            }

            // Cleans and releases the command buffer
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }

    Pass pass;
    RenderTargetHandle renderTextureHandle;

    public override void Create()
    {
        // Instantiates the render pass that applies the underwater effect
        pass = new Pass("Underwater Effects");

        // Name displayed inside the Renderer Feature list in the URP asset
        name = "Underwater Effects";

        // Assigns user settings to the pass so they can be forwarded to the shader
        pass.settings = settings;

        // Defines when this pass will execute in the render pipeline
        pass.renderPassEvent = settings.renderPassEvent;
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        // Retrieves the camera's color target (the final image before post-processing)
        var cameraColorTargetIdent = renderer.cameraColorTarget;

        // Pass receives the camera render target so it knows where to read from
        pass.Setup(cameraColorTargetIdent);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // Enqueues the render pass so URP will execute it each frame
        renderer.EnqueuePass(pass);
    }
}
