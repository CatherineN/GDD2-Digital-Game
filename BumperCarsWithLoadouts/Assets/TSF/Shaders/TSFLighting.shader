// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TSF/BaseLight" 
{
	Properties
	{

		[MaterialToggle(_TEX_ON)] _DetailTex("Enable Detail texture", Float) = 0 	//1
		_MainTex("Detail", 2D) = "white" {}        								//2
		_ToonShade("Shade", 2D) = "white" {}  										//3
		[MaterialToggle(_COLOR_ON)] _TintColor("Enable Color Tint", Float) = 0 	//4
		_Color("Base Color", Color) = (1,1,1,1)									//5	
		[MaterialToggle(_VCOLOR_ON)] _VertexColor("Enable Vertex Color", Float) = 0//6        
		_Brightness("Brightness 1 = neutral", Float) = 1.0							//7	
		_OutlineColor("Outline Color", Color) = (0.5,0.5,0.5,1.0)					//10
		_Outline("Outline width", Float) = 0.01									//11
																				//[MaterialToggle(_LIGHT_ON)] _Light("Use Lighting", Float) = 0				//12

	}

	Subshader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 250
		ZWrite On
		Cull Back
		Lighting On
		Fog{ Mode Off }

		Pass
		{
			Name "LIGHT"
			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			#pragma glsl_no_auto_normalization
			#pragma multi_compile _TEX_OFF _TEX_ON
			#pragma multi_compile _COLOR_OFF _COLOR_ON


			#if _TEX_ON
					sampler2D _MainTex;
				half4 _MainTex_ST;
			#endif

			struct appdata_base0
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				#if _TEX_ON
				half2 uv : TEXCOORD0;
				#endif
				fixed4 diff : COLOR0;
				half2 uvn : TEXCOORD1;
			};

			v2f vert(appdata_base0 v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				float3 n = mul((float3x3)UNITY_MATRIX_IT_MV, normalize(v.normal));
				normalize(n);
				n = n * float3(0.5,0.5,0.5) + float3(0.5,0.5,0.5);
				o.uvn = n.xy;
				#if _TEX_ON
						o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				#endif
				// Lights
				half nl = max(0, dot(n, _WorldSpaceLightPos0.xyz));
				o.diff = nl * _LightColor0;
				o.diff.rgb += ShadeSH9(half4(n, 1));
				return o;
			}

			sampler2D _ToonShade;
			fixed _Brightness;

			#if _COLOR_ON
				fixed4 _Color;
			#endif
			
			fixed4 frag(v2f i) : COLOR
			{
				#if _COLOR_ON
				fixed4 toonShade = tex2D(_ToonShade, i.uvn)*_Color;
				#else
				fixed4 toonShade = tex2D(_ToonShade, i.uvn);
				#endif
			
				#if _TEX_ON
				fixed4 detail = tex2D(_MainTex, i.uv);
				fixed4 col = toonShade * detail*_Brightness;
				#else
				fixed4 col = toonShade * _Brightness;
				#endif
				return col *= i.diff;
			}
		ENDCG
		}
		//UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
				Pass
			{
				Cull Front
				ZWrite On
				CGPROGRAM
#include "UnityCG.cginc"
#pragma fragmentoption ARB_precision_hint_fastest
#pragma glsl_no_auto_normalization
#pragma vertex vert
#pragma fragment frag


				struct appdata_t
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
			};

			fixed _Outline;


			v2f vert(appdata_t v)
			{
				v2f o;
				o.pos = v.vertex;
				float3 normal = normalize(v.normal);
				o.pos += float4(normal, 0.0) *_Outline * 0.01;
				o.pos = UnityObjectToClipPos(o.pos);
				return o;
			}

			fixed4 _OutlineColor;

			fixed4 frag(v2f i) :COLOR
			{
				return _OutlineColor;
			}

				ENDCG
			}
	}
	Fallback "Legacy Shaders/Diffuse"
}