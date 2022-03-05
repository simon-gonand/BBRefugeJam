Shader "Unlit/Hologram"
{
    Properties
    {
        [HDR] _ColRim("Rim Color", Color) = (0,0,0,1)

        [Header(Rim)][Space(10)]

        _RimStep("Rim Step", Float) = 0.5
        _RimSmooth("Rim Smooth", Float) = 0.5

        [Header(Lines)][Space(10)]
        _LineStep("Line Step", Range(0.0,1.0)) = 0.5
        _LineSmooth("Line Smooth", Range(0.0,1.0)) = 0.5
        _LineScroll("Line Scroll", Float) = 0.
        _LineTile("Line Tile", Float) = 30.
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue"="Transparent"}
        //Blend SrcAlpha OneMinusSrcAlpha
        Blend One One
        Cull Back
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
                float3 n : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 n : NORMAL;
                float3 dir : TEXCOORD0;
                float3 worldpos: TEXCOORD1;
            };


            v2f vert(appdata v)
            {
                v2f o;

                o.n = UnityObjectToWorldNormal(v.n);
                o.dir = normalize(WorldSpaceViewDir(v.vertex));
                o.worldpos = mul(unity_ObjectToWorld, float4(v.vertex.xyz,1));

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 _ColBase, _ColRim;
            half _RimStep, _RimSmooth;
            half _LineStep, _LineSmooth, _LineTile, _LineScroll;


            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = 0;

                float fresnel = dot(i.n, i.dir);
                fresnel = smoothstep(_RimStep + _RimSmooth, _RimStep, fresnel);

                float lines = smoothstep(_LineStep + _LineSmooth, _LineStep, frac(i.worldpos.y * _LineTile - _Time.y * _LineScroll) );

                float a = max(lines, fresnel);
                a = a * (1. - _ColRim.a) + _ColRim.a    ;

                a *= i.worldpos.y/3.;
                col = _ColRim;
                col.a = a;
                col.rgb *= col.a;
                col = max(0, col);
                return col;
            }
            ENDCG
        }
    }
}
