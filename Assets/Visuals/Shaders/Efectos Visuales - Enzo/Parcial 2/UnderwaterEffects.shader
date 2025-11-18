Shader "Custom/UnderwaterEffect"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NormalMap ("Distortion Map", 2D) = "bump" {}
        _normalUV ("UV Settings", Vector) = (1, 1, 0.1, 0.1)
        _color ("Tint Color", Color) = (0, 0.4, 0.7, 1)
        _FogDensity ("Depth Intensity", Range(0, 10)) = 0.5
        _alpha ("Blend Amount", Range(0, 1)) = 0.5
        _refraction ("Distortion", Range(0, 1)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _NormalMap;
            sampler2D_float _CameraDepthTexture;
            
            float4 _MainTex_ST;
            float4 _normalUV;
            fixed4 _color;
            float _FogDensity;
            float _alpha;
            float _refraction;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (VertexOutput i) : SV_Target
            {
                // Sample and unpack normal map with animation
                float2 normalUV = i.uv * _normalUV.xy + _normalUV.zw * _Time.y;
                fixed3 normal = UnpackNormal(tex2D(_NormalMap, normalUV));

                // Calculate refraction offset
                float2 refractionOffset = normal.xy * _refraction * 0.01;
                float2 distortedUV = i.uv + refractionOffset;

                // Sample depth and linearize
                float rawDepth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, distortedUV));
                float linearDepth = Linear01Depth(rawDepth);
                float depthFactor = linearDepth * linearDepth;

                // Calculate fog
                float fog = 1.0 - exp(-_FogDensity * depthFactor);
                
                // Sample main texture with distortion
                fixed4 sceneColor = tex2D(_MainTex, distortedUV);

                // Blend with underwater color
                float blendFactor = saturate(fog * 1000.0 + _alpha);
                return lerp(sceneColor, _color, blendFactor);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}