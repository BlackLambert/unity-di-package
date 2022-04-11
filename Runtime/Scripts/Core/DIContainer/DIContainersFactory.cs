namespace SBaier.DI
{
    public class DIContainersFactory : Factory<DIContainers>, Injectable
    {
        private GameObjectDeactivator _deactivator;

        public void Inject(Resolver resolver)
        {
            _deactivator = resolver.Resolve<GameObjectDeactivator>();
        }

        public DIContainers Create()
        {
            return new DIContainers(
                new BindingsContainer(),
                new SingleInstancesContainer(),
                new NonLazyContainer(),
                new DisposablesContainer(),
                new ObjectsContainer(),
                new GameObjectsContainer(_deactivator));
        }
	} 
}

