Shader "Custom/paralax" {
	Properties 
	{
		_DiffuseCol("Diffuse Color",Color)=(1,1,1)
		_Diffuse("Diffuse Map", 2D) = "white" {}
		_DiffuseAmount("Diffuse Contribution",Range(0,1))=1
		_DiffuseAmntMap("Diffuse Contribution Map",2D)="white"{}
		
		_Bumpmap("Bumpmap", 2D) = "white" {}
		_Height("Height", Range(0.0001,0.5)) = 0.05
		_Steps("Steps", Range(1,1000)) = 300
		_StepDistance("Step Distance",Range(0.0001,.02))=0.01
		
		_specColor("Specular Color",Color) = (1,1,1)
		_specMap("Specular Map",2D)="white"{}
		_specShiny("Shinyness",Range(0,1))=0.5
		_specShinyMap("Shinyness Map",2D)="white"{}
		_specAmount("Specular Contribution",Range(0,1))=1
		_specAmntMap("Specular Contribution Map",2D)="white"{}
	}
	
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#pragma surface surf SimpleSpecular vertex:vert fullforwardshadows
		#pragma target 3.0

		//Input
		float3 _DiffuseCol;
		sampler2D _Diffuse;
		float _DiffuseAmount;
		sampler2D _DiffuseAmntMap;

		sampler2D _Bumpmap;
		float _Height;
		int _Steps;
		float _StepDistance;

		float3 _specColor;
		sampler2D _specMap;
		float _specAmount,_specShiny;
		sampler2D _specShinyMap;
		sampler2D _specAmntMap;

		struct Input {
			//What Unity can give you
			float2 uv_Bumpmap;

			//What you have to calculate yourself
			float3 tangentViewDir;


			float2 uv_MainTex;
			float3 viewDir;
			float3 reflCoord;
			float3 worldRefl;
			INTERNAL_DATA
		};

		struct SurfaceOutputCustom{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			half Specular;
			fixed Gloss;
			fixed Alpha;
		
			float2 uv;
		};


		void vert(inout appdata_full i, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);

			//Transform the view direction from world space to tangent space			
			float3 worldVertexPos = mul(unity_ObjectToWorld, i.vertex).xyz;
			float3 worldViewDir = worldVertexPos - _WorldSpaceCameraPos;

			//To convert from world space to tangent space we need the following
			//https://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html
			float3 worldNormal = UnityObjectToWorldNormal(i.normal);
			float3 worldTangent = UnityObjectToWorldDir(i.tangent.xyz);
			float3 worldBitangent = cross(worldNormal, worldTangent) * i.tangent.w * unity_WorldTransformParams.w;

			//Use dot products instead of building the matrix
			o.tangentViewDir = float3(
				dot(worldViewDir, worldTangent),
				dot(worldViewDir, worldNormal),
				dot(worldViewDir, worldBitangent)
				);
		}

		half4 LightingSimpleSpecular (SurfaceOutputCustom s, half3 lightDir, half3 viewDir, half atten) {
			//diffuse
			const half NdotL = dot (s.Normal, lightDir);
			const half diff = NdotL * 0.5 + 0.5;
			
			//specular
			const half3 h=normalize(lightDir+viewDir);
			const float nh=max(0,dot(s.Normal,h));
			const float shiny=_specShiny*tex2D(_specShinyMap,s.uv).r;
			const float alpha=shiny*shiny*200;
			const float spec=pow(nh,alpha);

			const float3 diffuse=s.Albedo*_LightColor0.rgb*diff;
			const float3 specular=_specColor*tex2D(_specMap,s.uv) * _LightColor0.rgb * spec;
			
			half4 c;
			c.rgb = (diffuse + specular*_specAmount*tex2D(_specAmntMap,s.uv).r)*atten;
			c.a = s.Alpha;
			return c;
		}


		//Get the height from a uv position
		float getHeight(float2 texturePos){
			float4 colorNoise = tex2Dlod(_Bumpmap, float4(texturePos, 0, 0));

			//Calculate the height at this uv coordinate
			//Just use r because r = g = b  because color is grayscale
			//value of 1 = height of 0, value of 0 = height of -1
			float height = (colorNoise.r-1) * _Height;

			return height;
		}


		//Get the texture position by interpolation between the position where we hit terrain and the position before
		float2 getWeightedTexPos(float3 rayPos, float3 rayDir, float stepDistance)
		{
			//Move one step back to the position before we hit terrain
			float3 oldPos = rayPos - stepDistance * rayDir;

			float oldHeight = getHeight(oldPos.xz);

			//Always positive
			float oldDistToTerrain = abs(oldHeight - oldPos.y);

			float currentHeight = getHeight(rayPos.xz);

			//Always negative
			float currentDistToTerrain = rayPos.y - currentHeight;

			float weight = currentDistToTerrain / (currentDistToTerrain - oldDistToTerrain);

			//Calculate a weighted texture coordinate
			//If height is -2 and oldHeight is 2, then weightedTex is 0.5, which is good because we should use 
			//the exact middle between the coordinates
			float2 weightedTexPos = oldPos.xz * weight + rayPos.xz * (1 - weight);

			return weightedTexPos;
		}


		void surf (Input IN, inout SurfaceOutputCustom o) 
		{
			//Where is the ray starting? y is up and we always start at the surface
			float3 rayPos = float3(IN.uv_Bumpmap.x, 0, IN.uv_Bumpmap.y);

			//What's the direction of the ray?
			float3 rayDir = normalize(IN.tangentViewDir);

			//Find where the ray is intersecting with the terrain with a raymarch algorithm

			//The default color used if the ray doesnt hit anything
			float4 finalColor = 1;

			float2 finalUV;

			for (int i = 0; i < _Steps; i++)
			{
				//Get the current height at this uv coordinate
				float height = getHeight(rayPos.xz);

				//If the ray is below the surface
				if (rayPos.y < height)
				{
					//Get the texture position by interpolation between the position where we hit terrain and the position before
					float2 weightedTex = getWeightedTexPos(rayPos, rayDir, _StepDistance);
					finalColor = tex2Dlod(_Diffuse, float4(weightedTex, 0, 0));
					
					finalUV=weightedTex;
					//We have hit the terrain so we dont need to loop anymore	
					break;
				}
				
				//Move along the ray
				rayPos += _StepDistance * rayDir;
			}

			//Output
			o.Albedo = finalColor.rgb*_DiffuseCol*_DiffuseAmount*tex2D(_DiffuseAmntMap,finalUV);
			o.Normal=UnpackNormal(_Height*tex2D(_Bumpmap,finalUV));
			o.uv=finalUV;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
