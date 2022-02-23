using System;
using NUnit.Framework;

namespace SBaier.DI.Tests
{
    public class BindingKeyTests
    {
		private const string _bindingID1 = "ID";
		private const string _bindingID2 = "Id";
		private const int _bindingID3 = 12;
		private const string _bindingID4 = "12";

        [Test]
        public void BindingKey_Constructor0_TypeReturnsDefaultValue()
        {
            BindingKey bindingKey = GivenANewBindingKey();
            ThenTypeReturnsDefaultValue(bindingKey);
        }

        [Test]
        public void BindingKey_Constructor0_IDReturnsDefaultValue()
        {
            BindingKey bindingKey = GivenANewBindingKey();
            ThenIDReturnsDefaultValue(bindingKey);
        }

		[Test]
        public void BindingKey_Constructor1_TypeReturnsProvidedValue()
        {
            Type contract = typeof(Foo);
            BindingKey bindingKey = GivenANewBindingKey(contract);
            ThenTypeReturnsExpectedValue(bindingKey, contract);
        }

        [Test]
        public void BindingKey_Constructor1_IDReturnsNull()
        {
            Type contract = typeof(Foo);
            BindingKey bindingKey = GivenANewBindingKey(contract);
            ThenIDReturnsNull(bindingKey);
        }

        [Test]
        public void BindingKey_Constructor2_TypeReturnsProvidedValue()
        {
            Type contract = typeof(Foo);
            BindingKey bindingKey = GivenANewBindingKey(contract, _bindingID1);
            ThenTypeReturnsExpectedValue(bindingKey, contract);
        }

		[Test]
        public void BindingKey_Constructor2_IDReturnsProvidedValue()
        {
            Type contract = typeof(Foo);
            BindingKey bindingKey = GivenANewBindingKey(contract, _bindingID1);
            ThenIDReturnsExpectedValue(bindingKey, _bindingID1);
        }

        [Test]
        public void BindingKey_Equals_TwoKeysWithEqualPropertiesAreEqual()
		{
            Type contract = typeof(Foo);
            BindingKey bindingKey1 = GivenANewBindingKey(contract, _bindingID1);
            BindingKey bindingKey2 = GivenANewBindingKey(contract, _bindingID1);
            ThenKeysAreEqual(bindingKey1, bindingKey2);
            ThenKeysAreEqual((object)bindingKey1, (object)bindingKey2);
        }

        [Test]
        public void BindingKey_Equals_TwoKeysWithDifferentTypesAreNotEqual()
		{
            Type contract1 = typeof(Foo);
            Type contract2 = typeof(Bar);
            BindingKey bindingKey1 = GivenANewBindingKey(contract1, _bindingID1);
            BindingKey bindingKey2 = GivenANewBindingKey(contract2, _bindingID1);
            ThenKeysAreNotEqual(bindingKey1, bindingKey2);
            ThenKeysAreNotEqual((object)bindingKey1, (object)bindingKey2);
        }

        [Test]
        public void BindingKey_Equals_TwoKeysWithDifferentIDsAreNotEqual()
		{
            Type contract = typeof(Foo);
            BindingKey bindingKey1 = GivenANewBindingKey(contract, _bindingID1);
            BindingKey bindingKey2 = GivenANewBindingKey(contract, _bindingID2);
            ThenKeysAreNotEqual(bindingKey1, bindingKey2);
            ThenKeysAreNotEqual((object)bindingKey1, (object)bindingKey2);
        }

        [Test]
        public void BindingKey_Equals_TwoKeysWithDifferentIDTypesAreNotEqual()
		{
            Type contract = typeof(Foo);
            BindingKey bindingKey1 = GivenANewBindingKey(contract, _bindingID3);
            BindingKey bindingKey2 = GivenANewBindingKey(contract, _bindingID4);
            ThenKeysAreNotEqual(bindingKey1, bindingKey2);
            ThenKeysAreNotEqual((object)bindingKey1, (object)bindingKey2);
        }

		private BindingKey GivenANewBindingKey()
		{
            return new BindingKey();
        }

		private BindingKey GivenANewBindingKey(Type contract)
		{
            return new BindingKey(contract);
        }

        private BindingKey GivenANewBindingKey(Type contract, IComparable iD)
        {
            return new BindingKey(contract, iD);
        }

        private void ThenTypeReturnsExpectedValue(BindingKey bindingKey, Type contract)
        {
            Assert.AreEqual(contract, bindingKey.Type);
        }

        private void ThenIDReturnsNull(BindingKey bindingKey)
        {
            Assert.Null(bindingKey.ID);
        }

        private void ThenTypeReturnsDefaultValue(BindingKey bindingKey)
        {
            Assert.AreEqual(default(Type), bindingKey.Type);
        }

        private void ThenIDReturnsDefaultValue(BindingKey bindingKey)
        {
            Assert.AreEqual(default(IComparable), bindingKey.ID);
        }

        private void ThenIDReturnsExpectedValue(BindingKey bindingKey, string bindingID)
        {
            Assert.AreEqual(bindingID, bindingKey.ID);
        }

        private void ThenKeysAreEqual(BindingKey bindingKey1, BindingKey bindingKey2)
        {
            Assert.AreEqual(bindingKey1, bindingKey2);
        }

        private void ThenKeysAreEqual(object obj1, object obj2)
        {
            Assert.AreEqual(obj1, obj2);
        }

        private void ThenKeysAreNotEqual(BindingKey bindingKey1, BindingKey bindingKey2)
        {
            Assert.AreNotEqual(bindingKey1, bindingKey2);
        }

        private void ThenKeysAreNotEqual(object obj1, object obj2)
        {
            Assert.AreNotEqual(obj1, obj2);
        }

        private class Foo { }
        private class Bar { }

    }
}