Shader "OrderRush/GridFloor"
{
    Properties
    {
        _LineWidth ("Line Width", Float)  = 0.02
        _LineColor ("Line Color", Color)  = (1,1,1,1)
        _BaseColor ("Base Color", Color)  = (0.15,0.15,0.15,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        Pass
        {
            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float  _LineWidth;
            fixed4 _LineColor;
            fixed4 _BaseColor;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos      : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos      = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 grid   = frac(float2(i.worldPos.x, i.worldPos.z));
                float  lineX  = step(1.0 - _LineWidth, grid.x);
                float  lineY  = step(1.0 - _LineWidth, grid.y);
                float  isLine = saturate(lineX + lineY);
                return lerp(_BaseColor, _LineColor, isLine);
            }
            ENDCG
        }
    }
}