using UnityEngine;
using UnityEngine.UI;

namespace SBaier.DI
{
    public class AppContextInstaller : Installer
    {
        private DIContext _diContext;
        private GameObject _contextObject;
        private MonoBehaviour _behaviour;

        public AppContextInstaller(DIContext dIContext, GameObject contextObject, MonoBehaviour behaviour)
        {
            _diContext = dIContext;
            _contextObject = contextObject;
            _behaviour = behaviour;
        }

        public void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<GameObjectInjector>();
            binder.BindToNewSelf<GameObjectContextsReseter>();
            binder.BindToNewSelf<SceneInjector>();
            binder.BindInstance(_diContext).WithoutInjection();
            binder.BindToNewSelf<DIInstanceFactory>();
            binder.BindToSelf<DIContainers>().FromFactory();
            binder.Bind<Factory<DIContainers>>().ToNew<DIContainersFactory>();
            binder.BindToNewSelf<SceneContextProvider>().AsSingle();
            binder.Bind<Factory<ChildDIContext, Resolver>>().ToNew<ChildDIContextFactory>();
            binder.BindToSelf<MonoPoolCache>().FromMethod(CreatePoolCache).AsSingle();
            binder.BindToNewSelf<SceneObjectsDisabler>();
            binder.Bind<ObjectActivator>().ToNew<BasicObjectActivator>();
            binder.BindInstance(_behaviour);
            new BindingValidationInstaller().InstallBindings(binder);
            new QuitDetectorInstaller(_contextObject).InstallBindings(binder);
        }

		private MonoPoolCache CreatePoolCache()
		{
            GameObject cacheObject = new GameObject(nameof(MonoPoolCache));
            GameObject uiCacheObject = new GameObject("UI Cache");
            Canvas canvas = uiCacheObject.AddComponent<Canvas>();
            uiCacheObject.AddComponent<CanvasScaler>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            uiCacheObject.transform.SetParent(cacheObject.transform);
            cacheObject.transform.SetParent(_contextObject.transform);
            return cacheObject.AddComponent<MonoPoolCache>();
        }
	}
}
