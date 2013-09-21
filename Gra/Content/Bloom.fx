uniform extern texture ScreenTexture;	

sampler ScreenS = sampler_state
{
	Texture = <ScreenTexture>;	
};

#define Param 0.0005

float4 PixelShader(float2 texCoord : TEXCOORD0) : COLOR0
{

    float4 c = tex2D(ScreenS, texCoord);
    c += tex2D(ScreenS, texCoord - float2(Param,Param));
    c += tex2D(ScreenS, texCoord - float2(0,Param));
    c += tex2D(ScreenS, texCoord - float2(-Param,Param));
    
    c += tex2D(ScreenS, texCoord - float2(Param,0));
    c += tex2D(ScreenS, texCoord - float2(-Param,0));
    
    c += tex2D(ScreenS, texCoord - float2(Param,-Param));
    c += tex2D(ScreenS, texCoord - float2(0,-Param));
    c += tex2D(ScreenS, texCoord - float2(-Param,-Param));
    
    return c /9;
    
}


technique GaussianBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShader();
    }
}
