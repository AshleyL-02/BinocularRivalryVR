 Shader "XR/Stereo_Saturation"
{
   Properties
   {
       _MainTex ("Texture", 2D) = "white" {}
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
            fixed4 c_grayscale = (0.299 *c.r) + (0.587 * c.g) + (0.114 * c.b);
            return lerp(c_grayscale, c, unity_StereoEyeIndex);
         }
         ENDCG
      }
   }
} 