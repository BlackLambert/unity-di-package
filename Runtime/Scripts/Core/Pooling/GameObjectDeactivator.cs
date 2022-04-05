using UnityEngine;

namespace SBaier.DI
{
	public class GameObjectDeactivator
	{
		public void Deactivate(Transform transform)
		{
			Context[] contexts = transform.GetComponentsInChildren<Context>();
			foreach (Context context in contexts)
				context.Reset();
		}
	}
}
