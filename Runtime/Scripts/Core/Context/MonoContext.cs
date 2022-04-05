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

        protected Resolver _resolver => DIContext.GetResolver();
        protected Binder _binder => DIContext.GetBinder();


        public virtual void Init(Resolver baseResolver)
        {
            ValidateInitCall();
            Initialized = true;
            DoInit(baseResolver);
            InjectIntoInstallers();
            InstallBindings();
            DIContext.ValidateBindings();
            DoInjection();
            DIContext.CreateNonLazyInstances();
        }

        void Context.Reset()
        {
            Initialized = false;
            DIContext.Reset();
        }

        public void AddInstaller(Installer installer)
        {
            _installers.Add(installer);
        }

        protected abstract void DoInit(Resolver resolver);

        protected abstract void DoInjection();

        private void InstallBindings()
        {
            foreach (Installer installer in GetAllInstallers())
                InstallBindings(installer, DIContext.GetResolver());
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

        private void InjectIntoInstallers()
        {
            List<Installer> installers = GetAllInstallers();
            foreach (Installer installer in installers)
                (installer as Injectable)?.Inject(DIContext.GetResolver());
        }

        private void ValidateInitCall()
        {
            if (Initialized)
                throw CreateContextAlreadyInitializedException();
        }

        protected abstract ContextAlreadyInitializedException CreateContextAlreadyInitializedException();
	}
}

