namespace SBaier.DI
{
    public interface DIContext
    {
	    Resolver Resolver { get; }
	    Binder Binder { get; }
        void ValidateBindings();
        void CreateNonLazyInstances();
        TContract GetInstance<TContract>(Binding binding);
		void Reset();
	}
}