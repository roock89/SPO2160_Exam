Shader "Custom/RainbowShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Hue ("Saturation", Range(-1, 1)) = 0
        _Brightness ("Saturation", Range(-1, 1)) = 0
        _Contrast ("Saturation", Range(-1, 1)) = 0
        _Saturation ("Saturation", Range(-1, 1)) = 0
        _Alpha ("Saturation", Range(-1, 1)) = 0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Speed ("Texture move speed", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        float _Speed;
        float _Hue;
        float _Brightness;
        float _Contrast;
        float _Saturation;
        float _Alpha;


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float3 rgb_to_hsv(float3 RGB)
        {
            float r = RGB.x;
            float g = RGB.y;
            float b = RGB.z;

            float minChannel = min(r, min(g, b));
            float maxChannel = max(r, max(g, b));

            float h = 0;
            float s = 0;
            float v = maxChannel;

            float delta = maxChannel - minChannel;

            if (delta != 0)
            {
            s = delta / v;

            if (r == v) h = (g - b) / delta;
            else if (g == v) h = 2 + (b - r) / delta;
            else if (b == v) h = 4 + (r - g) / delta;
            }

            return float3(h, s, v);
        }

        float3 hsv_to_rgb(float3 HSV)
        {
            float3 RGB = HSV.z;

            float h = HSV.x;
            float s = HSV.y;
            float v = HSV.z;

            float i = floor(h);
            float f = h - i;

            float p = (1.0 - s);
            float q = (1.0 - s * f);
            float t = (1.0 - s * (1 - f));

            if (i == 0) { RGB = float3(1, t, p); }
            else if (i == 1) { RGB = float3(q, 1, p); }
            else if (i == 2) { RGB = float3(p, 1, t); }
            else if (i == 3) { RGB = float3(p, q, 1); }
            else if (i == 4) { RGB = float3(t, p, 1); }
            else /* i == -1 */ { RGB = float3(1, p, q); }

            RGB *= v;

            return RGB;
        }


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by colo
            float2 uvMoved = float2(IN.uv_MainTex.x + _Time.y * _Speed, IN.uv_MainTex.y);
            fixed4 c = tex2D (_MainTex, uvMoved);
            o.Albedo = c.rgb + _Saturation;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
}
