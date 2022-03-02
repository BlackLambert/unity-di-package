using Moq;
using System;
using System.Collections.Generic;

namespace SBaier.DI.Tests
{
	public class TestBinder : Binder
	{
		public List<BindingKey> BoundContracts { get; } = new List<BindingKey>();
		public List<Binding> Bindings { get; } = new List<Binding>();

		ConcreteBindingContext<TContract> Binder.Bind<TContract>(IComparable iD)
		{
			return new ConcreteBindingContext<TContract>(SetupBindingArguments<TContract>(iD));
		}

		ComponentCreationModeBindingContext<TContract> Binder.BindComponent<TContract>(IComparable iD)
		{
			return new ComponentCreationModeBindingContext<TContract>(SetupBindingArguments<TContract>(iD));
		}

		FromInstanceBindingContext Binder.BindInstance<TContract>(TContract instance, IComparable iD)
		{
			return new FromInstanceBindingContext(SetupBindingArguments<TContract>(iD));
		}

		ObjectCreationModeBindingContext<TContract> Binder.BindObject<TContract>(IComparable iD) 
		{
			return new ObjectCreationModeBindingContext<TContract>(SetupBindingArguments<TContract>(iD));
		}

		FromNewBindingContext<TContract> Binder.BindToNewSelf<TContract>(IComparable iD)
		{
			return new FromNewBindingContext<TContract>(SetupBindingArguments<TContract>(iD));
		}

		CreationModeBindingContext<TContract> Binder.BindToSelf<TContract>(IComparable iD)
		{
			return new CreationModeBindingContext<TContract>(SetupBindingArguments<TContract>(iD));
		}

		public void Clear()
		{
			BoundContracts.Clear();
			Bindings.Clear();
		}

		public bool IsBound<TContract>(IComparable iD = null)
		{
			BindingKey key = CreateBindingKey<TContract>(iD);
			return BoundContracts.Contains(key);
		}

		private BindingArguments SetupBindingArguments<TContract>(IComparable iD)
		{
			AddContract<TContract>(iD);
			BindingArguments args = CreateBindingArguments<TContract>();
			Bindings.Add(args.Binding);
			return args;
		}

		private void AddContract<TContract>(IComparable iD)
		{
			BoundContracts.Add(CreateBindingKey<TContract>(iD));
		}

		private BindingKey CreateBindingKey<TContract>(IComparable iD)
		{
			return new BindingKey(typeof(TContract), iD);
		}

		private BindingArguments CreateBindingArguments<TContract>()
		{
			Binding binding = new Binding(typeof(TContract));
			Mock<BindingStorage> storageMock = new Mock<BindingStorage>();
			return new BindingArguments(binding, storageMock.Object);
		}
	}
}
