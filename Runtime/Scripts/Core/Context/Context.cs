namespace SBaier.DI
{
    public interface Context
    {
        public void Init(Resolver baseResolver);
        public void Reset();
    }
}

