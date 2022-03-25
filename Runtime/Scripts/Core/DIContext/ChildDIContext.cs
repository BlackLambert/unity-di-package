namespace SBaier.DI
{
    public class ChildDIContext : DIContextBase
    {
		private Resolver _resolver;

		protected override void DoInjection(Resolver resolver)
		{
			base.DoInjection(resolver);
			_resolver = resolver;
		}

		protected override Resolver CreateResolver(BindingsContainer container, DIContext diContext)
		{
			Resolver containerResolver = new DIContainerResolver(container, diContext);
			Resolver detector = new CircularDependencyDetector(containerResolver);
			return new ChildResolver(_resolver, detector);
		}
	}
}