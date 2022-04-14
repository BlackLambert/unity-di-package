using UnityEngine;

namespace SBaier.DI
{
	public class GameObjectContextsReseter
	{
		public void Reset(GameObject gameObject)
		{
			DeactivateChildren(gameObject);
			gameObject.GetComponent<Context>()?.Reset();
		}

		private void DeactivateChildren(GameObject gameObject)
        {
			foreach (Transform child in gameObject.transform)
				Reset(child.gameObject);
		}
	}
}
