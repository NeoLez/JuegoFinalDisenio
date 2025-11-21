using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ToxicEffectScript : ScriptableRendererFeature
{
    // Arrastra aquí tu material con el shader tóxico
    public Material materialDelShader;
    public RenderPassEvent momentoDeRender = RenderPassEvent.BeforeRenderingPostProcessing;

    CustomPostProcessPass miPase;

   
    class CustomPostProcessPass : ScriptableRenderPass
    {
        Material materialUsado;
        RTHandle texturaCamara;

        public CustomPostProcessPass(Material mat, RenderPassEvent evt)
        {
            this.materialUsado = mat;
            this.renderPassEvent = evt;
        }

        
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            texturaCamara = renderingData.cameraData.renderer.cameraColorTargetHandle;
        }

        
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (materialUsado == null) return;
            if (renderingData.cameraData.isPreviewCamera) return;  // Evita aplicar el efecto en cámaras de vista previa
            CommandBuffer cmd = CommandBufferPool.Get("Mi Post Proceso Simple");

            // Aplica el material a la textura de la cámara
            Blit(cmd, texturaCamara, texturaCamara, materialUsado);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    
    public override void Create()
    {
        miPase = new CustomPostProcessPass(materialDelShader, momentoDeRender);
    }

    
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (materialDelShader != null)
        {
            renderer.EnqueuePass(miPase);
        }
    }
}