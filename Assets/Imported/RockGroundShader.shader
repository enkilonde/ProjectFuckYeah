// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:35157,y:33023,varname:node_9361,prsc:2|emission-2447-OUT,custl-8066-OUT,voffset-3032-OUT,tess-5461-OUT;n:type:ShaderForge.SFN_AmbientLight,id:7528,x:32784,y:33140,varname:node_7528,prsc:2;n:type:ShaderForge.SFN_Add,id:8821,x:32836,y:33339,varname:node_8821,prsc:2|A-2035-OUT,B-973-OUT;n:type:ShaderForge.SFN_Multiply,id:2035,x:32152,y:33560,varname:node_2035,prsc:2|A-5892-OUT,B-7677-OUT,C-3070-RGB;n:type:ShaderForge.SFN_Multiply,id:973,x:32579,y:33955,varname:node_973,prsc:2|A-353-RGB,B-9476-OUT;n:type:ShaderForge.SFN_Fresnel,id:9476,x:32208,y:34035,varname:node_9476,prsc:2|EXP-4915-OUT;n:type:ShaderForge.SFN_Color,id:353,x:32149,y:33774,ptovrint:False,ptlb:FresnelColor,ptin:_FresnelColor,varname:_FresnelColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.7517242,c4:1;n:type:ShaderForge.SFN_Slider,id:4915,x:31770,y:34067,ptovrint:False,ptlb:FresnelExponent,ptin:_FresnelExponent,varname:_FresnelExponent,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:25.25646,max:50;n:type:ShaderForge.SFN_Lerp,id:5892,x:31769,y:33363,varname:node_5892,prsc:2|A-998-RGB,B-5214-RGB,T-219-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:7677,x:31741,y:33615,varname:node_7677,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3070,x:31770,y:33773,varname:node_3070,prsc:2;n:type:ShaderForge.SFN_Color,id:998,x:31392,y:33334,ptovrint:False,ptlb:BackColor,ptin:_BackColor,varname:_BackColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5019608,c2:0.1137255,c3:0.1686275,c4:1;n:type:ShaderForge.SFN_Color,id:5214,x:31379,y:33557,ptovrint:False,ptlb:FrontColor,ptin:_FrontColor,varname:_FrontColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5058824,c2:0.1137255,c3:0.3176471,c4:1;n:type:ShaderForge.SFN_Dot,id:219,x:31381,y:33766,varname:node_219,prsc:2,dt:0|A-5349-OUT,B-1884-OUT;n:type:ShaderForge.SFN_LightVector,id:5349,x:31055,y:33572,varname:node_5349,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:1884,x:31051,y:33738,prsc:2,pt:False;n:type:ShaderForge.SFN_Lerp,id:9537,x:33449,y:32879,varname:node_9537,prsc:2|A-8205-RGB,B-8066-OUT,T-478-OUT;n:type:ShaderForge.SFN_Tex2d,id:8205,x:33576,y:33318,ptovrint:False,ptlb:BlendTexture,ptin:_BlendTexture,varname:_BlendTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f10578e2d0f489d4398d988b152b851c,ntxv:2,isnm:False|UVIN-2175-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:1811,x:31808,y:32696,varname:node_1811,prsc:2;n:type:ShaderForge.SFN_Slider,id:4886,x:32257,y:33052,ptovrint:False,ptlb:BlendSharpness,ptin:_BlendSharpness,varname:_BlendSharpness,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:5.273505,max:10;n:type:ShaderForge.SFN_Slider,id:7781,x:31833,y:33007,ptovrint:False,ptlb:BlendHeight,ptin:_BlendHeight,varname:_BlendHeight,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Add,id:3865,x:32276,y:32776,varname:node_3865,prsc:2|A-1811-Y,B-7781-OUT;n:type:ShaderForge.SFN_Multiply,id:2341,x:32628,y:32827,varname:node_2341,prsc:2|A-3865-OUT,B-4886-OUT;n:type:ShaderForge.SFN_Clamp01,id:478,x:33088,y:32907,varname:node_478,prsc:2|IN-2341-OUT;n:type:ShaderForge.SFN_Multiply,id:8066,x:33022,y:33147,varname:node_8066,prsc:2|A-7528-RGB,B-8821-OUT;n:type:ShaderForge.SFN_Distance,id:1954,x:33692,y:31655,varname:node_1954,prsc:2|A-9159-XYZ,B-9911-XYZ;n:type:ShaderForge.SFN_Lerp,id:2447,x:34275,y:32830,varname:node_2447,prsc:2|A-5201-RGB,B-6460-OUT,T-4019-OUT;n:type:ShaderForge.SFN_ViewPosition,id:9159,x:33391,y:31435,varname:node_9159,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:9911,x:33443,y:31589,varname:node_9911,prsc:2;n:type:ShaderForge.SFN_Divide,id:4739,x:33988,y:31648,varname:node_4739,prsc:2|A-1954-OUT,B-7238-OUT;n:type:ShaderForge.SFN_Power,id:7178,x:34168,y:31647,varname:node_7178,prsc:2|VAL-4739-OUT,EXP-3040-OUT;n:type:ShaderForge.SFN_Clamp01,id:4019,x:34346,y:31615,varname:node_4019,prsc:2|IN-7178-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7238,x:33777,y:31849,ptovrint:False,ptlb:TransitionDist,ptin:_TransitionDist,varname:_TransitionDist,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:25;n:type:ShaderForge.SFN_ValueProperty,id:3040,x:33982,y:31818,ptovrint:False,ptlb:TransitionFalloff,ptin:_TransitionFalloff,varname:_TransitionFalloff,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Tex2d,id:5201,x:33832,y:32092,ptovrint:False,ptlb:GridTexture,ptin:_GridTexture,varname:_GridTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:529239097d02f9f42b0ddd436c6fcbb0,ntxv:0,isnm:False|UVIN-9915-OUT;n:type:ShaderForge.SFN_TexCoord,id:8174,x:32869,y:31516,varname:node_8174,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector4Property,id:216,x:32784,y:31870,ptovrint:False,ptlb:TexGridMapping,ptin:_TexGridMapping,varname:_TexGridMapping,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10,v2:10,v3:0,v4:0;n:type:ShaderForge.SFN_Add,id:9915,x:33446,y:31797,varname:node_9915,prsc:2|A-5231-OUT,B-4994-OUT;n:type:ShaderForge.SFN_Multiply,id:5231,x:33232,y:31658,varname:node_5231,prsc:2|A-8174-UVOUT,B-8713-OUT;n:type:ShaderForge.SFN_Append,id:8713,x:33017,y:31735,varname:node_8713,prsc:2|A-216-X,B-216-Y;n:type:ShaderForge.SFN_Append,id:4994,x:33178,y:31870,varname:node_4994,prsc:2|A-216-Z,B-216-W;n:type:ShaderForge.SFN_Desaturate,id:8537,x:34078,y:33396,varname:node_8537,prsc:2|COL-8205-RGB;n:type:ShaderForge.SFN_Clamp01,id:3161,x:34247,y:33373,varname:node_3161,prsc:2|IN-8537-OUT;n:type:ShaderForge.SFN_Subtract,id:2292,x:34449,y:33376,varname:node_2292,prsc:2|A-3161-OUT,B-4892-OUT;n:type:ShaderForge.SFN_Vector1,id:4892,x:34258,y:33291,varname:node_4892,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:6048,x:34639,y:33376,varname:node_6048,prsc:2|A-2292-OUT,B-3555-OUT;n:type:ShaderForge.SFN_Slider,id:3555,x:34133,y:33659,ptovrint:False,ptlb:Bump,ptin:_Bump,varname:_Bump,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.7264968,max:1;n:type:ShaderForge.SFN_Append,id:3032,x:34862,y:33521,varname:node_3032,prsc:2|A-4645-OUT,B-6048-OUT,C-4645-OUT;n:type:ShaderForge.SFN_Vector1,id:4645,x:34602,y:33553,varname:node_4645,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:5461,x:34807,y:33706,ptovrint:False,ptlb:TesselateValue,ptin:_TesselateValue,varname:_TesselateValue,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:2175,x:33357,y:33595,varname:node_2175,prsc:2|A-8174-UVOUT,B-4742-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4742,x:33166,y:33750,ptovrint:False,ptlb:BlendTextureTiling,ptin:_BlendTextureTiling,varname:_BlendTextureTiling,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:6460,x:33834,y:32936,varname:node_6460,prsc:2|A-7553-RGB,B-8205-RGB;n:type:ShaderForge.SFN_Color,id:7553,x:33530,y:32645,ptovrint:False,ptlb:BlendColor,ptin:_BlendColor,varname:_BlendColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;proporder:353-4915-998-5214-8205-4742-4886-7781-7238-3040-5201-216-3555-5461-7553;pass:END;sub:END;*/

