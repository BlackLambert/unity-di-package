using System.Collections.Generic;

namespace SBaier.DI
{
    public class ObjectsContainer
    {
        private HashSet<UnityEngine.Object> _objects = new HashSet<UnityEngine.Object>();

        public void Add(UnityEngine.Object obj)
        {
            _objects.Add(obj);
        }

        public void Clear()
        {
            _objects.Clear();
        }

		public void Destroy()
        {
            foreach (UnityEngine.Object obj in _objects)
                UnityEngine.Object.Destroy(obj);
        }
	}
}
