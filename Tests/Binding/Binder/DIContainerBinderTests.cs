using System;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace SBaier.DI.Tests
{
    public class DIContainerBinderTests
    {
        private static IComparable[] _testIDs = new IComparable[] {default, 32, "Bla", FooEnum.Two, FooEnum.Zero };

        private Mock<BindingStorage> _bindingStorageMock;
        private BindingStorage BindingStorage => _bindingStorageMock.Object;
		private Binder _binder;

        private Binding _addedBinding = default;
        private IComparable _addedBindingID = default;
        private Type _addedBindingType = default;

		[SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {
            _addedBinding = default;
            _addedBindingID = default;
            _addedBindingType = default;
        }

        [Test]
        public void Bind_AddsBindingToStorage(
            [ValueSource(nameof(_testIDs))] IComparable iD)
		{
            GivenADefaultSetup<Foo>();
            WhenBindIsCalled<Foo>(iD);
            ThenBindingIsAddedToStorage<Foo>(iD);
		}

        [Test]
        public void BindToSelf_ChangesBindingAsExpected(
            [ValueSource(nameof(_testIDs))] IComparable iD)
		{
            GivenADefaultSetup<Foo>();
            WhenBindToSelfIsCalled<Foo>(iD);
            ThenBindingIsAddedToStorage<Foo>(iD);
            ThenBoundContractTypeIsConcreteType<Foo>();
		}

        [Test]
        public void BindToNewSelf_ChangesBindingAsExpected(
            [ValueSource(nameof(_testIDs))] IComparable iD)
		{
            GivenADefaultSetup<Foo>();
            WhenBindToNewSelfIsCalled<Foo>(iD);
            ThenBindingIsAddedToStorage<Foo>(iD);
            ThenBoundContractTypeIsConcreteType<Foo>();
            ThenCreationModeIs(InstanceCreationMode.FromNew);
            ThenCreateInstanceFunctionCreatesNewConcrete<Foo>();
        }

        [Test]
        public void BindInstance_ChangesBindingAsExpected(
            [ValueSource(nameof(_testIDs))] IComparable iD)
        {
            GivenADefaultSetup<Foo>();
            Foo foo = new Foo();
            WhenBindInstanceIsCalled<Foo>(iD, foo);
            ThenBindingIsAddedToStorage<Foo>(iD);
            ThenBoundContractTypeIsConcreteType<Foo>();
            ThenProvideInstanceReturns<Foo>(iD, foo);
            ThenCreationModeIs(InstanceCreationMode.FromInstance);
        }

        [Test]
        public void BindComponent_ChangesBindingAsExpected(
            [ValueSource(nameof(_testIDs))] IComparable iD)
        {
            GivenADefaultSetup<FooComponent>();
            WhenBindComponentIsCalled<FooComponent>(iD);
            ThenBindingIsAddedToStorage<FooComponent>(iD);
            ThenBoundContractTypeIsConcreteType<FooComponent>();
        }

        [Test]
        public void BindObject_ChangesBindingAsExpected(
            [ValueSource(nameof(_testIDs))] IComparable iD)
        {
            GivenADefaultSetup<Sprite>();
            WhenBindObjectIsCalled<Sprite>(iD);
            ThenBindingIsAddedToStorage<Sprite>(iD);
            ThenBoundContractTypeIsConcreteType<Sprite>();
        }

		private void GivenADefaultSetup<T>()
		{
			GivenAMockBindingStorage();
			GivenAnAddBindingSetup<T>();
			GivenANewBinder();
		}

        private void GivenANonResolvableSetup()
		{
			GivenAMockBindingStorage();
			GivenANewBinder();
		}

		private void GivenAMockBindingStorage()
		{
            Mock<BindingStorage> bindingStorageMock = new Mock<BindingStorage>();
            _bindingStorageMock = bindingStorageMock;
        }

        private void GivenAnAddBindingSetup<T>()
        {
            _bindingStorageMock.Setup(s => s.AddBinding<T>(It.IsAny<Binding>(), It.IsAny<IComparable>())).
                Callback<Binding, IComparable>((b, c) => OnAddBindingCalled(typeof(T), b, c));
        }

        private void GivenANewBinder()
		{
            _binder = new DIContainerBinder(BindingStorage);
        }

        private void WhenBindIsCalled<T>(IComparable iD)
        {
            _binder.Bind<T>(iD);
        }

        private void WhenBindToSelfIsCalled<T>(IComparable iD)
        {
            _binder.BindToSelf<T>(iD);
        }

        private void WhenBindToNewSelfIsCalled<T>(IComparable iD) where T : new()
        {
            _binder.BindToNewSelf<T>(iD);
        }

        private void WhenBindInstanceIsCalled<T>(IComparable iD, T instance)
        {
            _binder.BindInstance<T>(instance, iD);
        }

        private void WhenBindComponentIsCalled<T>(IComparable iD) where T : Component
        {
            _binder.BindComponent<T>(iD);
        }

        private void WhenBindObjectIsCalled<T>(IComparable iD) where T : UnityEngine.Object
        {
            _binder.BindObject<T>(iD);
        }

        private void ThenBindingIsAddedToStorage<T>(IComparable iD = default)
        {
            Assert.IsNotNull(_addedBinding);
            Assert.AreEqual(typeof(T), _addedBindingType);
            Assert.AreEqual(iD, _addedBindingID);
        }

        private void ThenBoundContractTypeIsConcreteType<T>()
        {
            Assert.AreEqual(typeof(T), _addedBindingType);
            Assert.AreEqual(typeof(T), _addedBinding.ConcreteType);
        }

        private void ThenCreationModeIs(InstanceCreationMode mode)
        {
            Assert.AreEqual(mode, _addedBinding.CreationMode);
        }

        private void ThenCreateInstanceFunctionCreatesNewConcrete<T>()
        {
            T instance = (T) _addedBinding.ProvideInstanceFunction();
            Assert.IsNotNull(instance);
        }

        private void ThenProvideInstanceReturns<T>(IComparable iD, T instance)
        {
            T providedInstance = (T) _addedBinding.ProvideInstanceFunction();
            Assert.AreEqual(instance, providedInstance);
        }

        private void ThenInstanceAmountModeIs(InstanceAmountMode amountMode)
        {
            Assert.AreEqual(amountMode, _addedBinding.AmountMode);
        }

        private void OnAddBindingCalled(Type contact, Binding binding, IComparable iD)
		{
            _addedBindingType = contact;
            _addedBindingID = iD;
            _addedBinding = binding;
		}

        private class Foo { }
        private class FooComponent : MonoBehaviour { }
        private enum FooEnum
		{
            Zero = 0,
            One = 1,
            Two = 2
		}
	}
}
