Shader "Unlit/ShimmerShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _AlphaColor("Alpha Color", Color) = (1,1,1,1)
        _Mask("Mask", 2D) = "white" {}
        _Width("Width", Range(-0.5,0.5)) = 0.2
        _Falloff("Falloff", Range(0,1)) = 0.2
        _Fraction("Fraction", Range(0, 1)) = 0.5
        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
            LOD 100

            Cull Off
            Lighting Off
            ZWrite Off
            Fog{ Mode Off }
            Blend SrcAlpha OneMinusSrcAlpha

            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

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
                float4 _MainTex_ST;

                fixed4 _Color;
                fixed4 _AlphaColor;
                sampler2D _Mask;

                float _Width;
                float _Falloff;
                float _Fraction;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {

                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                    float maskAlpha = tex2D(_Mask, i.uv).a;

                    float gradient = 1 / _Falloff;

                    // remap the fraction
                    _Fraction = _Fraction * (1 + 2 * _Width + 2 * _Falloff) - (_Width + _Falloff);

                    // lower ramp
                    float a1 = clamp(1 - gradient * (col.r - _Fraction - _Width), 0, 1);
                    float a2 = clamp(1 + gradient * (col.r - _Fraction + _Width), 0, 1);

                    float a = a1 * a2 * maskAlpha * _AlphaColor.a;

                    return fixed4(_Color.r, _Color.g, _Color.b, a);
                }
                ENDCG
            }
        }
}
