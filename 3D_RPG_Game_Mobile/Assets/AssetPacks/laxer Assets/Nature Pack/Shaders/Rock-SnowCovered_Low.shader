Shader "Custom/Nature-Pack/Rock-SnowCovered_Low" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Color map (RGB) Occlusion (A)", 2D) = "white" {}
		_BumpMap("Main Normal Map", 2D) = "bump" {}

		//moss
		_MossAmount("Moss Amount",Range(-1,1)) = 0
		_MossTex ("Moss tex (RGB), height (A)", 2D) = "alpha" {}
		_MossColor ("MossColor", Color) = (1,1,1,1)
		_MossBump ("Moss Normals",2D) = "bump"{}
		//global
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

		sampler2D _MainTex, _BumpMap,_MossBump,_MossTex;

		struct Input {
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

		half _Blend,_BlendSteepness,_MossAmount;
		fixed4 _Color,_MossColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 tex0 = tex2D (_MainTex, IN.uv_MainTex)*_Color;
			fixed4 tex1 = tex2D (_MossTex, IN.uv_MossTex) * _MossColor;
			fixed4 norm1 = tex2D (_MossBump, IN.uv_MossTex);
			
			half3 n1 = UnpackNormal(norm1);
			half3 nbase = UnpackNormal(tex2D(_BumpMap,IN.uv_MainTex));
			
			float3 wNormal = WorldNormalVector (IN, nbase);
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
			o.Normal = nbase * splat_control.r + n1 * splat_control.g;
			o.Alpha = 1;
		}
		ENDCG
	} 
	FallBack "Mobile/Bumped Diffuse"
}
