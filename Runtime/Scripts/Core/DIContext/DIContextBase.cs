using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class DIContextBase : DIContext, Injectable
    {
        private DIContainers _container;
        private DIInstanceFactory _instanceFactory;
        private InstantiationInfoValidator _bindingValidator;
        private GameObjectInjector _gameObjectInjector;

        private Resolver _dIContainerResolver;
        private Binder _dIContainerBinder;

        private BindingsContainer _bindings => _container.Bindings;
        private NonLazyContainer _nonLazyBindings => _container.NonLazyInstanceInfos;
        private SingleInstancesContainer _singleInstances => _container.SingleInstances;
        private DisposablesContainer _disposables => _container.DisposablesContainer;
        private ObjectsContainer _objects => _container.ObjectsContainer;

        void Injectable.Inject(Resolver resolver)
        {
            DoInjection(resolver);
            _dIContainerResolver = CreateResolver(_bindings, this);
            _dIContainerBinder = new DIContainerBinder(_container);
        }

        protected virtual void DoInjection(Resolver resolver)
        {
            _container = resolver.Resolve<DIContainers>();
            _instanceFactory = resolver.Resolve<DIInstanceFactory>();
            _bindingValidator = resolver.Resolve<InstantiationInfoValidator>();
            _gameObjectInjector = resolver.Resolve<GameObjectInjector>();
        }

        void DIContext.Reset()
        {
            DestroyInstances();
            _container.Reset();
        }

		public void ValidateBindings()
		{
            List<InstantiationInfo> instantiationInfo = _bindings.GetInstantiationInfos().ToList();
            instantiationInfo.AddRange(_nonLazyBindings.GetCopy());
            foreach (Binding binding in instantiationInfo)
                _bindingValidator.Validate(binding);
        }

        public void CreateNonLazyInstances()
        {
            foreach(InstantiationInfo instantiationInfo in _nonLazyBindings.GetCopy())
                CreateNonLazyInstance(instantiationInfo);
        }

        public Resolver GetResolver()
        {
            return _dIContainerResolver;
        }

        public Binder GetBinder()
        {
            return _dIContainerBinder;
        }

        public TContract GetInstance<TContract>(Binding binding)
        {
            _nonLazyBindings.TryRemoving(binding);
            return binding.AmountMode switch
            {
                InstanceAmountMode.Single => ResolveSingleInstance<TContract>(binding),
                InstanceAmountMode.PerRequest => CreateInstance<TContract>(binding),
                InstanceAmountMode.Undefined => throw new ArgumentException(),
                _ => throw new NotImplementedException()
            };
        }

        private TContract ResolveSingleInstance<TContract>(Binding binding)
        {
            if (_singleInstances.Has(binding))
                return _singleInstances.Get<TContract>(binding);
            return CreateSingleInstance<TContract>(binding);
        }

        private TContract CreateSingleInstance<TContract>(Binding binding)
        {
            TContract instance = CreateInstance<TContract>(binding);
            _singleInstances.Store(binding, instance);
            return instance;
        }

        private TContract CreateInstance<TContract>(InstantiationInfo instantiationInfo)
        {
            TContract instance = _instanceFactory.Create<TContract>(GetResolver(), instantiationInfo);
            TryInjection(instance, instantiationInfo);
            TryAddDisposables(instance, instantiationInfo);
            return instance;
        }

		private void TryAddDisposables<TContract>(TContract instance, InstantiationInfo instantiationInfo)
		{
            if (instantiationInfo.CreationMode == InstanceCreationMode.FromInstance)
                return;
			TryAddDisposable(instance as IDisposable);
            TryAddObject(instance as UnityEngine.Object);
        }

		private void TryAddDisposable(IDisposable disposable)
		{
            if (disposable == null)
                return;
            _disposables.Add(disposable);
        }

        private void TryAddObject(UnityEngine.Object obj)
        {
            if (obj == null)
                return;
            _objects.Add(obj);
        }

        private void DestroyInstances()
        {
            _disposables.Dispose();
            _objects.Destroy();
        }

        private void CreateNonLazyInstance(InstantiationInfo instantiationInfo)
		{
            if (!_nonLazyBindings.Has(instantiationInfo))
                return;
            if(instantiationInfo.ConcreteType.IsSubclassOf(typeof(Component)))
                CreateInstance<Component>(instantiationInfo);
            else if(instantiationInfo.ConcreteType.IsSubclassOf(typeof(UnityEngine.Object)))
                CreateInstance<UnityEngine.Object>(instantiationInfo);
            else 
                CreateInstance<object>(instantiationInfo);
            _nonLazyBindings.TryRemoving(instantiationInfo);
        }

        private void TryInjection<TContract>(TContract instance, InstantiationInfo instantiationInfo)
        {
            if (!instantiationInfo.InjectionAllowed)
                return;
            if (instance is Component)
                InjectIntoComponent(instance as Component, instantiationInfo);
            else if (instance is GameObject)
                InjectIntoGameObject(instance as GameObject, instantiationInfo);
            else
                InjectIntoInstance(instance, instantiationInfo);
        }

        private void InjectIntoGameObject(GameObject gameObject, InstantiationInfo instantiationInfo)
        {
            InjectIntoTransform(gameObject.transform, instantiationInfo);
        }

        private void InjectIntoComponent(Component component, InstantiationInfo instantiationInfo)
		{
            InjectIntoTransform(component.transform, instantiationInfo);
        }

        private void InjectIntoTransform(Transform transform, InstantiationInfo instantiationInfo)
        {
            _gameObjectInjector.InjectIntoContextHierarchy(transform, GetResolverFor(instantiationInfo));
        }

        private void InjectIntoInstance<TContract>(TContract instance, InstantiationInfo instantiationInfo)
		{
            Injectable injectable = instance as Injectable;
            if (injectable == null)
                return;
            injectable.Inject(GetResolverFor(instantiationInfo));
        }

        private Resolver GetResolverFor(InstantiationInfo instantiationInfo)
        {
            ArgumentsResolver result = new ArgumentsResolver(GetResolver());
            result.AddArguments(instantiationInfo.Arguments);
            return result;
        }

        protected abstract Resolver CreateResolver(BindingsContainer container, DIContext diContext);
	}
}

