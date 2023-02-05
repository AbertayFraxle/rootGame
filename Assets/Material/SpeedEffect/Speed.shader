// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Speed"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_SpeedLineAnim("SpeedLineAnim", Float) = 3
		_SpeedLinePower("SpeedLinePower", Float) = 1
		_SpeedLineRemap("SpeedLineRemap", Range( 0 , 1)) = 0.20853
		_Mask("Mask", Range( -1 , 1)) = 0
		_MaskPower("MaskPower", Float) = 1
		_MaskSmoothness("MaskSmoothness", Range( 0 , 1)) = 0
		_SpeedLineInt("SpeedLineInt", Float) = 1
		[HDR]_SpeedLineColor("SpeedLineColor", Color) = (1,1,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				float4 ase_texcoord4 : TEXCOORD4;
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform float _SpeedLineAnim;
			uniform float _SpeedLinePower;
			uniform float _SpeedLineRemap;
			uniform float _MaskSmoothness;
			uniform float _Mask;
			uniform float _MaskPower;
			uniform float _SpeedLineInt;
			uniform float4 _SpeedLineColor;
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord4 = screenPos;
				
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv_MainTex = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 screenPos = i.ase_texcoord4;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float temp_output_3_0 = ( ase_screenPosNorm.y - 0.46 );
				float temp_output_7_0 = length( ( temp_output_3_0 * 1.08 ) );
				float2 appendResult15 = (float2(( ( atan( temp_output_3_0 ) / UNITY_PI ) * 300.0 ) , temp_output_7_0));
				float simplePerlin2D21 = snoise( ( appendResult15 + ( -_Time.y * _SpeedLineAnim ) )*0.5 );
				simplePerlin2D21 = simplePerlin2D21*0.5 + 0.5;
				float temp_output_1_0_g1 = _SpeedLineRemap;
				float SpeedLine26 = saturate( ( ( pow( simplePerlin2D21 , _SpeedLinePower ) - temp_output_1_0_g1 ) / ( 0.0 - temp_output_1_0_g1 ) ) );
				float smoothstepResult33 = smoothstep( _MaskSmoothness , 1.0 , pow( saturate( ( temp_output_7_0 - _Mask ) ) , _MaskPower ));
				float SpeedLineMask35 = smoothstepResult33;
				

				finalColor = ( tex2D( _MainTex, uv_MainTex ) + ( SpeedLine26 * SpeedLineMask35 * _SpeedLineInt * _SpeedLineColor ) );

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18912
-62;562;1437;729;-1719.245;765.8414;1.190105;True;True
Node;AmplifyShaderEditor.ScreenPosInputsNode;1;-1088.701,-634.4467;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-875.2535,-428.3248;Inherit;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;0;False;0;False;0.46;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;3;-620.2535,-605.3248;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-597.2535,-358.3248;Inherit;False;Constant;_Float1;Float 1;0;0;Create;True;0;0;0;False;0;False;1.08;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ATanOpNode;10;-248.2535,-655.3248;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;12;-215.2535,-533.3248;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;17;334.6604,-484.4929;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;47.74646,-503.3248;Inherit;False;Constant;_SpeedLineTilling;SpeedLineTilling;0;0;Create;True;0;0;0;False;0;False;300;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-416.2535,-495.3248;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;11;-15.25354,-660.3248;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;7;-248.2535,-424.3248;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;261.7465,-660.3248;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;18;584.2604,-457.1928;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;607.6603,-288.193;Inherit;False;Property;_SpeedLineAnim;SpeedLineAnim;0;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;823.4604,-432.493;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;531.7465,-665.3248;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;1030.161,-646.9932;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-292.0531,-171.756;Inherit;False;Property;_Mask;Mask;3;0;Create;True;0;0;0;False;0;False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;21;1310.96,-656.0932;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;1286.259,-527.3932;Inherit;False;Property;_SpeedLinePower;SpeedLinePower;1;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;28;49.9469,-272.756;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;30;242.9469,-271.756;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;238.9777,-141.611;Inherit;False;Property;_MaskPower;MaskPower;4;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;1515.06,-827.6935;Inherit;False;Property;_SpeedLineRemap;SpeedLineRemap;2;0;Create;True;0;0;0;False;0;False;0.20853;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;22;1590.46,-669.0932;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;31;432.9469,-249.756;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;252.8351,-52.70911;Inherit;False;Property;_MaskSmoothness;MaskSmoothness;5;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;24;1967.46,-734.0933;Inherit;True;Inverse Lerp;-1;;1;09cbe79402f023141a4dc1fddd4c9511;0;3;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;43;2292.877,-732.5201;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;33;633.8351,-142.7091;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;26;2551.427,-739.293;Inherit;False;SpeedLine;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;35;967.8352,-139.7091;Inherit;False;SpeedLineMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;41;1963.746,194.3084;Inherit;False;Property;_SpeedLineColor;SpeedLineColor;7;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;40;1975.235,84.68829;Inherit;False;Property;_SpeedLineInt;SpeedLineInt;6;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;27;1987.911,-119.0871;Inherit;False;26;SpeedLine;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;36;1863.806,-339.1059;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;39;1967.913,-4.701688;Inherit;False;35;SpeedLineMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;2302.058,-113.4102;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;37;2061.616,-343.6358;Inherit;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;42;2565.28,-323.4122;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;2;-805.2535,-705.3248;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;2932.224,-620.9863;Float;False;True;-1;2;ASEMaterialInspector;0;4;Speed;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;3;0;1;2
WireConnection;3;1;4;0
WireConnection;10;0;3;0
WireConnection;5;0;3;0
WireConnection;5;1;6;0
WireConnection;11;0;10;0
WireConnection;11;1;12;0
WireConnection;7;0;5;0
WireConnection;13;0;11;0
WireConnection;13;1;14;0
WireConnection;18;0;17;0
WireConnection;19;0;18;0
WireConnection;19;1;20;0
WireConnection;15;0;13;0
WireConnection;15;1;7;0
WireConnection;16;0;15;0
WireConnection;16;1;19;0
WireConnection;21;0;16;0
WireConnection;28;0;7;0
WireConnection;28;1;29;0
WireConnection;30;0;28;0
WireConnection;22;0;21;0
WireConnection;22;1;23;0
WireConnection;31;0;30;0
WireConnection;31;1;32;0
WireConnection;24;1;25;0
WireConnection;24;3;22;0
WireConnection;43;0;24;0
WireConnection;33;0;31;0
WireConnection;33;1;34;0
WireConnection;26;0;43;0
WireConnection;35;0;33;0
WireConnection;38;0;27;0
WireConnection;38;1;39;0
WireConnection;38;2;40;0
WireConnection;38;3;41;0
WireConnection;37;0;36;0
WireConnection;42;0;37;0
WireConnection;42;1;38;0
WireConnection;0;0;42;0
ASEEND*/
//CHKSM=9FD38F2CCA85E5670F7639E21776576CBBFC36B9