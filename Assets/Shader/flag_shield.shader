// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:14,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-4521-OUT,alpha-1047-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4634,x:31471,y:33085,ptovrint:False,ptlb:depth_intensity,ptin:_depth_intensity,varname:_depth_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_DepthBlend,id:6792,x:31678,y:33052,varname:node_6792,prsc:2|DIST-4634-OUT;n:type:ShaderForge.SFN_OneMinus,id:4251,x:31860,y:32973,varname:node_4251,prsc:2|IN-6792-OUT;n:type:ShaderForge.SFN_Tex2d,id:4958,x:31792,y:32750,ptovrint:False,ptlb:node_4958,ptin:_node_4958,varname:_node_4958,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3a5a96df060a5cf4a9cc0c59e13486b7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:897,x:32024,y:32927,varname:node_897,prsc:2|A-4958-R,B-4251-OUT;n:type:ShaderForge.SFN_Add,id:1047,x:32348,y:33067,varname:node_1047,prsc:2|A-5042-OUT,B-897-OUT;n:type:ShaderForge.SFN_Multiply,id:6924,x:32247,y:32867,varname:node_6924,prsc:2|A-897-OUT,B-1325-RGB;n:type:ShaderForge.SFN_Color,id:1325,x:32019,y:33136,ptovrint:False,ptlb:highlight_color,ptin:_highlight_color,varname:_highlight_color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Fresnel,id:5042,x:32027,y:32608,varname:node_5042,prsc:2|EXP-5342-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5342,x:31774,y:32594,ptovrint:False,ptlb:fresnel_intensity,ptin:_fresnel_intensity,varname:_fresnel_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Color,id:7377,x:32172,y:32428,ptovrint:False,ptlb:node_7377,ptin:_node_7377,varname:_node_7377,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:8453,x:32383,y:32607,varname:node_8453,prsc:2|A-7377-RGB,B-5042-OUT;n:type:ShaderForge.SFN_Add,id:4521,x:32484,y:32797,varname:node_4521,prsc:2|A-8453-OUT,B-6924-OUT;proporder:4634-4958-5342-1325-7377;pass:END;sub:END;*/

Shader "Shader Forge/flag_shield" {
    Properties {
        _depth_intensity ("depth_intensity", Float ) = 0.1
        _node_4958 ("node_4958", 2D) = "white" {}
        _fresnel_intensity ("fresnel_intensity", Float ) = 1
        _highlight_color ("highlight_color", Color) = (1,0,0,1)
        _node_7377 ("node_7377", Color) = (1,0,0,1)
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
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _depth_intensity;
            uniform sampler2D _node_4958; uniform float4 _node_4958_ST;
            uniform float4 _highlight_color;
            uniform float _fresnel_intensity;
            uniform float4 _node_7377;
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
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
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float node_5042 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnel_intensity);
                float4 _node_4958_var = tex2D(_node_4958,TRANSFORM_TEX(i.uv0, _node_4958));
                float node_897 = (_node_4958_var.r*(1.0 - saturate((sceneZ-partZ)/_depth_intensity)));
                float3 emissive = ((_node_7377.rgb*node_5042)+(node_897*_highlight_color.rgb));
                float3 finalColor = emissive;
                return fixed4(finalColor,(node_5042+node_897));
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
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
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
