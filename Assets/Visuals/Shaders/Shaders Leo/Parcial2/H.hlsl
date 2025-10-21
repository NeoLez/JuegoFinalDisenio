//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
void Loop_float(bool UsesSubgraph, out float New)
{
    Bindings_SampleLight_d3670751bc62b909da89d7b36eaef0da_float Bindings;
    SG_SampleLight_d3670751bc62b909da89d7b36eaef0da_float(Bindings, out float Res, out float nothing);
    New = Res;
}
#endif //MYHLSLINCLUDE_INCLUDED
