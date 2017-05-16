// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-2861-OUT,alpha-8377-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9758,x:31718,y:33367,ptovrint:False,ptlb:depth_blend,ptin:_depth_blend,varname:node_9758,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_DepthBlend,id:9426,x:31888,y:33350,varname:node_9426,prsc:2|DIST-9758-OUT;n:type:ShaderForge.SFN_Color,id:2914,x:32268,y:33339,ptovrint:False,ptlb:highlight_color,ptin:_highlight_color,varname:node_2914,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:1785,x:32106,y:32961,varname:node_1785,prsc:2|A-6397-OUT,B-2914-RGB;n:type:ShaderForge.SFN_Tex2d,id:175,x:31819,y:32887,ptovrint:False,ptlb:highlight_texture,ptin:_highlight_texture,varname:node_175,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3a5a96df060a5cf4a9cc0c59e13486b7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6397,x:32106,y:33112,varname:node_6397,prsc:2|A-175-R,B-2246-OUT;n:type:ShaderForge.SFN_OneMinus,id:2246,x:32068,y:33314,varname:node_2246,prsc:2|IN-9426-OUT;n:type:ShaderForge.SFN_Fresnel,id:3100,x:32127,y:32754,varname:node_3100,prsc:2|EXP-1259-OUT;n:type:ShaderForge.SFN_Multiply,id:4450,x:32409,y:32687,varname:node_4450,prsc:2|A-4223-RGB,B-3100-OUT;n:type:ShaderForge.SFN_Color,id:4223,x:31996,y:32517,ptovrint:False,ptlb:fresnel_color,ptin:_fresnel_color,varname:node_4223,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9411765,c2:0.02768169,c3:0.02768169,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:1259,x:31863,y:32621,ptovrint:False,ptlb:fresnel_intensity,ptin:_fresnel_intensity,varname:node_1259,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Add,id:2861,x:32415,y:32965,varname:node_2861,prsc:2|A-4450-OUT,B-1785-OUT;n:type:ShaderForge.SFN_Add,id:8377,x:32415,y:33100,varname:node_8377,prsc:2|A-3100-OUT,B-6397-OUT;proporder:9758-2914-175-4223-1259;pass:END;sub:END;*/

Shader "Shader Forge/test-shield2" {
    Properties {
        _depth_blend ("depth_blend", Float ) = 1
        _highlight_color ("highlight_color", Color) = (1,0,0,1)
        _highlight_texture ("highlight_texture", 2D) = "white" {}
        _fresnel_color ("fresnel_color", Color) = (0.9411765,0.02768169,0.02768169,1)
        _fresnel_intensity ("fresnel_intensity", Float ) = 2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _depth_blend;
            uniform float4 _highlight_color;
            uniform sampler2D _highlight_texture; uniform float4 _highlight_texture_ST;
            uniform float4 _fresnel_color;
            uniform float _fresnel_intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float node_3100 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnel_intensity);
                float4 _highlight_texture_var = tex2D(_highlight_texture,TRANSFORM_TEX(i.uv0, _highlight_texture));
                float node_6397 = (_highlight_texture_var.r*(1.0 - saturate((sceneZ-partZ)/_depth_blend)));
                float3 emissive = ((_fresnel_color.rgb*node_3100)+(node_6397*_highlight_color.rgb));
                float3 finalColor = emissive;
                return fixed4(finalColor,(node_3100+node_6397));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
