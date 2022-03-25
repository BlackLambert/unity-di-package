using UnityEngine;

namespace SBaier.DI
{
    public class SceneContextInstaller : Installer
    {
        private GameObject _contextObject;
        private DIContext _diContext;

        public SceneContextInstaller(GameObject contextObject, DIContext dIContext)
        {
            _contextObject = contextObject;
            _diContext = dIContext;
        }
        
        public void InstallBindings(Binder binder)
        {
            binder.BindInstance(_contextObject.scene);
            binder.BindInstance(_diContext).WithoutInjection();
            binder.Bind<Factory<ChildDIContext, Resolver>>().ToNew<ChildDIContextFactory>();
        }
    }
}

