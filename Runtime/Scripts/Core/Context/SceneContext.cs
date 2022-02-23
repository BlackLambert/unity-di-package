using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneContext : MonoContext
    {
        private ChildDIContext _dIContext;
        protected override DIContext DIContext => _dIContext;

        [SerializeField]
        private string _iD = string.Empty;
        [SerializeField]
        private string _parentContextID = string.Empty;

        private SceneInjector _injector;
        private Scene _scene;
        private SceneContextProvider _sceneContextProvider;

        public string ID => _iD;
        public string ParentContextID => _parentContextID;


        protected override void DoInit(Resolver resolver)
		{
            _dIContext = CreateDIContext(resolver);
            InstallSceneContextBindings();
			ResolveDependencies();
			AddToProvider();
        }

        private void OnDestroy()
        {
            RemoveFromProvider();
        }

        private void AddToProvider()
		{
			_sceneContextProvider.Add(this);
		}

		private void RemoveFromProvider()
		{
			_sceneContextProvider.Remove(this);
		}

        private ChildDIContext CreateDIContext(Resolver resolver)
		{
            Factory<ChildDIContext, DIContext> contextFactory = resolver.Resolve<Factory<ChildDIContext, DIContext>>();
            DIContext parent = GetParentContext(resolver);
            return contextFactory.Create(parent);
        }

        private DIContext GetParentContext(Resolver resolver)
		{
            if (string.IsNullOrEmpty(_parentContextID))
                return resolver.Resolve<DIContext>();
            else
                return resolver.Resolve<SceneContextProvider>().Get(_parentContextID).DIContext;
        }

		private void InstallSceneContextBindings()
        {
            SceneContextInstaller installer = new SceneContextInstaller(gameObject, _dIContext);
            installer.InstallBindings(_binder);
        }

        private void ResolveDependencies()
        {
            _injector = _resolver.Resolve<SceneInjector>();
            _scene = _resolver.Resolve<Scene>();
            _sceneContextProvider = _resolver.Resolve<SceneContextProvider>();
        }

        protected override void DoInjection()
        {
            _injector.InjectIntoRootObjectsOf(_scene, _resolver);
        }

        protected override ContextAlreadyInitializedException CreateContextAlreadyInitializedException()
        {
            return new SceneContextAlreadyInitializedException(name);
        }
    }
}

