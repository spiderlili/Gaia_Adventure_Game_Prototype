Shader "UI/RoundedRectanlgeOne"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)

        _CornerRadius("Corner Radius", Range(0,1)) = 0

        _AntiAliasing("Anti-Aliasing", Range(0,0.1)) = 0

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Stencil
        {
            Ref[_Stencil]
            Comp[_StencilComp]
            Pass[_StencilOp]
            ReadMask[_StencilReadMask]
            WriteMask[_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest[unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask[_ColorMask]

        Pass
        {
            Name "Default"

            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"
                #include "UnityUI.cginc"

                #pragma multi_compile __ UNITY_UI_ALPHACLIP

                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    float4 color : COLOR;
                    float2 texcoord  : TEXCOORD0;
                    float4 worldPosition : TEXCOORD1;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                float4 _Color;
                float4 _TextureSampleAdd;
                float4 _ClipRect;

                v2f vert(appdata_t IN)
                {
                    v2f OUT;
                    UNITY_SETUP_INSTANCE_ID(IN);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                    OUT.worldPosition = IN.vertex;
                    OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                    OUT.texcoord = IN.texcoord;

                    OUT.color = IN.color * _Color;
                    return OUT;
                }

                sampler2D _MainTex;
                float _CornerRadius;

                float _AntiAliasing;

                float GetDistance(float2 a, float x, float y)
                {
                    float xSquared = (a.x - x) * (a.x - x);
                    float ySquared = (a.y - y) * (a.y - y);
                    return sqrt(xSquared + ySquared);
                }

                float4 frag(v2f IN) : SV_Target
                {
                    half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                    color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

                    float2 centre = (0.5, 0.5);

                    float myY = IN.texcoord.y > centre.y ? (1 - _CornerRadius) : _CornerRadius;

                    float myX = IN.texcoord.x > centre.x ? (1 - _CornerRadius) : _CornerRadius;

                    float2 nearestCorner = (myX, myY);
                    
                    if (IN.texcoord.x < _CornerRadius || IN.texcoord.x > 1 - _CornerRadius)
                    {
                        if (IN.texcoord.y < _CornerRadius || IN.texcoord.y > 1 - _CornerRadius)
                        {
                            if (GetDistance(IN.texcoord, myX, myY) > _CornerRadius)
                            {
                                color.a = 0;
                            }
                        }
                    }

                    if (IN.texcoord.x < _CornerRadius || IN.texcoord.x > 1 - _CornerRadius)
                    {
                        if (IN.texcoord.y < _CornerRadius || IN.texcoord.y > 1 - _CornerRadius)
                        {
                            fixed distance = GetDistance(IN.texcoord, myX, myY);
                            if (distance > _CornerRadius)
                            {
                                color.a = 0;

                                if (distance < _CornerRadius + _AntiAliasing)
                                {
                                    color.a *= ((_CornerRadius + _AntiAliasing) - distance) / _AntiAliasing;
                                }
                            }
                        }
                    }
                    
                    #ifdef UNITY_UI_ALPHACLIP
                            clip(color.a - 0.001);
                    #endif

                    return color;
                }
            ENDCG
        }
    }
}
