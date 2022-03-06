Shader "Unlit/PC_RGB"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Brightness ("Brightness", Float) = 1
        _Speed ("Speed", Float) = 0
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
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv-.5;
                return o;
            }

#define TAU 6.283185307179586
            float _Brightness, _Speed;

            fixed4 frag(v2f i) : SV_Target
            {
                float angle = atan2(i.uv.y, i.uv.x) / TAU;
                fixed4 col = tex2D(_MainTex, angle + _Speed * _Time.y) * _Brightness;
                return col;
            }
            ENDCG
        }
    }
}
