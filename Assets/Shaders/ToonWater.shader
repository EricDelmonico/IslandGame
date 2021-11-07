Shader "Roystan/Toon/Water"
{
    Properties
    {	
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1

        // As a new property in Properties.
        _SurfaceNoise("Surface Noise", 2D) = "white" {}

        // Add as a new property.
        _SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777

        // Control for what depth the shoreline is visible.
        _FoamDistance("Foam Distance", Float) = 0.4

        // Property to control scroll speed, in UVs per second.
        _SurfaceNoiseScroll("Surface Noise Scroll Amount", Vector) = (0.03, 0.03, 0, 0)

        // Two channel distortion texture.
        _SurfaceDistortion("Surface Distortion", 2D) = "white" {}
        // Control to multiply the strength of the distortion.
        _SurfaceDistortionAmount("Surface Distortion Amount", Range(0, 1)) = 0.27

        // Replace the _FoamDistance property with the following two properties.
        _FoamMaxDistance("Foam Maximum Distance", Float) = 0.4
        _FoamMinDistance("Foam Minimum Distance", Float) = 0.04

        _FoamColor("Foam Color", Color) = (1,1,1,1)
    }
    SubShader
    {
            // Add just inside the SubShader, below its opening curly brace.
        Tags
        {
            "RenderPipeline" = "HDRenderPipeline"
            "Queue" = "Transparent"
        }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
			CGPROGRAM
            // Insert just after the CGPROGRAM begins.
            #define SMOOTHSTEP_AA 0.01
            // Add inside the Pass, just above the CGPROGRAM's start.
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 alphaBlend(float4 top, float4 bottom)
            {
                float3 color = (top.rgb * top.a) + (bottom.rgb * (1 - top.a));
                float alpha = top.a + bottom.a * (1 - top.a);

                return float4(color, alpha);
            }

            struct appdata
            {
                float4 vertex : POSITION;
                // Add in the appdata struct.
                float4 uv : TEXCOORD0;
                // Add to appdata.
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                // Inside the v2f struct.
                float4 screenPosition : TEXCOORD2;
                // Add in the v2f struct.
                float2 noiseUV : TEXCOORD0;

                // New data in v2f.
                float2 distortUV : TEXCOORD1;

                // Add to v2f.
                float3 viewNormal : NORMAL;
            };

            // Above the vertex shader.
            sampler2D _SurfaceNoise;
            float4 _SurfaceNoise_ST;

            // Matching variables.
            sampler2D _SurfaceDistortion;
            float4 _SurfaceDistortion_ST;

            float _SurfaceDistortionAmount;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);

                // Inside the vertex shader.
                o.screenPosition = ComputeScreenPos(o.vertex);
                // Inside the vertex shader.
                o.noiseUV = TRANSFORM_TEX(v.uv, _SurfaceNoise);

                // Add to the vertex shader.
                o.distortUV = TRANSFORM_TEX(v.uv, _SurfaceDistortion);

                // Add to the vertex shader.
                o.viewNormal = COMPUTE_VIEW_NORMAL;

                return o;
            }

            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;

            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;

            // Matching property variable.
            float _SurfaceNoiseCutoff;

            // Replace the _FoamDistance variable with the following two variables.
            float _FoamMaxDistance;
            float _FoamMinDistance;

            float2 _SurfaceNoiseScroll;

            float4 _FoamColor;

            // As this refers to a global shader variable, it does not get declared in the Properties.
            sampler2D _CameraNormalsTexture;

            float4 frag (v2f i) : SV_Target
            {
                float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01);
                float depthDifference = existingDepthLinear - i.screenPosition.w;
                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

                // Add the fragment shader, just above the current noiseUV declaration line.
                float2 distortSample = (tex2D(_SurfaceDistortion, i.distortUV).xy * 2 - 1) * _SurfaceDistortionAmount;

                // Add in the fragment shader, above the existing surfaceNoiseSample line.
                float2 noiseUV = float2(i.noiseUV.x + _Time.y * _SurfaceNoiseScroll.x, i.noiseUV.y + _Time.y * _SurfaceNoiseScroll.y);
                float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).r;

                // Add to the fragment shader, just above the existing foamDepthDifference01 line.
                float3 existingNormal = tex2Dproj(_CameraNormalsTexture, UNITY_PROJ_COORD(i.screenPosition));

                // Add to the fragment shader, below the line sampling the normals texture.
                float3 normalDot = saturate(dot(existingNormal, i.viewNormal));

                // Add to the fragment shader, above the existing foamDepthDifference01 line.
                float foamDistance = lerp(_FoamMaxDistance, _FoamMinDistance, normalDot);
                float foamDepthDifference01 = saturate(depthDifference / foamDistance);
                float surfaceNoiseCutoff = foamDepthDifference01 * _SurfaceNoiseCutoff;

                float surfaceNoise = smoothstep(surfaceNoiseCutoff - SMOOTHSTEP_AA, surfaceNoiseCutoff + SMOOTHSTEP_AA, surfaceNoiseSample);

                // Place in the fragment shader, replacing the code in its place.
                // Add inside the fragment shader, just below the line declaring surfaceNoise.
                float4 surfaceNoiseColor = _FoamColor * surfaceNoise;
                surfaceNoiseColor.a *= surfaceNoise;

                return alphaBlend(surfaceNoiseColor, waterColor);
            }
            ENDCG
        }
    }
}