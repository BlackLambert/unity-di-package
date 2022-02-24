using Moq;
using System;
using System.Collections.Generic;

namespace SBaier.DI.Tests
{
	public class TestBinder : Binder
	{
		public List<BindingKey> BoundContracts { get; } = new List<BindingKey>();
		public List<Binding> Bindings { get; } = new List<Binding>();
		public List<Binding> NonResolvableBindings { get; } = new List<Binding>();

		BindingContext<TContract> Binder.Bind<TContract>(IComparable iD = null)
		{
			return new BindingContext<TContract>(CreateAndInitBindingArguments<TContract>(iD));
		}

		ToComponentBindingContext<TContract> Binder.BindComponent<TContract>(IComparable iD)
		{
			return new ToComponentBindingContext<TContract>(CreateAndInitBindingArguments<TContract>(iD));
		}

		AsBindingContext Binder.BindInstance<TContract>(TContract instance, IComparable iD)
		{
			return new AsBindingContext(CreateAndInitBindingArguments<TContract>(iD));
		}

		ToObjectBindingContext<TContract> Binder.BindObject<TContract>(IComparable iD) 
		{
			return new ToObjectBindingContext<TContract>(CreateAndInitBindingArguments<TContract>(iD));
		}

		FromNewBindingContext<TContract> Binder.BindToNewSelf<TContract>(IComparable iD)
		{
			return new FromNewBindingContext<TContract>(CreateAndInitBindingArguments<TContract>(iD));
		}

		ToBindingContext<TContract> Binder.BindToSelf<TContract>(IComparable iD)
		{
			return new ToBindingContext<TContract>(CreateAndInitBindingArguments<TContract>(iD));
		}

		NonResolvableBindingContext Binder.CreateNonResolvableInstance()
		{
			return new NonResolvableBindingContext(CreateAndInitNonResolvableArguments());
		}

		public void Clear()
		{
			BoundContracts.Clear();
			Bindings.Clear();
			NonResolvableBindings.Clear();
		}

		public bool IsBound<TContract>(IComparable iD = null)
		{
			BindingKey key = CreateBindingKey<TContract>(iD);
			return BoundContracts.Contains(key);
		}

		private BindingArguments CreateAndInitBindingArguments<TContract>(IComparable iD)
		{
			AddContract<TContract>(iD);
			BindingArguments args = CreateBindingArguments<TContract>();
			Bindings.Add(args.Binding);
			return args;
		}

		private BindingArguments CreateAndInitNonResolvableArguments()
		{
			BindingArguments args = CreateBindingArguments<object>();
			NonResolvableBindings.Add(args.Binding);
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
