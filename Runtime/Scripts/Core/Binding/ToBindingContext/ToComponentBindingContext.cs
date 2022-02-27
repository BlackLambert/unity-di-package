using UnityEngine;

namespace SBaier.DI
{
	public class ToComponentBindingContext<TConcrete> : ToBindingContext<TConcrete> where TConcrete : Component
	{
		public ToComponentBindingContext(BindingArguments arguments) : base(arguments) { }

		public FromBindingContext FromNewPrefabInstance(GameObject prefab)
		{
			ValidateHasComponent(prefab);
			_binding.CreationMode = InstanceCreationMode.FromPrefabInstance;
			_binding.ProvideInstanceFunction = () => prefab;
			return new FromBindingContext(_arguments);
		}

		public FromBindingContext FromNewPrefabInstance(TConcrete prefab)
		{
			_binding.CreationMode = InstanceCreationMode.FromPrefabInstance;
			_binding.ProvideInstanceFunction = () => prefab.gameObject;
			return new FromBindingContext(_arguments);
		}

		public FromBindingContext FromNewResourcePrefabInstance(string path)
		{
			ValidateResourcePath(path);
			_binding.CreationMode = InstanceCreationMode.FromResourcePrefabInstance;
			_binding.ProvideInstanceFunction = () => path;
			return new FromBindingContext(_arguments);
		}

		public FromBindingContext FromNewComponentOn(GameObject gameObject)
		{
			_binding.CreationMode = InstanceCreationMode.FromNewComponentOn;
			_binding.ProvideInstanceFunction = () => AddComponentTo(gameObject);
			return new FromBindingContext(_arguments);
		}

		public FromBindingContext FromNewComponentOnNewGameObject(
			string name = "", Transform parent = null, bool worldPositionStays = false)
		{
			name = string.IsNullOrEmpty(name) ? $"{nameof(TConcrete)}Object" : name;
			_binding.CreationMode = InstanceCreationMode.FromNewComponentOnNewGameObject;
			_binding.ProvideInstanceFunction = () => AddComponentToNew(name, parent, worldPositionStays);
			return new FromBindingContext(_arguments);
		}

		private TConcrete AddComponentTo(GameObject gameObject)
		{
			return gameObject.AddComponent<TConcrete>();
		}

		private TConcrete AddComponentToNew(string name, Transform parent, bool worldPositionStays)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.transform.SetParent(parent, worldPositionStays);
			return gameObject.AddComponent<TConcrete>();
		}

		private void ValidateResourcePath(string path)
		{
			if(Resources.Load<GameObject>(path) == null) 
				throw new MissingComponentException($"There is no ressource of type {typeof(TConcrete)} at path {path}");
		}

		private void ValidateHasComponent(GameObject gameObject)
		{
			TConcrete target = gameObject.GetComponent<TConcrete>();
			if (target == null)
				throw new MissingComponentException($"There is no component of type {typeof(TConcrete)} on gameObject {gameObject.name}");
		}
	}
}
