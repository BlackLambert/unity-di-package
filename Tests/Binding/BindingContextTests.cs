using Moq;
using NUnit.Framework;
using System;

namespace SBaier.DI.Tests
{
	public class BindingContextTests
	{
		private const int _barNumDefaultValue = 42;
		private const int _barNumConstructorValue = 12;

		[Test]
		public void BindingContext_To_BindsConcreteType()
		{
			Binding binding = GivenANewBinding();
			BindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToIsCalled(context);
			ThenBindingContainsConcreteType(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_BindsConcreteType()
		{
			Binding binding = GivenANewBinding();
			BindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenBindingContainsConcreteType(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_SetsCreationModeToFromNew()
		{
			Binding binding = GivenANewBinding();
			BindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenCreationModeIsFromNew(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_CreationFunctionCreatesInstanceOfConcreteType()
		{
			Binding binding = GivenANewBinding();
			BindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenCreationFunctionCreatesInstanceOfConcreteType(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_CreationFunctionCreatesDefaultConcreteType()
		{
			Binding binding = GivenANewBinding();
			BindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenCreationFunctionCreatesInstanceOfConcreteType(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_CreationFunctionCallesDefaultConstructor()
		{
			Binding binding = GivenANewBinding();
			BindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenConcreteInstanceIsCreatedByDefaultConstructor(binding);
		}

		private Binding GivenANewBinding()
		{
			return new Binding(typeof(Foo));
		}

		private BindingContext<Foo> GivenANewBindingContext(Binding binding)
		{
			BindingArguments arguments = new BindingArguments(binding, new Mock<BindingStorage>().Object);
			return new BindingContext<Foo>(arguments);
		}

		private void WhenToIsCalled(BindingContext<Foo> context)
		{
			context.To<Bar>();
		}

		private void WhenToNewIsCalled(BindingContext<Foo> context)
		{
			context.ToNew<Bar>();
		}

		private void ThenBindingContainsConcreteType(Binding binding)
		{
			Assert.AreEqual(typeof(Bar), binding.ConcreteType);
		}

		private void ThenCreationModeIsFromNew(Binding binding)
		{
			Assert.AreEqual(InstanceCreationMode.FromNew, binding.CreationMode);
		}

		private void ThenCreationFunctionCreatesInstanceOfConcreteType(Binding binding)
		{
			Assert.IsTrue(binding.CreateInstanceFunction() is Bar);
		}

		private void ThenConcreteInstanceIsCreatedByDefaultConstructor(Binding binding)
		{
			Bar bar = binding.CreateInstanceFunction() as Bar;
			Assert.AreEqual(_barNumConstructorValue, bar.Num);
		}

		private abstract class Foo{}
		private class Bar : Foo
		{
			public Bar()
			{
				Num = _barNumConstructorValue;
			}

			public int Num = _barNumDefaultValue;
		}
	}
}