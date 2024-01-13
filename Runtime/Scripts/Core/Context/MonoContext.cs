using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    [DisallowMultipleComponent]
    public abstract class MonoContext : MonoBehaviour, Context
    {
        [SerializeField]
        private MonoInstaller[] _monoInstallers = new MonoInstaller[0];

        [SerializeField]
        private ScriptableObjectInstaller[] _scriptableObjectInstallers = new ScriptableObjectInstaller[0];

        private List<Installer> _installers = new List<Installer>();
        protected abstract DIContext DIContext { get; }
        public bool Initialized { get; private set; } = false;

        protected Resolver _resolver => DIContext.Resolver;
        protected Binder _binder => DIContext.Binder;

	    protected virtual void OnDestroy()
		{
			if(Initialized)
                DIContext.Reset();
        }

		public virtual void Init(Resolver baseResolver)
        {
            ValidateInitCall();
            Initialized = true;
            DoInit(baseResolver);
            InstallBindings();
            DIContext.ValidateBindings();
            DoInjection();
            DIContext.CreateNonLazyInstances();
        }

        void Context.Reset()
        {
            ValidateResetCall();
            Initialized = false;
            DIContext.Reset();
        }

		public void AddInstaller(Installer installer)
        {
            _installers.Add(installer);
        }

        public Resolver GetResolver()
        {
            return _resolver;
        }

        protected abstract void DoInit(Resolver resolver);

        protected abstract void DoInjection();

        private void InstallBindings()
        {
            foreach (Installer installer in GetAllInstallers())
                InstallBindings(installer, DIContext.Resolver);
        }

        private List<Installer> GetAllInstallers()
        {
            List<Installer> result = new List<Installer>(_monoInstallers);
            result.AddRange(_installers);
            result.AddRange(_scriptableObjectInstallers);
            return result;
        }

        private void InstallBindings(Installer installer, Resolver resolver)
        {
            (installer as Injectable)?.Inject(resolver);
            installer.InstallBindings(_binder);
        }

        private void ValidateInitCall()
        {
            if (Initialized)
                throw CreateContextAlreadyInitializedException();
        }

        private void ValidateResetCall()
        {
            if (!Initialized)
                throw new InvalidOperationException("Trying to reset an uninitialized context");
        }

        protected abstract ContextAlreadyInitializedException CreateContextAlreadyInitializedException();
    }
}

