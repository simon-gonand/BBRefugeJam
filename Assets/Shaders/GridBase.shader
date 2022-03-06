Shader "Jam/Grid"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Base Color", Color) = (1,1,1,1)
        _ColorR("Blend Color R", Color) = (1,1,1,1)

        [Toggle]_OnlyVCol ("Only Use Vertex Color", Float) = 0

        [Header(Specular)][Space(10)]
        _SpecIntensity("Specular Intensity", Range(0.0,1.0)) = 0.
        _SpecSize("Specular Size", Range(0.0,1.0)) = 0.9
        _SpecSmooth("Specular Smooth", Float) = 0.

        [Header(Specular)][Space(10)]
        [HDR] _FresnelCol("Fresnel Color", Color) = (1,1,1,0)
        _FresnelMin ("Fresnel Min", Float) = 0
        _FresnelMax ("Fresnel Max", Float) = 1
    }
    SubShader
    {
        LOD 100

        Pass
        {
            Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase"}

            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1)
                float4 pos : SV_POSITION;
                float4 color : COLOR;
                float3 worldNormal : NORMAL;
                half4 worldPos : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST, _Color, _ColorR, _FresnelCol;
            float _SpecIntensity, _SpecSize, _SpecSmooth, _FresnelMin, _FresnelMax, _OnlyVCol;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos);
                float3 worldScale = float3(
                    length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x)), // scale x axis
                    length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y)), // scale y axis
                    length(float3(unity_ObjectToWorld[0].z, unity_ObjectToWorld[1].z, unity_ObjectToWorld[2].z))  // scale z axis
                    );
                o.worldPos = fixed4(worldScale, 1.);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                o.viewDir = normalize(WorldSpaceViewDir(v.pos));
                TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed3 col;

                float3 normal = normalize(i.worldNormal);

                float fresnel = dot(i.viewDir, normal);
                fresnel = smoothstep(_FresnelMax, _FresnelMin, fresnel);

                float NdotL = dot(_WorldSpaceLightPos0, normal);
                NdotL = step(0.0, NdotL);

                float3 r = reflect(-_WorldSpaceLightPos0, normal);
                float spec = smoothstep(_SpecSize, _SpecSize + _SpecSmooth, dot(r, i.viewDir));

                float shadow = step(0.5, SHADOW_ATTENUATION(i));
                shadow *= NdotL;
                shadow = (1 - (1 - shadow) * .75);

                float3 diffuse = shadow * _LightColor0;
                diffuse += ShadeSH9(half4(normal, 1));

                float2 worlduv = frac(i.worldPos.xz - .5);

                col.rgb = tex2D(_MainTex, frac(i.uv * i.worldPos.xz));
                col.rgb = lerp(_Color, _ColorR, col.r);
                col.rgb *= diffuse;
                col.rgb += spec * _LightColor0 * _SpecIntensity;

                col.rgb += fresnel * _FresnelCol.rgb * _FresnelCol.a;

                return fixed4(col, 1.);
            }
            ENDCG
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}