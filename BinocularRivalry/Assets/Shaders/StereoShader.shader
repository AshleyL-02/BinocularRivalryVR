Shader "Custom/StereoShader"
{
   Properties
   {
       _LeftEyeColor("Left Eye Color", COLOR) = (0,1,0,1)
       _RightEyeColor("Right Eye Color", COLOR) = (1,0,0,1)
   }

   SubShader
   {
      Tags { "RenderType" = "Overlay" }

      Pass
      {
         CGPROGRAM

         struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
    
            UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
        };

        //v2f output struct

        struct v2f
        {

            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
    
            UNITY_VERTEX_OUTPUT_STEREO //Insert
        };

        v2f vert (appdata v)
        {
            v2f o;
    
            UNITY_SETUP_INSTANCE_ID(v); //Insert
            UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert
    
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
        }

        UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex); //Insert

        fixed4 frag (v2f i) : SV_Target
        {
            UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i); //Insert
    
            fixed4 col = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv); //Insert
    
            // invert the colors
    
            col = 1 - col;
    
            return col;
        }   
        ENDCG
      }
   }
   FallBack "Diffuse"
}