Shader "Shader Forge/RockFacetsShader" {
    Properties {
        _FresnelColor ("FresnelColor", Color) = (0,1,0.7517242,1)
        _FresnelExponent ("FresnelExponent", Range(0, 50)) = 25.25646
        _BackColor ("BackColor", Color) = (0.5019608,0.1137255,0.1686275,1)
        _FrontColor ("FrontColor", Color) = (0.5058824,0.1137255,0.3176471,1)
        _BlendTexture ("BlendTexture", 2D) = "black" {}
        _BlendTextureTiling ("BlendTextureTiling", Float ) = 2
        _BlendSharpness ("BlendSharpness", Range(-10, 10)) = 5.273505
        _BlendHeight ("BlendHeight", Range(-10, 10)) = 0
        _TransitionDist ("TransitionDist", Float ) = 25
        _TransitionFalloff ("TransitionFalloff", Float ) = 5
        _GridTexture ("GridTexture", 2D) = "white" {}
        _TexGridMapping ("TexGridMapping", Vector) = (10,10,0,0)
        _Bump ("Bump", Range(-1, 1)) = 0.7264968
        _TesselateValue ("TesselateValue", Float ) = 2
        _BlendColor ("BlendColor", Color) = (1,1,1,1)
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _FresnelColor;
            uniform float _FresnelExponent;
            uniform float4 _BackColor;
            uniform float4 _FrontColor;
            uniform sampler2D _BlendTexture; uniform float4 _BlendTexture_ST;
            uniform float _TransitionDist;
            uniform float _TransitionFalloff;
            uniform sampler2D _GridTexture; uniform float4 _GridTexture_ST;
            uniform float4 _TexGridMapping;
            uniform float _Bump;
            uniform float _TesselateValue;
            uniform float _BlendTextureTiling;
            uniform float4 _BlendColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
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
                float node_4645 = 0.0;
                float2 node_2175 = (o.uv0*_BlendTextureTiling);
                float4 _BlendTexture_var = tex2Dlod(_BlendTexture,float4(TRANSFORM_TEX(node_2175, _BlendTexture),0.0,0));
                v.vertex.xyz += float3(node_4645,((saturate(dot(_BlendTexture_var.rgb,float3(0.3,0.59,0.11)))-0.5)*_Bump),node_4645);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _TesselateValue;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float2 node_9915 = ((i.uv0*float2(_TexGridMapping.r,_TexGridMapping.g))+float2(_TexGridMapping.b,_TexGridMapping.a));
                float4 _GridTexture_var = tex2D(_GridTexture,TRANSFORM_TEX(node_9915, _GridTexture));
                float2 node_2175 = (i.uv0*_BlendTextureTiling);
                float4 _BlendTexture_var = tex2D(_BlendTexture,TRANSFORM_TEX(node_2175, _BlendTexture));
                float3 emissive = lerp(_GridTexture_var.rgb,(_BlendColor.rgb*_BlendTexture_var.rgb),saturate(pow((distance(_WorldSpaceCameraPos,i.posWorld.rgb)/_TransitionDist),_TransitionFalloff)));
                float3 node_8066 = (UNITY_LIGHTMODEL_AMBIENT.rgb*((lerp(_BackColor.rgb,_FrontColor.rgb,dot(lightDirection,i.normalDir))*attenuation*_LightColor0.rgb)+(_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExponent))));
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _FresnelColor;
            uniform float _FresnelExponent;
            uniform float4 _BackColor;
            uniform float4 _FrontColor;
            uniform sampler2D _BlendTexture; uniform float4 _BlendTexture_ST;
            uniform float _TransitionDist;
            uniform float _TransitionFalloff;
            uniform sampler2D _GridTexture; uniform float4 _GridTexture_ST;
            uniform float4 _TexGridMapping;
            uniform float _Bump;
            uniform float _TesselateValue;
            uniform float _BlendTextureTiling;
            uniform float4 _BlendColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
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
                float node_4645 = 0.0;
                float2 node_2175 = (o.uv0*_BlendTextureTiling);
                float4 _BlendTexture_var = tex2Dlod(_BlendTexture,float4(TRANSFORM_TEX(node_2175, _BlendTexture),0.0,0));
                v.vertex.xyz += float3(node_4645,((saturate(dot(_BlendTexture_var.rgb,float3(0.3,0.59,0.11)))-0.5)*_Bump),node_4645);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _TesselateValue;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 node_8066 = (UNITY_LIGHTMODEL_AMBIENT.rgb*((lerp(_BackColor.rgb,_FrontColor.rgb,dot(lightDirection,i.normalDir))*attenuation*_LightColor0.rgb)+(_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExponent))));
                float3 finalColor = node_8066;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform sampler2D _BlendTexture; uniform float4 _BlendTexture_ST;
            uniform float _Bump;
            uniform float _TesselateValue;
            uniform float _BlendTextureTiling;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float node_4645 = 0.0;
                float2 node_2175 = (o.uv0*_BlendTextureTiling);
                float4 _BlendTexture_var = tex2Dlod(_BlendTexture,float4(TRANSFORM_TEX(node_2175, _BlendTexture),0.0,0));
                v.vertex.xyz += float3(node_4645,((saturate(dot(_BlendTexture_var.rgb,float3(0.3,0.59,0.11)))-0.5)*_Bump),node_4645);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _TesselateValue;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
