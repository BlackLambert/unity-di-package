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
			ConcreteBindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToIsCalled(context);
			ThenBindingContainsConcreteType(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_BindsConcreteType()
		{
			Binding binding = GivenANewBinding();
			ConcreteBindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenBindingContainsConcreteType(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_SetsCreationModeToFromNew()
		{
			Binding binding = GivenANewBinding();
			ConcreteBindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenCreationModeIsFromNew(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_CreationFunctionCreatesInstanceOfConcreteType()
		{
			Binding binding = GivenANewBinding();
			ConcreteBindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenCreationFunctionCreatesInstanceOfConcreteType(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_CreationFunctionCreatesDefaultConcreteType()
		{
			Binding binding = GivenANewBinding();
			ConcreteBindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenCreationFunctionCreatesInstanceOfConcreteType(binding);
		}
		
		[Test]
		public void BindingContext_ToNew_CreationFunctionCallesDefaultConstructor()
		{
			Binding binding = GivenANewBinding();
			ConcreteBindingContext<Foo> context = GivenANewBindingContext(binding);
			WhenToNewIsCalled(context);
			ThenConcreteInstanceIsCreatedByDefaultConstructor(binding);
		}

		private Binding GivenANewBinding()
		{
			return new Binding(typeof(Foo));
		}

		private ConcreteBindingContext<Foo> GivenANewBindingContext(Binding binding)
		{
			BindingArguments arguments = new BindingArguments(binding, new Mock<BindingStorage>().Object);
			return new ConcreteBindingContext<Foo>(null, arguments);
		}

		private void WhenToIsCalled(ConcreteBindingContext<Foo> context)
		{
			context.To<Bar>();
		}

		private void WhenToNewIsCalled(ConcreteBindingContext<Foo> context)
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
			Assert.IsTrue(binding.ProvideInstanceFunction() is Bar);
		}

		private void ThenConcreteInstanceIsCreatedByDefaultConstructor(Binding binding)
		{
			Bar bar = binding.ProvideInstanceFunction() as Bar;
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