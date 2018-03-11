﻿Shader "Custom/lava" {
	Properties {
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		[NoScaleOffset] _Normal ("Normal Map",2D)="green"{}

		_Color ("Color", Color) = (1,1,1,1)
		_MainTexNoise("Diffuse Offset Noise",Range(0,1))=0
		[NoScaleOffset] _MainTex ("Diffuse", 2D) = "white" {}
		_Color2 ("Color 2",Color)=(1,1,1,1)
		_MainTex2Noise("Diffuse 2 Offset Noise",Range(0,1))=0
		[NoScaleOffset] _MainTex2 ("Diffuse 2",2D)="black"{}
		_BlendVal("Blend Amount",Range(0,1))=0
		_BlendSoft("Blend Softness",Range(0,1))=.05
		_BlendEdge("Edge Color",Color)=(0,0,0,0)
		_BlendBright("Edge Brightness",float)=1
		_BlendTexNoise("Blend Pattern Noise",Float)=0
		[NoScaleOffset] _BlendTex("Blend Pattern",2D)="white"{}
		

		_DispMap("Displacement Texture",2D)="white"{}
		_DispMapNoise("Displacement Texture Noise",Float)=0
		_DispMapOffNoise("Displacement Offset Noise",Range(0,1))=0
		_DispMult("Displacement Multiplier",Range(0,2))=1
		_DispV("Displacement Velocity",Vector)=(1,0,0,0)
		_HighMap("Highlight Texture",2D)="white"{}
		_HighStr("Highlight Strength",Range(0,1))=1
		_HighV("Highlight Velocity",Vector)=(0.5,0,0,0)

		[NoScaleOffset] _NoiseTex("Noise Texture",2D)="grey"{}
		_NoiseOff("Noise Offset (instanced)",Vector)=(0,0,0,0)
	}
	SubShader {
		//Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		LOD 200

		//Pass {ColorMask 0}
		//ZWrite On
		//Blend OneMinusSrcAlpha SrcAlpha  


		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert fullforwardshadows alphatest:_Cutoff //alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex,_MainTex2;
		//float4 _MainTex_ST;

		struct Input {
			float2 uv_MainTex;

			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color,_Color2,_BlendEdge;
		sampler2D _Normal;


		sampler2D _BlendTex;
		float _BlendVal,_BlendSoft,_BlendBright,_BlendVar;

		sampler2D _DispMap;
		float4 _DispMap_ST;
		float _DispMult;
		float2 _DispV;

		sampler2D _HighMap;
		float4 _HighMap_ST;
		float _HighStr;
		float2 _HighV;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		#pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(float2,_NoiseOff)
		UNITY_INSTANCING_BUFFER_END(Props)

		sampler2D _NoiseTex;

		float
			_MainTexNoise,_MainTex2Noise,
			_BlendTexNoise,
			_DispMapNoise,_DispMapOffNoise;
		
		float4 noisefn(float4 base,float2 uv,float strength){
			float4 noise=tex2D(_NoiseTex,uv)*2-1;//generate noise from -1 to 1
			//scale noise by str
			noise*=strength;

			//input values of 0 and 1 should never change, 0.5 should have most variance
			float4 offset=base*(1-base);

			//apply noise to our offset
			offset*=noise;

			return base+offset;
		}

		void vert (inout appdata_full v) {
			//tex2Dlod(_DispTex, float4(v.texcoord.xy,0,0)).r * _Displacement;
			float3 offset=v.normal;
			//offset.x=0;
			//offset.y=0;
			//normalize(offset);
			const float4 noiseOffset=float4(UNITY_ACCESS_INSTANCED_PROP(Props,_NoiseOff),0,0);
			
			const float2 uv=v.texcoord.xy*_DispMap_ST.xy + _DispMap_ST.zw;
			const float4 t=float4(uv+_DispV*_Time.y+noiseOffset*_DispMapOffNoise,0,0);//offset the uv by time and by noise ammount
			//const float4 t=float4(v.texcoord.xy+_DispV*_Time.y,0,0);

			float4 offMap=tex2Dlod(_DispMap, t);//get the texture

			//these two lines are a condensed version of noisefn
			//this is done because tex2D is not useable from a vertex shader, but tex2Dlod is not what we want elsewhere
			const float4 noise=(tex2Dlod(_NoiseTex,t)*2-1)*_DispMapNoise;//generate noise from -1 to 1
			offMap=offMap+noise*offMap*(1-offMap);
			//these two lines are eqivalent to this following line, but actually work
			//offMap=noisefn(offMap,t,_DispMapNoise);//apply an amount of noise to the texture

			offset*=(offMap.r*2-1) * _DispMult;
			v.vertex.xyz += offset;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			const float2 noiseOffset=UNITY_ACCESS_INSTANCED_PROP(Props,_NoiseOff);

			const float2 mainUV=IN.uv_MainTex+_DispV*_Time.y;
			const float2 emUV=IN.uv_MainTex*_HighMap_ST.xy + _HighMap_ST.zw + _HighV*_Time.y;

			//blend main textures together to get 
			fixed4 diff = tex2D(_MainTex, mainUV+noiseOffset*_MainTexNoise)*_Color;
			fixed4 diff2 = tex2D(_MainTex2,mainUV+noiseOffset*_MainTex2Noise)*_Color2;
			float4 val=tex2D(_BlendTex,mainUV);
			val=noisefn(val,mainUV+noiseOffset,_BlendTexNoise);

			val.a=(val.r+val.g+val.b)/3;
			//val.a=val.r;

			const float4 d=_BlendVal-val;
			float4 edgeCol=float4(0,0,0,0);
			
			val=min(max(d/_BlendSoft,0),1);
			edgeCol=_BlendEdge*frac(val)*_BlendBright;			

			fixed4 c=diff+val*(diff2-diff);

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Emission=tex2D(_HighMap, emUV) * _HighStr+edgeCol;
			o.Normal=UnpackNormal(tex2D(_Normal,mainUV));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}