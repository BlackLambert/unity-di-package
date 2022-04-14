using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-9999)]
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
        private SceneObjectsDisabler _sceneDisabler;

        public string ID => _iD;
        public string ParentContextID => _parentContextID;

        private void Awake()
        {
            AppContext appContext = FindOrCreateAppContext();
            Resolver parentResolver = appContext.GetResolverFor(this);
            Init(parentResolver);
        }

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

        private void OnApplicationQuit()
        {
            DisableSceneObjects();
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
            Factory<ChildDIContext, Resolver> contextFactory = resolver.Resolve<Factory<ChildDIContext, Resolver>>();
            return contextFactory.Create(resolver);
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
            _sceneDisabler = _resolver.Resolve<SceneObjectsDisabler>();
        }

        protected override void DoInjection()
        {
            _injector.InjectIntoRootObjectsOf(_scene, _resolver);
        }

        protected override ContextAlreadyInitializedException CreateContextAlreadyInitializedException()
        {
            return new SceneContextAlreadyInitializedException(name);
        }

        private AppContext FindOrCreateAppContext()
        {
            AppContext appContext = FindObjectOfType<AppContext>();
            if (appContext == null)
                appContext = CreateAppContext();
            return appContext;
        }

        private AppContext CreateAppContext()
        {
            GameObject appContextObject = new GameObject(nameof(AppContext));
            return appContextObject.AddComponent<AppContext>();
        }

        private void DisableSceneObjects()
        {
            _sceneDisabler.DisableObjectsOf(_scene);
        }
    }
}

