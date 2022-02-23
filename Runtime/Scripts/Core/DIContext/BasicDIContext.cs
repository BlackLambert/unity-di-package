namespace SBaier.DI
{
	public class BasicDIContext : DIContextBase
	{
		protected override Resolver CreateResolver(BindingsContainer container, DIContext diContext)
		{
			Resolver containerResolver = new DIContainerResolver(container, diContext);
			return new CircularDependencyDetector(containerResolver);
		}
	}
}