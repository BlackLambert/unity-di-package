using UnityEngine;

namespace SBaier.DI
{
	public class GameObjectContextsReseter
	{
		public void Reset(GameObject gameObject)
		{
			ResetChildren(gameObject);
			gameObject.GetComponent<Context>()?.Reset();
		}

		private void ResetChildren(GameObject gameObject)
        {
			foreach (Transform child in gameObject.transform)
				Reset(child.gameObject);
		}
	}
}
