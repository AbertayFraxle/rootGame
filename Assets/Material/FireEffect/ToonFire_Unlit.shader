
Shader "Unlit/ToonFire_Unlit"
{
    Properties
    {
        //_SparkleColor("SparkleColor", Color) = (1.,.4,.2, 1)
        _OutColor("OutColor", Color) = (0.95,0.1,0.2,1)
        _MidFireTop("MidFireTop", Color) = (0.9,0.3,0.2,1)
        _MidFireBottom("MidFireBottom", Color) = (0.9,0.6,0.2,1)
        _InnerColor("InnerColor", Color) = (0.9,0.8,0.2,1)
    }
    SubShader
    {
        LOD 100
        Tags
        {
            "RenderPipeline" = "UniversalPipeline" "RenderType" = "Transparent" "Queue" = "Transparent"
        }
        Pass
        {
            Name "Forward"

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "HLSLSupport.cginc"

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            //#include "ToonFireInclude.hlsl"

            #pragma multi_compile_instancing

            float2 hash(float2 p) 
            {
                p = float2(dot(p, float2(137.1, 373.7)), dot(p, float2(269.5, 183.7)));
                return frac(sin(p) * 43758.37);
            }

            float worley(float2 p, float in_timeInSeconds) 
            {
                float2 n = floor(p);
                float2 f = frac(p);
                float r = 1.;
                for (int i = -2; i <= 2; i++) 
                {
                    for (int j = -2; j <= 2; j++) 
                    {
                        float2 o = hash(n + float2(i, j));
                        o = sin(in_timeInSeconds / 2. + hash(n + float2(i, j)) * 6.28) * 0.5 + 0.5;//animate
                        o += float2(i, j);
                        float D1 = distance(o, f);//Euclidean
                        r = min(r, D1);
                    }
                }
                return r;
            }

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


            UNITY_INSTANCING_BUFFER_START(ToonFire_ase)
            UNITY_DEFINE_INSTANCED_PROP(float4, _SparkleColor)
            UNITY_DEFINE_INSTANCED_PROP(float4, _OutColor)
            UNITY_DEFINE_INSTANCED_PROP(float4, _MidFireBottom)
            UNITY_DEFINE_INSTANCED_PROP(float4, _MidFireTop)
            UNITY_DEFINE_INSTANCED_PROP(float4, _InnerColor)
            UNITY_INSTANCING_BUFFER_END(ToonFire_ase)

            v2f vert(appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.vertex = TransformObjectToHClip(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 _SparkleColor_Instance = UNITY_ACCESS_INSTANCED_PROP(ToonFire_ase, _SparkleColor);
                float4 _OutColor_Instance = UNITY_ACCESS_INSTANCED_PROP(ToonFire_ase, _OutColor);
                float4 _MidFireTop_Instance = UNITY_ACCESS_INSTANCED_PROP(ToonFire_ase, _MidFireTop);
                float4 _MidFireBottom_Instance = UNITY_ACCESS_INSTANCED_PROP(ToonFire_ase, _MidFireBottom);
                float4 _InnerColor_Instance = UNITY_ACCESS_INSTANCED_PROP(ToonFire_ase, _InnerColor);

                fixed4 fragColor;
                float iTime = _Time.y;
                float2 uv = i.uv;

                uv = uv * 2.0 - 1.0;

                float c = worley(uv + float2(0., -iTime), iTime) * 0.5;

                c += worley(uv * 2. + float2(sin(iTime * 2.) * 0.5, -iTime * 6.), iTime) * 0.5; //2 Layers worley

                c += (-uv.y - 0.3) * 0.6; //y mask

                float2 p = uv;
                p.x *= 1.5 + smoothstep(-0.3, 1., uv.y) * 1.5;

                float m = smoothstep(1., .5, length(p)); //circle mask

                float c0 = smoothstep(.4, .6, m * c * 3.); //out fire

                float c1 = smoothstep(.5, .52, m * c * 2.); //mid fire

                float c2 = smoothstep(.5, .52, m * c * 1.2 * (-uv.y + 0.3)); //inner fire

                float c3 = pow(worley(uv * 6. + float2(sin(iTime * 4.) * 1., -iTime * 16.), iTime), 1);
                c3 = smoothstep(.5, 1., c3) * m; //sparkle

                float4 col = (float4)0;
                // col=fixed4(_SparkleColor_Instance.xyz, 1.0)*c3;//sparkle
                col = lerp(col,fixed4(_OutColor_Instance.xyz, (uv.y + .8)), c0); //out
                col = lerp(col,fixed4(lerp(_MidFireTop_Instance.xyz, _MidFireBottom_Instance.xyz, -uv.y), 1.0),
                           c1); //mid
                col.xyz = lerp(col, _InnerColor_Instance, c2); //inner

                // if (!IsGammaSpace())
                // {
                // col.xyz = FastSRGBToLinear(col.xyz);
                // }

                fragColor = col;

                return fragColor;
            }
            ENDHLSL
        }
    }
}