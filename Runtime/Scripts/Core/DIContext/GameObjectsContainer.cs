using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
	public class GameObjectsContainer
	{
        private HashSet<GameObject> _objects = new HashSet<GameObject>();
        private GameObjectContextsReseter _reseter;

        public GameObjectsContainer(GameObjectContextsReseter reseter)
		{
            _reseter = reseter;
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
            _reseter.Reset(gameObject);
            UnityEngine.Object.Destroy(gameObject);
		}
	}
}