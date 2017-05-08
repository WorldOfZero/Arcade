// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Low Poly Shader developed as part of World of Zero: http://youtube.com/worldofzerodevelopment
// Based upon the example at: http://www.battlemaze.com/?p=153

Shader "Custom/LowPolyShader" {
	Properties{
		[HDR]_BackgroundColor("Background Color", Color) = (1,0,0,1)
		[HDR]_ForegroundColor("Foreground Color", Color) = (0,0,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Cutoff("Cutoff", Range(0,1)) = 0.25
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			Pass
		{

			CULL FRONT

			CGPROGRAM
			#include "UnityCG.cginc" 
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom

			// Use shader model 4.0 target, we need geometry shader support
			#pragma target 4.0

			sampler2D _MainTex;

			struct v2g
			{
				float4 pos : SV_POSITION;
				float3 norm : NORMAL;
				float2 uv : TEXCOORD0;
				float3 color : TEXCOORD1;
			};

			struct g2f
			{
				float4 pos : SV_POSITION;
				float3 norm : NORMAL;
				//float2 uv : TEXCOORD0;
				float3 diffuseColor : TEXCOORD1;
				//float3 specularColor : TEXCOORD2;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _BackgroundColor;
			fixed4 _ForegroundColor;

			v2g vert(appdata_full v)
			{
				float3 v0 = v.vertex.xyz;

				v2g OUT;
				OUT.pos = v.vertex;
				OUT.norm = v.normal;
				OUT.uv = v.texcoord;
				OUT.color = tex2Dlod(_MainTex, v.texcoord).rgb;
				return OUT;
			}

			[maxvertexcount(3)]
			void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
			{
				float3 lightPosition = _WorldSpaceLightPos0;

				float3 v0 = IN[0].pos.xyz;
				float3 v1 = IN[1].pos.xyz;
				float3 v2 = IN[2].pos.xyz;

				float3 normal = normalize(cross(v0 - v1, v1 - v2));

				float3 color = (IN[0].color + IN[1].color + IN[2].color) / 3;

				g2f OUT;
				OUT.pos = mul(UNITY_MATRIX_MVP, IN[0].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color;
				triStream.Append(OUT);

				OUT.pos = mul(UNITY_MATRIX_MVP, IN[1].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color;
				triStream.Append(OUT);

				OUT.pos = mul(UNITY_MATRIX_MVP, IN[2].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color;
				triStream.Append(OUT);
			}

			half4 frag(g2f IN) : COLOR
			{
				return float4(IN.diffuseColor.rgb, 1.0) * _BackgroundColor;
			}
			ENDCG

		}

			Pass
			{

				CULL BACK

				CGPROGRAM
#include "UnityCG.cginc" 
#pragma vertex vert
#pragma fragment frag
#pragma geometry geom

				// Use shader model 4.0 target, we need geometry shader support
#pragma target 4.0

				sampler2D _MainTex;

			struct v2g
			{
				float4 pos : SV_POSITION;
				float3 norm : NORMAL;
				float2 uv : TEXCOORD0;
				float3 color : TEXCOORD1;
			};

			struct g2f
			{
				float4 pos : SV_POSITION;
				float3 norm : NORMAL;
				//float2 uv : TEXCOORD0;
				float3 diffuseColor : TEXCOORD1;
				//float3 specularColor : TEXCOORD2;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _BackgroundColor;
			fixed4 _ForegroundColor;
			half _Cutoff;

			v2g vert(appdata_full v)
			{
				float3 v0 = v.vertex.xyz;

				v2g OUT;
				OUT.pos = v.vertex;
				OUT.norm = v.normal;
				OUT.uv = v.texcoord;
				OUT.color = tex2Dlod(_MainTex, v.texcoord).rgb;
				return OUT;
			}

			[maxvertexcount(3)]
			void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
			{
				float3 lightPosition = _WorldSpaceLightPos0;

				float3 v0 = IN[0].pos.xyz;
				float3 v1 = IN[1].pos.xyz;
				float3 v2 = IN[2].pos.xyz;

				float3 normal = normalize(cross(v0 - v1, v1 - v2));

				float3 color = (IN[0].color + IN[1].color + IN[2].color) / 3;

				g2f OUT;
				OUT.pos = mul(UNITY_MATRIX_MVP, IN[0].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color;
				triStream.Append(OUT);

				OUT.pos = mul(UNITY_MATRIX_MVP, IN[1].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color;
				triStream.Append(OUT);

				OUT.pos = mul(UNITY_MATRIX_MVP, IN[2].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color;
				triStream.Append(OUT);
			}

			half4 frag(g2f IN) : COLOR
			{
				clip(IN.diffuseColor.rgb - _Cutoff);
				return _ForegroundColor - float4(IN.diffuseColor.rgb * 0.1, 0.0);
			}
				ENDCG

			}
		}
}
