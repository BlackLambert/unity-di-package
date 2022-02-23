using UnityEngine;

namespace SBaier.DI
{
    public class AppContextInstaller : Installer
    {
        private DIContext _diContext;
        private GameObject _contextObject;

        public AppContextInstaller(DIContext dIContext, GameObject contextObject)
        {
            _diContext = dIContext;
            _contextObject = contextObject;
        }

        public void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<GameObjectInjector>();
            binder.BindToNewSelf<SceneInjector>();
            binder.BindToSelf<DIContext>().FromInstanceAsSingle(_diContext);
            binder.BindToNewSelf<DIInstanceFactory>();
            binder.BindToSelf<DIContainers>().FromFactory();
            binder.Bind<Factory<DIContainers>>().ToNew<DIContainersFactory>();
            binder.CreateNonResolvableInstance().OfComponent<LoadedSceneInitializer>().FromNewComponentOn(_contextObject).AsSingle().NonLazy();
            binder.BindToNewSelf<SceneContextProvider>().AsSingle();
            binder.Bind<Factory<ChildDIContext, DIContext>>().ToNew<ChildDIContextFactory>();
            new BindingValidationInstaller().InstallBindings(binder);
            new QuitDetectorInstaller(_contextObject).InstallBindings(binder);
        }
    }
}
