namespace SBaier.DI
{
    public interface DIContext
    {
        void ValidateBindings();
        void CreateNonLazyInstances();
        Resolver GetResolver();
		Binder GetBinder();
        TContract GetInstance<TContract>(Binding binding);
    }
}