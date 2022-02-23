using UnityEngine;

namespace SBaier.DI
{
    public class GameObjectContext : MonoContext
    {
        private ChildDIContext _currentContext;
        private GameObjectInjector _injector;
        protected override DIContext DIContext => _currentContext;
        
        protected override void DoInit(Resolver resolver)
        {
            _injector = resolver.Resolve<GameObjectInjector>();
            DIContext parentContext = resolver.Resolve<DIContext>();
            Factory<ChildDIContext, DIContext> contextFactory = resolver.Resolve<Factory<ChildDIContext, DIContext>>();
            _currentContext = contextFactory.Create(parentContext);
        }

        protected override void DoInjection()
        {
            Debug.Log($"{name} injecting into hierarchy");
            _injector.InjectIntoHierarchy(transform, DIContext.GetResolver());
        }

        protected override ContextAlreadyInitializedException CreateContextAlreadyInitializedException()
        {
            return new GameObjectContextAlreadyInitializedException(name);
        }
    }
}

