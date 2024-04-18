Shader "Custom/WaterShader"
{
    Properties
    {
        _Color("Color", Color) = (.5,.5,.5,1)
        _MainTex("Albedo (RGB)", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 c = tex2D(_MainTex, i.uv) * _Color;
                return c;
            }
            ENDCG
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Offset 1,1
            ZWrite Off // Disable depth writing

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _Color; // Color for deep water
            half _WaveStrength; // Strength of wave effect

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half depth = i.pos.z / i.pos.w; // Calculate depth based on clip space
                half alpha = 1.0 - depth; // Inverse depth for transparency
                alpha = saturate(alpha * _WaveStrength); // Apply wave strength

                return half4(_Color.rgb, alpha); // Set color with adjusted alpha
            }
            ENDCG
        }
    }
}