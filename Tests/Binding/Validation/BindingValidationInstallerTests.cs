using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SBaier.DI;
using System;
using Moq;

namespace SBaier.DI.Tests
{
    public class BindingValidationInstallerTests
    {
        private Mock<Binder> _binderMock;
        private Binder Binder => _binderMock.Object;
        private Type _boundType;
		private BindingValidationInstaller _installer;

		[SetUp]
        public void Setup()
        {
            
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void BindingValidationInstaller_InstallBindings_BindsBindingValidator()
		{
			GivenADefaultNewSetup<BindingValidator>();
			WhenInstallBindingsIsCalledOn(Binder);
			ThenIsBound(typeof(BindingValidator));
		}

        [Test]
        public void BindingValidationInstaller_InstallBindings_BindsFromBindingValidator()
		{
			GivenADefaultNewSetup<FromBindingValidatorImpl>();
			WhenInstallBindingsIsCalledOn(Binder);
			ThenIsBound(typeof(FromBindingValidatorImpl));
		}

		private void GivenADefaultNewSetup<T>() where T : new()
		{
			GivenABinderMock();
			GivenABindSetup<T>();
			GivenABindNewSetup<T>();
			GivenANewInstaller();
		}

		private void GivenABinderMock()
		{
            _binderMock = new Mock<Binder>();
        }

        private void GivenABindSetup<T>()
        {
            _binderMock.Setup(b => b.Bind<T>(default)).Callback(SetBoundType<T>);
            _binderMock.Setup(b => b.BindToSelf<T>(default)).Callback(SetBoundType<T>);
        }

        private void GivenABindNewSetup<T>() where T : new()
        {
            _binderMock.Setup(b => b.BindToNewSelf<T>(default)).Callback(SetBoundType<T>);
        }

        private void GivenANewInstaller()
        {
            _installer = new BindingValidationInstaller();
        }

        private void SetBoundType<T>()
		{
            _boundType = typeof(T);

        }

        private void WhenInstallBindingsIsCalledOn(Binder binder)
        {
            _installer.InstallBindings(_binderMock.Object);
        }

        private void ThenIsBound(Type type)
        {
            Assert.AreEqual(type, _boundType, $"Required class {type} is not bound by {nameof(BindingValidationInstaller)}");
        }
    }
}