namespace SBaier.DI
{
    public class ChildDIContextFactory : Factory<ChildDIContext, DIContext>
    {
        public ChildDIContext Create(DIContext parent)
        {
            ChildDIContext result = new ChildDIContext();
            (result as Injectable).Inject(parent.GetResolver());
            return result;
        }
    }
}

