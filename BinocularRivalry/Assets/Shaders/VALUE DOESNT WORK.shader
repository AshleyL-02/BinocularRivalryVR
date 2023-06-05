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

         // https://mattlockyer.github.io/iat455/documents/rgb-hsv.pdf
         float3 rgb_to_hsv(float r, float g, float b) {
            // calculate max and min color values
            float m_max = max(r, max(g, b));
            float m_min = min(r, min(g, b));
            float diff = m_max - m_min;

            // calculate hue
            float h = 0;
            if (m_max == r) {
               h = (g - b) / diff;
            }
            else if (m_max == g) {
               h = ((b - r) / diff) + 2;
            }
            else if (m_max == b) {
               h = ((r - g) / diff) + 4;
            }
            h = 60 * h;

            // calculate value
            float v = m_max;

            // calculate saturation
            float s = (v == 0) ? 0 : (diff / v);

            // return hsv
            return float3(h, s, v);

         }

         float3 hsv_to_rgb(float h, float s, float v) {
            // calculate scaled hsv
            if (h < 0) {
               h = fmod(h / 60, 6) + 6;
            }
            else {
               h = fmod(h / 60, 6);
            }

            // calculate intermediate values
            float alpha = v * (1 - s);
            float beta = v * (1 - (h - floor(h)) * s);
            float gamma = v * (1 - (1 - (h - floor(h))) * s);

            // calculate and return rgb
            if (0 <= h && h < 1) {
               return float3(v, gamma, alpha);
            }
            else if (1 <= h < 2) {
               return float3(beta, v, alpha);
            }
            else if (2 <= h < 3) {
               return float3(alpha, v, gamma);
            }
            else if (3 <= h < 4) {
               return float3(alpha, beta, v);
            }
            else if (4 <= h < 5) {
               return float3(gamma, alpha, v);
            }
            else if (5 <= h < 6) {
               return float3(v, alpha, beta);
            }
            else {
               return float3(1, 0, 0);
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
            // calculate value

            fixed4 c = tex2D(_MainTex, i.uv);
            float3 c_hsv = rgb_to_hsv(c.r, c.g, c.b);
            float4 c_value = float4(hsv_to_rgb(c_hsv.x, c_hsv.y, min(1, c_hsv.z * _ValueScale)) / 255.0, 1);
            return lerp(c_value, c, unity_StereoEyeIndex);
         }
         ENDCG
      }
   }
} 