using System;
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

        public Resolver Resolver => _dIContainerResolver;
        public Binder Binder => _dIContainerBinder;

        private BindingsContainer _bindings => _container.Bindings;
        private NonLazyContainer _nonLazyBindings => _container.NonLazyInstanceInfos;
        private SingleInstancesContainer _singleInstances => _container.SingleInstances;
        private DisposablesContainer _disposables => _container.DisposablesContainer;
        private ObjectsContainer _objects => _container.ObjectsContainer;
        private GameObjectsContainer _gameObjects => _container.GameObjectsContainer;

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
            _disposables.Dispose();
            _objects.Destroy();
            _container.Reset();
        }

        public void ValidateBindings()
        {
            foreach (InstantiationInfo info in _bindings.GetInstantiationInfos())
                _bindingValidator.Validate(info);
            foreach (Binding binding in _nonLazyBindings.Bindings)
                _bindingValidator.Validate(binding);
        }

        public void CreateNonLazyInstances()
        {
            foreach (Binding binding in _nonLazyBindings.Bindings)
                CreateNonLazyInstance(binding);
        }

        public TContract GetInstance<TContract>(Binding binding)
        {
            _nonLazyBindings.TryAddToCreated(binding);
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
            return _singleInstances.Has(binding)
                ? _singleInstances.Get<TContract>(binding)
                : CreateSingleInstance<TContract>(binding);
        }

        private TContract CreateSingleInstance<TContract>(Binding binding)
        {
            TContract instance = CreateInstance<TContract>(binding);
            _singleInstances.Store(binding, instance);
            return instance;
        }

        private TContract CreateInstance<TContract>(InstantiationInfo instantiationInfo)
        {
            TContract instance = _instanceFactory.Create<TContract>(Resolver, instantiationInfo);
            TryInjection(instance, instantiationInfo);
            StoreInstance(instance, instantiationInfo);
            return instance;
        }

        private void StoreInstance<TContract>(TContract instance, InstantiationInfo instantiationInfo)
        {
            if (instantiationInfo.CreationMode == InstanceCreationMode.FromInstance)
                return;
            TryAddDisposable(instance as IDisposable);
            if (instance is Component component && !TryAddGameObject(component.gameObject, instantiationInfo))
                _objects.Add(component);
        }

        private void TryAddDisposable(IDisposable disposable)
        {
            if (disposable == null)
                return;
            _disposables.Add(disposable);
        }

        private bool TryAddGameObject(GameObject gameObject, InstantiationInfo instantiationInfo)
        {
            switch (instantiationInfo.CreationMode)
            {
                case InstanceCreationMode.FromPrefabInstance:
                case InstanceCreationMode.FromResourcePrefabInstance:
                case InstanceCreationMode.FromMethod:
                    _gameObjects.Add(gameObject);
                    return true;
            }

            return false;
        }

        private void CreateNonLazyInstance(Binding binding)
        {
            if (!_nonLazyBindings.ShallCreate(binding))
                return;
            if (binding.ConcreteType.IsSubclassOf(typeof(Component)))
                GetInstance<Component>(binding);
            else if (binding.ConcreteType.IsSubclassOf(typeof(UnityEngine.Object)))
                GetInstance<UnityEngine.Object>(binding);
            else
                GetInstance<object>(binding);
            _nonLazyBindings.TryAddToCreated(binding);
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
            if (instance is not Injectable injectable)
                return;
            injectable.Inject(GetResolverFor(instantiationInfo));
        }

        private Resolver GetResolverFor(InstantiationInfo instantiationInfo)
        {
            ArgumentsResolver result = new ArgumentsResolver(Resolver);
            result.AddArguments(instantiationInfo.Arguments);
            return result;
        }

        protected abstract Resolver CreateResolver(BindingsContainer container, DIContext diContext);
    }
}