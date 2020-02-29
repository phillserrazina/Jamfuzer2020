Shader "Custom/Outline" {
	Properties{
		_MainTex("MainTex", 2D) = "white" {}
		_Outline("_Outline", Range(0,2)) = 0
		_OutlineColor("Color", Color) = (1, 1, 1, 1)
	}
		SubShader{
			Pass {
				Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
				Cull Off
				ZTest Always
				ZWrite Off

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct v2f {
					float4 pos : SV_POSITION;
				};

				float _Outline;
				float4 _OutlineColor;

				float4 vert(appdata_base v) : SV_POSITION {
					v2f o;
					v.vertex.xyz *= _Outline;
					o.pos = UnityObjectToClipPos(v.vertex);
					return o.pos;
				}

				half4 frag(v2f i) : COLOR {
					return _OutlineColor;
				}

				ENDCG
			}

			CGPROGRAM
			#pragma surface surf Lambert

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex);
			}

			ENDCG
		}
			FallBack "Diffuse"
}