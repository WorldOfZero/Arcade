// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Low Poly Shader developed as part of World of Zero: http://youtube.com/worldofzerodevelopment
// Based upon the example at: http://www.battlemaze.com/?p=153

Shader "Custom/LowPolyShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			Pass
		{

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
			fixed4 _Color;

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
				float4 worldNormal = normalize(mul(unity_ObjectToWorld, normal));

				float3 color = (IN[0].color + IN[1].color + IN[2].color) / 3;
				float lightStrength = max(dot(normalize(lightPosition), worldNormal), 0);

				g2f OUT;
				OUT.pos = mul(UNITY_MATRIX_MVP, IN[0].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color * lightStrength;
				triStream.Append(OUT);

				OUT.pos = mul(UNITY_MATRIX_MVP, IN[1].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color * lightStrength;
				triStream.Append(OUT);

				OUT.pos = mul(UNITY_MATRIX_MVP, IN[2].pos);
				OUT.norm = normal;
				OUT.diffuseColor = color * lightStrength;
				triStream.Append(OUT);
			}

			half4 frag(g2f IN) : COLOR
			{
				return float4(_WorldSpaceLightPos0.rgb, 1.0); // float4(IN.diffuseColor.rgb, 1.0);
			}
			ENDCG

		}
		}
}
