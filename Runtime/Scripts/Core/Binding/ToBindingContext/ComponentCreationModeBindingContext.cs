using UnityEngine;

namespace SBaier.DI
{
	public class ComponentCreationModeBindingContext<TConcrete> : 
		CreationModeBindingContext<TConcrete> where TConcrete : Component
	{
		public ComponentCreationModeBindingContext(BindingArguments arguments) : base(arguments) { }

		public ArgumentsBindingContext FromNewPrefabInstance(GameObject prefab)
		{
			ValidateHasComponent(prefab);
			_binding.CreationMode = InstanceCreationMode.FromPrefabInstance;
			_binding.ProvideInstanceFunction = () => prefab;
			return new ArgumentsBindingContext(_arguments);
		}

		public ArgumentsBindingContext FromNewPrefabInstance(TConcrete prefab)
		{
			_binding.CreationMode = InstanceCreationMode.FromPrefabInstance;
			_binding.ProvideInstanceFunction = () => prefab.gameObject;
			return new ArgumentsBindingContext(_arguments);
		}

		public ArgumentsBindingContext FromNewResourcePrefabInstance(string path)
		{
			ValidateResourcePath(path);
			_binding.CreationMode = InstanceCreationMode.FromResourcePrefabInstance;
			_binding.ProvideInstanceFunction = () => path;
			return new ArgumentsBindingContext(_arguments);
		}

		public ArgumentsBindingContext FromNewComponentOn(GameObject gameObject)
		{
			_binding.CreationMode = InstanceCreationMode.FromNewComponentOn;
			_binding.ProvideInstanceFunction = () => AddComponentTo(gameObject);
			return new ArgumentsBindingContext(_arguments);
		}

		public ArgumentsBindingContext FromNewComponentOnNewGameObject(
			string name = "", Transform parent = null, bool worldPositionStays = false)
		{
			name = string.IsNullOrEmpty(name) ? $"{nameof(TConcrete)}Object" : name;
			_binding.CreationMode = InstanceCreationMode.FromNewComponentOnNewGameObject;
			_binding.ProvideInstanceFunction = () => AddComponentToNew(name, parent, worldPositionStays);
			return new ArgumentsBindingContext(_arguments);
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
