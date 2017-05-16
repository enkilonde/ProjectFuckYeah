// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-2957-OUT,custl-2159-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:8068,x:32044,y:33247,varname:node_8068,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3406,x:32065,y:33105,varname:node_3406,prsc:2;n:type:ShaderForge.SFN_LightVector,id:6869,x:31640,y:32968,varname:node_6869,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:9684,x:31640,y:33096,prsc:2,pt:True;n:type:ShaderForge.SFN_Dot,id:7782,x:31852,y:33011,cmnt:Lambert,varname:node_7782,prsc:2,dt:0|A-6869-OUT,B-9684-OUT;n:type:ShaderForge.SFN_Multiply,id:1941,x:32394,y:32947,cmnt:Diffuse Contribution,varname:node_1941,prsc:2|A-541-OUT,B-3406-RGB,C-8068-OUT;n:type:ShaderForge.SFN_Color,id:5927,x:31852,y:32848,ptovrint:False,ptlb:color1,ptin:_color1,varname:_color1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1304528,c2:0.04914576,c3:0.7426471,c4:1;n:type:ShaderForge.SFN_Slider,id:5328,x:31596,y:33614,ptovrint:False,ptlb:blending power,ptin:_blendingpower,varname:_blendingpower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:2159,x:32516,y:33126,cmnt:Combine,varname:node_2159,prsc:2|A-1941-OUT,B-695-OUT;n:type:ShaderForge.SFN_Lerp,id:541,x:32213,y:32763,varname:node_541,prsc:2|A-1157-RGB,B-5927-RGB,T-7782-OUT;n:type:ShaderForge.SFN_Fresnel,id:6519,x:31990,y:33539,varname:node_6519,prsc:2|EXP-5328-OUT;n:type:ShaderForge.SFN_Color,id:8578,x:31775,y:33317,ptovrint:False,ptlb:blending,ptin:_blending,varname:_blending,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2316177,c2:0.502282,c3:0.875,c4:1;n:type:ShaderForge.SFN_Multiply,id:695,x:32181,y:33426,varname:node_695,prsc:2|A-8578-RGB,B-6519-OUT;n:type:ShaderForge.SFN_Color,id:1157,x:31866,y:32603,ptovrint:False,ptlb:color 2,ptin:_color2,varname:_color2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:2957,x:32798,y:32674,varname:node_2957,prsc:2|A-379-OUT,B-2159-OUT,T-4509-OUT;n:type:ShaderForge.SFN_Clamp01,id:4509,x:32394,y:32533,varname:node_4509,prsc:2|IN-8793-OUT;n:type:ShaderForge.SFN_Multiply,id:8793,x:32107,y:32297,varname:node_8793,prsc:2|A-9747-OUT,B-1442-OUT,C-7673-R;n:type:ShaderForge.SFN_Add,id:9747,x:31832,y:32205,varname:node_9747,prsc:2|A-791-Y,B-242-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:791,x:31521,y:32167,varname:node_791,prsc:2;n:type:ShaderForge.SFN_Slider,id:242,x:31367,y:32364,ptovrint:False,ptlb:BlendHeight,ptin:_BlendHeight,varname:_BlendHeight,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Slider,id:1442,x:31745,y:32385,ptovrint:False,ptlb:BlendSHarpness,ptin:_BlendSHarpness,varname:_BlendSHarpness,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:6.367521,max:10;n:type:ShaderForge.SFN_Tex2d,id:7673,x:32040,y:31952,ptovrint:False,ptlb:node_7673,ptin:_node_7673,varname:_node_7673,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:5818,x:32946,y:31895,ptovrint:False,ptlb:NoiseCOlor,ptin:_NoiseCOlor,varname:_NoiseCOlor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:379,x:32970,y:32160,varname:node_379,prsc:2|A-5818-RGB,B-7673-RGB;proporder:5927-5328-8578-1157-242-1442-7673-5818;pass:END;sub:END;*/

Shader "Shader Forge/light_customlight" {
    Properties {
        _color1 ("color1", Color) = (0.1304528,0.04914576,0.7426471,1)
        _blendingpower ("blending power", Range(0, 1)) = 1
        _blending ("blending", Color) = (0.2316177,0.502282,0.875,1)
        _color2 ("color 2", Color) = (1,0,0,1)
        _BlendHeight ("BlendHeight", Range(-10, 10)) = 0
        _BlendSHarpness ("BlendSHarpness", Range(-10, 10)) = 6.367521
        _node_7673 ("node_7673", 2D) = "white" {}
        _NoiseCOlor ("NoiseCOlor", Color) = (1,0,0,1)
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
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform float4 _color1;
            uniform float _blendingpower;
            uniform float4 _blending;
            uniform float4 _color2;
            uniform float _BlendHeight;
            uniform float _BlendSHarpness;
            uniform sampler2D _node_7673; uniform float4 _node_7673_ST;
            uniform float4 _NoiseCOlor;
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
                float4 _node_7673_var = tex2D(_node_7673,TRANSFORM_TEX(i.uv0, _node_7673));
                float3 node_2159 = ((lerp(_color2.rgb,_color1.rgb,dot(lightDirection,normalDirection))*_LightColor0.rgb*attenuation)+(_blending.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_blendingpower))); // Combine
                float3 emissive = lerp((_NoiseCOlor.rgb*_node_7673_var.rgb),node_2159,saturate(((i.posWorld.g+_BlendHeight)*_BlendSHarpness*_node_7673_var.r)));
                float3 finalColor = emissive + node_2159;
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
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform float4 _color1;
            uniform float _blendingpower;
            uniform float4 _blending;
            uniform float4 _color2;
            uniform float _BlendHeight;
            uniform float _BlendSHarpness;
            uniform sampler2D _node_7673; uniform float4 _node_7673_ST;
            uniform float4 _NoiseCOlor;
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
                float3 node_2159 = ((lerp(_color2.rgb,_color1.rgb,dot(lightDirection,normalDirection))*_LightColor0.rgb*attenuation)+(_blending.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_blendingpower))); // Combine
                float3 finalColor = node_2159;
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
