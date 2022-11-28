Shader "Custom/GrayscaleExceptAroundCharacter"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)

        _ScreenWidth("Screen width", Float) = 1920
        _ScreenHeight("Screen height", Float) = 1080

        _Radius("Radius", Float) = 20
        _Smoothness("Smoothness", Float) = 0.5

        _Position("Position", vector) = (0.5,0.5,0.5,1)
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

            struct v2f
            {
                float4 vertex:      SV_POSITION;
                float4 position:    TEXCOORD1;
                float2 uv:          TEXCOORD0;
            };

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float3 _Color;
            float _Radius;
            float _Smoothness;
            float3 _Position;
            float _ScreenHeight;
            float _ScreenWidth;

            v2f vert(appdata_base v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.position = v.vertex;
                o.uv = v.texcoord;

                return o;
            }

            float circle(float2 pt, float2 center, float radius, bool soften)
            {
                //circle with soft edge
                float2 p = (pt - center);
                p.x = p.x * _ScreenWidth;
                p.y = p.y * _ScreenHeight;

                //float edge = (soften) ? radius * _Smoothness : 0.0;
                float edge = lerp(0, radius * _Smoothness, soften); // =  0 + (radius * _Smoothness - 0)*soften
                //smoothstep(edge0,edge1,n) returns 0 if n < edge 0 , returns 1 if n > edge1 and some
                //lerp in between
                return 1.0 - smoothstep(radius - edge, radius + edge, length(p));
            }


            float4 frag(v2f i) : SV_Target
            {
               float3 renderTex = tex2D(_MainTex, i.uv).rgb;
               float2 pos = i.position;
               _Color = renderTex.rgb * _Color;
               // 
               //normalize position for screen
               float2 targetPos = float2(_Position.x / _ScreenWidth, _Position.y / _ScreenHeight);
               float inCircle = circle(pos, targetPos, _Radius, true);
               float3 color = fixed3(_Color * inCircle);
               float3 finalColor = lerp(color, renderTex.rrr, 1 - inCircle);
               return float4(finalColor, 1); //new fixed4(finalColor.x, finalColor.y, finalColor.z, 1);
            }

            ENDCG
        }
    }
}
