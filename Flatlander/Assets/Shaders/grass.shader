Shader "Custom/grass" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_WindTheta("Wind Angle",range(0,360))=0
		_windFreq("Wind Frequency",Float)=1
		_windStr("Wind Strength",Float)=1
	}
	SubShader {
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		LOD 200
		Cull Off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert fullforwardshadows alphatest:_Cutoff

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float _WindTheta,_windFreq,_windStr;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		#define M_PI 3.1415926535897932384626433832795
		const static float toRad=M_PI/180;

		void vert (inout appdata_full v) {
			float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
			float3 winddir=(0,0,0);
			sincos(_WindTheta*toRad,winddir.x,winddir.z);

			float t=dot(worldPos,winddir)+_Time.y;

			float y=(v.vertex.y+1)/2;
			//float y=(UnityObjectToClipPos(v.vertex).y+1)/2;

			v.vertex.xyz+=winddir*_windStr*sin(t*_windFreq)*y;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
