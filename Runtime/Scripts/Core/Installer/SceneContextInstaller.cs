using UnityEngine;
using UnityEngine.SceneManagement;

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
            binder.BindToSelf<Scene>().FromInstanceAsSingle(_contextObject.scene);
            binder.BindToSelf<DIContext>().FromInstanceAsSingle(_diContext);
            binder.Bind<Factory<ChildDIContext, DIContext>>().ToNew<ChildDIContextFactory>();
        }
    }
}

