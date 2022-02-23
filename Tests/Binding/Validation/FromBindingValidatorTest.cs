using System.Collections.Generic;
using NUnit.Framework;

namespace SBaier.DI.Tests
{
    public class FromBindingValidatorTest
    {
        private List<Binding> _bindings = new List<Binding>();
        private FromBindingValidator _validator;

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {
            _bindings.Clear();
        }

        [Test]
        public void FromBindingValidator_Validate_ValidBindingsPass()
        {
            GivenValidBindings();
            foreach (Binding binding in _bindings)
            {
                GivenADefaultValidator();
                TestDelegate test = () => WhenValidateIsCalledOn(binding);
                ThenThrowsNoException(test);
            }
        }

        [Test]
        public void FromBindingValidator_Validate_InvalidBindingsCauseException()
        {
            GivenInvalidBindings();
            foreach (Binding binding in _bindings)
            {
                GivenADefaultValidator();
                TestDelegate test = () => WhenValidateIsCalledOn(binding);
                ThenThrowsInvalidBindingException(test);
            }
        }

		private void GivenInvalidBindings()
		{
            _bindings.Add(Create(InstanceCreationMode.Undefined));
        }

		private void GivenValidBindings()
		{
            _bindings.Add(Create(InstanceCreationMode.FromFactory));
            _bindings.Add(Create(InstanceCreationMode.FromInstance));
            _bindings.Add(Create(InstanceCreationMode.FromMethod));
            _bindings.Add(Create(InstanceCreationMode.FromNew));
		}

		private void GivenADefaultValidator()
		{
            _validator = new FromBindingValidatorImpl();
        }

        private void WhenValidateIsCalledOn(Binding binding)
        {
            _validator.Validate(binding);
        }

        private void ThenThrowsNoException(TestDelegate test)
        {
            Assert.DoesNotThrow(test);
        }

        private void ThenThrowsInvalidBindingException(TestDelegate test)
        {
            Assert.Throws<InvalidBindingException>(test);
        }

        private Binding Create(InstanceCreationMode creationMode)
		{
            Binding binding = new Binding(typeof(Foo));
            binding.CreationMode = creationMode;
            return binding;
        }

        private class Foo { }
	}
}
