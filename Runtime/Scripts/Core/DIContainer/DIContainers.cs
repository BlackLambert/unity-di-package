using System;

namespace SBaier.DI
{
    public class DIContainers : BindingStorage
    {
        public BindingsContainer Bindings { get; }
        public SingleInstancesContainer SingleInstances { get; }
        public NonLazyContainer NonLazyInstanceInfos { get; }

        public DIContainers(BindingsContainer bindings,
            SingleInstancesContainer singleInstances,
            NonLazyContainer nonLazyBindings)
		{
            Bindings = bindings;
            SingleInstances = singleInstances;
            NonLazyInstanceInfos = nonLazyBindings;
        }

		public void AddBinding<TContract>(Binding binding, IComparable iD = null)
		{
            Bindings.AddBinding<TContract>(binding, iD);
        }

		public void AddToNonLazy(InstantiationInfo instantiationInfo)
		{
            NonLazyInstanceInfos.Add(instantiationInfo);
		}

		public void RemoveBinding(BindingKey key)
		{
            Bindings.Remove(key);
        }
	}
}