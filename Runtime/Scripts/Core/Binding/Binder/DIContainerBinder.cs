using System;
using UnityEngine;

namespace SBaier.DI
{
	public class DIContainerBinder : Binder
	{
		private BindingStorage _container;

		public DIContainerBinder(BindingStorage container)
		{
			_container = container;
		}

        public ConcreteBindingContext<TContract> Bind<TContract>(IComparable iD = default)
        {
            Binding binding = CreateBinding<TContract>();
            BindingArguments arguments = new BindingArguments(binding, _container);
            return new ConcreteBindingContext<TContract>(iD, arguments);
        }

        public CreationModeBindingContext<TContract> BindToSelf<TContract>(IComparable iD = default)
        {
            return Bind<TContract>(iD).To<TContract>();
        }

        public FromNewBindingContext<TContract> BindToNewSelf<TContract>(IComparable iD = default) where TContract : new()
        {
            return Bind<TContract>(iD).ToNew<TContract>();
        }

        public FromInstanceBindingContext BindInstance<TContract>(TContract instance, IComparable iD = null)
        {
            return BindToSelf<TContract>(iD).FromInstance(instance);
        }

        public ComponentCreationModeBindingContext<TContract> BindComponent<TContract>(IComparable iD = null) where TContract : Component
        {
            return Bind<TContract>(iD).ToComponent<TContract>();
        }

        public ObjectCreationModeBindingContext<TContract> BindObject<TContract>(IComparable iD = null) where TContract : UnityEngine.Object
        {
            return Bind<TContract>(iD).ToObject<TContract>();
        }

        private Binding CreateBinding<TContract>()
        {
            Type contractType = typeof(TContract);
            return new Binding(contractType);
        }
	}
}
