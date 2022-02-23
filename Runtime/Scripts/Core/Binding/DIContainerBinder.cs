using System;
using UnityEngine;

namespace SBaier.DI
{
	public class DIContainerBinder : Binder
	{
		private DIContainers _container;

		public DIContainerBinder(DIContainers container)
		{
			_container = container;
		}

        public BindingContext<TContract> Bind<TContract>(IComparable iD = default)
        {
            Binding binding = CreateBinding<TContract>();
            _container.AddBinding<TContract>(binding, iD);
            return new BindingContext<TContract>(new BindingArguments(binding, _container));
        }

        public ToBindingContext<TContract> BindToSelf<TContract>(IComparable iD = default)
        {
            return Bind<TContract>(iD).To<TContract>();
        }

        public FromNewBindingContext<TContract> BindToNewSelf<TContract>(IComparable iD = default) where TContract : new()
        {
            return Bind<TContract>(iD).ToNew<TContract>();
        }

        public AsBindingContext BindInstance<TContract>(TContract instance, IComparable iD = null)
        {
            return BindToSelf<TContract>(iD).FromInstanceAsSingle(instance);
        }

        public NonResolvableBindingContext CreateNonResolvableInstance()
        {
            Binding binding = CreateBinding<object>();
            return new NonResolvableBindingContext(new BindingArguments(binding, _container));
        }

        private Binding CreateBinding<TContract>()
        {
            Type contractType = typeof(TContract);
            return new Binding(contractType);
        }

		public ToComponentBindingContext<TContract> BindComponent<TContract>(IComparable iD = null) where TContract : Component
		{
            return Bind<TContract>().ToComponent<TContract>();
		}

		public ToObjectBindingContext<TContract> BindObject<TContract>(IComparable iD = null) where TContract : UnityEngine.Object
		{
            return Bind<TContract>().ToObject<TContract>();
		}
	}
}
