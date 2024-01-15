using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SBaier.DI.Tests
{
    public class ArgumentsBindingContextTest
    {
        private static object[] _testArguments =
        {
            new object[] { 33, null },
            new object[] { "bli", 1 },
            new object[] { 'd', "eww" },
            new object[] { new Foo(), Bar.Hi },
            new object[] { Bar.Ho, 'f' }
        };

        private static object[] _asNonResolvableTestArguments =
        {
            new object[] { new BindingKey(typeof(int), null) },
            new object[] { new BindingKey(typeof(string), "blubb") },
            new object[] { new BindingKey(typeof(Foo), Bar.Ho) },
        };

        private BindingArguments _bindingArguments;
        private ArgumentsBindingContext _bindingContext;
        private List<BindingKey> _removedBindingKeys;
        private List<InstantiationInfo> _addedNonLazyBindings;
        private Mock<BindingStorage> _bindingStorageMock;

        [SetUp]
        public void Setup()
        {
            _removedBindingKeys = new List<BindingKey>();
            _addedNonLazyBindings = new List<InstantiationInfo>();
        }

        [TearDown]
        public void TearDown()
        {
            _bindingArguments = null;
            _bindingContext = null;
        }

        [Test]
        [TestCaseSource(nameof(_testArguments))]
        public void WithArgument_StoresProvidedArgument(object argument, IComparable iD)
        {
            GivenADefaultSetup();
            WhenWithArgumentsIsCalled(argument, iD);
            ThenIsStored(argument);
        }

        [Test]
        [TestCaseSource(nameof(_testArguments))]
        public void WithArgument_ArgumentHasProvidedID(object argument, IComparable iD)
        {
            GivenADefaultSetup();
            WhenWithArgumentsIsCalled(argument, iD);
            ThenHasID(iD);
        }

        [Test]
        [TestCaseSource(nameof(_asNonResolvableTestArguments))]
        public void AsNonResolvable_ClearsBindingKeys(BindingKey key)
        {
            GivenADefaultSetup();
            GivenTheBindingKey(key);
            ThenHasBindingKey(key);
            WhenAsNonResolvableIsCalled();
            ThenHasNoKeys();
        }

        [Test]
        [TestCaseSource(nameof(_asNonResolvableTestArguments))]
        public void AsNonResolvable_RemovesBindings(BindingKey key)
		{
            GivenADefaultSetup();
            GivenAStorageMockRemoveBindingSetup();
            GivenTheBindingKey(key);
            WhenAsNonResolvableIsCalled();
            ThenRemovedBindingsContains(key);
        }


        [Test]
        public void AsNonResolvable_AddsBindingToNonResolvables()
        {
            GivenADefaultSetup();
            GivenAStorageMockAddToNonLazySetup();
            WhenAsNonResolvableIsCalled();
            ThenBindingIsAddedToNonLazy(_bindingArguments.Binding);
        }

		private void GivenADefaultSetup()
		{
            GivenNewBindingArguments();
            GivenANewArgumentsBindingContext();
        }

		private void GivenNewBindingArguments()
        {
			Binding binding = new Binding(typeof(int));
            _bindingStorageMock = new Mock<BindingStorage>();
            _bindingArguments = new BindingArguments(binding, _bindingStorageMock.Object);
        }

        private void GivenTheBindingKey(BindingKey bindingKey)
        {
            _bindingArguments.Keys.Add(bindingKey);
        }

        private void GivenANewArgumentsBindingContext()
		{
            _bindingContext = new ArgumentsBindingContext(_bindingArguments);
        }

        private void GivenAStorageMockRemoveBindingSetup()
        {
            _bindingStorageMock.Setup(storage => storage.RemoveBinding(It.IsAny<BindingKey>())).
                Callback((BindingKey key) => _removedBindingKeys.Add(key));
        }

        private void GivenAStorageMockAddToNonLazySetup()
        {
            _bindingStorageMock.Setup(storage => storage.AddToNonLazy(It.IsAny<Binding>())).
                Callback((InstantiationInfo info) => _addedNonLazyBindings.Add(info));
        }

        private void WhenWithArgumentsIsCalled(object argument, IComparable iD)
        {
            _bindingContext.WithArgument(argument, iD);
        }

        private void WhenAsNonResolvableIsCalled()
        {
            _bindingContext.AsNonResolvable();
        }

        private void ThenIsStored(object argument)
        {
            Assert.IsTrue(_bindingArguments.Binding.Arguments.ContainsValue(argument));
        }

        private void ThenHasID(IComparable iD)
        {
            BindingKey key = new BindingKey(typeof(object), iD);
            Assert.IsTrue(_bindingArguments.Binding.Arguments.ContainsKey(key));
        }

        private void ThenHasNoKeys()
        {
            Assert.IsTrue(_bindingArguments.Keys.Count == 0);
        }

        private void ThenHasBindingKey(BindingKey key)
        {
            Assert.IsTrue(_bindingArguments.Keys.Contains(key));
        }

        private void ThenRemovedBindingsContains(BindingKey key)
        {
            Assert.IsTrue(_removedBindingKeys.Contains(key));
        }

        private void ThenBindingIsAddedToNonLazy(Binding binding)
        {
            Assert.IsTrue(_addedNonLazyBindings.Contains(binding));
        }

        private class Foo
		{

		}

        private enum Bar
		{
            Hi,
            Ho
		}
    }
}
