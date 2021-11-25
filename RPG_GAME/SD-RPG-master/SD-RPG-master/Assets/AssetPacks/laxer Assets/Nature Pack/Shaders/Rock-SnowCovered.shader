Shader "Custom/Nature-Pack/Rock-SnowCovered" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Color map (RGB) Occlusion (A)", 2D) = "white" {}
		_BumpMap("Main Normal Map", 2D) = "bump" {}
		//rocks
		_DetailAlbedoMap ("Rock tex (RGB), height (A)", 2D) = "white" {}
		_DetailNormalMap ("Rock Normals",2D) = "bump"{}
		_RockMOES ("Rock Metallic Occlusion Emission Smoothness (RGBA)", 2D) = "white" {}
		//moss
		_MossAmount("Moss Amount",Range(-1,1)) = 0
		_MossTex ("Moss tex (RGB), height (A)", 2D) = "alpha" {}
		_MossColor ("MossColor", Color) = (1,1,1,1)
		_MossBump ("Moss Normals",2D) = "bump"{}
		_MossMOES ("Moss Metallic Occlusion Emission Smoothness (RGBA)", 2D) = "white" {}
		//global
		_OcclusionStrength ("OcclusionStrength",Range(0,1)) = 0
		_Metallic ("MetallicStrength",Range(0,1)) = 0.2
		_Glossiness ("SmoothnessStrength",Range(0,1)) = 0.1
		_Emission ("EmissionStrength", Color) = (0,0,0,0)
		_Blend ("Blending",Range(0.0001,1)) = 0.5
		_BlendSteepness ("Blending Curve",Range(0,1)) = 0.5


	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 400
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _DetailAlbedoMap,_MainTex, _BumpMap,_DetailNormalMap,_MossBump,_MossTex,_RockMOES,_MossMOES;

		struct Input {
			float2 uv_DetailAlbedoMap;
			float2 uv_MossTex;
			float2 uv_MainTex;
			float3 worldNormal; INTERNAL_DATA
			float3 worldPos;
		};
		half3 DecompressNormal(fixed2 compressedNormal){
			half3 n;
		    n.xy = compressedNormal*2-1;
		    n.z = clamp(sqrt(1-dot(n.xy, n.xy)),0.05,1);
		    return n;
		}
		inline float3 combineNormals (float3 n1, float3 n2){
			n1 += float3(0, 0, 1);
			n2 *= float3(-1, -1, 1);
		    return n1 * dot(n1, n2) / n1.z - n2;
		}

		half _Glossiness,_Metallic,_OcclusionStrength,_Emission,_Blend,_BlendSteepness,_MossAmount;
		fixed4 _Color,_MossColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 colormap = tex2D (_MainTex, IN.uv_MainTex)*_Color;
			fixed4 tex0 = tex2D (_DetailAlbedoMap, IN.uv_DetailAlbedoMap);
			tex0 = colormap*tex0*4;
			fixed4 tex1 = tex2D (_MossTex, IN.uv_MossTex) * _MossColor;
			fixed4 norm0 = tex2D (_DetailNormalMap, IN.uv_DetailAlbedoMap);
			fixed4 norm1 = tex2D (_MossBump, IN.uv_MossTex);
			fixed4 meos0 = tex2D (_RockMOES, IN.uv_MainTex);
			fixed4 meos1 = tex2D (_MossMOES, IN.uv_MossTex);
			
			half3 n0 = UnpackNormal(norm0);
			half3 n1 = UnpackNormal(norm1);
			half3 nbase = UnpackNormal(tex2D(_BumpMap,IN.uv_MainTex));
			
			float3 wNormal = WorldNormalVector (IN, combineNormals(nbase,n0));
			float mfactor = wNormal.y*0.5+0.5+_MossAmount;
			half2 splat_control = half2(1-mfactor,mfactor);
			splat_control = splat_control * _BlendSteepness + fixed2(tex0.a * splat_control.x, tex1.a * splat_control.y)*(2-_BlendSteepness);
			
			half height = splat_control.r;
			height = max(splat_control.g, height);
			
			splat_control.r = clamp((splat_control.r - height)+_Blend,0,1);
			splat_control.g = clamp((splat_control.g - height)+_Blend,0,1);
			
			half sum = splat_control.r + splat_control.g;
			splat_control = splat_control / sum;			
			
			o.Albedo = tex0 * splat_control.r + tex1 * splat_control.g;
			o.Normal = combineNormals(nbase,n0 * splat_control.r + n1 * splat_control.g);
			o.Metallic = (meos0.r * splat_control.r + meos1.r * splat_control.g) * _Metallic;
			o.Smoothness = (meos0.a * splat_control.r + meos1.a * splat_control.g) * _Glossiness;
			o.Occlusion = (meos0.g * splat_control.r + meos1.g * splat_control.g) * _OcclusionStrength * colormap.a + (1-_OcclusionStrength);
			o.Emission = (tex0 * meos0.b * splat_control.r + tex1 * meos1.b * splat_control.g) * _Emission;
			o.Alpha = 1;
		}
		ENDCG
	} 
	FallBack "Custom/Nature-Pack/Rock-SnowCovered_Low"
}
