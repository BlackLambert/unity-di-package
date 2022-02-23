namespace SBaier.DI
{
    public class DIContainersFactory : Factory<DIContainers>
    {
        public DIContainers Create()
        {
            return new DIContainers(
                new BindingsContainer(),
                new SingleInstancesContainer(),
                new NonLazyContainer());
        }
    } 
}

