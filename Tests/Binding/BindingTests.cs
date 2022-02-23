using NUnit.Framework;
using System;

namespace SBaier.DI.Tests
{
    public class BindingTests
    {
        [Test]
        public void Binding_ToString_ContainsAllPropertyValues()
        {
            Binding binding = GivenANewBinding();
            string bindingString = WhenToStringIsCalledOn(binding);
            ThenStringContainsAllPropertyValues(bindingString, binding);
        }

        [Test]
        public void Binding_ToString_ContainsClassName()
        {
            Binding binding = GivenANewBinding();
            string bindingString = WhenToStringIsCalledOn(binding);
            ThenStringContainsClassName(bindingString);
        }

		private Binding GivenANewBinding()
		{
            Binding binding = new Binding(typeof(Foo));
            binding.ConcreteType = typeof(Bar);
            binding.AmountMode = InstanceAmountMode.Single;
            binding.CreateInstanceFunction = () => new Bar();
            binding.CreationMode = InstanceCreationMode.FromMethod;
            binding.InjectionAllowed = true;
            AddArguments(binding);
            return binding;
        }

		private string WhenToStringIsCalledOn(Binding binding)
        {
            return binding.ToString();
        }

        private void ThenStringContainsAllPropertyValues(string bindingString, Binding binding)
        {
            Assert.IsTrue(bindingString.Contains(binding.ConcreteType.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.AmountMode.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.CreateInstanceFunction.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.CreationMode.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.InjectionAllowed.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.Arguments.ToString()));
        }

        private void ThenStringContainsClassName(string bindingString)
        {
            Assert.IsTrue(bindingString.Contains(nameof(Binding)));
        }

        private void AddArguments(Binding binding)
        {
            binding.Arguments.Add(new BindingKey(typeof(Bar), 42), new Bar());
            binding.Arguments.Add(new BindingKey(typeof(int)), 2);
            binding.Arguments.Add(new BindingKey(typeof(string)), "Foo");
        }

        private abstract class Foo { }
        private class Bar : Foo { }
	}
}