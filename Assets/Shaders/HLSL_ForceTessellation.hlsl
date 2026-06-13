#include "HLSL_TessLogic.hlsl"

#pragma hull hull
#pragma domain domain

void ForceTess_float(in float2 UV, in float3 WorldPos, in float3 WorldNormal, in float3 WorldTangent, out float Dummy)
{
    Dummy = 0;
}