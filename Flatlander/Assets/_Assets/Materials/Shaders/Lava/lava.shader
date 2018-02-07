Shader "Custom/lava" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_DispMap("Displacement Texture",2D)="white"{}
		_DispMult("Displacement Multiplier",Range(0,2))=1
		_DispV("Displacement Velocity",Vector)=(1,0,0,0)
		_HighMap("Highlight Texture",2D)="white"{}
		_HighStr("Highlight Strength",Range(0,1))=1
		_HighV("Highlight Velocity",Vector)=(0.5,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		sampler2D _DispMap;
		float _DispMult;
		float2 _DispV;

		sampler2D _HighMap;
		float _HighStr;
		float2 _HighV;

		void vert (inout appdata_full v) {
			//tex2Dlod(_DispTex, float4(v.texcoord.xy,0,0)).r * _Displacement;
			float3 offset=v.normal;
			//offset.x=0;
			//offset.y=0;
			//normalize(offset);
			const float4 t=float4(v.texcoord.xy+_DispV*_Time.y,0,0);
			offset*=(tex2Dlod(_DispMap, t).r*2-1) * _DispMult;
			v.vertex.xyz += offset;
		}

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex+_DispV*_Time.y) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Emission=tex2D(_HighMap, IN.uv_MainTex+_HighV*_Time.y) * _HighStr;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
