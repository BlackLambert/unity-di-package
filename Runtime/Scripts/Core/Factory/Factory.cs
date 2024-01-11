namespace SBaier.DI
{
    public interface Factory
    {
        
    }
    
    public interface Factory<out TInstance> : Factory
    {
        public TInstance Create();
    }
    
    public interface Factory<out TInstance, in TArg> : Factory
    {
        public TInstance Create(TArg arg);
    }
    
    public interface Factory<out TInstance, in TArg1, in TArg2> : Factory
    {
        public TInstance Create(TArg1 arg1, TArg2 arg2);
    }
}

