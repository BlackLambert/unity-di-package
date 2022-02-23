using UnityEngine;

namespace SBaier.DI
{
    public class AppContext : MonoContext
    {
        private BasicDIContext _dIContext;
        protected override DIContext DIContext => _dIContext;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Init(new Bootstrapper().Resolver);
        }

        protected override void DoInit(Resolver resolver)
        {
            _dIContext = resolver.Resolve<BasicDIContext>();
            InstallSceneContextBindings();
            DIContext.ValidateBindings();
        }

        private void InstallSceneContextBindings()
        {
            AppContextInstaller installer = new AppContextInstaller(_dIContext, gameObject);
            installer.InstallBindings(_binder);
        }

		protected override void DoInjection()
		{
            SceneContext sceneContext = FindObjectOfType<SceneContext>();
            if (sceneContext == null)
                throw new MissingSceneContextException();
            sceneContext.Init(_resolver);
        }

		protected override ContextAlreadyInitializedException CreateContextAlreadyInitializedException()
		{
            return new AppContextAlreadyInitializedException(name);
        }
	}
}
