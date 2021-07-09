Shader "Mobile/Diffuse Outline"
{
    Properties
    {
        _Color("Highlight Color", Color) = (0,1,0)
        _Outline("Outline Width", Range(0.0, 0.5)) = .1
        _MainTex("Base (RGB)", 2D) = "white" {}
    }

        SubShader
    {
        Tags {"Queue" = "Geometry+1" "RenderType" = "Opaque"}

        ZWrite Off
        ZTest Always

        CGPROGRAM
        #pragma surface surf Unlit vertex:vert

        fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            return fixed4(s.Albedo, s.Alpha);
        }

        uniform fixed4 _Color;
        uniform fixed _Outline;
        uniform sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v)
        {
            v.vertex.xyz += v.normal * _Outline;
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _Color;
        }
        ENDCG

        ZWrite On
        ZTest LEqual

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd

        uniform sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    }

        // Fall back to just tinting the object if we can't do an outline.
            SubShader
        {
            CGPROGRAM
            #pragma surface surf Lambert noforwardadd

            sampler2D _MainTex;
            fixed4 _Color;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            }
            ENDCG
        }
}