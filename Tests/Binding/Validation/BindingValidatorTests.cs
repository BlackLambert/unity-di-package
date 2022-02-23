using Moq;
using NUnit.Framework;
using System;

namespace SBaier.DI.Tests
{
    public class BindingValidatorTests
    {
        private BasicInstanceResolver _resolver;
        private BindingValidator _bindingValidator;
        private Binding _binding;
        private Mock<FromBindingValidator> _fromValidatorMock;
        private bool _validateOfFromValidatorCalled = false;

        [SetUp]
        public void Setup()
        {
            _resolver = new BasicInstanceResolver();
        }

        [TearDown]
        public void TearDown()
        {
            _validateOfFromValidatorCalled = false;
        }

        [Test]
        public void BindingValidator_Validate_CallsFromValidator()
        {
            GivenADefaultBinding();
            GivenADefaultValidator();
            WhenValidateIsCalledOn(_binding);
            ThenValidateOfFromValidatorCalled();
        }

        [Test]
        public void BindingValidator_Validate_NullBindingThrowsException()
        {
            GivenNoBinding();
            GivenADefaultValidator();
            TestDelegate test = () => WhenValidateIsCalledOn(_binding);
            ThenThrowsArgumentNullException(test);
        }

		private void GivenNoBinding()
		{
            _binding = null;

        }

		private void GivenADefaultBinding()
		{
            _binding = new Binding(typeof(Foo));
		}

		private void GivenADefaultValidator()
		{
            _bindingValidator = new BindingValidator();
            _fromValidatorMock = new Mock<FromBindingValidator>();
            _fromValidatorMock.Setup(v => v.Validate(It.IsAny<Binding>())).
                Callback(SetValidateOfFromValidatorCalledTrue);
            _resolver.Add(_fromValidatorMock.Object);
            ((Injectable)_bindingValidator).Inject(_resolver);
        }

		private void WhenValidateIsCalledOn(Binding binding)
		{
            _bindingValidator.Validate(binding);
		}

		private void ThenValidateOfFromValidatorCalled()
		{
            Assert.True(_validateOfFromValidatorCalled);
        }

        private void ThenThrowsArgumentNullException(TestDelegate test)
        {
            Assert.Throws<ArgumentNullException>(test);
        }

        private void SetValidateOfFromValidatorCalledTrue()
		{
            _validateOfFromValidatorCalled = true;
        }

        public class Foo { }
    }
}
