using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SBaier.DI.Tests
{
    public class ComponentCreationModeBindingContextTests
    {
        private static List<GameObject> _createdObjects = new ();
        private ComponentCreationModeBindingContext<Foo> _context;
        private Mock<BindingStorage> _bindingStorageMock;
        private BindingStorage BindingStorage => _bindingStorageMock.Object;
        private Binding _binding;

        [TearDown]
        public void Destruct()
        {
            Clear(_createdObjects);
        }

        public static GameObject[] CreateValidTestPrefabs()
        {
            GameObject prefab1 = new GameObject("One");
            prefab1.AddComponent<Foo>();
            GameObject prefab2 = new GameObject("Two");
            prefab2.AddComponent<Foo>();
            prefab2.AddComponent<Bar>();
            GameObject prefab3 = new GameObject("Three");
            prefab3.AddComponent<Foo>();
            GameObject[] result = { prefab1, prefab2, prefab3 };
            _createdObjects.AddRange(result);
            return result;
        }

        public static GameObject[] CreateTestPrefabsWithoutComponent()
        {
            GameObject prefab1 = new GameObject("One");
            GameObject prefab2 = new GameObject("Two");
            prefab2.AddComponent<Bar>();
            GameObject prefab3 = new GameObject("Three");
            GameObject[] result = { prefab1, prefab2, prefab3 };
            _createdObjects.AddRange(result);
            return result;
        }

        private static void Clear(List<GameObject> objects)
        {
            Debug.Log("Clearing Objects");
            foreach (GameObject createdObject in objects)
            {
                Object.Destroy(createdObject);
            }
            objects.Clear();
        }

        [Test]
        public void FromNewPrefabInstance_ThrowsExceptionIfPrefabDoesNotHaveComponent(
            [ValueSource(nameof(CreateTestPrefabsWithoutComponent))] GameObject prefab)
        {
            Debug.Log(nameof(FromNewPrefabInstance_ThrowsExceptionIfPrefabDoesNotHaveComponent));
            GivenADefaultSetup();
            Action test = () => WhenFromNewPrefabInstanceIsCalled(prefab);
            ThenThrowsException<MissingComponentException>(test);
        }

        [Test]
        public void FromNewPrefabInstance_SetsBindingCreationMode(
            [ValueSource(nameof(CreateValidTestPrefabs))] GameObject prefab)
        {
            Debug.Log(nameof(FromNewPrefabInstance_SetsBindingCreationMode));
            GivenADefaultSetup();
            WhenFromNewPrefabInstanceIsCalled(prefab);
            ThenCreationModeIsSetTo(InstanceCreationMode.FromPrefabInstance);
        }

        [Test]
        public void FromNewPrefabInstance_InstanceProvideFunctionReturnsPrefab(
            [ValueSource(nameof(CreateValidTestPrefabs))] GameObject prefab)
        {
            Debug.Log(nameof(FromNewPrefabInstance_InstanceProvideFunctionReturnsPrefab));
            GivenADefaultSetup();
            WhenFromNewPrefabInstanceIsCalled(prefab);
            ThenProvideInstanceFunctionReturns(prefab);
        }

        private void GivenADefaultSetup()
        {
            GivenAMockBindingStorage();
            GivenANewBinding();
            GivenANewContext();
        }

        private void GivenANewBinding()
        {
            _binding = new Binding(typeof(Foo));
        }

        private void GivenAMockBindingStorage()
        {
            Mock<BindingStorage> bindingStorageMock = new Mock<BindingStorage>();
            _bindingStorageMock = bindingStorageMock;
        }

        private void GivenANewContext()
        {
            BindingArguments arguments = new BindingArguments(_binding, BindingStorage);
            _context = new ComponentCreationModeBindingContext<Foo>(arguments);
        }

        private void WhenFromNewPrefabInstanceIsCalled(GameObject prefab)
        {
            _context.FromNewPrefabInstance(prefab);
        }

        private void ThenThrowsException<T>(Action test) where T : Exception
        {
            Assert.Throws<T>(() => test());
        }

        private void ThenCreationModeIsSetTo(InstanceCreationMode creationMode)
        {
            Assert.AreEqual(_binding.CreationMode, creationMode);
        }

        private void ThenProvideInstanceFunctionReturns(object prefab)
        {
            Assert.AreEqual(_binding.ProvideInstanceFunction(), prefab);
        }

        public class Foo : MonoBehaviour{}

        public class Bar : MonoBehaviour{}
    }
}
