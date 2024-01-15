using System;

namespace SBaier.DI
{
    public class DIContainers : BindingStorage
    {
        public BindingsContainer Bindings { get; }
        public SingleInstancesContainer SingleInstances { get; }
        public NonLazyContainer NonLazyInstanceInfos { get; }
        public DisposablesContainer DisposablesContainer { get; }
        public ObjectsContainer ObjectsContainer { get; }
		internal GameObjectsContainer GameObjectsContainer { get; }

		public DIContainers(BindingsContainer bindings,
            SingleInstancesContainer singleInstances,
            NonLazyContainer nonLazyBindings,
            DisposablesContainer disposablesContainer,
            ObjectsContainer objectsContainer,
            GameObjectsContainer gameObjectsContainer)
		{
            Bindings = bindings;
            SingleInstances = singleInstances;
            NonLazyInstanceInfos = nonLazyBindings;
            DisposablesContainer = disposablesContainer;
            ObjectsContainer = objectsContainer;
            GameObjectsContainer = gameObjectsContainer;
        }

		public void AddBinding<TContract>(Binding binding, IComparable iD = null)
		{
            Bindings.AddBinding<TContract>(binding, iD);
        }

		public void AddToNonLazy(Binding binding)
		{
            NonLazyInstanceInfos.Add(binding);
		}

		public void RemoveBinding(BindingKey key)
		{
            Bindings.Remove(key);
        }

		public void Reset()
		{
            Bindings.Clear();
            SingleInstances.Clear();
            NonLazyInstanceInfos.Clear();
            DisposablesContainer.Clear();
			ObjectsContainer.Clear();
            GameObjectsContainer.Clear();
        }
	}
}