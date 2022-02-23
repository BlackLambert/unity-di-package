namespace SBaier.DI
{
    public class ChildDIContext : DIContextBase
    {
        private DIContext _baseContext;

		protected override void DoInjection(Resolver resolver)
		{
			base.DoInjection(resolver);
			_baseContext = resolver.Resolve<DIContext>();
		}

		protected override Resolver CreateResolver(BindingsContainer container, DIContext diContext)
		{
			Resolver containerResolver = new DIContainerResolver(container, diContext);
			Resolver detector = new CircularDependencyDetector(containerResolver);
			return new ChildResolver(_baseContext.GetResolver(), detector);
		}
	}
}