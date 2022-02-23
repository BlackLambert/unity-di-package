using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class DIContextBase : DIContext, Injectable
    {
        private DIContainers _container;
        private DIInstanceFactory _instanceFactory;
        private BindingValidator _bindingValidator;
        private GameObjectInjector _gameObjectInjector;

        private Resolver _dIContainerResolver;
        private Binder _dIContainerBinder;

        private BindingsContainer _bindings => _container.Bindings;
        private NonLazyContainer _nonLazyBindings => _container.NonLazyBindings;
        private SingleInstancesContainer _singleInstances => _container.SingleInstances;

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
            _bindingValidator = resolver.Resolve<BindingValidator>();
            _gameObjectInjector = resolver.Resolve<GameObjectInjector>();
        }

        public void ValidateBindings()
		{
            IEnumerable<Binding> bindings = _bindings.GetBindings();
            foreach (Binding binding in bindings)
                _bindingValidator.Validate(binding);
        }

        public void CreateNonLazyInstances()
        {
            foreach(Binding binding in _nonLazyBindings.GetCopy())
                CreateNonLazyInstance(binding);
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

        private TContract CreateInstance<TContract>(Binding binding)
        {
            TContract instance = _instanceFactory.Create<TContract>(GetResolver(), binding);
            TryInjection(instance, binding);
            return instance;
        }

        private void CreateNonLazyInstance(Binding binding)
		{
            if (!_nonLazyBindings.Has(binding))
                return;
            CreateInstance<object>(binding);
            _nonLazyBindings.TryRemoving(binding);
        }

        private void TryInjection<TContract>(TContract instance, Binding binding)
        {
            if (!binding.InjectionAllowed)
                return;
            if (instance is Component)
                InjectIntoComponent(instance as Component, binding);
            else if (instance is GameObject)
                InjectIntoGameObject(instance as GameObject, binding);
            else
                InjectIntoInstance(instance, binding);
        }

        private void InjectIntoGameObject(GameObject gameObject, Binding binding)
        {
            InjectIntoTransform(gameObject.transform, binding);
        }

        private void InjectIntoComponent(Component component, Binding binding)
		{
            InjectIntoTransform(component.transform, binding);
        }

        private void InjectIntoTransform(Transform transform, Binding binding)
        {
            _gameObjectInjector.InjectIntoContextHierarchy(transform, GetResolverFor(binding));
        }

        private void InjectIntoInstance<TContract>(TContract instance, Binding binding)
		{
            Injectable injectable = instance as Injectable;
            if (injectable == null)
                return;
            injectable.Inject(GetResolverFor(binding));
        }

        private Resolver GetResolverFor(Binding binding)
        {
            ArgumentsResolver result = new ArgumentsResolver(GetResolver());
            result.AddArguments(binding.Arguments);
            return result;
        }

        protected abstract Resolver CreateResolver(BindingsContainer container, DIContext diContext);
    }
}

