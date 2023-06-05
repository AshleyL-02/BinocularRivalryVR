 Shader "XR/Stereo_HSV"
{
   Properties
   {
      _MainTex ("Texture", 2D) = "white" {}
      _Hue ("Hue", Range(-1.0, 1.0)) = 0.0
      _Saturation ("Saturation", Range(0.0, 2.0)) = 1.0
      _Value ("Value", Range(0.0, 2.0)) = 1.0
   }

   SubShader
   {
      Pass
      {
         CGPROGRAM

         #pragma vertex vert
         #pragma fragment frag

         // texture inputs
         sampler2D _MainTex;
         float4 _MainTex_ST;

         // color adjustment inputs
         float _Hue;
         float _Saturation;
         float _Value;

         #include "UnityCG.cginc"

         struct appdata
         {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;

            UNITY_VERTEX_INPUT_INSTANCE_ID
         };

         struct v2f
         {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;

            UNITY_VERTEX_INPUT_INSTANCE_ID 
            UNITY_VERTEX_OUTPUT_STEREO
         };

         // from https://docs.unity3d.com/Packages/com.unity.shadergraph@6.9/manual/Colorspace-Conversion-Node.html
         float3 rgb_to_hsv(float3 rgb) {
            float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
            float4 P = lerp(float4(rgb.bg, K.wz), float4(rgb.gb, K.xy), step(rgb.b, rgb.g));
            float4 Q = lerp(float4(P.xyw, rgb.r), float4(rgb.r, P.yzx), step(P.x, rgb.r));
            float D = Q.x - min(Q.w, Q.y);
            float  E = 1e-10;
            return float3(abs(Q.z + (Q.w - Q.y)/(6.0 * D + E)), D / (Q.x + E), Q.x);
         }

         float3 hsv_to_rgb(float3 hsv) {
            float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
            float3 P = abs(frac(hsv.xxx + K.xyz) * 6.0 - K.www);
            return hsv.z * lerp(K.xxx, saturate(P - K.xxx), hsv.y);
         }

         v2f vert (appdata v)
         {
            v2f o;

            UNITY_SETUP_INSTANCE_ID(v);
            UNITY_INITIALIZE_OUTPUT(v2f, o);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);

            return o;
         }

         fixed4 frag (v2f i) : SV_Target
         {
            UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

            fixed4 c = tex2D(_MainTex, i.uv);
            float3 c_hsv = rgb_to_hsv(c.rgb);
            float3 c_rgb = hsv_to_rgb(float3(fmod(c_hsv.x + _Hue, 1), min(1, c_hsv.y * _Saturation), min(1, c_hsv.z * _Value)));

            return lerp(float4(c_rgb, 1), c, unity_StereoEyeIndex);
         }
         ENDCG
      }
   }
} 