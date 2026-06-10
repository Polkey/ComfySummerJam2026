
struct TessellationFactors
{
    float edge[3] : SV_TessFactor;
    float inside : SV_InsideTessFactor;
};

[domain("tri")]
[patchconstantfunc("patchConstantFunction")]
[partitioning("fractional_odd")]
[outputtopology("triangle_cw")]
[outputcontrolpoints(3)]
PackedVaryings hull(InputPatch<PackedVaryings, 3> patch, uint id : SV_OutputControlPointID)
{
    return patch[id];
}

TessellationFactors CalcTriEdgeTessFactors(float3 vertexTessFactors)
{
    TessellationFactors f;
    f.edge[0] = 0.5 * (vertexTessFactors.y + vertexTessFactors.z);
    f.edge[1] = 0.5 * (vertexTessFactors.x + vertexTessFactors.z);
    f.edge[2] = 0.5 * (vertexTessFactors.x + vertexTessFactors.y);
    f.inside = (vertexTessFactors.x + vertexTessFactors.y + vertexTessFactors.z) / 3;

    return f;
}

float CalcDistanceTessFactor(float3 worldPosition)
{
    const float minDist = 2;
    float dist = distance(worldPosition, _WorldSpaceCameraPos);
    float factor = clamp(1 - (dist - minDist) / (_maxTessellationDistance - minDist), 0.01, 1);
    
    return clamp(factor * _Tessellation, 0, _Tessellation);

}

TessellationFactors DistanceBasedTess(PackedVaryings vertex0, PackedVaryings vertex1, PackedVaryings vertex2)
{
    float3 vertexTessFactors;
    
    vertexTessFactors.x = CalcDistanceTessFactor(vertex0.positionWS);
    vertexTessFactors.y = CalcDistanceTessFactor(vertex1.positionWS);
    vertexTessFactors.z = CalcDistanceTessFactor(vertex2.positionWS);
    
    return CalcTriEdgeTessFactors(vertexTessFactors);
}

TessellationFactors patchConstantFunction(InputPatch<PackedVaryings, 3> patch)
{
    return DistanceBasedTess(patch[0], patch[1], patch[2]);

}

//TEXTURE2D(_WaveMapLarge);
//SAMPLER(sampler_WaveMapLarge);

void vert(inout PackedVaryings IN)
{
    
    float2 waveUV1 =    (IN.positionWS.xz *
                        float2(1., 3.) +
                        (_Time * _WaveSpeed * 
                        normalize(float2(-.2, -1.)))) / 
                        _WaveScale;
    
    float2 waveUV2 =    (IN.positionWS.xz *
                        float2(1., 3.) + 
                        (_Time * _WaveSpeed * 
                        normalize(float2(1, -.6)))) / 
                        (_WaveScale * 1.3);
    
    float4 largeWaves1 = SAMPLE_TEXTURE2D_LOD(_WaveMapLarge, sampler_WaveMapLarge, waveUV1, 0);
    float4 largeWaves2 = SAMPLE_TEXTURE2D_LOD(_WaveMapLarge, sampler_WaveMapLarge, waveUV2, 0);
    float waves = (largeWaves1.z * 2. - 1.) + (largeWaves2.z * 2. - 1.);
    
    IN.positionWS += waves * _HeightMult * float3(0., 1., 0.);
    
    float3 objectPos = TransformWorldToObject(IN.positionWS);

    IN.positionCS = TransformObjectToHClip(objectPos);
}

#define INTERPOLATE(fieldName) data.fieldName = \
                        patch[0].fieldName * barycentricCoordinates.x + \
                        patch[1].fieldName * barycentricCoordinates.y + \
                        patch[2].fieldName * barycentricCoordinates.z;

[domain("tri")]
PackedVaryings domain(TessellationFactors factors, OutputPatch<PackedVaryings, 3> patch, float3 barycentricCoordinates : SV_DomainLocation)
{
    PackedVaryings data;
    ZERO_INITIALIZE(PackedVaryings, data);
    
    INTERPOLATE(positionWS)
    INTERPOLATE(positionCS)
    INTERPOLATE(normalWS)
    INTERPOLATE(tangentWS)
    INTERPOLATE(texCoord0)
    
    vert(data);
    return data;
}