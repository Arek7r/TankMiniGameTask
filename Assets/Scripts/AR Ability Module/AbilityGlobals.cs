namespace AR_Ability_Module
{
    public class AbilityGlobals
    { }
    
    public enum TypeActivation
    {
        Instant, //normal case
        TwoStep, // when we need use second time ability
    }

    public enum AbilityState
    {
        Normal,
        WaitingForActivation,
        Activated,
    }
}
