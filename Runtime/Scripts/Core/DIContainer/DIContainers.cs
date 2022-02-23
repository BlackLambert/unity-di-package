using System;

namespace SBaier.DI
{
    public class DIContainers : BindingStorage
    {
        public BindingsContainer Bindings { get; }
        public SingleInstancesContainer SingleInstances { get; }
        public NonLazyContainer NonLazyBindings { get; }

        public DIContainers(BindingsContainer bindings,
            SingleInstancesContainer singleInstances,
            NonLazyContainer nonLazyBindings)
		{
            Bindings = bindings;
            SingleInstances = singleInstances;
            NonLazyBindings = nonLazyBindings;
        }

		public void AddBinding<TContract>(Binding binding, IComparable iD = null)
		{
            Bindings.AddBinding<TContract>(binding, iD);
        }

		public void AddToNonLazy(Binding binding)
		{
            NonLazyBindings.Add(binding);
		}
	}
}