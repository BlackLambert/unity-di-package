using System;
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
            _currentContext = CreateDIContext(resolver);
			InstallGameObjectContextBindings();
		}

		private ChildDIContext CreateDIContext(Resolver resolver)
		{
			Factory<ChildDIContext, Resolver> contextFactory = resolver.Resolve<Factory<ChildDIContext, Resolver>>();
			return contextFactory.Create(resolver);
		}

		private void InstallGameObjectContextBindings()
		{
            GameObjectContextInstaller installer = new GameObjectContextInstaller(_currentContext);
            installer.InstallBindings(_binder);
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

