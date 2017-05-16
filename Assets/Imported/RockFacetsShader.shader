// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34120,y:32889,varname:node_9361,prsc:2|emission-9537-OUT,custl-8066-OUT;n:type:ShaderForge.SFN_AmbientLight,id:7528,x:33098,y:33171,varname:node_7528,prsc:2;n:type:ShaderForge.SFN_Add,id:8821,x:33150,y:33370,varname:node_8821,prsc:2|A-2035-OUT,B-973-OUT;n:type:ShaderForge.SFN_Multiply,id:2035,x:32466,y:33591,varname:node_2035,prsc:2|A-9064-OUT,B-7677-OUT,C-3070-RGB;n:type:ShaderForge.SFN_Multiply,id:973,x:32893,y:33986,varname:node_973,prsc:2|A-353-RGB,B-9476-OUT;n:type:ShaderForge.SFN_Fresnel,id:9476,x:32522,y:34066,varname:node_9476,prsc:2|EXP-4915-OUT;n:type:ShaderForge.SFN_Color,id:353,x:32463,y:33805,ptovrint:False,ptlb:node_Fresnel color,ptin:_node_Fresnelcolor,varname:_node_Fresnelcolor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.7517242,c4:1;n:type:ShaderForge.SFN_Slider,id:4915,x:32084,y:34098,ptovrint:False,ptlb:FresnelExponent,ptin:_FresnelExponent,varname:_FresnelExponent,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.7,max:4;n:type:ShaderForge.SFN_LightAttenuation,id:7677,x:32055,y:33646,varname:node_7677,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3070,x:32084,y:33804,varname:node_3070,prsc:2;n:type:ShaderForge.SFN_Color,id:998,x:31706,y:33365,ptovrint:False,ptlb:BackColor,ptin:_BackColor,varname:_BackColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.1,c3:0.1,c4:1;n:type:ShaderForge.SFN_Color,id:5214,x:31693,y:33588,ptovrint:False,ptlb:FrontColor,ptin:_FrontColor,varname:_FrontColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5058824,c2:0.1137255,c3:0.3176471,c4:1;n:type:ShaderForge.SFN_Dot,id:219,x:31695,y:33797,varname:node_219,prsc:2,dt:0|A-5349-OUT,B-1884-OUT;n:type:ShaderForge.SFN_LightVector,id:5349,x:31369,y:33603,varname:node_5349,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:1884,x:31365,y:33769,prsc:2,pt:False;n:type:ShaderForge.SFN_Lerp,id:9537,x:33763,y:32910,varname:node_9537,prsc:2|A-8205-RGB,B-8066-OUT,T-478-OUT;n:type:ShaderForge.SFN_Tex2d,id:8205,x:33231,y:32484,ptovrint:False,ptlb:BlendTexture,ptin:_BlendTexture,varname:_BlendTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False;n:type:ShaderForge.SFN_FragmentPosition,id:1811,x:32134,y:32733,varname:node_1811,prsc:2;n:type:ShaderForge.SFN_Slider,id:4886,x:32583,y:33089,ptovrint:False,ptlb:BlendSharpness,ptin:_BlendSharpness,varname:_BlendSharpness,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:5.273505,max:10;n:type:ShaderForge.SFN_Slider,id:7781,x:32159,y:33044,ptovrint:False,ptlb:BlendHeight,ptin:_BlendHeight,varname:_BlendHeight,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Add,id:3865,x:32602,y:32813,varname:node_3865,prsc:2|A-1811-Y,B-7781-OUT;n:type:ShaderForge.SFN_Multiply,id:2341,x:32954,y:32864,varname:node_2341,prsc:2|A-3865-OUT,B-4886-OUT;n:type:ShaderForge.SFN_Clamp01,id:478,x:33414,y:32944,varname:node_478,prsc:2|IN-2341-OUT;n:type:ShaderForge.SFN_Multiply,id:8066,x:33336,y:33178,varname:node_8066,prsc:2|A-7528-RGB,B-8821-OUT;n:type:ShaderForge.SFN_Lerp,id:9064,x:32112,y:33390,varname:node_9064,prsc:2|A-998-RGB,B-5214-RGB,T-219-OUT;proporder:353-4915-998-5214-8205-4886-7781;pass:END;sub:END;*/

Shader "Shader Forge/RockFacetsShader" {
    Properties {
        _node_Fresnelcolor ("node_Fresnel color", Color) = (0,1,0.7517242,1)
        _FresnelExponent ("FresnelExponent", Range(0, 4)) = 2.7
        _BackColor ("BackColor", Color) = (0.5,0.1,0.1,1)
        _FrontColor ("FrontColor", Color) = (0.5058824,0.1137255,0.3176471,1)
        _BlendTexture ("BlendTexture", 2D) = "black" {}
        _BlendSharpness ("BlendSharpness", Range(-10, 10)) = 5.273505
        _BlendHeight ("BlendHeight", Range(-10, 10)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _node_Fresnelcolor;
            uniform float _FresnelExponent;
            uniform float4 _BackColor;
            uniform float4 _FrontColor;
            uniform sampler2D _BlendTexture; uniform float4 _BlendTexture_ST;
            uniform float _BlendSharpness;
            uniform float _BlendHeight;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _BlendTexture_var = tex2D(_BlendTexture,TRANSFORM_TEX(i.uv0, _BlendTexture));
                float3 node_8066 = (UNITY_LIGHTMODEL_AMBIENT.rgb*((lerp(_BackColor.rgb,_FrontColor.rgb,dot(lightDirection,i.normalDir))*attenuation*_LightColor0.rgb)+(_node_Fresnelcolor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExponent))));
                float3 emissive = lerp(_BlendTexture_var.rgb,node_8066,saturate(((i.posWorld.g+_BlendHeight)*_BlendSharpness)));
                float3 finalColor = emissive + node_8066;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _node_Fresnelcolor;
            uniform float _FresnelExponent;
            uniform float4 _BackColor;
            uniform float4 _FrontColor;
            uniform sampler2D _BlendTexture; uniform float4 _BlendTexture_ST;
            uniform float _BlendSharpness;
            uniform float _BlendHeight;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 node_8066 = (UNITY_LIGHTMODEL_AMBIENT.rgb*((lerp(_BackColor.rgb,_FrontColor.rgb,dot(lightDirection,i.normalDir))*attenuation*_LightColor0.rgb)+(_node_Fresnelcolor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExponent))));
                float3 finalColor = node_8066;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
