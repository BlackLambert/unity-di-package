using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
	public class GameObjectsContainer
	{
        private HashSet<GameObject> _objects = new HashSet<GameObject>();
        private GameObjectDeactivator _deactivator;

        public GameObjectsContainer(GameObjectDeactivator deactivator)
		{
            _deactivator = deactivator;
        }

        public void Add(GameObject gameObject)
        {
            _objects.Add(gameObject);
        }

        public void Clear()
        {
            _objects.Clear();
        }

        public void Destroy()
        {
			foreach (GameObject gameObject in _objects)
				Destroy(gameObject);
		}

		private void Destroy(GameObject gameObject)
		{
            _deactivator.Deactivate(gameObject.transform);
            UnityEngine.Object.Destroy(gameObject);
		}
	}
}