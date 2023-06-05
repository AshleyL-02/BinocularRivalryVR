 Shader "XR/Stereo_Value"
{
   Properties
   {
       _MainTex ("Texture", 2D) = "white" {}
       _ValueScale ("Value", Range(1.0, 5.0)) = 1.0
   }

   SubShader
   {
      Pass
      {
         CGPROGRAM

         #pragma vertex vert
         #pragma fragment frag

         float4 _LeftEyeColor;
         float4 _RightEyeColor;

         sampler2D _MainTex;
         float4 _MainTex_ST;
         int _ValueScale;

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

         float3 rgb_to_hsv(float r, float g, float b) {
            // calculate max and min color values
            float c_max = max(r, max(g, b));
            float c_min = min(r, min(g, b));

            // calculate value
            float v = c_max / 255.0;

            // calculate saturation
            float s = (c_max > 0) ? (1 - (c_min / c_max)) : 0;

            // calculate hue
            float n = (r - (0.5 * g) - (0.5 * b));
            float d = sqrt((r * r) + (g * g) + (b * b) - (r * g) - (r * b) - (g * b));
            float h = (g >= b) ? degrees(acos(n / d)) : (360 - degrees(acos(n / d)));
         
            // return hsv
            return float3(h, s, v);
         }

         float3 hsv_to_rgb(float h, float s, float v) {
            // calculate max and min color values
            float c_max = 255.0 * v;
            float c_min = c_max * (1 - s);

            // calculate intermediate value z
            float z = (c_max - c_min) * (1 - abs(fmod(h / 60.0, 2) - 1));

            // calculate and return r, g, and b values
            if (0 <= h < 60) {
               return float3(c_max, z + c_min, c_min);
            }
            else if (60 <= h < 120) {
               return float3(z + c_min, c_max, c_min);
            }
            else if (120 <= h < 180) {
               return float3(c_min, c_max, z + c_min);
            }
            else if (180 <= h < 240) {
               return float3(c_min, z + c_min, c_max);
            }
            else if (240 <= h < 300) {
               return float3(z + c_min, c_min, c_max);
            }
            else {
               return float3(c_max, c_min, z + c_min);
            }
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
            float3 c_hsv = rgb_to_hsv(c.r * 255.0, c.g * 255.0, c.b * 255.0);
            float4 c_value = float4(hsv_to_rgb(c_hsv.x, c_hsv.y, min(1, c_hsv.z * _ValueScale)) / 255.0, 1);

            return lerp(c_value, c, unity_StereoEyeIndex);
         }
         ENDCG
      }
   }
} 