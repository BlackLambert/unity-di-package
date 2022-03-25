namespace SBaier.DI
{
    public class ChildDIContextFactory : Factory<ChildDIContext, Resolver>
    {
        public ChildDIContext Create(Resolver parentResolver)
        {
            ChildDIContext result = new ChildDIContext();
            (result as Injectable).Inject(parentResolver);
            return result;
        }
    }
}

