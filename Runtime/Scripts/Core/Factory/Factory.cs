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
}

