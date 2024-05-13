void GetMainLightDirection_half(float3 RimLightDirection, bool IsUseMainLight, out float3 MainLightDirection){
    #ifdef SHADERGRAPH_PREVIEW
        MainLightDirection = float3(0.5,0.5,0);
    #else
        if (IsUseMainLight){
            MainLightDirection = GetMainLight().direction;
        }
        else{
            MainLightDirection = RimLightDirection;
        }
    #endif
}