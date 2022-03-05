Shader "Unlit/Meteorite"
{
    Properties
    {
        _NoiseTex ("Noise", 2D) = "white" {}
        _Noise1Params("Noise 1 : Tiling and Speed", Vector) = (1,1,0,0)
        _Noise2Params("Noise 2 : Tiling and Speed", Vector) = (1,1,0,0)
        _Distortion("Distortion", Float) = 0.1
        [HDR]_Color1("Color 1", Color) = (1,1,1,1)
        [HDR]_Color2("Color 2", Color) = (0,0,0,1)

        [HDR] _FresnelCol("Fresnel Color", Color) = (1,1,1,0)
        _FresnelMin("Fresnel Min", Float) = 0
        _FresnelMax("Fresnel Max", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD3;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                o.color = v.color;
                o.viewDir = normalize(WorldSpaceViewDir(v.pos));
                return o;
            }

            sampler2D _NoiseTex;
            float _FresnelMax, _FresnelMin, _Distortion;
            float4 _Color1, _Color2, _FresnelCol, _Noise1Params, _Noise2Params;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = 1.;
                fixed noise1 = tex2D(_NoiseTex, i.uv * _Noise1Params.xy + _Noise1Params.zw * _Time.y).r - .5;
                fixed noise2 = tex2D(_NoiseTex, i.uv * _Noise2Params.xy + _Noise2Params.zw * _Time.y + noise1 * _Distortion).r;

                col.rgb = lerp(_Color1, _Color2, noise2);
                float fresnel = dot(i.viewDir, i.worldNormal);
                fresnel = smoothstep(_FresnelMax, _FresnelMin, fresnel);
                col.rgb = lerp(col.rgb, _FresnelCol.rgb, fresnel);

                return col;
            }
            ENDCG
        }
    }
}
