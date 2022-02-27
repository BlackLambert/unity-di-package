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
            binder.BindSingleInstance(_contextObject.scene);
            binder.BindSingleInstance(_diContext).WithoutInjection();
            binder.Bind<Factory<ChildDIContext, DIContext>>().ToNew<ChildDIContextFactory>();
        }
    }
}

