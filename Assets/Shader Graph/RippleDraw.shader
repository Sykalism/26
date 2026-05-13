Shader "Custom/RippleDraw"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
        _RippleTime ("RippleTime", Float) = 0
        _RippleScale ("RippleScale", Float) = 20
        _RippleSpeed ("RippleSpeed", Float) = 6
        _RippleThickness ("RippleThickness", Float) = 0
        _RippleSoftness ("RippleSoftness", Float) = 0.3
        _RippleStrenght ("RippleStrenght", Float) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            ZWrite Off
            Cull Off

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float4 _Center;
            float _RippleTime;
            float _RippleScale;
            float _RippleSpeed;
            float _RippleThickness;
            float _RippleSoftness;
            float _RippleStrenght;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv;

                half4 oldColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

                float dist = distance(uv, _Center.xy);
                dist *= _RippleScale;

                float fade = exp(-_RippleTime * 3);

                float wavePos = dist - (_RippleTime * 6);

                float ripple = 1 - smoothstep
                (
                    _RippleThickness,
                    _RippleThickness + _RippleSoftness,
                    abs(wavePos)
                );

                ripple *= fade;

                return saturate(oldColor + ripple * _RippleStrenght);
            }

            ENDHLSL
        }
    }
}