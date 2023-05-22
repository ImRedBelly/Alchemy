Shader "Custom/SingleColorShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
        _EdgeThreshold ("Edge Threshold", Range(0, 1)) = 0.5
        _EdgeWidth ("Edge Width", Range(0, 0.1)) = 0.01
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        
        Blend SrcAlpha OneMinusSrcAlpha
        
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
                float4 color : COLOR;
            };
            
            sampler2D _MainTex;
            fixed4 _Color;
            float _EdgeThreshold;
            float _EdgeWidth;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = _Color;
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                fixed edgeAlpha = smoothstep(_EdgeThreshold - _EdgeWidth, _EdgeThreshold + _EdgeWidth, color.a);
                fixed4 edgeColor = lerp(fixed4(0, 0, 0, 0), i.color, edgeAlpha);
                return color * (1 - edgeAlpha) + edgeColor;
            }
            
            ENDCG
        }
    }
}
